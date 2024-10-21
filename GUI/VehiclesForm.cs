using BLL;
using DAL;
using BLL.Services;
using Guna.UI2.WinForms;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class VehiclesForm : Form
    {
        #region Thuộc tính
        private bool isEditing = false;

        private static VehiclesForm instance;

        public static VehiclesForm Instance
        {
            get
            {
                if (instance == null) instance = new VehiclesForm();
                return instance;
            }
        }
        #endregion

        public VehiclesForm()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
        }

        public void VehiclesForm_Load(object sender, EventArgs e)
        {
            this.LoadAllVehicles();
        }

        public void LoadAllVehicles()
        {
            // Tải tất cả dữ liệu xe vào DataGridView
            VehicleService.LoadAllVehicles(dgvVehicles);
            this.UpdateControlsWithSelectedRowData();
        }

        private void btnEditVehicle_Click(object sender, EventArgs e)
        {
            // Nếu không đang ở chế độ lưu, bật chế độ chỉnh sửa
            if (!this.InSaveMode())
            {
                this.ToggleEditMode();
                return;
            }

            // Kiểm tra tính hợp lệ của các trường
            if (!this.ValidateFields()) return;

            // Xác nhận hành động sửa xe
            if (this.ConfirmAction($"Bạn có chắc chắn muốn sửa xe '{txtCarName.Text} {txtCarNo.Text}'?"))
            {
                Vehicle vehicle = this.GetVehicle(); // Lấy thông tin xe
                if (VehicleService.EditVehicle(vehicle)) // Gọi dịch vụ để sửa xe
                {
                    FormHelper.ShowNotify("Sửa xe thành công."); // Thông báo thành công
                    this.LoadAllVehicles(); // Tải lại danh sách xe
                }
                else
                    FormHelper.ShowError("Không thể sửa xe."); // Thông báo lỗi nếu không thành công

            }
            else return; // Dừng lại nếu không xác nhận
            this.ToggleEditMode();
        }


        private bool ValidateFields()
        {
            // Kiểm tra nếu tên xe hoặc số xe trống, hiển thị thông báo
            if (string.IsNullOrEmpty(txtCarName.Text))
            {
                FormHelper.ShowToolTip(txtCarName, toolTip, "Vui lòng nhập tên xe.");
                return false;
            }
            if (string.IsNullOrEmpty(txtCarNo.Text))
            {
                FormHelper.ShowToolTip(txtCarNo, toolTip, "Vui lòng nhập số xe.");
                return false;
            }
            return true;
        }
        private void ToggleEditMode()
        {
            // Bật hoặc tắt chế độ chỉnh sửa
            FormHelper.ToggleEditMode(ref this.isEditing, this.btnEdit_BasicDetail, txtCarName, txtCarNo, txtSeats, txtWeight, dtpManuYear, cboStatus);
        }
        

        private bool InSaveMode()
        {
            // Kiểm tra nếu nút đang ở chế độ lưu
            return btnEdit_BasicDetail.Text == Constant.SAVE_MODE;
        }

        private bool ConfirmAction(string message)
        {
            // Xác nhận hành động từ người dùng
            DialogResult result = FormHelper.ShowConfirm(message);
            return result == DialogResult.Yes;
        }

        private Vehicle GetVehicle()
        {
            // Lấy thông tin xe từ các trường dữ liệu
            return new Vehicle
            {
                VehicleID = int.Parse(lblVehicleD.Text),
                VehicleName = txtCarName.Text,
                VehicleNumber = txtCarNo.Text,
                IsTruck = chkTruck.Checked,
                IsPassengerCar = chkPassengerCar.Checked,
                ManufacturerYear = int.Parse(dtpManuYear.Text),
                Weight = int.Parse(txtWeight.Text),
                Seats = int.Parse(txtSeats.Text),
                Notes = txtNotes.Text,
                Updated_At = DateTime.Now,
            };
        }

        private void btnOpenAddVehicleForm_Click(object sender, EventArgs e)
        {
            // Mở form thêm xe mới và tải lại dữ liệu sau khi đóng form
            FormHelper.OpenFormDialog(new AddVehicleForm());
            this.LoadAllVehicles();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Xóa các hàng cũ trong DataGridView, lấy từ khóa tìm kiếm và tải dữ liệu xe phù hợp
            FormHelper.ClearDataGridViewRow(dgvVehicles);

            string keyword = txtSearch.Text.ToLower();
            VehicleService.SearchVehicles(dgvVehicles, keyword);
            this.UpdateControlsWithSelectedRowData();
        }

        private void dgvVehicles_SelectionChanged(object sender, EventArgs e)
        {
            this.UpdateControlsWithSelectedRowData();
        }

        private void UpdateControlsWithSelectedRowData()
        {
            // Kiểm tra nếu có hàng được chọn và gán dữ liệu xe được chọn vào các trường
            if (!this.HasSelectedRow()) return;

            var selectedRow = dgvVehicles.SelectedRows[0];

            if (selectedRow.Tag is Vehicle selectedVehicle)
                this.AssignDataToControls(selectedVehicle);
        }

        private void AssignDataToControls(Vehicle selectedVehicle)
        {
            // Gán dữ liệu xe được chọn vào các điều khiển
            string vehicleID = selectedVehicle.VehicleID.ToString();

            FormHelper.SetLabelID(lblVehicleD, vehicleID);
            txtCarName.Text = selectedVehicle.VehicleName;
            txtCarNo.Text = selectedVehicle.VehicleNumber;
            chkPassengerCar.Checked = (bool)selectedVehicle.IsPassengerCar;
            chkTruck.Checked = (bool)selectedVehicle.IsTruck;
            // Gán giá trị năm sản xuất, mặc định ngày 1/1 của năm đó
            int year = selectedVehicle.ManufacturerYear.GetValueOrDefault(2000);
            dtpManuYear.Value = new DateTime(year, 1, 1);
            txtWeight.Text = selectedVehicle.Weight.ToString();
            txtSeats.Text = selectedVehicle.Seats.ToString();
            txtNotes.Text = selectedVehicle.Notes;

        }

        private void btnDeleteVehicle_Click(object sender, EventArgs e)
        {
            // Xác nhận và xóa xe được chọn
            if (!this.HasSelectedRow()) return;

            if (this.ConfirmAction($"Bạn có chắc chắn muốn xóa xe '{txtCarName.Text} {txtCarNo.Text}'?"))
            {
                if (VehicleService.DeleteVehicle(int.Parse(lblVehicleD.Text)))
                {
                    FormHelper.ShowNotify("Xóa xe thành công.");
                    this.LoadAllVehicles();
                }
                else
                    FormHelper.ShowError("Không thể xóa xe.");
            }
        }

        private bool HasSelectedRow()
        {
            // Kiểm tra nếu có hàng được chọn trong DataGridView
            return dgvVehicles.SelectedRows.Count > 0;
            FormHelper.OpenFormDialog(new AddVehicleForm());
        }
    }
}
