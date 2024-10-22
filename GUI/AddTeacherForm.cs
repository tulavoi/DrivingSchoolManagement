using BLL.Services;
using GUI.Validators;
using Guna.UI2.WinForms;
using System;
using System.Windows.Forms;

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

            FormHelper.SetDateTimePickerMaxValue(dtpDOB, dtpGraduated);
        }

        private void LoadComboboxes()
        {
            ComboboxService.AssignLicensesToCombobox(cboLicense);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!this.ValidateFields()) return;

        }

        private bool ValidateFields()
        {
            string license = cboLicense.Text;

            if (!TeacherValidator.ValidateFullName(txtFullName, toolTip)) return false;

            if (!TeacherValidator.ValidateCitizenID(txtCitizenId, toolTip)) return false;

            if (!TeacherValidator.ValidateEmail(txtEmail, toolTip)) return false;

            if (!TeacherValidator.ValidatePhoneNumber(txtPhone, toolTip)) return false;

            if (!TeacherValidator.ValidateAddress(txtAddress, toolTip)) return false;

            if (!TeacherValidator.IsTeacherEligible(dtpDOB, dtpGraduated, license, toolTip)) return false;

            return true;
        }

        private void txtFullName_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormHelper.CheckLetterKeyPress(e);
        }

        private void numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormHelper.CheckNumericKeyPress(e);
        }

        private void dtpGraduated_ValueChanged(object sender, EventArgs e)
        {
            TeachersForm.Instance.SetGraduateYears(dtpGraduated.Value, txtGraduateYears);
        }
    }
}
