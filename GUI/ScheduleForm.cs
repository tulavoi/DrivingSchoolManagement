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
			this.LoadSampleData();
		}

        private void LoadSampleData()
        {
            // Tạo bảng dữ liệu mẫu
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Space", typeof(string));
            dataTable.Columns.Add("CourseName", typeof(string));
            dataTable.Columns.Add("LearnerName", typeof(string));
            dataTable.Columns.Add("TeacherName", typeof(string));
            dataTable.Columns.Add("VehicleName", typeof(string));
            dataTable.Columns.Add("Date", typeof(string));
            dataTable.Columns.Add("Session", typeof(string));
            dataTable.Columns.Add("Status", typeof(string));

            // Thêm các hàng dữ liệu mẫu vào DataTable
            dataTable.Rows.Add("", "Basic Driving", "Mai Nguyen Hoang Vu", "Mai Nguyen Hoang Vu", "Toyota", "12/10/2022", "8H30 - 11H30", "Scheduled");
            dataTable.Rows.Add("", "Basic Driving", "Truong Anh Thanh Cong", "Truong Anh Thanh Cong", "Toyota", "12/10/2022", "8H30 - 11H30", "Scheduled");
            dataTable.Rows.Add("", "Basic Driving", "Le Nguyen Xuan Duoc", "Le Nguyen Xuan Duoc", "Toyota", "12/10/2022", "8H30 - 11H30", "Scheduled");

            // Chèn dữ liệu mẫu vào DataGridView
            dgvSchedules.DataSource = dataTable;
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
