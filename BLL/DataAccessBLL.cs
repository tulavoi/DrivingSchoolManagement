using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
	public class DataAccessBLL
	{
        #region Properties
        private static DataAccessBLL instance;

        public static DataAccessBLL Instance { 
			get
			{
				if (instance == null)
					instance = new DataAccessBLL();
				return instance;
			}  
		}
        #endregion

        public bool SetupConnection(string serverName, string databaseName)
		{
			DataAccess.SetConnectionString(serverName, databaseName);
			return DataAccess.TestConnection();
		}
	}
}
