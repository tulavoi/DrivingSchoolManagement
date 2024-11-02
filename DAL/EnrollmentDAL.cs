using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
	public class EnrollmentDAL : BaseDAL<Enrollment>
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

		protected override IEnumerable<dynamic> QueryAllData()
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable<dynamic> QueryDataByFilter(string filterString)
		{
			throw new NotImplementedException();
		}

		protected override IEnumerable<dynamic> QueryDataByKeyword(string keyword)
		{
			throw new NotImplementedException();
		}

		#region Create
		public bool AddEnrollment(int learnerID, int courseID)
		{
			var enrollment = new Enrollment
			{
				LearnerID = learnerID,
				CourseID = courseID,
				EnrollmentDate = DateTime.Now,
				IsComplete = false
			};
			return AddData(enrollment);
		}
		#endregion
	}
}
