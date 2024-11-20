using BLL.Services;
using DAL;
using GUI.Validators;
using Guna.UI2.WinForms;
using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GUI
{
    public partial class AddTeacherForm : Form
    {
        public AddTeacherForm()
        {
            InitializeComponent();

            FormHelper.ApplyRoundedCorners(this, 20);
        }

        private void AddTeacherForm_Load(object sender, EventArgs e)
        {
            shadowAddTeacherForm.SetShadowForm(this);
            this.LoadComboboxes();
        }

        private void LoadComboboxes()
        {
            ComboboxService.AssignLicensesToCombobox(cboLicenses);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!this.ValidateFields()) return;

            Teacher teacher = this.GetTeacher();
            if (TeacherService.AddTeacher(teacher))
            {
                FormHelper.ShowNotify("Teacher added successfully.");
                this.ResetControls();
            }
            else
                FormHelper.ShowError("Failed to add teacher.");
        }

        private void ResetControls()
        {
            txtFullName.Text = "";
            txtCitizenId.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            dtpDOB.Value = DateTime.Now;
            txtAddress.Text = "";
            cboGender.SelectedIndex = 0;
            cboNationality.SelectedIndex = 0;
            cboLicenses.SelectedIndex = 0;
            txtLicenseNumber.Text = "";
            dtpBeginningDate.Value = DateTime.Now;
        }

        private Teacher GetTeacher()
        {
            return new Teacher
            {
                FullName = txtFullName.Text,
                CitizenID = txtCitizenId.Text,
                DateOfBirth = dtpDOB.Value,
                Gender = cboGender.Text,
				PhoneNumber = txtPhone.Text,
                Email = txtEmail.Text,
                Nationality = cboNationality.Text,
                Address = txtAddress.Text,
                EmploymentDate = DateTime.Now,
                LicenseID = Convert.ToInt32(cboLicenses.SelectedValue),
                LicenseNumber = txtLicenseNumber.Text,
                BeginningDate = dtpBeginningDate.Value,
                StatusID = Constant.StatusID_Active,
                Created_At = DateTime.Now
            };
        }

        private bool ValidateFields()
        {
            string license = cboLicenses.Text;

            if (!TeacherValidator.ValidateFullName(txtFullName, toolTip)) return false;

            if (!TeacherValidator.ValidateCitizenID(txtCitizenId, toolTip)) return false;

            if (!TeacherValidator.ValidateEmail(txtEmail, toolTip)) return false;

            if (!TeacherValidator.ValidatePhoneNumber(txtPhone, toolTip)) return false;

            if (!TeacherValidator.ValidateAddress(txtAddress, toolTip)) return false;

            if (!TeacherValidator.ValidateLicense(cboLicenses, toolTip)) return false;

            if (!TeacherValidator.ValidateLicenseNumber(txtLicenseNumber, toolTip)) return false;

            if (!TeacherValidator.IsTeacherEligible(dtpDOB, dtpBeginningDate, license, toolTip)) return false;

            return true;
        }

        private void txtFullName_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormHelper.CheckLetterKeyPress(e, txtFullName);
        }

        private void numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormHelper.CheckNumericKeyPress(e);
        }

		private void dtpBeginningDate_ValueChanged(object sender, EventArgs e)
		{
			TeachersForm.Instance.SetBeginningYears(dtpBeginningDate.Value, txtBeginningYears);
		}
	}
}
