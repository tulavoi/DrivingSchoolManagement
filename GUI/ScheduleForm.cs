using BLL.Services;
using BLL.Services.SendEmail;
using DAL;
using GUI.Validators;
using Guna.UI2.WinForms;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Management;
using System.Text;
using System.Threading.Tasks;
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

        private void ScheduleForm_Load(object sender, EventArgs e)
        {
            this.LoadComboboxes();

			this.LoadAllSchedules();
        }

        private void LoadAllSchedules()
        {
			ScheduleService.LoadAllSchedules(dgvSchedules);
            this.UpdateControlsWithSelectedRowData();
        }

        private void UpdateControlsWithSelectedRowData()
        {
            Schedule schedule = this.GetSelectedSchedule();
			this.AssignDataToControls(schedule);
		}

        private void AssignDataToControls(Schedule schedule)
        {
            if (schedule == null) return;

            // Gán các trường dữ liệu vào controls
            string scheduleID = "ID: " + schedule.ScheduleID.ToString();

            FormHelper.SetLabelID(lblScheduleID, scheduleID);

            string vehicleNameNumber = $"{schedule.Vehicle.VehicleName}              {schedule.Vehicle.VehicleNumber}";

   //         cboLearners.Text = schedule.Learner.FullName;
			//cboCourses.Text = schedule.Course.CourseName;
			cboTeachers.Text = schedule.Teacher.FullName;
			cboVehicles.Text = vehicleNameNumber;
			cboSessions.Text = schedule.Session.Session1;
            dtpSessionDate.Value = schedule.SessionDate.Value;
		}

        private Schedule GetSelectedSchedule()
        {
            if (!FormHelper.HasSelectedRow(dgvSchedules)) return null;

            var selectedRow = dgvSchedules.SelectedRows[0];

            if (selectedRow.Tag is Schedule selectedSchedule) return selectedSchedule;

            return null;
        }

        private void LoadComboboxes()
        {
            ComboboxService.AssignCoursesToCombobox(cboCourses);
            ComboboxService.AssignLearnersToCombobox(cboLearners);
			ComboboxService.AssignTeachersToCombobox(cboTeachers);
            ComboboxService.AssignSessionsToCombobox(cboSessions);
            //ComboboxService.AssignVehiclesToCombobox(cboVehicles);
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

            if (this.ConfirmAction($"Are you sure to edit schedule for '{cboLearners.Text}'?"))
            {
                Schedule schedule = this.GetSchedule();
                string errorMessage = "";

                var result = ScheduleService.EditSchedule(schedule, out errorMessage);

                if (result)
                    FormHelper.ShowNotify("Schedule edited successfully.");
                else
                    this.HandleScheduleAddError(errorMessage);
            }

            this.ToggleEditMode();
            this.LoadAllSchedules();
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
                SessionID = Convert.ToInt32(cboSessions.SelectedValue),
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
            var comboBoxes = new List<Guna2ComboBox> { cboLearners, cboCourses, cboTeachers, cboVehicles, cboSessions };

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
            FormHelper.ToggleEditMode(ref this.isEditing, this.btnEdit, cboTeachers, cboSessions, dtpSessionDate, cboVehicles);
        }

        private void btnDelete_Click(object sender, EventArgs e)
		{
            if (!this.HasSelectedRow()) return;

            if (string.IsNullOrEmpty(lblScheduleID.Text)) return;

            if (this.ConfirmAction($"Are you sure to delete schedule of '{cboLearners.Text}'?"))
            {
                int scheduleID = FormHelper.GetObjectID(lblScheduleID.Text);
                var result = ScheduleService.DeleteSchedule(scheduleID);
                if (result)
                {
                    CourseService.UpdateHoursStudied(Convert.ToInt32(cboCourses.SelectedValue), -2);
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

        // Thay đổi màu text của cột index = 7 trong datagridview
        private void dgvSchedules_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex == 7)
			{
				e.CellStyle.ForeColor = Color.FromArgb(253, 100, 119);
				e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
			}
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
            //string recipientMail = isForLearner ? schedule.Learner.Email : schedule.Teacher.Email;
            //string recipientInfo = isForLearner
            //                        ? $"{schedule.Teacher.FullName} – {schedule.Teacher.Phone} – {schedule.Teacher.Email}"
            //                        : $"{schedule.Learner.FullName} – {schedule.Learner.PhoneNumber} – {schedule.Learner.Email}";
            //string role = isForLearner ? "Learner" : "Teacher";

            //string emailBody = this.GetScheduleEmailBody(schedule, recipientInfo, role);

            //return new MailContent
            //{
            //    To = recipientMail,
            //    Subject = $"Driving School",
            //    Body = emailBody
            //};
            return null;
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

        private void cboCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FormHelper.HasSelectedItem(cboCourses)) return;
            int courseID = Convert.ToInt32(cboCourses.SelectedValue.ToString());

            ComboboxService.AssignTeachersToCombobox(cboTeachers, courseID);

            ComboboxService.AssignVehiclesToCombobox(cboVehicles, courseID);
			this.UpdateControlsWithSelectedRowData();
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
    }
}
