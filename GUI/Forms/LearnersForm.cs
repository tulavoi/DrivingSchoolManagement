﻿using BLL.Services;
using BLL.Services.SendEmail;
using DAL;
using GUI.ReportViewers;
using GUI.Validators;
using System;
using System.Windows.Forms;

namespace GUI
{
	public partial class LearnersForm : Form
	{
		#region Properties
		private bool isEditing = false;
		private bool isClicked = false;
		private Learner selectedLearner;
        private static LearnersForm instance;

		public static LearnersForm Instance
		{
			get
			{
				if (instance == null) instance = new LearnersForm();
				return instance;
			}
		}
		#endregion

		public LearnersForm()
		{
			InitializeComponent();
			FormHelper.ApplyRoundedCorners(this, 20);
		}

		public void LearnersForm_Load(object sender, EventArgs e)
		{
			this.LoadAllLearners();
			this.LoadComboboxes();

			// cboStatus_Filter đang được set mặc định selectedIndex = 1
			// Gọi event để lọc ngay form vừa load
			cboStatus_Filter_SelectedIndexChanged(sender, e);

            pnlMenuButtonPrint.Visible = false;
            this.isClicked = false;
        }

		private void LoadComboboxes()
		{
			ComboboxService.AssignStatesToCombobox(cboStates);
			ComboboxService.GetAvailableAndLearnerCourses(cboCourses, FormHelper.GetObjectID(lblLearnerID.Text));
		}

		public void LoadAllLearners()
		{
			LearnerService.LoadAllLearners(dgvLearners);
			this.UpdateControlsWithSelectedRowData();
		}

		private void btnEditLearner_Click(object sender, EventArgs e)
		{
			if (!this.InSaveMode())
			{
				this.ToggleEditMode();
				return;
			}

			if (!this.ValidateFields()) return;

			if (this.ConfirmAction($"Are you sure to edit learner '{txtLearnerName.Text}'?"))
			{
				Learner learner = this.GetLearner();
				int courseID = Convert.ToInt32(cboCourses.SelectedValue.ToString());
				var result = LearnerService.EditLearner(learner, courseID);
				FormHelper.ShowActionResult(result, "Learner edited successfully.", "Failed to edit learner.");
			}

			cboStatus_Filter_SelectedIndexChanged(sender, e);
			this.ToggleEditMode();
		}

		private bool ValidateFields()
		{
			if (!LearnerValidator.ValidateFullName(txtLearnerName, toolTip)) return false;

			if (!LearnerValidator.ValidateCitizenID(txtCitizenId, toolTip)) return false;

			if (!LearnerValidator.ValidateEmail(txtEmail, toolTip)) return false;

			if (!LearnerValidator.ValidatePhoneNumber(txtPhone, toolTip)) return false;

			if (!LearnerValidator.IsLearnerEligible(dtpDOB, toolTip)) return false;

			if (!LearnerValidator.ValidateAddress(txtAddress, toolTip)) return false;

			if (!LearnerValidator.ValidateSelectedCourse(cboCourses, toolTip)) return false;

			if (!LearnerValidator.ValidateEligibleCourse(dtpDOB, lblLicenseName.Text, cboCourses, toolTip)) return false;

			return true;
		}

		private void ToggleEditMode()
		{
			FormHelper.ToggleEditMode(ref this.isEditing, this.btnEditLearner, txtLearnerName,
				txtAddress, txtEmail, txtPhone, cboGender, dtpDOB, cboNationality, txtCitizenId, cboStates);
		}

		private bool InSaveMode()
		{
			return btnEditLearner.Text == Constant.SAVE_MODE;
		}

		private bool ConfirmAction(string message)
		{
			DialogResult result = FormHelper.ShowConfirm(message);
			return result == DialogResult.Yes;
		}

