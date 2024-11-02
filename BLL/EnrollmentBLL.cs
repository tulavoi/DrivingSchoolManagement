using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
	public class EnrollmentBLL
	{
		#region Properties
		private static EnrollmentDAL instance;
		public static EnrollmentDAL Instance
		{
			get
			{
				if (instance == null) instance = new EnrollmentDAL();
				return instance;
			}
		}
		#endregion

		public Enrollment GetEnrollmentByID(int id)
		{
			return EnrollmentDAL.Instance.GetEnrollmentByID(id);
		}
	}
}
