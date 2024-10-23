﻿using BLL.Services;
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
        //private bool ValidateFields()
        //{
        //    // Kiểm tra các trường thông tin của học viên
        //    if (!LearnerValidator.ValidateFullName(txtName, toolTip)) return false;

        //    if (!LearnerValidator.ValidateCitizenID(txtCitizenId, toolTip)) return false;

        //    if (!LearnerValidator.ValidateEmail(txtEmail, toolTip)) return false;

        //    if (!LearnerValidator.ValidatePhoneNumber(txtPhone, toolTip)) return false;

        //    if (!LearnerValidator.ValidateAddress(txtAddress, toolTip)) return false;

        //    // Kiểm tra học viên có đủ điều kiện về độ tuổi
        //    if (!LearnerValidator.IsLearnerEligible(dtpDOB, toolTip)) return false;

        //    return true;
        //}
        private bool ValidateFields()
        {
            // Kiểm tra các trường thông tin của xe
            if (!VehicleValidator.ValidateName(txtName, toolTip)) return false;

            if (!VehicleValidator.ValidateVehicleNumber(txtCarNo, toolTip)) return false;

            if (!VehicleValidator.ValidateManufactureYear(dtpManuYear, toolTip)) return false;

            if (!VehicleValidator.ValidateWeight(txtWeight, toolTip)) return false;

            if (!VehicleValidator.ValidateSeats(txtSeats, toolTip)) return false;

            // Kiểm tra xem xe có phải là xe tải hay xe chở khách không
            if (!VehicleValidator.ValidateTruck(chkTruck, toolTip)) return false;

            if (!VehicleValidator.ValidatePassengerCar(chkPassengerCar, toolTip)) return false;

            return true;
        }

        private Vehicle GetVehicle()
        {
            return new Vehicle()
            {
                VehicleName = txtName.Text,                       // Tên xe
                VehicleNumber = txtCarNo.Text,                   // Số xe
                ManufacturerYear = dtpManuYear.Value.Year,      // Năm sản xuất
                IsTruck = chkTruck.Checked,                       // Kiểm tra nếu là xe tải
                IsPassengerCar = chkPassengerCar.Checked,        // Kiểm tra nếu là xe khách
                Weight = int.Parse(txtWeight.Text),              // Trọng lượng
                Seats = int.Parse(txtSeats.Text),                // Số ghế
                IsMaintenance = true,                            // Mặc định không bảo trì
                Created_At = DateTime.Now,                       // Thời gian tạo
                Updated_At = DateTime.Now                        // Thời gian cập nhật
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
    }
}
