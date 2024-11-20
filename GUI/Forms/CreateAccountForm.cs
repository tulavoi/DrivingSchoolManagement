using BLL.Services;
using DAL;
using GUI.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

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
            Account account = this.GetAccount();

            if (!this.ValidateFields()) return;

            if (AccountService.AddAccount(account))
                FormHelper.ShowNotify("Account added successfully.");
            else
                FormHelper.ShowError("Failed to add account.");

        }

        private Account GetAccount()
        {
            return new Account
            {
                Email = txtEmail.Text,
                Permission = cboPermission.SelectedItem.ToString() == "Admin" ? true : false,
                Created_At = DateTime.Now,
                Updated_At = DateTime.Now
            };
        }

        private bool ValidateFields()
        {
            if (!AccountValidator.ValidatePermission(cboPermission, toolTip)) return false;
            if (!AccountValidator.ValidateEmail(txtEmail, toolTip)) return false;
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
