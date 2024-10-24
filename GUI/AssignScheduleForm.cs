using BLL.Services;
using BLL.Services.SendEmail;
using DAL;
using GUI.Validators;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

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
            ComboboxService.AssignCoursesToCombobox(cboCourses);
            ComboboxService.AssignLearnersToCombobox(cboLearners);
            ComboboxService.AssignTeachersToCombobox(cboTeachers);
            ComboboxService.AssignSessionsToCombobox(cboSessions);
            ComboboxService.AssignVehiclesToCombobox(cboVehicles);
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

        private void txtSearchCourse_TextChanged(object sender, EventArgs e)
        {
            string keyword = this.GetKeyword(txtSearchCourse);
            CourseService.SearchCourse(cboCourses, keyword);
        }

        private void txtSearchLearner_TextChanged(object sender, EventArgs e)
        {
            string keyword = this.GetKeyword(txtSearchLearner);
            LearnerService.SearchLearners(cboLearners, keyword);
        }

        private void txtSearchTeacher_TextChanged(object sender, EventArgs e)
        {
            string keyword = this.GetKeyword(txtSearchTeacher);
            TeacherService.SearchTeachers(cboTeachers, keyword);
        }

        private void txtSearchVehicle_TextChanged(object sender, EventArgs e)
        {
            string keyword = this.GetKeyword(txtSearchVehicle);
            VehicleService.SearchVehicles(cboVehicles, keyword);
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
            var comboBoxes = new List<Guna2ComboBox> { cboCourses, cboLearners, cboTeachers, cboVehicles, cboSessions };

            foreach (var comboBox in comboBoxes)
            {
                if (!ScheduleValidator.CheckRequiredAndShowToolTip(comboBox, toolTip))
                    return false;
            }

            return true;    
        }

        private void cboLearners_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
