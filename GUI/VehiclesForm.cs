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
        public void VehiclesForm_Load(object sender, EventArgs e)
        {
            this.LoadAllVehicles();
            
        }
        private void InitializeComboBox()
        {
            cboStatus_Filter.Items.Clear();
            cboStatus_Filter.Items.Add("Status"); // Thêm mục mặc định
            cboStatus_Filter.Items.Add("Available");
            cboStatus_Filter.Items.Add("Maintenance");
            cboStatus_Filter.SelectedIndex = 0; // Chọn mục mặc định
        }
        public void LoadAllVehicles()
        {
            // Load all learner data into the DataGridView
            VehicleService.LoadAllVehicles(dgvVehicles);
            this.UpdateControlsWithSelectedRowData();
        }
        //      private void btnEdit_BasicDetail_Click(object sender, EventArgs e)
        //      {
        //	if (this.chkTruck.Checked) this.txtWeight.Enabled = true;

        //	if (this.chkPassengerCar.Checked) this.txtSeats.Enabled = true;

        //	FormHelper.ToggleEditMode(ref isEditing_BasicDetails, this.btnEdit_BasicDetail, this.txtCarName, this.txtCarNo, this.dtpManuYear, this.chkTruck, this.chkPassengerCar);
        //}
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
                        this.LoadAllVehicles(); // Tải lại danh sách xe
                    }
                    else
                    {
                        FormHelper.ShowError("Failed to edit vehicle.");
                    }

            }
            this.ToggleEditMode();


        }



        private bool InSaveMode()
        {
            return btnEdit_BasicDetail.Text == Constant.SAVE_MODE;
        }

        private void ToggleEditMode()
        {
            FormHelper.ToggleEditMode(ref this.isEditing_BasicDetails, this.btnEdit_BasicDetail,
                                       txtCarName,txtCarNo,
                                       dtpManuYear,chkTruck,
                                       chkPassengerCar, this.txtWeight,
                                       txtSeats, this.txtNotes,
                                       this.cboStatus);
        }


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

                // Kiểm tra nếu txtSeats không rỗng, nếu không gán null
                Seats = string.IsNullOrWhiteSpace(txtSeats.Text) ? (int?)null : int.Parse(txtSeats.Text),
                IsMaintenance = cboStatus.SelectedItem.ToString() == "Available",
                Notes = txtNotes.Text,
                Updated_At = DateTime.Now,
            };

            return vehicle;
        }


        private void ToggleEditModeNote()
        {
            FormHelper.ToggleEditMode(ref this.isEditing_MaintenanceDetails, this.btnEdit_MaintenanceDetail, cboStatus, txtNotes);
        }
        private bool InSaveModeNote()
        {
            return btnEdit_MaintenanceDetail.Text == Constant.SAVE_MODE;
        }
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
                    Vehicle vehicle=this.GetVehicle();

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



        private bool ConfirmAction(string message)
        {
            DialogResult result = FormHelper.ShowConfirm(message);
            return result == DialogResult.Yes;
        }
        private void btnOpenAddVehicleForm_Click(object sender, EventArgs e)
		{
			FormHelper.OpenFormDialog(new AddVehicleForm());
            this.LoadAllVehicles();
        }

		private void chkTruck_CheckedChanged(object sender, EventArgs e)
		{
			this.txtWeight.Enabled = this.chkTruck.Checked;

			if (this.chkTruck.Checked)
			{
				this.chkPassengerCar.Checked = false;
				this.txtSeats.Enabled = false;
			}
		}

		private void chkPassengerCar_CheckedChanged(object sender, EventArgs e)
		{
			txtSeats.Enabled = chkPassengerCar.Checked;

			if (chkPassengerCar.Checked)
			{
				chkTruck.Checked = false;
				txtWeight.Enabled = false;
			}
		}
       
        private void AssignDataToControls(Vehicle selectedVehicle)
        {
            // Gán dữ liệu của xe vào các điều khiển trên form
            string vehicleID = selectedVehicle.VehicleID.ToString();

            FormHelper.SetLabelID(lblVehicleD, vehicleID); // Gán ID cho nhãn
            txtCarName.Text = selectedVehicle.VehicleName; // Gán tên xe
            txtCarNo.Text = selectedVehicle.VehicleNumber; // Gán số xe
            dtpManuYear.Value = new DateTime((int)selectedVehicle.ManufacturerYear, 1, 1); // Gán năm sản xuất
            txtWeight.Text = selectedVehicle.Weight.ToString(); // Gán trọng lượng
            txtSeats.Text = selectedVehicle.Seats.ToString(); // Gán số ghế
            txtNotes.Text = selectedVehicle.Notes; // Gán ghi chú
            // Gán trạng thái cho checkbox
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

        private bool HasSelectedRow()
        {
            // Check if any row is selected in the DataGridView
            return dgvVehicles.SelectedRows.Count > 0;
        }
        private void dgvVehicles_SelectionChanged(object sender, EventArgs e)
        {
            this.UpdateControlsWithSelectedRowData();
        }
        private void UpdateControlsWithSelectedRowData()
        {
            // Check if a row is selected and assign selected Vehicle data to controls
            if (!this.HasSelectedRow()) return;

            var selectedRow = dgvVehicles.SelectedRows[0];

            if (selectedRow.Tag is Vehicle selectedVehicle)
                this.AssignDataToControls(selectedVehicle);
        }

        private void cboStatus_Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
          
                // Xóa các dòng hiện tại trong DataGridView
                FormHelper.ClearDataGridViewRow(dgvVehicles);

                // Nếu chưa chọn bất kỳ trạng thái nào (Index < 1), tải tất cả xe
                if (cboStatus_Filter.SelectedIndex < 1)
                {
                    this.LoadAllVehicles();
                }
                else
                {
                    // Lấy trạng thái đã chọn
                    string status = cboStatus_Filter.SelectedItem.ToString();

                    // Lọc xe dựa trên trạng thái đã chọn
                    if (status == "Available")
                    {
                        VehicleService.FilterVehiclesByStatus(dgvVehicles, "Available");
                    }
                    else if (status == "Maintenance")
                    {
                        VehicleService.FilterVehiclesByStatus(dgvVehicles, "Maintenance");
                    }

                    // Cập nhật controls với dữ liệu của dòng đã chọn
                    this.UpdateControlsWithSelectedRowData();
                }
        }

        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormHelper.CheckNumericKeyPress(e);

        }

        private void txtSeats_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormHelper.CheckNumericKeyPress(e);

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Clear old rows in the DataGridView, get the search keyword, and load matching learner data
            FormHelper.ClearDataGridViewRow(dgvVehicles);

            string keyword = txtSearch.Text.ToLower();
            VehicleService.SearchVehicles(dgvVehicles, keyword);
            this.UpdateControlsWithSelectedRowData();
        }
    }
}
