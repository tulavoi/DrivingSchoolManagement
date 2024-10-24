using BLL.Services;
using DAL;
using GUI.Validators;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GUI
{
    public partial class AssignScheduleForm : Form
    {
        #region Properties
        private DateTime _date;
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
            ComboboxService.AssignLearnersToCombobox(cboLearners);
            ComboboxService.AssignCoursesToCombobox(cboCourses);
            ComboboxService.AssignTeachersToCombobox(cboTeachers);
            ComboboxService.AssignVehiclesToCombobox(cboVehicles);
            ComboboxService.AssignSessionsToCombobox(cboSessions);
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

        private void txtSearchLearner_TextChanged(object sender, EventArgs e)
        {
            string keyword = this.GetKeyword(txtSearchLearner);
            LearnerService.SearchLearners(cboLearners, keyword);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!this.ValidateFields()) return;

            Schedule schedule = this.GetSchedule();

            string errorMessage = "";
            var result = ScheduleService.AddSchedule(schedule, out errorMessage);

            if (result)
                FormHelper.ShowNotify("Schedule added successfully.");
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
                LearnerID = Convert.ToInt32(cboLearners.SelectedValue),
                TeacherID = Convert.ToInt32(cboTeachers.SelectedValue),
                VehicleID = Convert.ToInt32(cboVehicles.SelectedValue),
                CourseID = Convert.ToInt32(cboCourses.SelectedValue),
                SessionID = Convert.ToInt32(cboSessions.SelectedValue),
                SessionDate = DateTime.Parse(lblDateAssign.Text),
                Created_At = DateTime.Now
            };
        }

        private bool ValidateFields()
        {
            var comboBoxes = new List<Guna2ComboBox> { cboLearners, cboCourses, cboTeachers, cboVehicles, cboSessions };

            foreach (var comboBox in comboBoxes)
            {
                if (!ScheduleValidator.CheckRequiredAndShowToolTip(comboBox, toolTip))
                    return false;
            }

            return true;
        }

        private void cboLearners_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FormHelper.HasSelectedItem(cboLearners))
            {
                cboCourses.Enabled = false;
                return;
            }
            cboCourses.Enabled = true;
            int learnerID = Convert.ToInt32(cboLearners.SelectedValue.ToString());

            ComboboxService.AssignCoursesToCombobox(cboCourses, learnerID);
        }

        private void cboCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FormHelper.HasSelectedItem(cboCourses))
            {
                cboTeachers.Enabled = false;
                return;
            }
            cboTeachers.Enabled = true;
            int courseID = Convert.ToInt32(cboCourses.SelectedValue.ToString());

            ComboboxService.AssignTeachersToCombobox(cboTeachers, courseID);
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

            ComboboxService.AssignVehiclesToCombobox(cboVehicles, courseID);
        }

        private void cboVehicles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FormHelper.HasSelectedItem(cboVehicles))
            {
                cboSessions.Enabled = false;
                return;
            }
            cboSessions.Enabled = true;
        }
    }
}
