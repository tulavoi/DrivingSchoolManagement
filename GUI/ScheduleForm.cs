using BLL.Services;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Management;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace GUI
{
	public partial class ScheduleForm : Form
	{
		#region Properties
		private CalendarManager calendarManager;
        private bool isEditing = false;
        #endregion

        public ScheduleForm()
		{
			InitializeComponent();
			FormHelper.ApplyRoundedCorners(this, 20);

			calendarManager = new CalendarManager(pnlMatrix, dtpSchedule.Value);
			calendarManager.LoadMatrix();

			this.SetCurrentDate();
        }

        private void LoadComboboxes()
        {
            ComboboxService.AssignCoursesToCombobox(cboCourses);
            ComboboxService.AssignLearnersToCombobox(cboLearners);
			ComboboxService.AssignTeachersToCombobox(cboTeachers);
            ComboboxService.AssignSessionsToCombobox(cboSessions);
			ComboboxService.AssignVehiclesToCombobox(cboVehicles);
        }

        public void SetCurrentDate()
		{
			dtpSchedule.Value = DateTime.Now;
		}

		private void dtpSchedule_ValueChanged(object sender, EventArgs e)
		{
			calendarManager.AddNumberToMatrixByDate(dtpSchedule.Value);
		}

		private void btnToday_Click(object sender, EventArgs e)
		{
			this.SetCurrentDate();
		}

		private void btnPrevMonth_Click(object sender, EventArgs e)
		{
			dtpSchedule.Value = dtpSchedule.Value.AddMonths(-1);
		}

		private void btnNextMonth_Click(object sender, EventArgs e)
		{
			dtpSchedule.Value = dtpSchedule.Value.AddMonths(+1);
		}

        private void btnOpenAddScheduleForm_Click(object sender, EventArgs e)
        {
			calendarManager.OpenAssignScheduleForm(dtpSchedule.Value);
        }

        private void ScheduleForm_Load(object sender, EventArgs e)
        {
			this.LoadComboboxes();
		}

        private void btnEdit_Click(object sender, EventArgs e)
        {
			FormHelper.ToggleEditMode(ref this.isEditing, this.btnEdit, cboCourses, cboLearners, cboTeachers, cboSessions, dtpSessionDate, cboVehicles);
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (FormHelper.ConfirmDelete())
			{

			}
		}

		private void dgvSchedules_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex == 7)
			{
				e.CellStyle.ForeColor = Color.FromArgb(253, 100, 119);
				e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
			}
		}
	}
}
