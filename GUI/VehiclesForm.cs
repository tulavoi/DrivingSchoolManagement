using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Compilation;
using System.Windows.Forms;
using BLL.Services;
using DAL;
using GUI.Validators;
using Guna.UI2.WinForms;

namespace GUI
{
    public partial class VehiclesForm : Form
    {
        #region Properties
        private bool isEditing_BasicDetails = false;
        private bool isEditing_MaintenanceDetails = false;
        #endregion
        public VehiclesForm()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
        }
        // Tải dữ liệu xe khi form được nạp
        public void VehiclesForm_Load(object sender, EventArgs e)
        {
            this.LoadAllVehicles();
        }
        // Tải tất cả dữ liệu xe vào DataGridView
        public void LoadAllVehicles()
        {
            VehicleService.LoadAllVehicles(dgvVehicles);
            this.UpdateControlsWithSelectedRowData();
        }
        // Chỉnh sửa thông tin chi tiết cơ bản của xe
        private void btnEdit_BasicDetail_Click(object sender, EventArgs e)
        {
            // Nếu xe là truck thì bật trường weight, tắt passenger car và đóng seats
            if (this.chkTruck.Checked)
            {
                this.txtWeight.Enabled = true;
                this.txtSeats.Enabled = false; // Tắt trường seats
                this.chkPassengerCar.Checked = false; // Tắt passenger car
            }
            else
            {
                txtWeight.Clear();
                this.txtWeight.Enabled = false; // Tắt trường weight nếu không chọn truck
            }

            // Nếu xe là passenger car thì bật trường seats, tắt truck và đóng weight
            if (this.chkPassengerCar.Checked)
            {
                this.txtSeats.Enabled = true;
                this.txtWeight.Enabled = false; // Tắt trường weight
                this.chkTruck.Checked = false; // Tắt truck
            }
            else
            {
                txtSeats.Clear();
                this.txtSeats.Enabled = false; // Tắt trường seats nếu không chọn passenger car
            }

            // Gán giá trị cho thuộc tính IsTruck và IsPassengerCar
            bool isTruck = this.chkTruck.Checked; // Gán true nếu truck được chọn, ngược lại gán false
            bool isPassengerCar = this.chkPassengerCar.Checked; // Gán true nếu passenger car được chọn, ngược lại gán false          

            // Nếu đang ở chế độ lưu
            if (!this.InSaveMode())
            {
                this.ToggleEditMode();
                return;
            }

            // Kiểm tra điều kiện không cho phép các trường txtWeight và txtSeats null
            if (isTruck && string.IsNullOrWhiteSpace(txtWeight.Text))
            {
                FormHelper.ShowError("Weight cannot be null when the vehicle is a truck.");
                return;
            }
            else if (isPassengerCar && string.IsNullOrWhiteSpace(txtSeats.Text))
            {
                FormHelper.ShowError("Seats cannot be null when the vehicle is a passenger car.");
                return;
            }

            // Xác thực thông tin
            if (!this.ValidateFields()) return;

            // Xác nhận hành động
            if (this.ConfirmAction($"Are you sure to edit vehicle '{txtCarName.Text}'?"))
            {
                // Lấy thông tin xe từ các điều khiển
                Vehicle vehicle = this.GetVehicle();
                vehicle.IsTruck = isTruck; // Gán giá trị IsTruck cho vehicle
                vehicle.IsPassengerCar = isPassengerCar; // Gán giá trị IsPassengerCar cho vehicle

                // Chỉnh sửa xe
                if (VehicleService.EditVehicle(vehicle))
                {
                    FormHelper.ShowNotify("Vehicle edited successfully.");
                    this.UpdateControlsWithSelectedRowData();
                    this.LoadAllVehicles(); // Tải lại danh sách xe

                    // Cập nhật lại giá trị cho txtWeight và txtSeats
                    if (isTruck)
                    {
                        txtWeight.Text = vehicle.Weight.ToString(); // Giả sử vehicle có thuộc tính Weight
                    }
                    else
                    {
                        txtWeight.Clear(); // Xóa dữ liệu nếu không phải truck
                    }

                    if (isPassengerCar)
                    {
                        txtSeats.Text = vehicle.Seats.ToString(); // Giả sử vehicle có thuộc tính Seats
                    }
                    else
                    {
                        txtSeats.Clear(); // Xóa dữ liệu nếu không phải passenger car
                    }
                }
                else
                {
                    FormHelper.ShowError("Failed to edit vehicle.");
                }
            }
            this.ToggleEditMode();
            this.UpdateControlsWithSelectedRowData();
        }
    
