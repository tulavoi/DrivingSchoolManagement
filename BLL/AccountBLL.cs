using DAL;
using Guna.UI2.WinForms;
using System.Collections.Generic;

namespace BLL
{
    public class AccountBLL
    {
        #region Properties
        private static AccountBLL instance;
        public static AccountBLL Instance
        {
            get
            {
                if (instance == null)
                    instance = new AccountBLL();
                return instance;
            }
        }
        #endregion

        public bool CheckLogin(string email, string pass)
        {
            return AccountDAL.Instance.CheckLogin(email, pass);
        }

        public void LoadAllAccounts(Guna2DataGridView dgv)
        {
            List<Account> accounts = AccountDAL.Instance.GetAllAccount();
            this.AddAccountsToDataGridView(dgv, accounts);
        }

        private void AddAccountsToDataGridView(Guna2DataGridView dgv, List<Account> accounts)
        {
            dgv.Rows.Clear();
            foreach (var account in accounts)
            {
                int rowIndex = dgv.Rows.Add();

                if (rowIndex != -1 && rowIndex < dgv.Rows.Count)
                {
                    dgv.Rows[rowIndex].Tag = account;
                    dgv.Rows[rowIndex].Cells["Email"].Value = account.Email;
                    dgv.Rows[rowIndex].Cells["Permission"].Value = account.Permission == true ? "Admin" : "Teacher";
                    dgv.Rows[rowIndex].Cells["Created_At"].Value = account.Created_At.GetValueOrDefault().ToString("dd/MM/yyyy");
                }
            }
        }

        public bool AddAccount(Account account)
        {
            return AccountDAL.Instance.AddAccount(account);
        }

        public bool EditAccount(Account account)
        {
            return AccountDAL.Instance.EditAccount(account);
        }

        public bool DeleteAccount(int accountID)
        {
            return AccountDAL.Instance.DeleteAccount(accountID);
        }

        public void SearchAccounts(Guna2DataGridView dgv, string keyword)
        {
            List<Account> accounts = AccountDAL.Instance.SearchAccounts(keyword);
            this.AddAccountsToDataGridView(dgv, accounts);
        }
        
        public void FilterAccounts(Guna2DataGridView dgv, string permissionName)
        {
            bool permission = false;
            if (permissionName == "Teacher") permission = false;
            if (permissionName == "Admin") permission = true;
            List<Account> accounts = AccountDAL.Instance.FilterAccountsByPermission(permission);
            this.AddAccountsToDataGridView(dgv, accounts);
        }



    }
}
