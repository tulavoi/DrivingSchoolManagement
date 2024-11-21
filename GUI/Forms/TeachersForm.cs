using BLL.Services;
using BLL.Services.SendEmail;
using DAL;
using GUI.Validators;
using Guna.UI2.WinForms;
using System;
using System.Windows.Forms;

namespace GUI
{
	public partial class TeachersForm : Form
	{
		#region Properties
		private static TeachersForm instance;

		public static TeachersForm Instance
		{
			get
			{
				if (instance == null) instance = new TeachersForm();
				return instance;
			}
		}

		private bool isEditing = false;

		#endregion

		public TeachersForm()
		{
			InitializeComponent();
			FormHelper.ApplyRoundedCorners(this, 20);
		}

		private void TeachersForm_Load(object sender, EventArgs e)
		{
			this.LoadComboboxes();
			this.LoadAllTeachers();
			cboStatus_Filter_SelectedIndexChanged(sender, e);
			FormHelper.SetDateTimePickerMaxValue(dtpBeginningDate, dtpDOB);
		}

		private void LoadAllTeachers()
		{
			TeacherService.LoadAllTeachers(dgvTeachers);
			this.UpdateControlsWithSelectedRowData();
		}

		private void UpdateControlsWithSelectedRowData()
		{
			var teacher = this.GetSelectedTeacher();
			this.AssignDataToControls(teacher);
		}

		private void AssignDataToControls(Teacher teacher)
		{
			if (teacher == null) return;

			// Gán các trường dữ liệu vào controls
			string teacherID = "ID: " + teacher.TeacherID.ToString();

			FormHelper.SetLabelID(lblTeacherID, teacherID);

			txtFullName.Text = teacher.FullName;
			txtCitizenId.Text = teacher.CitizenID.ToString();
			txtEmail.Text = teacher.Email;
			txtPhone.Text = teacher.PhoneNumber.ToString();
			dtpDOB.Value = teacher.DateOfBirth.Value;
			cboGender.Text = teacher.Gender.ToString();
			txtAddress.Text = teacher.Address;
			cboLicenses.Text = teacher.License.LicenseName.ToString();
			dtpBeginningDate.Value = teacher.BeginningDate.Value;
			this.SetBeginningYears(dtpBeginningDate.Value, txtBeginningYears);
			cboStates.Text = teacher.Status.StatusName;
			txtLicenseNumber.Text = teacher.LicenseNumber;
		}

		public void SetBeginningYears(DateTime graduateDate, Guna2TextBox txt)
		{
			txt.Text = this.GetBeginningYears(graduateDate) + " Years";
		}

		private string GetBeginningYears(DateTime graduateDate)
		{
			int years = DateTime.Now.Year - graduateDate.Year;
			return years.ToString();
		}

		private Teacher GetSelectedTeacher()
		{
			if (!this.HasSelectedRow()) return null;

			var selectedRow = dgvTeachers.SelectedRows[0];

			if (selectedRow.Tag is Teacher selectedTeacher) return selectedTeacher;

			return null;
		}

		private bool HasSelectedRow()
		{
			// Kiểm tra xem có dòng nào trong datagridview được chọn hay k
			return dgvTeachers.SelectedRows.Count > 0;
		}

		private void LoadComboboxes()
		{
			ComboboxService.AssignLicensesToCombobox(cboLicenses);
			ComboboxService.AssignStatesToCombobox(cboStates);
		}

		private void btnEditTeacher_Click(object sender, EventArgs e)
		{
			if (!this.InSaveMode())
			{
				this.ToggleEditMode();
				return;
			}

			if (!this.ValidateFields()) return;

			if (this.ConfirmAction($"Are you sure to edit teacher '{txtFullName.Text}'?"))
			{
				Teacher teacher = this.GetTeacher();

				var result = TeacherService.EditTeacher(teacher);
				FormHelper.ShowActionResult(result, "Teacher edited successfully.", "Failed to edit teacher.");
			}

			this.ToggleEditMode();
			cboStatus_Filter_SelectedIndexChanged(sender, e);
		}

		private Teacher GetTeacher()
		{
			return new Teacher
			{
				TeacherID = FormHelper.GetObjectID(lblTeacherID.Text),
				FullName = txtFullName.Text,
				CitizenID = txtCitizenId.Text,
				DateOfBirth = dtpDOB.Value,
				Gender = cboGender.Text,
				PhoneNumber = txtPhone.Text,
				Email = txtEmail.Text,
				Nationality = cboNationality.Text,
				Address = txtAddress.Text,
				LicenseID = Convert.ToInt32(cboLicenses.SelectedValue),
				BeginningDate = dtpBeginningDate.Value,
				LicenseNumber = txtLicenseNumber.Text,
				StatusID = Convert.ToInt32(cboStates.SelectedValue.ToString()),
				Updated_At = DateTime.Now
			};
		}

