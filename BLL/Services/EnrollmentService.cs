using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public static class EnrollmentService
	{
		public static Enrollment GetEnrollmentByLearnerID(int learnerID)
		{
			return EnrollmentBLL.Instance.GetEnrollmentByLearnerID(learnerID);
		}

		public static Enrollment GetEnrollmentByCourseID(int id)
		{
			return EnrollmentBLL.Instance.GetEnrollmentByCourseID(id);
		}
	}
}
