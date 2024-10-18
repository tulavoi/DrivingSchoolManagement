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
    public partial class AssignScheduleForm : Form
    {
        #region Properties
        private DateTime _date;
        #endregion

        public AssignScheduleForm(DateTime date)
        {
            InitializeComponent();
            _date = date;
            FormHelper.ApplyRoundedCorners(this, 20);
        }

        private void AssignScheduleForm_Load(object sender, EventArgs e)
        {
            shadowForm.SetShadowForm(this);

            this.AssignDateToLabel();
        }

        private void AssignDateToLabel()
        {
            lblDateAssign.Text = _date.ToString("dd/MM/yyyy");
        }

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
