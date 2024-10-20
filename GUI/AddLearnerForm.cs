using DAL;
using BLL.Services;
using Guna.UI2.WinForms.Suite;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class AddLearnerForm : Form
    {
        public AddLearnerForm()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20); // Áp dụng góc bo tròn cho form
        }

        private void AddLearnerForm_Load(object sender, EventArgs e)
        {
            shadowAddLearnerForm.SetShadowForm(this); // Đặt bóng cho form
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            // Tạo Learner mới
            Learner learner = GetLearner();

            // Gọi service để thêm learner vào database
            if (LearnerService.AddLearner(learner))
                FormHelper.ShowNotify("Learner added successfully.");
            else
                FormHelper.ShowError("Failed to add learner.");
        }

        // Hàm kiểm tra dữ liệu nhập vào
        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                FormHelper.ShowToolTip(txtName, toolTip, "Please enter learner's full name.");
                return false;
            }

            if (!DateTime.TryParse(dtpDOB.Text, out _))
            {
                FormHelper.ShowToolTip(dtpDOB, toolTip, "Please enter a valid date of birth.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCitizenId.Text))
            {
                FormHelper.ShowToolTip(txtCitizenId, toolTip, "Please enter citizen ID.");
                return false;
            }

            return true;
        }

        // Hàm lấy thông tin Learner từ form để lưu vào database
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
                Status = "Active",  // Mặc định là 'Active'
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now
            };
        }

        // Nút hủy bỏ thao tác
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form khi nhấn hủy
        }
    }
}
