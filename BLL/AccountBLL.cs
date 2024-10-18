using DAL;

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
	}
}