		private bool ValidateFields()
		{
			string license = cboLicenses.Text;

			//if (!TeacherValidator.ValidateFullName(txtFullName, toolTip)) return false;

			//if (!TeacherValidator.ValidateCitizenID(txtCitizenId, toolTip)) return false;

			//if (!TeacherValidator.ValidateEmail(txtEmail, toolTip)) return false;

			//if (!TeacherValidator.ValidatePhoneNumber(txtPhone, toolTip)) return false;

			//if (!TeacherValidator.ValidateAddress(txtAddress, toolTip)) return false;

			//if (!TeacherValidator.ValidateLicenseNumber(txtLicenseNumber, toolTip)) return false;

			//if (!TeacherValidator.IsTeacherEligible(dtpDOB, dtpBeginningDate, license, toolTip)) return false;

			if (!TeacherValidator.ValidateFullName(txtFullName, toolTip)) return false;

			if (!TeacherValidator.ValidateCitizenID(txtCitizenId, toolTip)) return false;

			if (!TeacherValidator.ValidateEmail(txtEmail, toolTip)) return false;

			if (!TeacherValidator.ValidatePhoneNumber(txtPhone, toolTip)) return false;

			if (!TeacherValidator.ValidateAddress(txtAddress, toolTip)) return false;

			if (!TeacherValidator.ValidateLicense(cboLicenses, toolTip)) return false;

			if (!TeacherValidator.ValidateLicenseNumber(txtLicenseNumber, toolTip)) return false;

			if (!TeacherValidator.IsTeacherEligible(dtpDOB, dtpBeginningDate, license, toolTip)) return false;

			return true;
		}

		private void ToggleEditMode()
		{
			FormHelper.ToggleEditMode(ref this.isEditing, this.btnEdit, txtFullName, txtPhone, 
				txtEmail, cboGender, dtpDOB, txtAddress, txtCitizenId, dtpBeginningDate, 
				cboNationality, cboLicenses, cboStates, txtLicenseNumber);
		}

		private bool InSaveMode()
		{
			return btnEdit.Text == Constant.SAVE_MODE;
		}

		private bool ConfirmAction(string message)
		{
			DialogResult result = FormHelper.ShowConfirm(message);
			return result == DialogResult.Yes;
		}

		private void btnOpenAddTeacherForm_Click(object sender, EventArgs e)
		{
			FormHelper.OpenFormDialog(new AddTeacherForm());
			cboStatus_Filter_SelectedIndexChanged(sender, e);
		}

		private void btnDeleteTeacher_Click(object sender, EventArgs e)
		{
			if (!this.HasSelectedRow()) return;

			if (string.IsNullOrEmpty(txtFullName.Text)) return;

			if (this.ConfirmAction($"Are you sure to delete teacher '{txtFullName.Text}'?"))
			{
				int teacherID = FormHelper.GetObjectID(lblTeacherID.Text);

				var result = TeacherService.DeleteTeacher(teacherID);

				FormHelper.ShowActionResult(result, "Teacher deleted successfully.", "Failed to delete teacher.");

				// Sau khi xóa xong, hiển thị lại toàn bộ data có status Active
				cboStatus_Filter_SelectedIndexChanged(sender, e);
			}
		}

		private void dgvTeachers_SelectionChanged(object sender, EventArgs e)
		{
			this.UpdateControlsWithSelectedRowData();
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			// Xóa các dòng cũ trong dgv, lấy keyword từ txtSearch
			// Load dữ liệu search được, gán các thông tin của dòng đc chọn trong dgv sang controls
			FormHelper.ClearDataGridViewRow(dgvTeachers);

			string keyword = txtSearch.Text.ToLower();

			// Nếu không nhập ký tự tìm kiếm thì sẽ hiển thị data dựa vào cboStatus
			if (string.IsNullOrEmpty(keyword))
				cboStatus_Filter_SelectedIndexChanged(sender, e);
			else
			{
				TeacherService.SearchTeachers(dgvTeachers, keyword);
				this.UpdateControlsWithSelectedRowData();
			}
		}

		private void numeric_KeyPress(object sender, KeyPressEventArgs e)
		{
			FormHelper.CheckNumericKeyPress(e);
		}

		private async void btnSendMail_ClickAsync(object sender, EventArgs e)
		{
			var teacher = this.GetSelectedTeacher();
			var mailContent = this.CreateMailContent(teacher);
			var result = await FormHelper.SendMailAsync(mailContent);

			FormHelper.ShowActionResult(result, "Email sent successfully.", "Failed to send email.");
		}

		private MailContent CreateMailContent(Teacher teacher)
		{
			return new MailContent
			{
				To = teacher.Email,
				Subject = $"Driving School",
				Body = $"<h1>Hello {teacher.FullName},</h1>" +
					   $"<p>{txtMessage.Text}</p>"
			};
		}

		private void cboStatus_Filter_SelectedIndexChanged(object sender, EventArgs e)
		{
			FormHelper.ClearDataGridViewRow(dgvTeachers);

			if (FormHelper.HasSelectedItem(cboStatus_Filter))
			{
				string status = cboStatus_Filter.Text;
				TeacherService.FilterTeachersByStatus(dgvTeachers, status);
				this.UpdateControlsWithSelectedRowData();
			}
			else
				this.LoadAllTeachers();
		}

		private void dtpBeginningDate_ValueChanged(object sender, EventArgs e)
		{
			this.SetBeginningYears(dtpBeginningDate.Value, txtBeginningYears);
		}

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }
    }
}
