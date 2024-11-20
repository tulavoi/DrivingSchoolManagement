using BLL.Services;
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
            txtFee.Text = "";
            txtDurationInHours.Text = "";
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
                Fee = Convert.ToInt32(txtFee.Text),
                DurationInHours = Convert.ToInt32(txtDurationInHours.Text),
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

            if (!CourseValidator.ValidateFee(txtFee, toolTip)) return false;

            if (!CourseValidator.ValidateDuration(txtDurationInHours, toolTip)) return false;
            
            return true;
        }

        private void cboLicense_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FormHelper.HasSelectedItem(cboLicense)) return;
            LoadCourseDetails();
        }

        private void LoadCourseDetails()
        {
            // Giá tiền cho từng loại bằng
            int price = 0;
            string prefix = string.Empty;
            string hours = string.Empty;

            // Thiết lập giá và prefix dựa vào lựa chọn của người dùng
            switch (cboLicense.Text)
            {
                case "B":
                    price = Constant.Tuition_B;
                    prefix = "B-";
                    hours = Constant.DurationHours_B.ToString();
                    break;
                case "C":
                    price = Constant.Tuition_C;
                    prefix = "C-";
                    hours = Constant.DurationHours_C.ToString();
                    break;
                case "D":
                    price = Constant.Tuition_D;
                    prefix = "D-";
                    hours = Constant.DurationHours_D.ToString();
                    break;
                case "E":
                    price = Constant.Tuition_E;
                    prefix = "E-";
                    hours = Constant.DurationHours_E.ToString();
                    break;
                default:
                    break;
            }

            txtFee.Text = price.ToString();
            txtDurationInHours.Text = hours.ToString();

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
