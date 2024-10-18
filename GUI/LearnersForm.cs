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
    public partial class LearnersForm : Form
    {
        #region Properties
        private bool isEditing = false;

        #endregion

		public LearnersForm()
        {
            InitializeComponent();
        }

        private void btnEditLearner_Click(object sender, EventArgs e)
        {
			FormHelper.ToggleEditMode(ref this.isEditing, this.btnEditLearner, txtLearnerName, txtPhone, txtEmail, cboGender, dtpDOB, txtAddress, txtCitizenId, cboNationality);
		}

		private void btnDeleteLearner_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show($"Are you sure to delete the learner '{txtLearnerName.Text}'?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (rs == DialogResult.Yes) 
            { 

            }
        }

        private void btnOpenAddLearnerForm_Click(object sender, EventArgs e)
        {
            FormHelper.OpenPopupForm(new AddLearnerForm());
        }
    }
}
