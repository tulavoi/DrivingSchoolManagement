using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
	public class TeacherDAL : BaseDAL<Teacher>
	{
		#region Properties
		private static TeacherDAL instance;
		public static TeacherDAL Instance
		{
			get
			{
				if (instance == null) instance = new TeacherDAL();
				return instance;
			}
		}
		#endregion

		#region All teachers
		protected override IEnumerable<dynamic> QueryAllData()
		{
			using (var db = DataAccess.GetDataContext())
			{
				var data = from teacher in db.Teachers
						   join license in db.Licenses on teacher.LicenseID equals license.LicenseID
						   join status in db.Status on teacher.StatusID equals status.StatusID
						   select new
						   {
							   teacher.TeacherID,
							   teacher.FullName,
							   teacher.CitizenID,
							   license.LicenseID,
							   license.LicenseName,
							   teacher.DateOfBirth,
							   teacher.Gender,
							   teacher.PhoneNumber,
							   teacher.Email,
							   teacher.Nationality,
							   teacher.Address,
							   teacher.EmploymentDate,
							   status.StatusID,
							   status.StatusName,
							   teacher.BeginningDate,
							   teacher.LicenseNumber,
							   teacher.Created_At,
							   teacher.Updated_At,
						   };

				return data.ToList();
			}
		}

		public List<Teacher> GetAllTeachers()
		{
			return GetAll(item => this.MapToTeacher(item));
		}
		#endregion

		#region Filter by status
		protected override IEnumerable<dynamic> QueryDataByFilter(string statusName)
		{
			using (var db = DataAccess.GetDataContext())
			{
				var data = from teacher in db.Teachers
						   join license in db.Licenses on teacher.LicenseID equals license.LicenseID
						   join status in db.Status on teacher.StatusID equals status.StatusID
						   where status.StatusName == statusName
						   select new
						   {
							   teacher.TeacherID,
							   teacher.FullName,
							   teacher.CitizenID,
							   license.LicenseID,
							   license.LicenseName,
							   teacher.DateOfBirth,
							   teacher.Gender,
							   teacher.PhoneNumber,
							   teacher.Email,
							   teacher.Nationality,
							   teacher.Address,
							   teacher.EmploymentDate,
							   status.StatusID,
							   status.StatusName,
							   teacher.BeginningDate,
							   teacher.LicenseNumber,
							   teacher.Created_At,
							   teacher.Updated_At,
						   };
				return data.ToList();
			}
		}

		public List<Teacher> FilterTeachersByStatus(string status)
		{
			return FilterData(status, item => this.MapToTeacher(item));
		}
		#endregion

		#region Search
		protected override IEnumerable<dynamic> QueryDataByKeyword(string keyword)
		{
			using (var db = DataAccess.GetDataContext())
			{
				var data = from teacher in db.Teachers
						   join license in db.Licenses on teacher.LicenseID equals license.LicenseID
						   join status in db.Status on teacher.StatusID equals status.StatusID
						   where (teacher.FullName.Contains(keyword) || teacher.Nationality.Contains(keyword))
						   select new
						   {
							   teacher.TeacherID,
							   teacher.FullName,
							   teacher.CitizenID,
							   license.LicenseID,
							   license.LicenseName,
							   teacher.DateOfBirth,
							   teacher.Gender,
							   teacher.PhoneNumber,
							   teacher.Email,
							   teacher.Nationality,
							   teacher.Address,
							   teacher.EmploymentDate,
							   status.StatusID,
							   status.StatusName,
							   teacher.BeginningDate,
							   teacher.LicenseNumber,
							   teacher.Created_At,
							   teacher.Updated_At,
						   };

				return data.ToList();
			}
		}

		public List<Teacher> SearchTeachers(string keyword)
		{
			return SearchData(keyword, item => this.MapToTeacher(item));
		}
		#endregion

		#region Create
		public bool AddTeacher(Teacher teacher)
		{
			return AddData(teacher);
		}
		#endregion

		#region Edit
		public bool EditTeacher(Teacher teacher)
		{
			return EditData(t => t.TeacherID == teacher.TeacherID,          // Điều kiện tìm teacher theo id
							t =>                                            // Action cập nhật các thuộc tính
							{
								t.FullName = teacher.FullName;
								t.CitizenID = teacher.CitizenID;
								t.DateOfBirth = teacher.DateOfBirth;
								t.Gender = teacher.Gender;
								t.PhoneNumber = teacher.PhoneNumber;
								t.Email = teacher.Email;
								t.Nationality = teacher.Nationality;
								t.Address = teacher.Address;
								t.LicenseID = teacher.LicenseID;
								t.BeginningDate = teacher.BeginningDate;
								t.StatusID = teacher.StatusID;
								t.Updated_At = DateTime.Now;
							});
		}
		#endregion

		#region Delete
		public bool DeleteTeacher(int teacherID)
		{
			return UpdateStatus(t => t.TeacherID == teacherID, StatusID_Inactive); // Điều kiện tìm teacher theo id
		}
		#endregion

		#region Get teachers for course
		public List<Teacher> GetTeacherForCourse(int courseID, int sessionID, DateTime curDate)
		{
			using (DrivingSchoolDataContext db = DataAccess.GetDataContext())
			{
				var scheduledTeachers = (from sche in db.Schedules
										 where sche.SessionID == sessionID
												&& sche.SessionDate == curDate
										 select sche.TeacherID).Distinct().ToList();

				var availableTeachers = (from teacher in db.Teachers
										 where !scheduledTeachers.Contains(teacher.TeacherID)
											   && teacher.LicenseID >= (from course in db.Courses
																		where course.CourseID == courseID
																		select course.LicenseID).FirstOrDefault()
										 select teacher).ToList();
				if (availableTeachers == null)
					return null;

				return availableTeachers;
			}
		}
		#endregion

		private Teacher MapToTeacher(dynamic item)
		{
			return new Teacher
			{
				TeacherID = item.TeacherID,
				FullName = item.FullName,
				CitizenID = item.CitizenID,
				License = new License
				{
					LicenseID = item.LicenseID,
					LicenseName = item.LicenseName
				},
				DateOfBirth = item.DateOfBirth,
				Gender = item.Gender,
				PhoneNumber = item.PhoneNumber,
				Email = item.Email,
				Nationality = item.Nationality,
				Address = item.Address,
				EmploymentDate = item.EmploymentDate,
				Status = new Status
				{
					StatusID = item.StatusID,
					StatusName = item.StatusName,
				},
				BeginningDate = item.BeginningDate,
				LicenseNumber = item.LicenseNumber,
				Created_At = item.Created_At,
				Updated_At = item.Updated_At
			};
		}
	}
}
