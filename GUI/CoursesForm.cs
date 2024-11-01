using BLL;
using BLL.Services;
using DAL;
using GUI.Validators;
using System;
using System.Drawing;
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
            cboStatus_Filter_SelectedIndexChanged(sender, e);
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

            int? durationHours = course.DurationInHours;
            int? hoursStudied = course.HoursStudied;

            txtCourseName.Text = course.CourseName;
            cboLicenses.SelectedValue = course.LicenseID;
            txtFee.Text = course.Fee.ToString();
            txtDurationInHours.Text = durationHours.ToString();
            cboStates.Text = course.Status.StatusName;
            this.SetLearnerName(course.CourseID);
            txtHoursStudied.Text = hoursStudied.ToString();

            if (hoursStudied == durationHours)
            {
                lblCompleteCourse.Text = "Complete";
                lblCompleteCourse.ForeColor = Color.FromArgb(90, 211, 116);

                if (string.IsNullOrEmpty(txtLearner.Tag.ToString())) return;
                int learnerID = Convert.ToInt32(txtLearner.Tag.ToString());
                LearnerService.UpdateLicense(learnerID, FormHelper.GetObjectID(lblCourseID.Text));
            }
            else
            {
                lblCompleteCourse.Text = "Incomplete";
                lblCompleteCourse.ForeColor = Color.FromArgb(253, 100, 119);
            }
        }

        private void SetLearnerName(int courseID)
        {
            Schedule schedule = ScheduleBLL.Instance.GetLearnerByCourseID(courseID);
            if (schedule == null)
            {
                txtLearner.Text = string.Empty;
                txtLearner.Tag = string.Empty;
                return;
            }
            //txtLearner.Text = schedule.Enrollment.Learner.FullName;
            //txtLearner.Tag = schedule.Enrollment.Learner.LearnerID;
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
            ComboboxService.AssignStatesToCombobox(cboStates);
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
            cboStatus_Filter_SelectedIndexChanged(sender, e);
        }

        private Course GetCourse()
        {
            return new Course
            {
                CourseID = FormHelper.GetObjectID(lblCourseID.Text),
                CourseName = txtCourseName.Text,
                LicenseID = Convert.ToInt32(cboLicenses.SelectedValue),
                Fee = Convert.ToInt32(txtFee.Text),
                StatusID = Convert.ToInt32(cboStates.SelectedValue.ToString()),
                DurationInHours = Convert.ToInt32(txtDurationInHours.Text),
                Updated_At = DateTime.Now
            };
        }

        private bool ValidateFields()
        {
            if (!CourseValidator.ValidateLicense(cboLicenses, toolTip)) return false;
            if (!CourseValidator.ValidateFee(txtFee, toolTip)) return false;
            if (!CourseValidator.ValidateDuration(txtDurationInHours, toolTip)) return false;
            if (!CourseValidator.ValidateCourseName(txtCourseName, toolTip)) return false;
            return true;
        }

        private void ToggleEditMode()
        {
            FormHelper.ToggleEditMode(ref this.isEditing, this.btnEdit, cboLicenses, cboStates);
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
            cboStatus_Filter_SelectedIndexChanged(sender, e);
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
                cboStatus_Filter_SelectedIndexChanged(sender, e);
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

        private void cboStatus_Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormHelper.ClearDataGridViewRow(dgvCourses);
            if (FormHelper.HasSelectedItem(cboStatus_Filter))
            {
                string status = cboStatus_Filter.Text;
                CourseService.FilterLearnersByStatus(dgvCourses, status);
                this.UpdateControlsWithSelectedRowData();
            }
            else
                this.LoadAllCourses();
        }

        private void cboLicenses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FormHelper.HasSelectedItem(cboLicenses)) return;
            string licenseName = cboLicenses.Text;
            string courseName = txtCourseName.Text;
            string[] parts = courseName.Split('-');
            txtCourseName.Text = $"{licenseName}-{parts[1]}";

            if (licenseName == "B")
            {
                txtFee.Text = Constant.Tuition_B.ToString();
                txtDurationInHours.Text = Constant.DurationHours_B.ToString();
            }
            if (licenseName == "C")
            {
                txtFee.Text = Constant.Tuition_C.ToString();
                txtDurationInHours.Text = Constant.DurationHours_C.ToString();
            }
            if (licenseName == "D")
            {
                txtFee.Text = Constant.Tuition_D.ToString();
                txtDurationInHours.Text = Constant.DurationHours_D.ToString();
            }
                
            if (licenseName == "E")
            {
                txtFee.Text = Constant.Tuition_E.ToString();
                txtDurationInHours.Text = Constant.DurationHours_E.ToString();
            }
        }

        private void numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormHelper.CheckNumericKeyPress(e);
        }
    }
}
