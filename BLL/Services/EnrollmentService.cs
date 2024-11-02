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
		public static Enrollment GetEnrollmentByID(int id)
		{
			return EnrollmentBLL.Instance.GetEnrollmentByID(id);
		}
	}
}
