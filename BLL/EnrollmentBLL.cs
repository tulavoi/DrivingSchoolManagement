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
		private static EnrollmentBLL instance;
		public static EnrollmentBLL Instance
		{
			get
			{
				if (instance == null) instance = new EnrollmentBLL();
				return instance;
			}
		}
		#endregion

		public Enrollment GetEnrollmentByLearnerID(int id)
		{
			return EnrollmentDAL.Instance.GetEnrollmentByLearnerID(id);
		}

		public Enrollment GetEnrollmentByCourseID(int id)
		{
			return EnrollmentDAL.Instance.GetEnrollmentByCourseID(id);
		}
	}
}
