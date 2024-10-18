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
	public partial class AccountsForm : Form
	{
		public AccountsForm()
		{
			InitializeComponent();
			FormHelper.ApplyRoundedCorners(this, 20);
		}

		private void btnOpenAddAccountForm_Click(object sender, EventArgs e)
		{
			CreateAccountForm frm = new CreateAccountForm();
			frm.ShowDialog();
		}
	}
}
