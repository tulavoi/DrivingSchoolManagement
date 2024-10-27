using BLL.Services;
using BLL.Services.SendEmail;
using DAL;
using GUI.Validators;
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
            this.LoadComboboxes();

            // cboStatus_Filter đang được set mặc định seelctedIndex = 1
            // Gọi event để lọc ngay form vừa load
            cboStatus_Filter_SelectedIndexChanged(sender, e);
        }

        private void LoadComboboxes()
        {
            ComboboxService.AssignStatesToCombobox(cboStates);
        }

        public void LoadAllLearners()
        {
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

            if (this.ConfirmAction($"Are you sure to edit learner '{txtLearnerName.Text}'?"))
            {
                Learner learner = this.GetLearner();
                var result = LearnerService.EditLearner(learner);
                FormHelper.ShowActionResult(result, "Learner edited successfully.", "Failed to edit learner.");
            }

            cboStatus_Filter_SelectedIndexChanged(sender, e);
            this.ToggleEditMode();
        }

        private bool ValidateFields()
        {
            // Kiểm tra các trường thông tin của học viên
            if (!LearnerValidator.ValidateFullName(txtLearnerName, toolTip)) return false;

            if (!LearnerValidator.ValidateCitizenID(txtCitizenId, toolTip)) return false;

            if (!LearnerValidator.ValidateEmail(txtEmail, toolTip)) return false;

            if (!LearnerValidator.ValidatePhoneNumber(txtPhone, toolTip)) return false;

            if (!LearnerValidator.IsLearnerEligible(dtpDOB, toolTip)) return false;

            if (!LearnerValidator.ValidateAddress(txtAddress, toolTip)) return false;

            return true;
        }

        private void ToggleEditMode()
        {
            FormHelper.ToggleEditMode(ref this.isEditing, this.btnEditLearner, txtLearnerName, txtAddress, txtEmail, txtPhone, cboGender, dtpDOB, cboNationality, txtCitizenId, cboStates);
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
                LearnerID = FormHelper.GetObjectID(lblLearnerID.Text),
                FullName = txtLearnerName.Text,
                Address = txtAddress.Text,
                Email = txtEmail.Text,
                CitizenID = txtCitizenId.Text,
                PhoneNumber = txtPhone.Text,
                Gender = cboGender.Text,
                DateOfBirth = dtpDOB.Value,
                StatusID = Convert.ToInt32(cboStates.SelectedValue.ToString()),
                Updated_At = DateTime.Now,
            };
        }

        private void btnOpenAddLearnerForm_Click(object sender, EventArgs e)
        {
            FormHelper.OpenFormDialog(new AddLearnerForm());
            this.LoadAllLearners(); // Reload the learner data after the add form is closed
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Clear old rows in the DataGridView, get the search keyword, and load matching learner data
            FormHelper.ClearDataGridViewRow(dgvLearners);

            string keyword = txtSearch.Text.ToLower();

            // Nếu không nhập ký tự tìm kiếm thì sẽ hiển thị data dựa vào cboStatus
            if (string.IsNullOrEmpty(keyword))
                cboStatus_Filter_SelectedIndexChanged(sender, e);
            else
            {
                LearnerService.SearchLearners(dgvLearners, keyword);
                this.UpdateControlsWithSelectedRowData();
            }
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
            string learnerID = "ID: " + selectedLearner.LearnerID.ToString();

            FormHelper.SetLabelID(lblLearnerID, learnerID);
            txtLearnerName.Text = selectedLearner.FullName;
            txtAddress.Text = selectedLearner.Address;
            txtCitizenId.Text = selectedLearner.CitizenID.ToString();
            txtEmail.Text = selectedLearner.Email;
            txtPhone.Text = selectedLearner.PhoneNumber;
            cboGender.Text = selectedLearner.Gender;
            dtpDOB.Value = (DateTime)selectedLearner.DateOfBirth;
            cboStates.Text = selectedLearner.Status.StatusName;
        }

        private void btnDeleteLearner_Click(object sender, EventArgs e)
        {
            if (!this.HasSelectedRow()) return;

            if (this.ConfirmAction($"Are you sure to delete learner '{txtLearnerName.Text}'?"))
            {
                int learnerID = FormHelper.GetObjectID(lblLearnerID.Text);

                var result = LearnerService.DeleteLearner(learnerID);

                FormHelper.ShowActionResult(result, "Learner deleted successfully.", "Failed to delete learner.");

                // Sau khi xóa xong, hiển thị lại toàn bộ data có status Active
                cboStatus_Filter_SelectedIndexChanged(sender, e);
            }
        }

        private bool HasSelectedRow()
        {
            // Check if any row is selected in the DataGridView
            return dgvLearners.SelectedRows.Count > 0;
        }

        private MailContent CreateMailContent(Learner learner)
        {
            // Tạo nội dung mail dựa trên thông tin của học viên
            return new MailContent
            {
                To = learner.Email, // Địa chỉ email học viên
                Subject = $"Driving School", // Tiêu đề email chứa tên học viên
                Body = $"<h1>Hello {learner.FullName},</h1>" +
                       $"<p>{txtMessage.Text}</p>"
            };
        }

        private void txtCitizenId_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormHelper.CheckNumericKeyPress(e);
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormHelper.CheckNumericKeyPress(e);
        }

        private async void btnSendMail_Click(object sender, EventArgs e)
        {
            var learner = this.GetLearner();
            var mailContent = this.CreateMailContent(learner);
            var result = await FormHelper.SendMailAsync(mailContent);

            FormHelper.ShowActionResult(result, "Email sent successfully.", "Failed to send email.");
        }

        private void cboStatus_Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormHelper.ClearDataGridViewRow(dgvLearners);

            if (FormHelper.HasSelectedItem(cboStatus_Filter))
            {
                string status = cboStatus_Filter.Text;
                LearnerService.FilterLearnersByStatus(dgvLearners, status);
                this.UpdateControlsWithSelectedRowData();
            }
            else
                this.LoadAllLearners();
        }
    }
}
