using BLL;
using DAL;
using GUI.Services;
using Guna.UI2.WinForms;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class LearnersForm : Form
    {
        #region Properties
        private bool isEditing = false;

        private static LearnersForm instance;

        public static LearnersForm Instance
        {
            get
            {
                if (instance == null) instance = new LearnersForm();
                return instance;
            }
        }
        #endregion

        public LearnersForm()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
        }

        public void LearnersForm_Load(object sender, EventArgs e)
        {
            this.LoadAllLearners();
        }

        public void LoadAllLearners()
        {
            // Load all learner data into the DataGridView
            LearnerService.LoadAllLearners(dgvLearners);
            this.UpdateControlsWithSelectedRowData();
        }

        private void btnEditLearner_Click(object sender, EventArgs e)
        {
            if (!this.InSaveMode())
            {
                this.ToggleEditMode();
                return;
            }

            if (!this.ValidateFields()) return;

            if (this.ConfirmAction($"Are you sure to edit learner '{lblLearnerID.Text}'?"))
            {
                Learner learner = this.GetLearner();
                if (LearnerService.EditLearner(learner))
                {
                    FormHelper.ShowNotify("Learner edited successfully.");
                    this.LoadAllLearners();
                }
                else
                    FormHelper.ShowError("Failed to edit learner.");
            }
            else return;
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrEmpty(txtLearnerName.Text))
            {
                FormHelper.ShowToolTip(txtLearnerName, toolTip, "Please enter the learner's name.");
                return false;
            }
            if (cboGender.SelectedIndex < 0)
            {
                FormHelper.ShowToolTip(cboGender, toolTip, "Please select a gender.");
                return false;
            }
            return true;
        }

        private void ToggleEditMode()
        {
            FormHelper.ToggleEditMode(ref this.isEditing, this.btnEditLearner, txtLearnerName, txtAddress, txtEmail, txtPhone, cboGender, dtpDOB);
        }

        private bool InSaveMode()
        {
            return btnEditLearner.Text == Constant.SAVE_MODE;
        }

        private bool ConfirmAction(string message)
        {
            DialogResult result = FormHelper.ShowConfirm(message);
            return result == DialogResult.Yes;
        }

        private Learner GetLearner()
        {
            return new Learner
            {
                LearnerID = int.Parse(lblLearnerID.Text),
                FullName = txtLearnerName.Text,
                Address = txtAddress.Text,
                Email = txtEmail.Text,
                CitizenID=txtCitizenId.Text,
                PhoneNumber = txtPhone.Text,
                Gender = cboGender.Text,
                DateOfBirth = dtpDOB.Value,
                Updated_At = DateTime.Now,
            };
        }

        private void btnOpenAddLearnerForm_Click(object sender, EventArgs e)
        {
            FormHelper.OpenPopupForm(new AddLearnerForm());
            this.LoadAllLearners(); // Reload the learner data after the add form is closed
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Clear old rows in the DataGridView, get the search keyword, and load matching learner data
            FormHelper.ClearDataGridViewRow(dgvLearners);

            string keyword = txtSearch.Text.ToLower();
            LearnerService.SearchLearners(dgvLearners, keyword);
            this.UpdateControlsWithSelectedRowData();
        }

        private void dgvLearners_SelectionChanged(object sender, EventArgs e)
        {
            this.UpdateControlsWithSelectedRowData();
        }

        private void UpdateControlsWithSelectedRowData()
        {
            // Check if a row is selected and assign selected learner data to controls
            if (!this.HasSelectedRow()) return;

            var selectedRow = dgvLearners.SelectedRows[0];

            if (selectedRow.Tag is Learner selectedLearner)
                this.AssignDataToControls(selectedLearner);
        }

        private void AssignDataToControls(Learner selectedLearner)
        {
            // Assign learner data to form controls
            string learnerID = selectedLearner.LearnerID.ToString();

            FormHelper.SetLabelID(lblLearnerID, learnerID);
            txtLearnerName.Text = selectedLearner.FullName;
            txtAddress.Text = selectedLearner.Address;
            txtCitizenId.Text = selectedLearner.CitizenID.ToString();
            txtEmail.Text = selectedLearner.Email;
            txtPhone.Text = selectedLearner.PhoneNumber;
            cboGender.Text = selectedLearner.Gender;
            dtpDOB.Value = (DateTime)selectedLearner.DateOfBirth;
        }

        private void btnDeleteLearner_Click(object sender, EventArgs e)
        {
            if (!this.HasSelectedRow()) return;

            if (this.ConfirmAction($"Are you sure to delete learner '{lblLearnerID.Text}'?"))
            {
                if (LearnerService.DeleteLearner(int.Parse(lblLearnerID.Text)))
                {
                    FormHelper.ShowNotify("Learner deleted successfully.");
                    this.LoadAllLearners();
                }
                else
                    FormHelper.ShowError("Failed to delete learner.");
            }
        }

        private bool HasSelectedRow()
        {
            // Check if any row is selected in the DataGridView
            return dgvLearners.SelectedRows.Count > 0;
        }
    }
}
