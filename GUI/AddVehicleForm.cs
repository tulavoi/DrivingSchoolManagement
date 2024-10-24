using BLL.Services;
using DAL;
using GUI.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class AddVehicleForm : Form
    {
        public AddVehicleForm()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
        }

		private void AddVehicleForm_Load(object sender, EventArgs e)
		{
            this.shadowForm.SetShadowForm(this);

			// Nếu như khởi động form mà chkPassengerCar đang được chọn thì bật txtSeats
			if (this.chkPassengerCar.Checked) this.txtSeats.Enabled = true;

            FormHelper.SetDateTimePickerMaxValue(dtpManuYear);
		}

		private void chkPassengerCar_CheckedChanged(object sender, EventArgs e)
		{
			// Optimze this code
			if (this.chkPassengerCar.Checked)
			{
				this.txtWeight.Enabled = false;

				this.chkTruck.Checked = false;
				this.txtSeats.Enabled = true;
			}
			else
			{
				if (!this.chkPassengerCar.Checked)
					this.txtSeats.Enabled = false;
			}
		}

		private void chkTruck_CheckedChanged(object sender, EventArgs e)
		{
			// Optimze this code
			if (this.chkTruck.Checked)
			{
				this.txtWeight.Enabled = true;

				this.chkPassengerCar.Checked = false;
				this.txtSeats.Enabled = false;
			}
			else
			{
				if (!this.chkTruck.Checked)
					this.txtWeight.Enabled = false;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            // Tạo vehicle mới
            Vehicle vehicle = GetVehicle();

            // Gọi service để thêm vehicle vào database
            if (VehicleService.AddVehicle(vehicle))
                FormHelper.ShowNotify("Vehicle added successfully.");
            else
                FormHelper.ShowError("Failed to add vehicle.");
        }

        private bool ValidateFields()
        {
            // Kiểm tra các trường thông tin của xe
            if (!VehicleValidator.ValidateName(txtCarName, toolTip)) return false;

            if (!VehicleValidator.ValidateVehicleNumber(txtCarNo, toolTip)) return false;

            if (!VehicleValidator.ValidateManufactureYear(dtpManuYear, toolTip)) return false;

            if (!VehicleValidator.ValidateWeightAndSeats(chkPassengerCar, chkTruck, txtSeats, txtWeight, toolTip)) return false;

            return true;
        }

        private Vehicle GetVehicle()
        {
            return new Vehicle()
            {
                VehicleName = txtCarName.Text,                      
                VehicleNumber = txtCarNo.Text,                
                ManufacturerYear = dtpManuYear.Value.Year,    
                IsTruck = chkTruck.Checked,                      
                IsPassengerCar = chkPassengerCar.Checked,     
                Weight = !string.IsNullOrEmpty(txtWeight.Text) ? int.Parse(txtWeight.Text) : 0,         
                Seats = !string.IsNullOrEmpty(txtSeats.Text) ? int.Parse(txtSeats.Text) : 0,            
                IsMaintenance = true,               
                Created_At = DateTime.Now,             
            };
        }

        private void txtSeats_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormHelper.CheckNumericKeyPress(e);
        }

        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormHelper.CheckNumericKeyPress(e);

        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormHelper.CheckLetterKeyPress(e, txtCarName);
        }
    }
}
