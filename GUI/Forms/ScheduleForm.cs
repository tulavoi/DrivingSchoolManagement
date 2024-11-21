using BLL.Services;
using BLL.Services.SendEmail;
using DAL;
using GUI.ReportViewers;
using GUI.Validators;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.AxHost;

namespace GUI
{
	public partial class ScheduleForm : Form
	{
		#region Properties
		private CalendarManager calendarManager;
		private bool isEditing = false;
		private Schedule selectedSchedule;
		private string vehicleNameNumber = "";
		private bool isClicked = false;
		#endregion

		public ScheduleForm()
		{
			InitializeComponent();
			FormHelper.ApplyRoundedCorners(this, 20);

			calendarManager = new CalendarManager(pnlMatrix, dtpSchedule.Value);
			calendarManager.LoadMatrix();

			this.SetCurrentDate();

			pnlMenuButtonPrint.Visible = false; // ẩn panel menu button print khi form khởi động
			isClicked = false;
		}

		private void ScheduleForm_Load(object sender, EventArgs e)
		{
			this.LoadComboboxes();
			this.LoadAllSchedules();
		}

		private void LoadComboboxes()
		{
			ComboboxService.AssignSessionsToCombobox(cboSessions);
		}

		private void LoadAllSchedules()
		{
			ScheduleService.LoadAllSchedules(dgvSchedules);
			this.UpdateControlsWithSelectedRowData();
		}

		private void UpdateControlsWithSelectedRowData()
		{
			this.selectedSchedule = this.GetSelectedSchedule();
			this.AssignDataToControls(this.selectedSchedule);
		}

		private void AssignDataToControls(Schedule schedule)
		{
			if (schedule == null) return;

			// Gán các trường dữ liệu vào controls
			string scheduleID = "ID: " + schedule.ScheduleID.ToString();
			FormHelper.SetLabelID(lblScheduleID, scheduleID);

			this.vehicleNameNumber = $"{schedule.Vehicle.VehicleName}              {schedule.Vehicle.VehicleNumber}";

			txtLearnerName.Text = schedule.Enrollment.Learner.FullName;
			txtLearnerName.Tag = schedule.Enrollment.Learner.LearnerID;
			txtCourseName.Text = schedule.Enrollment.Course.CourseName;
			txtCourseName.Tag = schedule.Enrollment.Course.CourseID;
			txtCurrentSession.Text = schedule.Session.Session1;
			txtCurrentSession.Tag = schedule.Session.SessionID;

			dtpSessionDate.Value = schedule.SessionDate.Value;

			int courseID = Convert.ToInt32(txtCourseName.Tag);
			int sessionID = Convert.ToInt32(cboSessions.SelectedValue);

			this.AssignTeacherAndVehicle(courseID, sessionID, dtpSessionDate.Value);

			cboSessions.SelectedIndex = 0;
			cboTeachers.Text = schedule.Teacher.FullName;
			cboVehicles.Text = this.vehicleNameNumber;
		}

		private Schedule GetSelectedSchedule()
		{
			if (!FormHelper.HasSelectedRow(dgvSchedules)) return null;

			var selectedRow = dgvSchedules.SelectedRows[0];

			if (selectedRow.Tag is Schedule selectedSchedule) return selectedSchedule;

			return null;
		}

		public void SetCurrentDate()
		{
			dtpSchedule.Value = DateTime.Now;
		}

		private void dtpSchedule_ValueChanged(object sender, EventArgs e)
		{
			if (calendarManager == null) return;
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
			this.LoadAllSchedules();
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (!this.InSaveMode())
			{
				this.ToggleEditMode();
				return;
			}

			if (!this.ValidateFields()) return;

			if (this.ConfirmAction($"Are you sure to edit schedule for '{txtLearnerName.Text}'?"))
			{
				Schedule schedule = this.GetSchedule();
				string errorMessage = "";

				var result = ScheduleService.EditSchedule(schedule, out errorMessage);

				if (result)
					FormHelper.ShowNotify("Schedule edited successfully.");
				else
					this.HandleScheduleAddError(errorMessage);
			}
			this.ResetForm();
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
				ScheduleID = FormHelper.GetObjectID(lblScheduleID.Text),
				TeacherID = Convert.ToInt32(cboTeachers.SelectedValue),
				VehicleID = Convert.ToInt32(cboVehicles.SelectedValue),
				SessionID = Convert.ToInt32(txtCurrentSession.Tag),
				SessionDate = dtpSessionDate.Value
			};
		}

		private bool ConfirmAction(string message)
		{
			DialogResult result = FormHelper.ShowConfirm(message);
			return result == DialogResult.Yes;
		}