		private Learner GetLearner()
		{
			return new Learner
			{
				LearnerID = FormHelper.GetObjectID(lblLearnerID.Text),
				FullName = txtLearnerName.Text,
				Address = txtAddress.Text,
				Email = txtEmail.Text,
				CitizenID = txtCitizenId.Text,
				PhoneNumber = txtPhone.Text,
				Gender = cboGender.Text,
				DateOfBirth = dtpDOB.Value,
				Nationality = cboNationality.Text,
				StatusID = Convert.ToInt32(cboStates.SelectedValue.ToString()),
				Updated_At = DateTime.Now,
			};
		}

		private void btnOpenAddLearnerForm_Click(object sender, EventArgs e)
		{
			FormHelper.OpenFormDialog(new AddLearnerForm());
			cboStatus_Filter_SelectedIndexChanged(sender, e);
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			// Clear old rows in the DataGridView, get the search keyword, and load matching learner data
			FormHelper.ClearDataGridViewRow(dgvLearners);

			string keyword = txtSearch.Text.ToLower();

			// Nếu không nhập ký tự tìm kiếm thì sẽ hiển thị data dựa vào cboStatus
			if (string.IsNullOrEmpty(keyword))
				cboStatus_Filter_SelectedIndexChanged(sender, e);
			else
			{
				LearnerService.SearchLearners(dgvLearners, keyword);
				this.UpdateControlsWithSelectedRowData();
			}
		}

		private void dgvLearners_SelectionChanged(object sender, EventArgs e)
		{
			this.UpdateControlsWithSelectedRowData();
		}

		private void UpdateControlsWithSelectedRowData()
		{
            this.selectedLearner = this.GetSelectedLearner();
			this.AssignDataToControls(this.selectedLearner);
		}

        private Learner GetSelectedLearner()
        {
            if (!FormHelper.HasSelectedRow(dgvLearners)) return null;

			var selectedRow = dgvLearners.SelectedRows[0];

			if (selectedRow.Tag is Learner selectedLearner) return selectedLearner;

			return null;
		}

        private void AssignDataToControls(Learner selectedLearner)
		{
			if (selectedLearner == null) return;

			// Assign learner data to form controls
			string learnerID = "ID: " + selectedLearner.LearnerID.ToString();

			FormHelper.SetLabelID(lblLearnerID, learnerID);
			txtLearnerName.Text = selectedLearner.FullName;
			txtAddress.Text = selectedLearner.Address;
			txtCitizenId.Text = selectedLearner.CitizenID.ToString();
			txtEmail.Text = selectedLearner.Email;
			txtPhone.Text = selectedLearner.PhoneNumber;
			cboGender.Text = selectedLearner.Gender;
			dtpDOB.Value = (DateTime)selectedLearner.DateOfBirth;
			cboStates.Text = selectedLearner.Status.StatusName;
			cboNationality.Text = selectedLearner.Nationality;

			ComboboxService.GetAvailableAndLearnerCourses(cboCourses, FormHelper.GetObjectID(lblLearnerID.Text));

			var enrollment = EnrollmentService.GetEnrollmentByLearnerID(selectedLearner.LearnerID);
			if (enrollment == null)
			{
				cboCourses.SelectedIndex = 0;
				return;
			}
			cboCourses.Text = enrollment.Course.CourseName;

            if (selectedLearner.IsPass == true) btnConfirmPass.Visible = false;
			else btnConfirmPass.Visible = true;
        }

        private void btnDeleteLearner_Click(object sender, EventArgs e)
		{
			if (!FormHelper.HasSelectedRow(dgvLearners)) return;

			if (this.ConfirmAction($"Are you sure to delete learner '{txtLearnerName.Text}'?"))
			{
				int learnerID = FormHelper.GetObjectID(lblLearnerID.Text);

				var result = LearnerService.DeleteLearner(learnerID);

				FormHelper.ShowActionResult(result, "Learner deleted successfully.", "Failed to delete learner.");

				// Sau khi xóa xong, hiển thị lại toàn bộ data có status Active
				cboStatus_Filter_SelectedIndexChanged(sender, e);
			}
		}

