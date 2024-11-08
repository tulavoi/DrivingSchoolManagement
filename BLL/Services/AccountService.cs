using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AccountService
    {
        public static void LoadAllAccounts(Guna2DataGridView dgv)
        {
            AccountBLL.Instance.LoadAllAccounts(dgv);
        }

        public static bool AddAccount(Account account)
        {
            return AccountBLL.Instance.AddAccount(account);
        }

        public static bool EditAccount(Account account)
        {
            return AccountBLL.Instance.EditAccount(account);
        }

        public static bool DeleteAccount(int accountID)
        {
            return AccountBLL.Instance.DeleteAccount(accountID);
        }

        public static void SearchAccounts(Guna2DataGridView dgv, string keyword)
        {
            AccountBLL.Instance.SearchAccounts(dgv, keyword);
        }     
        public static void FilterAccounts(Guna2DataGridView dgv, string keyword)
        {
            AccountBLL.Instance.FilterAccounts(dgv, keyword);
        }
    }
}
