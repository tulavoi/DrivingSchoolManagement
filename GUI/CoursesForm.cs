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
    public partial class CoursesForm : Form
    {
		#region Properties
		private bool isEditing = false;

		#endregion

		public CoursesForm()
        {
            InitializeComponent();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
			FormHelper.ToggleEditMode(ref this.isEditing, this.btnEdit, txtFee, cboLicenses);
		}

        private void btnOpenAddCourseForm_Click(object sender, EventArgs e)
        {
			FormHelper.OpenPopupForm(new AddCourseForm());
        }
    }
}