		private MailContent CreateMailContent(Learner learner)
		{
			// Tạo nội dung mail dựa trên thông tin của học viên
			return new MailContent
			{
				To = learner.Email, // Địa chỉ email học viên
				Subject = $"Driving School", // Tiêu đề email chứa tên học viên
				Body = $"<h1>Hello {learner.FullName},</h1>" +
					   $"<p>{txtMessage.Text}</p>"
			};
		}

		private async void btnSendMail_Click(object sender, EventArgs e)
		{
			var learner = this.GetLearner();
			var mailContent = this.CreateMailContent(learner);
			var result = await FormHelper.SendMailAsync(mailContent);

			FormHelper.ShowActionResult(result, "Email sent successfully.", "Failed to send email.");
		}

		private void cboStatus_Filter_SelectedIndexChanged(object sender, EventArgs e)
		{
			FormHelper.ClearDataGridViewRow(dgvLearners);

			if (FormHelper.HasSelectedItem(cboStatus_Filter))
			{
				string status = cboStatus_Filter.Text;
				LearnerService.FilterLearnersByStatus(dgvLearners, status);
				this.UpdateControlsWithSelectedRowData();
			}
			else
				this.LoadAllLearners();
		}

		private void numericKeyPress(object sender, KeyPressEventArgs e)
		{
			FormHelper.CheckNumericKeyPress(e);
		}

		private void cboCourses_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!FormHelper.HasSelectedItem(cboCourses))
			{
				this.ConfigureForm(false);
				return;
			}
			this.ConfigureForm(true);
			this.AssignCourseToDetailLabels();
		}

		private void AssignCourseToDetailLabels()
		{
			int courseID = Convert.ToInt32(cboCourses.SelectedValue.ToString());
			var course = CourseService.GetCourse(courseID);
			if (course == null) return;
			lblLicenseName.Text = course.License.LicenseName;
            lblDurationHours.Text = course.DurationInHours + " (h)";
			lblFee.Text = course.Fee?.ToString("N0") + " VND" ?? "N/A";
            lblStartDate.Text = course.StartDate.Value.ToString("dd/MM/yyyy");
			lblEndDate.Text = course.EndDate.Value.ToString("dd/MM/yyyy");
		}

		private void ConfigureForm(bool showDetails)
		{
			pnlCourseDetails.Visible = showDetails;
			pnlBasicDetails.Width = 670;
			pnlBasicDetails.Height = showDetails ? 435 : 400;
		}

        private void btnConfirmPass_Click(object sender, EventArgs e)
        {
			if (string.IsNullOrEmpty(lblLearnerID.Text)) return;

			if (this.ConfirmAction($"Are you sure the leanrer '{txtLearnerName.Text}' passed?"))
			{
				var result = LearnerService.ConfirmPass(FormHelper.GetObjectID(lblLearnerID.Text));
                FormHelper.ShowActionResult(result, "Updated successfully", "Failed to update");
            }
            cboStatus_Filter_SelectedIndexChanged(sender, e);
            this.ToggleEditMode();
        }

        private void btnOpenMenuButtonPrint_Click(object sender, EventArgs e)
        {
            this.isClicked = !this.isClicked;
            pnlMenuButtonPrint.Visible = this.isClicked;
            btnOpenMenuButtonPrint.Checked = this.isClicked;
        }

        private void btnPrintLearnerDetail_Click(object sender, EventArgs e)
        {
            LearnerDetailsRV learnerDetailsRV = new LearnerDetailsRV(selectedLearner.LearnerID);
			learnerDetailsRV.Show();
        }

        private void btnEligibleLearners_Click(object sender, EventArgs e)
        {
			EligibleLearnersRV eligibleLearnersRV = new EligibleLearnersRV();
			eligibleLearnersRV.Show();
        }

        private void btnPrintAllLearners_Click(object sender, EventArgs e)
        {
            AllLearnersRV allLearnersRV = new AllLearnersRV();
            allLearnersRV.Show();
        }
    }
}
