using BLL.Services;
using DAL;
using GUI.Validators;
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
	public partial class AccountsForm : Form
	{
        #region Properties
        private Account selectedAccount;
        #endregion

        public AccountsForm()
		{
			InitializeComponent();
			FormHelper.ApplyRoundedCorners(this, 20);
		}

        private void LoadAllAccounts()
        {
            AccountService.LoadAllAccounts(dgvAccounts);
            this.UpdateControlsWithSelectedRowData();
            this.ResetChangePassControls();
        }

        private bool isEditing = false;

        private void UpdateControlsWithSelectedRowData()
        {
            if (!FormHelper.HasSelectedRow(dgvAccounts)) return;

            this.selectedAccount = this.GetSelectedAccount();
            this.AssignDataToControls(this.selectedAccount);
        }

        private void AssignDataToControls(Account selectedAccount)
        {
            if (selectedAccount == null) { return; }
            // Assign account data to form controls
            string AccountId ="ID: " + selectedAccount.AccountID.ToString();

            FormHelper.SetLabelID(lblAccountID, AccountId);
            txtEmail.Text = selectedAccount.Email;
            if (selectedAccount.Permission == true)
            {
                cboPermission.Text = "Admin";
            } else
            {
                cboPermission.Text = "Teacher";
            }
        }

        private Account GetSelectedAccount()
        {
            var selectedRow = dgvAccounts.SelectedRows[0];
            if (selectedRow.Tag is Account selectedAccount) return selectedAccount;

            return null;
        }

        private bool HasSelectedRow()
        {
            return dgvAccounts.SelectedRows.Count > 0;
        }

        private void btnOpenAddAccountForm_Click(object sender, EventArgs e)
		{
			CreateAccountForm frm = new CreateAccountForm();
			frm.ShowDialog();
            LoadAllAccounts();
		}

        private void AccountsForm_Load(object sender, EventArgs e)
        {
            this.LoadAllAccounts();
        }

        private void dgvAccounts_SelectionChanged(object sender, EventArgs e)
        {
            this.UpdateControlsWithSelectedRowData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!this.HasSelectedRow()) return;

            if (string.IsNullOrEmpty(txtEmail.Text)) return;

            if (this.ConfirmAction($"Are you sure to delete account '{txtEmail.Text}'?"))
            {
                int accountID = FormHelper.GetObjectID(lblAccountID.Text);
                var result = AccountService.DeleteAccount(accountID);
                FormHelper.ShowActionResult(result, "Accounnt deleted successfully.", "Failed to delete account.");
                this.LoadAllAccounts();
            }
        }

        private bool ConfirmAction(string message)
        {
            DialogResult result = FormHelper.ShowConfirm(message);
            return result == DialogResult.Yes;
        }

        private void ToggleEditMode()
        {
            FormHelper.ToggleEditMode(ref this.isEditing, this.btnEdit, txtEmail, cboPermission);
        }

        private bool InSaveMode()
        {
            return btnEdit.Text == Constant.SAVE_MODE;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!this.InSaveMode())
            {
                this.ToggleEditMode();
                return;
            }

            if (!this.ValidateFields()) return;

            if (this.ConfirmAction($"Are you sure to edit account '{txtEmail.Text}'?"))
            {
                Account account = this.GetAccount();

                var result = AccountService.EditAccount(account);
                FormHelper.ShowActionResult(result, "Account edited successfully.", "Failed to edit account.");
                this.LoadAllAccounts();
            }

            this.ToggleEditMode();
        }

        private Account GetAccount()
        {
            return new Account
            {
                AccountID = FormHelper.GetObjectID(lblAccountID.Text),
                Password = string.IsNullOrEmpty(txtConfirmPass.Text) ? this.selectedAccount.Password : txtConfirmPass.Text,
                Email = txtEmail.Text,
                Permission = cboPermission.Text == "Admin" ? true:false,
                Updated_At = DateTime.Now
            };
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Xóa các dòng cũ trong dgv, lấy keyword từ txtSearch
            // Load dữ liệu search được, gán các thông tin của dòng đc chọn trong dgv sang controls
            FormHelper.ClearDataGridViewRow(dgvAccounts);

            string keyword = txtSearch.Text.ToLower();

            AccountService.SearchAccounts(dgvAccounts, keyword);
            this.UpdateControlsWithSelectedRowData();
        }

        private void cboPermission_Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            AccountService.FilterAccounts(dgvAccounts, cboPermission_Filter.Text);
            this.UpdateControlsWithSelectedRowData();
            this.ResetChangePassControls();
        }

        private void ResetChangePassControls()
        {
            txtOldPass.Text = "";
            txtNewPass.Text = "";
            txtConfirmPass.Text = "";
        }

        private bool ValidateFields()
        {
            if (!AccountValidator.ValidatePermission(cboPermission, toolTip)) return false;
            if (!AccountValidator.ValidateEmail(txtEmail, toolTip)) return false;
            return true;
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            if (!ValidateFieldsForChangePass()) return;

            if (this.ConfirmAction($"Are you sure to change password?"))
            {
                Account account = this.GetAccount();

                var result = AccountService.EditAccount(account);
                FormHelper.ShowActionResult(result, "Change password successfully.", "Failed to change password.");
                this.LoadAllAccounts();
            }
        }

        private bool ValidateFieldsForChangePass()
        {
            var textboxes = new List<Guna2TextBox>
            {
                txtOldPass, txtNewPass, txtConfirmPass
            };

            if (!AccountValidator.ValidateRequiedPass(textboxes, toolTip)) return false;

            if (!AccountValidator.ValidateOldPass(txtOldPass, toolTip, this.selectedAccount.Password)) return false;

            if (!AccountValidator.ValidateNewPass(txtNewPass, toolTip)) return false;

            if (!AccountValidator.ValidateConfirmPass(txtConfirmPass, toolTip, txtNewPass.Text)) return false;

            return true;
        }
    }
}
