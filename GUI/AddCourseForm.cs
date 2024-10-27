using BLL.Services;
using DAL;
using GUI.Validators;
using Guna.UI2.WinForms;
using System;
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
            LoadComboboxes();

        }

        private void LoadComboboxes()
        {
            ComboboxService.AssignLicensesToCombobox(cboLicense);
            var licenses = new List<string> { "B", "C", "D", "E" };

            // Gán DataSource cho combobox
            cboLicense.DataSource = licenses;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!this.ValidateFields()) return;

            Course course = this.GetCourse();
            if (CourseService.AddCourse(course))
                FormHelper.ShowNotify("Course added successfully.");
            else
                FormHelper.ShowError("Failed to add course.");
        }

        private Course GetCourse()
        {
            int licenseID;
            switch (cboLicense.Text)
            {
                case "B":
                    licenseID = 1001;
                    break;
                case "C":
                    licenseID = 1002;
                    break;
                case "D":
                    licenseID = 1003;
                    break;
                case "E":
                    licenseID = 1004;
                    break;
                default:
                    licenseID = 0; // Hoặc xử lý trường hợp không hợp lệ
                    break;
            }

            return new Course
            {
                CourseName = txtName.Text,
                LicenseID = licenseID,
                Fee = Convert.ToInt32(txtFee.Text),
                DurationInHours = Convert.ToInt32(txtDurationInHours.Text),
                Created_At = DateTime.Now
            };
        }


        private bool ValidateFields()
        {
            if (!CourseValidator.ValidateCourseName(txtName, toolTip)) return false;

            if (!CourseValidator.ValidateFee(txtFee, toolTip)) return false;

            if (!CourseValidator.ValidateDuration(txtDurationInHours, toolTip)) return false;

            return true;
        }

        private void numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormHelper.CheckNumericKeyPress(e);
        }

        private void cboLicense_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem có lựa chọn nào không
            if (cboLicense.SelectedItem == null) return;

            LoadCourseDetails(); // Gọi hàm để load thông tin khóa học
        }

        private void LoadCourseDetails()
        {
            // Giá tiền cho từng loại bằng
            int price = 0;
            string prefix = string.Empty;

            // Thiết lập giá và prefix dựa vào lựa chọn của người dùng
            switch (cboLicense.SelectedItem.ToString())
            {
                case "B":
                    price = 500000; // Giá cho bằng B
                    prefix = "B-";
                    break;
                case "C":
                    price = 600000; // Giá cho bằng C
                    prefix = "C-";
                    break;
                case "D":
                    price = 700000; // Giá cho bằng D
                    prefix = "D-";
                    break;
                case "E":
                    price = 800000; // Giá cho bằng E
                    prefix = "E-";
                    break;
                default:
                    break;
            }

            // Cập nhật giá tiền vào txtFee
            txtFee.Text = price.ToString();

            // Sinh số ngẫu nhiên cho phần sau của tên
            Random random = new Random();
            int randomNumber = random.Next(1000, 9000); // Sinh số ngẫu nhiên từ 1 đến 99
            DateTime now = DateTime.Now;
            // Cập nhật tên tự sinh vào txtName
            txtName.Text = $"{prefix}{now:yyyyMMddHHmmss}"; 
        }
    }
}
