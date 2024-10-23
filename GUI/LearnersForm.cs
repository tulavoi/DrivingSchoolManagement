using BLL;
using DAL;
using BLL.Services;
using Guna.UI2.WinForms;
using System;
using System.Windows.Forms;
using BLL.Services.SendEmail;
using System.Threading.Tasks;
using GUI.Validators;

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

                // Xác nhận hành động
                if (this.ConfirmAction($"Are you sure to edit learner '{txtLearnerName.Text}'?"))
                {
                    Learner learner = this.GetLearner();
                    if (LearnerService.EditLearner(learner))
                    {
                        FormHelper.ShowNotify("Learner edited successfully.");
                        this.LoadAllLearners();
                    }
                    else
                    {
                        FormHelper.ShowError("Failed to edit learner.");
                    }
                }

                // Tắt chế độ chỉnh sửa sau khi xác nhận (dù có hoặc không)
                this.ToggleEditMode();
            

        }

        private bool ValidateFields()
        {
            // Kiểm tra các trường thông tin của học viên
            if (!LearnerValidator.ValidateFullName(txtLearnerName, toolTip)) return false;

            if (!LearnerValidator.ValidateCitizenID(txtCitizenId, toolTip)) return false;

            if (!LearnerValidator.ValidateEmail(txtEmail, toolTip)) return false;

            if (!LearnerValidator.ValidatePhoneNumber(txtPhone, toolTip)) return false;

            if (!LearnerValidator.ValidateAddress(txtAddress, toolTip)) return false;

            // Kiểm tra học viên có đủ điều kiện về độ tuổi
            if (!LearnerValidator.IsLearnerEligible(dtpDOB, toolTip)) return false;

            return true;
        }


        private void ToggleEditMode()
        {
            FormHelper.ToggleEditMode(ref this.isEditing, this.btnEditLearner, txtLearnerName, txtAddress, txtEmail, txtPhone, cboGender, dtpDOB, cboNationality, txtCitizenId);
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
                CitizenID = txtCitizenId.Text,
                PhoneNumber = txtPhone.Text,
                Gender = cboGender.Text,
                DateOfBirth = dtpDOB.Value,
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

            if (this.ConfirmAction($"Are you sure to delete learner '{txtLearnerName.Text}'?"))
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

        private async void btnSendSMS_Click(object sender, EventArgs e)
        {

            // Lấy thiết lập gửi mail
            var mailSetting = FormHelper.GetMailSettings();

            // Kiểm tra tính hợp lệ của thiết lập mail
            if (!FormHelper.IsMailSettingValid(mailSetting))
            {
                FormHelper.ShowError("MailSetting invalid.");
                return;
            }

            // Lấy học viên hiện tại đang được chọn
            var learner = this.GetLearner();

            // Tạo nội dung email dựa trên thông tin của học viên
            var mailContent = this.CreateMailContent(learner);

            // Khởi tạo dịch vụ gửi mail
            var sendMailService = new SendMailService(mailSetting);
            // Gửi email bằng Task.Run để chạy ngầm, tránh khóa giao diện
            var result = await Task.Run(() => sendMailService.SendMail(mailContent));

            // Hiển thị kết quả gửi mail (thành công hoặc thất bại)
            FormHelper.ShowActionResult(result, "Email sent successfully.", "Failed to send learner information.");
        }

        private MailContent CreateMailContent(Learner learner)
        {
            // Tạo nội dung mail dựa trên thông tin của học viên
            return new MailContent
            {
                To = learner.Email, // Địa chỉ email học viên
                Subject = $"Learner Information: {learner.FullName}", // Tiêu đề email chứa tên học viên
                Body = $"<h1>Thông tin học viên</h1>" +
                       $"<p>Họ và tên: {learner.FullName}</p>" +
                       $"<p>Ngày sinh: {learner.DateOfBirth.ToString()}</p>" +
                       $"<p>Giới tính: {learner.Gender}</p>" +
                       $"<p>Số điện thoại: {learner.PhoneNumber}</p>" +
                       $"<p>Địa chỉ: {learner.Address}</p>" +
                       $"<p>Email: {learner.Email}</p>" +
                       $"<p>Số CMND/CCCD: {learner.CitizenID}</p>" +
                       $"<p>Loại bằng lái hiện tại: {learner.CurrentLicenseID}</p>"
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
    }
}
