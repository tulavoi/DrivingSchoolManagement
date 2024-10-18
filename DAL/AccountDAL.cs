using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AccountDAL
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
