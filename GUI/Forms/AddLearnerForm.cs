﻿using BLL.Services;
using DAL;
using GUI.Validators;
using Guna.UI2.WinForms;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class AddLearnerForm : Form
    {
        public AddLearnerForm()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
        }

        private void AddLearnerForm_Load(object sender, EventArgs e)
        {
            shadowAddLearnerForm.SetShadowForm(this);
            FormHelper.SetCurrentDate(dtpBeginningDate, dtpDOB);
            this.LoadCombobox();
            FormHelper.FocusControl(txtName);
        }

        private void LoadCombobox()
        {
            ComboboxService.AssignLicensesToCombobox(cboLicenses);
            ComboboxService.AssignAvailableCourseToCombobox(cboCourses);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!this.ValidateFields()) return;

            Learner learner = this.GetLearner();
            int courseID = Convert.ToInt32(cboCourses.SelectedValue.ToString());
            var result = LearnerService.AddLearner(learner, courseID); // Tạo learner mới

            FormHelper.ShowActionResult(result, "Learner added successfully.", "Failed to add learner.");
            if (result) // Nếu thêm thành công thì reset controls
                this.ResetControls();
        }

        private void ResetControls()
        {
            txtName.Text = "";
            txtCitizenId.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            dtpDOB.Value = DateTime.Now;
            txtAddress.Text = "";
            cboGender.SelectedIndex = 0;
            cboNationality.SelectedIndex = 0;
            cboCourses.SelectedIndex = 0;
        }

        private bool ValidateFields()
        {
            if (!LearnerValidator.ValidateFullName(txtName, toolTip)) return false;

            if (!LearnerValidator.ValidateCitizenID(txtCitizenId, toolTip)) return false;

            if (!LearnerValidator.ValidateEmail(txtEmail, toolTip)) return false;

            if (!LearnerValidator.ValidatePhoneNumber(txtPhone, toolTip)) return false;

            if (!LearnerValidator.IsLearnerEligible(dtpDOB, toolTip)) return false;

            if (!LearnerValidator.ValidateAddress(txtAddress, toolTip)) return false;

            if (!LearnerValidator.ValidateSelectedCourse(cboCourses, toolTip)) return false;

            if (!LearnerValidator.ValidateEligibleCourse(dtpDOB, lblLicenseName.Text, cboCourses, toolTip)) return false;

            return true;
        }

        private Learner GetLearner()
        {
            return new Learner()
            {
                FullName = txtName.Text,
                DateOfBirth = DateTime.Parse(dtpDOB.Text),
                Gender = cboGender.Text,
                PhoneNumber = txtPhone.Text,
                Email = txtEmail.Text,
                Address = txtAddress.Text,
                CitizenID = txtCitizenId.Text,
                Nationality = cboNationality.Text,
                StatusID = Constant.StatusID_Active, // Mặc định là 'Active', StatusID = 1001, StatusName = Active
                Created_At = DateTime.Now
            };
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCitizenId_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormHelper.CheckNumericKeyPress(e);
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormHelper.CheckNumericKeyPress(e);
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormHelper.CheckLetterKeyPress(e, txtName);
        }

        private void cboCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FormHelper.HasSelectedItem(cboCourses))
            {
                this.DisplayOrHideCourseDetail(false);
                return;
            }
            this.DisplayOrHideCourseDetail(true);
            this.AssignCourseToDetailLabels();

            string licenseName = cboCourses.Text.Split('-')[0];

            //if (!FormHelper.HasSelectedItem(cboCourses))
            //{
            //    this.TogglePanelVisibility(false, pnlCourseDetails);
            //    this.TogglePanelVisibility(false, pnlLicenseDetails);
            //    return;
            //}
            //this.TogglePanelVisibility(true, pnlCourseDetails);
            //AssignCourseToDetailLabels();

            //string licenseName = cboCourses.Text.Split('-')[0];
            //bool isLicenseVisibleDetails = licenseName == "D" || licenseName == "E";
            //this.TogglePanelVisibility(isLicenseVisibleDetails, pnlLicenseDetails);
        }

        //private void TogglePanelVisibility(bool isVisible, Guna2Panel pnl)
        //{
        //    pnl.Visible = isVisible;

        //    int baseHeight = 390; // Form Height mặc định
        //    int courseDetailsHeight = pnlCourseDetails.Visible ? 455 : baseHeight;
        //    int licenseDetailsHeight = pnlLicenseDetails.Visible ? 540 : courseDetailsHeight;

        //    this.Height = licenseDetailsHeight;
        //    FormHelper.ApplyRoundedCorners(this, 20);
        //}

        //private void DisplayOrHideLicenseDetail(bool showDetails)
        //{
        //    pnlLicenseDetails.Visible = showDetails;
        //    this.Height = showDetails ? 540 : 390;
        //    FormHelper.ApplyRoundedCorners(this, 20);
        //}

        private void AssignCourseToDetailLabels()
        {
            int courseID = Convert.ToInt32(cboCourses.SelectedValue.ToString());
            var course = CourseService.GetCourse(courseID);
            if (course == null) return;
            lblLicenseName.Text = course.License.LicenseName;
            if (course.License.LicenseName != "D" && course.License.LicenseName != "E")
                lblDurationHours.Text = course.DurationInHours.ToString();
            lblStartDate.Text = course.StartDate.Value.ToString("dd/MM/yyyy");
            lblEndDate.Text = course.EndDate.Value.ToString("dd/MM/yyyy");
        }

        private void DisplayOrHideCourseDetail(bool showDetails)
        {
            pnlCourseDetails.Visible = showDetails;
            this.Height = showDetails ? 540 : 465;
            FormHelper.ApplyRoundedCorners(this, 20);
        }

        private void dtpBeginningDate_ValueChanged(object sender, EventArgs e)
        {
            // Lấy hàm có sẵn ở TeachersForm
            TeachersForm.Instance.SetBeginningYears(dtpBeginningDate.Value, txtBeginningYears); 
        }
    }
}
