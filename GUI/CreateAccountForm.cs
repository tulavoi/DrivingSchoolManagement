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
	public partial class CreateAccountForm : Form
	{
		public CreateAccountForm()
		{
			InitializeComponent();
			FormHelper.ApplyRoundedCorners(this, 20);
		}

		private void CreateAccountForm_Load(object sender, EventArgs e)
		{
			shadowForm.SetShadowForm(this);
		}

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}