		private bool ValidateFields()
		{
			var comboBoxes = new List<Guna2ComboBox> { cboTeachers, cboVehicles };

			foreach (var comboBox in comboBoxes)
			{
				if (!ScheduleValidator.CheckRequiredAndShowToolTip(comboBox, toolTip))
					return false;
			}

			return true;
		}

		private bool InSaveMode()
		{
			return btnEdit.Text == Constant.SAVE_MODE;
		}

		private void ToggleEditMode()
		{
			FormHelper.ToggleEditMode(ref this.isEditing, this.btnEdit, cboSessions, dtpSessionDate, cboTeachers, cboVehicles);
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (!this.HasSelectedRow()) return;

			if (string.IsNullOrEmpty(lblScheduleID.Text)) return;

			if (this.ConfirmAction($"Are you sure to delete schedule of '{txtLearnerName.Text}'?"))
			{
				int scheduleID = FormHelper.GetObjectID(lblScheduleID.Text);
				var result = ScheduleService.DeleteSchedule(scheduleID);
				if (result)
				{
					CourseService.UpdateHoursStudied(Convert.ToInt32(txtCourseName.Tag), -2);
					FormHelper.ShowNotify("Schedule deleted successfully.");
				}
				else
					FormHelper.ShowError("Failed to delete schedule.");
				this.LoadAllSchedules();
			}
		}

		private bool HasSelectedRow()
		{
			return dgvSchedules.SelectedRows.Count > 0;
		}

		private void dgvSchedules_SelectionChanged(object sender, EventArgs e)
		{
			this.UpdateControlsWithSelectedRowData();
		}

		private async void btnSendSchedule_Learner_ClickAsync(object sender, EventArgs e)
		{
			var schedule = this.GetSelectedSchedule();
			await SendScheduleEmailAsync(schedule, isForLearner: true);
		}

		private async void btnSendSchedule_Teacher_ClickAsync(object sender, EventArgs e)
		{
			var schedule = this.GetSelectedSchedule();
			await SendScheduleEmailAsync(schedule, isForLearner: false);
		}

		private async Task SendScheduleEmailAsync(Schedule schedule, bool isForLearner)
		{
			var mailContent = CreateMailContent(schedule, isForLearner);
			var result = await FormHelper.SendMailAsync(mailContent);

			// Hiển thị kết quả gửi mail
			FormHelper.ShowActionResult(result, "Schedule sent successfully.", "Failed to send schedule.");
		}

		private MailContent CreateMailContent(Schedule schedule, bool isForLearner)
		{
			string recipientMail = isForLearner ? schedule.Enrollment.Learner.Email : schedule.Teacher.Email;
			string recipientInfo = isForLearner
									? $"{schedule.Teacher.FullName} – {schedule.Teacher.PhoneNumber} – {schedule.Teacher.Email}"
									: $"{schedule.Enrollment.Learner.FullName} – {schedule.Enrollment.Learner.PhoneNumber} – {schedule.Enrollment.Learner.Email}";
			string role = isForLearner ? "Learner" : "Teacher";

			string emailBody = this.GetScheduleEmailBody(schedule, recipientInfo, role);

			return new MailContent
			{
				To = recipientMail,
				Subject = $"Driving School",
				Body = emailBody
			};
		}

