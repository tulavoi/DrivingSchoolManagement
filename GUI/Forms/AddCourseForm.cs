﻿using BLL.Services;
using DAL;
using GUI.Validators;
using Guna.UI2.WinForms;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GUI
{
    public partial class AddCourseForm : Form
    {
        public AddCourseForm()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
        }

        private void AddCourseForm_Load(object sender, EventArgs e)
        {
            shadowForm.SetShadowForm(this);
            dtpStartDate.MinDate = DateTime.Now;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!this.ValidateFields()) return;

            Course course = this.GetCourse();
            var result = CourseService.AddCourse(course);
            FormHelper.ShowActionResult(result, "Course added successfully.", "Failed to add course.");
            if (result) // Nếu thêm thành công thì reset controls
                this.ResetControls();
        }

        private void ResetControls()
        {
            cboLicense.SelectedIndex = 0;
            txtName.Text = "";
            dtpStartDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now;
        }

        private Course GetCourse()
        {
            return new Course
            {
                CourseName = txtName.Text,
                LicenseID = this.GetLicenseID(),
                Fee = 0,
                DurationInHours = 0,
                StatusID = Constant.StatusID_Active,
                HoursStudied = 0,
                StartDate = dtpStartDate.Value,
                EndDate = dtpEndDate.Value,
				Created_At = DateTime.Now
            };
        }

        private int? GetLicenseID()
        {
            int licenseID;
            switch (cboLicense.Text)
            {
                case "B":
                    licenseID = Constant.LicenseID_B;
                    break;
                case "C":
                    licenseID = Constant.LicenseID_C;
                    break;
                case "D":
                    licenseID = Constant.LicenseID_D;
                    break;
                case "E":
                    licenseID = Constant.LicenseID_E;
                    break;
                default:
                    licenseID = Constant.LicenseID_None;
                    break;
            }
            return licenseID;
        }

        private bool ValidateFields()
        {
            if (!CourseValidator.ValidateLicense(cboLicense, toolTip)) return false;
            
            if (!CourseValidator.ValidateCourseName(txtName, toolTip)) return false;

            return true;
        }

        private void cboLicense_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FormHelper.HasSelectedItem(cboLicense)) return;
            this.LoadCourseDetails();
        }

        private void LoadCourseDetails()
        {
            // Giá tiền cho từng loại bằng
            string prefix = string.Empty;

            // Thiết lập giá và prefix dựa vào lựa chọn của người dùng
            switch (cboLicense.Text)
            {
                case "B":
                    prefix = "B-";
                    break;
                case "C":
                    prefix = "C-";
                    break;
                case "D":
                    prefix = "D-";
                    break;
                case "E":
                    prefix = "E-";
                    break;
                default:
                    break;
            }

            // Cập nhật tên tự sinh vào txtName
            txtName.Text = $"{prefix}{DateTime.Now.ToString("yyMMddhhmmss")}"; 
        }

		private void dtpStartDate_ValueChanged(object sender, EventArgs e)
		{
            dtpEndDate.Enabled = true;
            dtpEndDate.Value = dtpStartDate.Value.AddMonths(6);
            dtpEndDate.Enabled = false;
		}
	}
}
