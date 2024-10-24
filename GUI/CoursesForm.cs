using BLL;
using BLL.Services;
using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GUI
{
    public partial class CoursesForm : Form
    {
        #region Properties
        private static CoursesForm instance;

        public static CoursesForm Instance
        {
            get
            {
                if (instance == null) instance = new CoursesForm();
                return instance;
            }
        }

        private bool isEditing = false;
        #endregion

        public CoursesForm()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
        }

        private void CoursesForm_Load(object sender, EventArgs e)
        {
            this.LoadComboboxes();
            this.LoadAllCourses();
        }

        private void LoadAllCourses()
        {
            CourseService.LoadAllCourses(dgvCourses);
            this.UpdateControlsWithSelectedRowData();
        }

        private void UpdateControlsWithSelectedRowData()
        {
            var course = this.GetSelectedCourse();
            this.AssignDataToControls(course);
        }

        private void AssignDataToControls(Course course)
        {
            if (course == null) return;

            string courseID = "ID: " + course.CourseID.ToString();
            FormHelper.SetLabelID(lblCourseID, courseID);

            txtCourseName.Text = course.CourseName;
            cboLicenses.SelectedValue = course.LicenseID;
            txtFee.Text = course.Fee.ToString();
            txtDurationInHours.Text = course.DurationInHours.ToString();
        }

        private Course GetSelectedCourse()
        {
            if (!this.HasSelectedRow()) return null;

            var selectedRow = dgvCourses.SelectedRows[0];
            if (selectedRow.Tag is Course selectedCourse) return selectedCourse;

            return null;
        }

        private bool HasSelectedRow()
        {
            return dgvCourses.SelectedRows.Count > 0;
        }

        private void LoadComboboxes()
        {
            ComboboxService.AssignLicensesToCombobox(cboLicenses);
        }
       

        private void btnEditCourse_Click(object sender, EventArgs e)
        {
            if (!this.InSaveMode())
            {
                this.ToggleEditMode();
                return;
            }

            if (!this.ValidateFields()) return;

            if (this.ConfirmAction($"Are you sure to edit course '{txtCourseName.Text}'?"))
            {
                Course course = this.GetCourse();

                var result = CourseService.EditCourse(course);
                FormHelper.ShowActionResult(result, "Course edited successfully.", "Failed to edit course.");
            }

            this.ToggleEditMode();
            this.LoadAllCourses();
        }

        private Course GetCourse()
        {
            return new Course
            {
                CourseID = FormHelper.GetObjectID(lblCourseID.Text),
                CourseName = txtCourseName.Text,
                LicenseID = Convert.ToInt32(cboLicenses.SelectedValue),
                Fee = Convert.ToDecimal(txtFee.Text),
                DurationInHours = Convert.ToInt32(txtDurationInHours.Text),
                Updated_At = DateTime.Now
            };
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrEmpty(txtCourseName.Text)) return false;
            if (string.IsNullOrEmpty(txtFee.Text)) return false;
            if (string.IsNullOrEmpty(txtDurationInHours.Text)) return false;
            return true;
        }
 


        private void ToggleEditMode()
        {
            FormHelper.ToggleEditMode(ref this.isEditing, this.btnEdit, txtCourseName, txtFee, txtDurationInHours, cboLicenses);
        }

        private bool InSaveMode()
        {
            return btnEdit.Text == "Save";
        }

        private bool ConfirmAction(string message)
        {
            DialogResult result = FormHelper.ShowConfirm(message);
            return result == DialogResult.Yes;
        }

        private void btnOpenAddCourseForm_Click(object sender, EventArgs e)
        {
            FormHelper.OpenFormDialog(new AddCourseForm());
            this.LoadAllCourses();
        }

        private void btnDeleteCourse_Click(object sender, EventArgs e)
        {
            if (!this.HasSelectedRow()) return;

            if (string.IsNullOrEmpty(txtCourseName.Text)) return;

            if (this.ConfirmAction($"Are you sure to delete course '{txtCourseName.Text}'?"))
            {
                int courseID = FormHelper.GetObjectID(lblCourseID.Text);
                var result = CourseService.DeleteCourse(courseID);
                FormHelper.ShowActionResult(result, "Course deleted successfully.", "Failed to delete course.");
                this.LoadAllCourses();
            }
        }

        private void txtSearchCourse_TextChanged(object sender, EventArgs e)
        {
            FormHelper.ClearDataGridViewRow(dgvCourses);

            string keyword = txtSearchCourse.Text.ToLower();
            CourseService.SearchCourses(dgvCourses, keyword);
            this.UpdateControlsWithSelectedRowData();
        }

        private void dgvCourses_SelectionChanged(object sender, EventArgs e)
        {
            this.UpdateControlsWithSelectedRowData();

        }
        private void cboLicenses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLicenses.SelectedIndex < 1)
            {
                this.LoadAllCourses(); // Tải toàn bộ dữ liệu
            }
            else
            {
                // Lấy tên loại bằng từ combobox
                string selectedLicense = cboLicenses.SelectedItem.ToString();
                int selectedLicenseID = GetLicenseIDByName(selectedLicense);

                foreach (DataGridViewRow row in dgvCourses.Rows)
                {
                    // Lấy giá trị LicenseID từ DataGridView
                    int licenseValue = Convert.ToInt32(row.Cells[4].Value); // Thay đổi tên cột nếu cần

                    // So sánh LicenseID
                    if (licenseValue == selectedLicenseID)
                    {
                        row.Visible = true; // Hiển thị dòng nếu điều kiện khớp
                    }
                    else
                    {
                        row.Visible = false; // Ẩn dòng nếu điều kiện không khớp
                    }
                }

                // Cập nhật dữ liệu vào điều khiển (nếu cần)
                this.UpdateControlsWithSelectedRowData();
            }
        }

        // Phương thức để chuyển đổi LicenseName thành LicenseID
        private int GetLicenseIDByName(string licenseName)
        {
            switch (licenseName)
            {
                case "B":
                    return 1001;
                case "C":
                    return 1002;
                case "D":
                    return 1003;
                case "E":
                    return 1004;
                default:
                    return -1; // Hoặc có thể trả về một giá trị mặc định nào đó
            }
        }

    }
}
