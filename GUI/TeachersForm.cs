using BLL;
using BLL.Services;
using DAL;
using Guna.UI2.WinForms;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
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

            FormHelper.SetDateTimePickerMaxValue(dtpDOB, dtpGraduated);
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

            txtTeacherName.Text = teacher.FullName;
            txtCitizenId.Text = teacher.CitizenID.ToString();
            txtEmail.Text = teacher.Email;
            txtPhone.Text = teacher.Phone.ToString();
            dtpDOB.Value = teacher.DateOfBirth.Value;
            cboGender.Text = teacher.Gender.ToString();
            txtAddress.Text = teacher.Address;
            cboLicense.Text = teacher.License.LicenseName.ToString();
            dtpGraduated.Value = teacher.GraduatedDate.Value;
            this.SetGraduateYears(dtpGraduated.Value, txtGraduateYears);
        }

        public void SetGraduateYears(DateTime graduateDate, Guna2TextBox txt)
        {
            txt.Text = this.GetGraduateYears(graduateDate) + " Years";
        }

        private string GetGraduateYears(DateTime graduateDate)
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
            ComboboxService.AssignLicensesToCombobox(cboLicense);
        }

        private void btnEditTeacher_Click(object sender, EventArgs e)
        {
			FormHelper.ToggleEditMode(ref this.isEditing, this.btnEditTeacher, txtTeacherName, txtPhone, txtEmail, cboGender, dtpDOB, txtAddress, txtCitizenId, dtpGraduated, cboNationality, cboLicense);
		}

		private void btnOpenAddTeacherForm_Click(object sender, EventArgs e)
        {
            FormHelper.OpenFormDialog(new AddTeacherForm());
            this.LoadAllTeachers();
        }

        private void btnDeleteTeacher_Click(object sender, EventArgs e)
        {
			if (FormHelper.ConfirmDelete())
			{

			}
		}

        private void dtpGraduated_ValueChanged(object sender, EventArgs e)
        {
            this.SetGraduateYears(dtpGraduated.Value, txtGraduateYears);
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

            TeacherService.SearchTeachers(dgvTeachers, keyword);
            this.UpdateControlsWithSelectedRowData();
        }

        private void numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormHelper.CheckNumericKeyPress(e);
        }
    }
}
