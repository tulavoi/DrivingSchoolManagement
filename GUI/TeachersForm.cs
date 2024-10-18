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

namespace GUI
{
    public partial class TeachersForm : Form
    {
        #region Properties
        private bool isEditing = false;

        #endregion

        public TeachersForm()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
        }

        private void btnEditTeacher_Click(object sender, EventArgs e)
        {
			FormHelper.ToggleEditMode(ref this.isEditing, this.btnEditTeacher, txtTeacherName, txtPhone, txtEmail, cboGender, dtpDOB, txtAddress, txtCitizenId, dtpGraduated, cboNationality, cboLicense);
		}

		private void btnOpenAddTeacherForm_Click(object sender, EventArgs e)
        {
            FormHelper.OpenPopupForm(new AddTeacherForm());
        }

        private void btnDeleteTeacher_Click(object sender, EventArgs e)
        {
			if (FormHelper.ConfirmDelete())
			{

			}
		}
    }
}
