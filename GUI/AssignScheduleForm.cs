using BLL.Services;
using DAL;
using GUI.Validators;
using Guna.UI2.WinForms;
using Org.BouncyCastle.Cms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GUI
{
    public partial class AssignScheduleForm : Form
    {
        #region Properties
        private DateTime _date;
        private string activeStatus = "Active";
        #endregion

        public AssignScheduleForm(DateTime date)
        {
            InitializeComponent();
            _date = date;
            FormHelper.ApplyRoundedCorners(this, 20);
        }

        private void AssignScheduleForm_Load(object sender, EventArgs e)
        {
            shadowForm.SetShadowForm(this);

            this.AssignDateToLabel();

            this.LoadComboboxes();
        }

        private void LoadComboboxes()
        {
            ComboboxService.AssignCoursesToCombobox(cboCourses, this.activeStatus);
        }

        private void AssignDateToLabel()
        {
            lblDateAssign.Text = _date.ToString("dd/MM/yyyy");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string GetKeyword(Guna2TextBox txtSearchLearner)
        {
            return txtSearchLearner.Text;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!this.ValidateFields()) return;

            Schedule schedule = this.GetSchedule();

            string errorMessage = "";
            var result = ScheduleService.AddSchedule(schedule, out errorMessage);

            if (result)
            {
                CourseService.UpdateHoursStudied(Convert.ToInt32(cboCourses.SelectedValue), 2);
                FormHelper.ShowNotify("Schedule added successfully.");
                this.ResetCombobox();

			}
            else
                this.HandleScheduleAddError(errorMessage);
        }

        private void HandleScheduleAddError(string errorMessage)
        {
            if (!string.IsNullOrEmpty(errorMessage))
                FormHelper.ShowError(errorMessage);
            else
                FormHelper.ShowError("Failed to add schedule");
        }

        private Schedule GetSchedule()
        {
            return new Schedule
            {
                Enrollment = new Enrollment
                {
                    LearnerID = Convert.ToInt32(txtLearnerName.Tag),
                    CourseID = Convert.ToInt32(cboCourses.SelectedValue),
                },
                TeacherID = Convert.ToInt32(cboTeachers.SelectedValue),
                VehicleID = Convert.ToInt32(cboVehicles.SelectedValue),
                SessionID = Convert.ToInt32(cboSessions.SelectedValue),
                SessionDate = DateTime.Parse(lblDateAssign.Text),
                StatusID = Constant.StatusID_Active,
                Created_At = DateTime.Now
            };
        }

        private bool ValidateFields()
        {
            var comboBoxes = new List<Guna2ComboBox> { cboCourses, cboTeachers, cboVehicles, cboSessions };

            foreach (var comboBox in comboBoxes)
            {
                if (!ScheduleValidator.CheckRequiredAndShowToolTip(comboBox, toolTip))
                    return false;
            }
            return true;
        }

        private void ResetCombobox()
        {
            cboCourses.Enabled = false;
            cboCourses.SelectedIndex = 0;
            cboTeachers.Enabled = false;
            cboTeachers.SelectedIndex = 0;
            cboVehicles.Enabled = false;
            cboVehicles.SelectedIndex = 0;
            cboSessions.Enabled = false;
            cboSessions.SelectedIndex = 0;
        }

        private void cboCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FormHelper.HasSelectedItem(cboCourses))
            {
                cboSessions.Enabled = false;
                this.ConfigureForm(false);
				return;
            }
			cboSessions.Enabled = true;
            int courseID = Convert.ToInt32(cboCourses.SelectedValue.ToString());

			this.ConfigureForm(true);
			this.SetLearnerName(courseID);
            ComboboxService.AssignSessionsToCombobox(cboSessions, courseID, _date);
        }

		private void ConfigureForm(bool showDetails)
		{
			pnlLearner.Visible = showDetails;
			this.Width = 590;
			this.Height = showDetails ? 385 : 350;
			FormHelper.ApplyRoundedCorners(this, 20);
		}

		private void SetLearnerName(int courseID)
		{
			var enrollment = EnrollmentService.GetEnrollmentByCourseID(courseID);
            if (enrollment == null) return;
            txtLearnerName.Text = enrollment.Learner.FullName;
            txtLearnerName.Tag = enrollment.Learner.LearnerID;
		}

		private void cboTeachers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FormHelper.HasSelectedItem(cboTeachers))
            {
                cboVehicles.Enabled = false;
                return;
            }
            cboVehicles.Enabled = true;
            int courseID = Convert.ToInt32(cboCourses.SelectedValue.ToString());
			int sessionID = Convert.ToInt32(cboSessions.SelectedValue.ToString());
            ComboboxService.AssignVehiclesToCombobox(cboVehicles, courseID, sessionID, _date);
        }

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
            string keyword = this.GetKeyword(txtSearchCourse);
            string status = "Active";
            CourseService.SearchCourses(cboCourses, keyword, status);
        }

		private void cboSessions_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!FormHelper.HasSelectedItem(cboSessions))
			{
				cboTeachers.Enabled = false;
				return;
			}
			cboTeachers.Enabled = true;
			int courseID = Convert.ToInt32(cboCourses.SelectedValue.ToString());
			int sessionID = Convert.ToInt32(cboSessions.SelectedValue.ToString());

			ComboboxService.AssignTeachersToCombobox(cboTeachers, courseID, sessionID, _date);
		}
	}
}
