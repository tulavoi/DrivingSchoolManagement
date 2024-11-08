using DAL;
using BLL.Services;
using Guna.UI2.WinForms.Suite;
using System;
using System.Windows.Forms;
using GUI.Validators;

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
            this.LoadCombobox();
            FormHelper.FocusControl(txtName);
        }

        private void LoadCombobox()
        {
            ComboboxService.AssignAvailableToCombobox(cboCourses);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!this.ValidateFields()) return;

            Learner learner = this.GetLearner();
            int courseID = Convert.ToInt32(cboCourses.SelectedValue.ToString());
			var result = LearnerService.AddLearner(learner, courseID); // Tạo learner mới

			FormHelper.ShowActionResult(result, "Learner added successfully.", "Failed to add learner.");
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
				this.ConfigureForm(false);
                return;
			}
			this.ConfigureForm(true);
            this.AssignCourseToDetailLabels();
		}

		private void AssignCourseToDetailLabels()
		{
			int courseID = Convert.ToInt32(cboCourses.SelectedValue.ToString());
			var course = CourseService.GetCourse(courseID);
            if (course == null) return;
            lblLicenseName.Text = course.License.LicenseName;
            lblDurationHours.Text = course.DurationInHours.ToString();
            lblStartDate.Text = course.StartDate.Value.ToString("dd/MM/yyyy");
            lblEndDate.Text = course.EndDate.Value.ToString("dd/MM/yyyy");
		}

		private void ConfigureForm(bool showDetails)
		{
			pnlCourseDetails.Visible = showDetails;
			this.Width = 670;
			this.Height = showDetails ? 455 : 390;
			FormHelper.ApplyRoundedCorners(this, 20);
		}

		
	}
}
