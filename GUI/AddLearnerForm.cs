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
        }

        private void LoadCombobox()
        {
            ComboboxService.AssignLicensesToCombobox(cboLicenses);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            Learner learner = GetLearner();

            if (LearnerService.AddLearner(learner))
                FormHelper.ShowNotify("Learner added successfully.");
            else
                FormHelper.ShowError("Failed to add learner.");
        }

        private bool ValidateFields()
        {
            if (!LearnerValidator.ValidateFullName(txtName, toolTip)) return false;

            if (!LearnerValidator.ValidateCitizenID(txtCitizenId, toolTip)) return false;

            if (!LearnerValidator.ValidateEmail(txtEmail, toolTip)) return false;

            if (!LearnerValidator.ValidatePhoneNumber(txtPhone, toolTip)) return false;

            if (!LearnerValidator.IsLearnerEligible(dtpDOB, toolTip)) return false;

            if (!LearnerValidator.ValidateAddress(txtAddress, toolTip)) return false;

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
                CurrentLicenseID = Convert.ToInt32(cboLicenses.SelectedValue.ToString()),
                StatusID = 1001, // Mặc định là 'Active', StatusID = 1001, StatusName = Active
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
    }
}