        // Kiểm tra nếu đang ở chế độ lưu
        private bool InSaveMode()
        {
            return btnEdit_BasicDetail.Text == Constant.SAVE_MODE;
        }
        // Chuyển đổi chế độ chỉnh sửa
        private void ToggleEditMode()
        {
            FormHelper.ToggleEditMode(ref this.isEditing_BasicDetails, this.btnEdit_BasicDetail,
                                       txtCarName, txtCarNo,
                                       dtpManuYear, chkTruck,
                                       chkPassengerCar, this.txtWeight,
                                       txtSeats, this.txtNotes,
                                       this.cboStatus);
        }
        // Xác thực các trường thông tin của xe
        private bool ValidateFields()
        {
            // Kiểm tra các trường thông tin của xe
            if (!VehicleValidator.ValidateName(txtCarName, toolTip)) return false;

            if (!VehicleValidator.ValidateVehicleNumber(txtCarNo, toolTip)) return false;

            if (!VehicleValidator.ValidateManufactureYear(dtpManuYear, toolTip)) return false;

            if (!VehicleValidator.ValidateWeight(txtWeight, toolTip)) return false;

            if (!VehicleValidator.ValidateSeats(txtSeats, toolTip)) return false;

            return true;
        }
        // Lấy thông tin xe từ các điều khiển
        private Vehicle GetVehicle()
        {
            Vehicle vehicle = new Vehicle
            {
                VehicleID = int.Parse(lblVehicleD.Text),
                VehicleName = txtCarName.Text,
                VehicleNumber = txtCarNo.Text,
                IsTruck = chkTruck.Checked,
                IsPassengerCar = chkPassengerCar.Checked,
                ManufacturerYear = dtpManuYear.Value.Year,
                Weight = string.IsNullOrWhiteSpace(txtWeight.Text) ? (int?)null : int.Parse(txtWeight.Text),
                Seats = string.IsNullOrWhiteSpace(txtSeats.Text) ? (int?)null : int.Parse(txtSeats.Text),
                IsMaintenance = cboStatus.SelectedItem.ToString() == "Available",
                Notes = txtNotes.Text,
                Updated_At = DateTime.Now,
            };
            return vehicle;
        }
        // Chuyển đổi chế độ chỉnh sửa cho ghi chú
        private void ToggleEditModeNote()
        {
            FormHelper.ToggleEditMode(ref this.isEditing_MaintenanceDetails, this.btnEdit_MaintenanceDetail, cboStatus, txtNotes);
        }
        // Kiểm tra nếu đang ở chế độ lưu cho ghi chú
        private bool InSaveModeNote()
        {
            return btnEdit_MaintenanceDetail.Text == Constant.SAVE_MODE;
        }
        // Chỉnh sửa ghi chú của xe
        private void btnEdit_Maintenance_Click(object sender, EventArgs e)
        {
            if (!this.InSaveModeNote())
            {
                this.ToggleEditModeNote();
                return;
            }
            // Xác thực thông tin ghi chú
            if (!this.Validate()) return;
            if (this.ConfirmAction("Are you sure to edit the note?"))
            {
                // Lấy thông tin ghi chú từ trường txtNotes
                Vehicle vehicle = this.GetVehicle();
                // Cập nhật ghi chú thông qua dịch vụ
                if (VehicleService.EditVehicle(vehicle))
                {
                    FormHelper.ShowNotify("Note edited successfully.");
                    this.LoadAllVehicles(); // Tải lại danh sách ghi chú nếu cần
                }
                else
                {
                    FormHelper.ShowError("Failed to edit note.");
                }
            }
            this.ToggleEditModeNote();
        }
        // Xóa xe
        private void btnDeleteVehicle_Click(object sender, EventArgs e)
        {
            if (!this.HasSelectedRow()) return;
            if (this.ConfirmAction($"Are you sure to delete vehicle '{txtCarName.Text}'?"))
            {
                if (VehicleService.DeleteVehicle(int.Parse(lblVehicleD.Text)))
                {
                    FormHelper.ShowNotify("Vehicle deleted successfully.");
                    this.LoadAllVehicles();
                }
                else
                    FormHelper.ShowError("Failed to delete vehicle.");
            }
        }
        // Xác nhận hành động
        private bool ConfirmAction(string message)
        {
            DialogResult result = FormHelper.ShowConfirm(message);
            return result == DialogResult.Yes;
        }
        // Mở form thêm xe mới
        private void btnOpenAddVehicleForm_Click(object sender, EventArgs e)
        {
            FormHelper.OpenFormDialog(new AddVehicleForm());
            this.LoadAllVehicles();
        }
        // Kiểm tra trạng thái xe là truck
        private void chkTruck_CheckedChanged(object sender, EventArgs e)
        {
            this.txtWeight.Enabled = this.chkTruck.Checked;
            if (this.chkTruck.Checked)
            {
                this.chkPassengerCar.Checked = false;
                this.txtSeats.Enabled = false;
            }
        }
        // Kiểm tra trạng thái xe là passenger car
        private void chkPassengerCar_CheckedChanged(object sender, EventArgs e)
        {
            txtSeats.Enabled = chkPassengerCar.Checked;
            if (chkPassengerCar.Checked)
            {
                chkTruck.Checked = false;
                txtWeight.Enabled = false;
            }
        }
        // Gán dữ liệu của xe vào các điều khiển trên form
        private void AssignDataToControls(Vehicle selectedVehicle)
        {
            string vehicleID = selectedVehicle.VehicleID.ToString();
            FormHelper.SetLabelID(lblVehicleD, vehicleID); // Gán ID cho nhãn
            txtCarName.Text = selectedVehicle.VehicleName; // Gán tên xe
            txtCarNo.Text = selectedVehicle.VehicleNumber; // Gán số xe
            dtpManuYear.Value = new DateTime((int)selectedVehicle.ManufacturerYear, 1, 1); // Gán năm sản xuất
            txtWeight.Text = selectedVehicle.Weight.ToString(); // Gán trọng lượng
            txtSeats.Text = selectedVehicle.Seats.ToString(); // Gán số ghế
            txtNotes.Text = selectedVehicle.Notes; // Gán ghi chú
            chkTruck.Checked = (bool)selectedVehicle.IsTruck;
            chkPassengerCar.Checked = (bool)selectedVehicle.IsPassengerCar;
            if (selectedVehicle.IsMaintenance == true)
            {
                cboStatus.Text = "Available";
            }
            else
            {
                cboStatus.Text = "Maintenance";
            }
        }
        // Kiểm tra có dòng nào được chọn trong DataGridView
        private bool HasSelectedRow()
        {
            return dgvVehicles.SelectedRows.Count > 0;
        }
        // Cập nhật dữ liệu điều khiển khi thay đổi lựa chọn trong DataGridView
        private void dgvVehicles_SelectionChanged(object sender, EventArgs e)
        {
            this.UpdateControlsWithSelectedRowData();
        }
        // Cập nhật điều khiển với dữ liệu của dòng đã chọn
        private void UpdateControlsWithSelectedRowData()
        {
            if (!this.HasSelectedRow()) return;
            var selectedRow = dgvVehicles.SelectedRows[0];
            if (selectedRow.Tag is Vehicle selectedVehicle)
                this.AssignDataToControls(selectedVehicle);
        }
        // Lọc dữ liệu trong DataGridView theo trạng thái
        private void cboStatus_Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboStatus_Filter.SelectedIndex < 1)
            {
                this.LoadAllVehicles(); // Tải toàn bộ dữ liệu
            }
            else
            {
                string selectedStatus = cboStatus_Filter.SelectedItem.ToString();
                foreach (DataGridViewRow row in dgvVehicles.Rows)
                {
                    string statusValue = row.Cells[4].Value?.ToString();
                    if ((selectedStatus == "Available" && statusValue == "Available") ||
                        (selectedStatus == "Maintenance" && statusValue == "Maintenance"))
                    {
                        row.Visible = true; // Hiển thị dòng nếu điều kiện khớp
                    }
                    else
                    {
                        row.Visible = false; // Ẩn dòng nếu điều kiện không khớp
                    }
                }
                this.UpdateControlsWithSelectedRowData();
            }
        }
        // Kiểm tra dữ liệu nhập vào trọng lượng xe
        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormHelper.CheckNumericKeyPress(e);
        }
        // Kiểm tra dữ liệu nhập vào số ghế xe
        private void txtSeats_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormHelper.CheckNumericKeyPress(e);
        }
        // Tìm kiếm xe theo từ khóa
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FormHelper.ClearDataGridViewRow(dgvVehicles);
            string keyword = txtSearch.Text.ToLower();
            VehicleService.SearchVehicles(dgvVehicles, keyword);
            this.UpdateControlsWithSelectedRowData();
        }
    }
}
