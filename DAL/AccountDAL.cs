using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AccountDAL : BaseDAL<Account>
    {
		#region Properties
		private static AccountDAL instance;
		public static AccountDAL Instance
		{
			get
			{
				if (instance == null)
					instance = new AccountDAL();
				return instance;
			}
		}
        #endregion

        #region Read
        protected override IEnumerable<dynamic> QueryAllData()
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from account in db.Accounts
                           select new
                           {
                               account.AccountID,
                               account.Email,
                               account.Password,
                               account.Permission,
                               account.Created_At,
                               account.Updated_At
                           };

                return data.ToList();
            }
        }

        public List<Account> GetAllAccount()
        {
            return GetAll(item => this.MapToAccount(item));
        }
        #endregion

        #region Create
        public bool AddAccount(Account account)
        {
            return AddData(account);
        }
        #endregion

        #region Edit
        public bool EditAccount(Account account)
        {
            return EditData(c => c.AccountID == account.AccountID,           // Điều kiện tìm course theo id
                            c =>                                          // Action cập nhật các thuộc tính
                            {
                                c.Email = account.Email;
                                c.Permission = account.Permission;
                                c.Updated_At = DateTime.Now;
                            });
        }
        #endregion

        #region Delete
        public bool DeleteAccount(int accountID)
        {
            return DeleteData(acc => acc.AccountID == accountID); // Điều kiện tìm invoice theo code
        }
        #endregion

        #region Search
        protected override IEnumerable<dynamic> QueryDataByKeyword(string keyword)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from account in db.Accounts
                           where (account.Email.Contains(keyword) || account.Permission.ToString().Contains(keyword))
                           select new
                           {
                               account.AccountID,
                               account.Email,
                               account.Permission,
                               account.Password,
                               account.Created_At,
                               account.Updated_At,
                           };

                return data.ToList();
            }
        }

        public List<Account> SearchAccounts(string keyword)
        {
            return SearchData(keyword, item => this.MapToAccount(item));
        }

        #endregion

        #region Filter by status
        protected override IEnumerable<dynamic> QueryDataByFilter(string statusName)
        {
            return QueryAllData();
        }

        public List<Account> FilterAccountsByStatus(string status)
        {
            return FilterData(status, item => this.MapToAccount(item));
        }
        #endregion
        public List<Account> FilterAccountsByPermission(bool permission)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var accounts = from account in db.Accounts 
                               where account.Permission == permission
                               select account;
                return accounts.ToList();
            }
        }

        private Account MapToAccount(dynamic item)
        {
            return new Account
            {
                AccountID = item.AccountID,
                Email = item.Email,
                Password = item.Password,
                Permission = item.Permission,
                Created_At = item.Created_At,
                Updated_At = item.Updated_At
            };
        }

        public bool CheckLogin(string email, string pass)
        {
            using (var db = DataAccess.GetDataContext())
            {
				var query = db.Accounts.Where(a => a.Email == email && a.Password == pass).FirstOrDefault();

				if (query != null)
					return true;
                else 
                    return false;
			}
        }
    }
}