		private string GetScheduleEmailBody(Schedule schedule, string recipientInfo, string role)
		{
			string introMessage = role == "Learner"
				? $"Hello {schedule.Enrollment.Learner.FullName},<br><br>We are pleased to inform you about your upcoming lesson schedule:"
				: $"Hello {schedule.Teacher.FullName},<br><br>We are pleased to inform you about the upcoming lesson you will be conducting:";

			string contactMessage = role == "Learner"
				? "If you have any questions, feel free to contact your teacher via email or reach out to the school directly."
				: "If you have any questions or need further details, please contact the administration or the learner directly.";

			var sb = new StringBuilder();
			sb.Append($@"
                <h1>{introMessage}</h1>
                <table style='border: 1px solid #ccc; border-collapse: collapse; width: 100%;'>
                    <tr>
                        <th style='border: 1px solid #ccc; padding: 8px;'>Lesson Date</th>
                        <td style='border: 1px solid #ccc; padding: 8px;'>{schedule.SessionDate:MM/dd/yyyy}</td>
                    </tr>
                    <tr>
                        <th style='border: 1px solid #ccc; padding: 8px;'>Session</th>
                        <td style='border: 1px solid #ccc; padding: 8px;'>{schedule.Session.Session1}</td>
                    </tr>
                    <tr>
                        <th style='border: 1px solid #ccc; padding: 8px;'>Course</th>
                        <td style='border: 1px solid #ccc; padding: 8px;'>{schedule.Enrollment.Course.CourseName}</td>
                    </tr>
                    <tr>
                        <th style='border: 1px solid #ccc; padding: 8px;'>{role}</th>
                        <td style='border: 1px solid #ccc; padding: 8px;'>{recipientInfo}</td>
                    </tr>
                    <tr>
                        <th style='border: 1px solid #ccc; padding: 8px;'>Vehicle</th>
                        <td style='border: 1px solid #ccc; padding: 8px;'>{schedule.Vehicle.VehicleName}</td>
                    </tr>
                </table>
                <p style='margin-top: 20px;'>{contactMessage}</p>
                <p>Best regards,</p>
                <p>Your Driving School</p>
            ");

			return sb.ToString();
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			FormHelper.ClearDataGridViewRow(dgvSchedules);

			string keyword = txtSearch.Text.ToLower();

			ScheduleService.SearchSchedules(dgvSchedules, keyword);
			this.UpdateControlsWithSelectedRowData();
		}

		private void cboSession_Filter_SelectedIndexChanged(object sender, EventArgs e)
		{
			FormHelper.ClearDataGridViewRow(dgvSchedules);

			if (cboSession_Filter.SelectedIndex < 1)
				this.LoadAllSchedules();
			else
			{
				string filterString = cboSession_Filter.Text;
				ScheduleService.FilterScheduleBySession(dgvSchedules, filterString);
				this.UpdateControlsWithSelectedRowData();
			}
		}

		private void cboSessions_SelectedIndexChanged(object sender, EventArgs e)
		{
			int courseID = Convert.ToInt32(txtCourseName.Tag);
			DateTime sessionDate = dtpSessionDate.Value;
			var selectedSession = cboSessions.SelectedItem as Session;
			if (selectedSession.SessionID == 0) return;

			if (selectedSession.SessionID == this.selectedSchedule.Session.SessionID && dtpSessionDate.Value == this.selectedSchedule.SessionDate)
			{
				txtCurrentSession.Text = this.selectedSchedule.Session.Session1;
				txtCurrentSession.Tag = this.selectedSchedule.Session.SessionID;
				this.AssignTeacherAndVehicle(courseID, selectedSession.SessionID, sessionDate);
				cboTeachers.Text = this.selectedSchedule.Teacher.FullName;
				cboVehicles.Text = this.vehicleNameNumber;
				return;
			}

			txtCurrentSession.Text = cboSessions.Text == "Current Session" ? "" : cboSessions.Text; // Nếu như cboSession.Text là "Current Session" thì gán ""
			txtCurrentSession.Tag = cboSessions.SelectedValue;
			
			this.AssignTeacherAndVehicle(courseID, selectedSession.SessionID, sessionDate);
		}

		private void AssignTeacherAndVehicle(int courseID, int sessionID, DateTime sessionDate)
		{
			ComboboxService.AssignTeacherInCourseToCombobox(cboTeachers, courseID, sessionID, sessionDate);
			ComboboxService.AssignVehicleInCourseToCombobox(cboVehicles, courseID, sessionID, sessionDate);
		}

		private void btnRefreshForm_Click(object sender, EventArgs e)
		{
			this.ResetForm();
		}

		private void ResetForm()
		{
			this.ToggleEditMode();
			this.LoadAllSchedules();
			cboSession_Filter.SelectedIndex = 0;
			dtpSchedule.Value = DateTime.Now;
			txtSearch.Text = "";
		}

		private void dtpSessionDate_ValueChanged(object sender, EventArgs e)
		{
			cboSessions.SelectedIndex = 0;
			if (this.selectedSchedule.SessionDate != dtpSessionDate.Value)
			{
				txtCurrentSession.Text = "";
				txtCurrentSession.Tag = "";
			}

			int courseID = Convert.ToInt32(txtCourseName.Tag);
			int sessionIDInRow = this.selectedSchedule.Session.SessionID; // Lấy sessionID của schedule đang chọn trên datagridview
			ComboboxService.AssignSessionsToCombobox(cboSessions, courseID, dtpSessionDate.Value, sessionIDInRow);
		}

        private void btnOpenMenuButtonPrint_Click(object sender, EventArgs e)
        {
            isClicked = !isClicked;
            pnlMenuButtonPrint.Visible = isClicked;
            btnOpenMenuButtonPrint.Checked = isClicked;
        }

        private void btnPrintScheduleByDate_Click(object sender, EventArgs e)
        {
			ScheduleByDateRV scheduleByDateRV = new ScheduleByDateRV();
			scheduleByDateRV.Show();
        }
    }
}
