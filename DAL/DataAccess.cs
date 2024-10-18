using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
	public static class DataAccess
	{
		private static string connectionString;

		public static void SetConnectionString(string serverName, string databaseName)
		{
			connectionString = $"Data Source={serverName}; Initial Catalog={databaseName}; Integrated Security=True; Encrypt=True; TrustServerCertificate=True";
		}

		public static DrivingSchoolDataContext GetDataContext()
		{
			return new DrivingSchoolDataContext(connectionString);
		}

		private static SqlConnection GetConnection()
		{
			return new SqlConnection(connectionString);
		}

		public static bool TestConnection()
		{
			try
			{
				using (SqlConnection conn = GetConnection())
				{
					conn.Open();
					return true;
				}
			}
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
                return false;
			}
		}
	}
}
