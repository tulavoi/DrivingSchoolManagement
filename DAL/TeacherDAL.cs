using DTO;
using System;
using System.Collections.Generic;
using System.Data;
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
                           orderby teacher.TeacherID descending
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
                           orderby teacher.TeacherID descending
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
                           orderby teacher.TeacherID descending
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

				// Lấy licenseID của course
				var courseLicenseID = (from course in db.Courses
									   where course.CourseID == courseID
									   select course.LicenseID).FirstOrDefault();

				var availableTeachers = (from teacher in db.Teachers
										 where !scheduledTeachers.Contains(teacher.TeacherID)
											   && teacher.LicenseID >= courseLicenseID
										 select teacher).ToList();

				if (availableTeachers == null)
					return null;

				return availableTeachers;
			}
		}
		#endregion

		#region Lấy giáo viên hiện có trong khóa học và các giáo viên phù hợp
		public List<Teacher> GetTeacherForCourseAndInCourse(int courseID, int sessionID, DateTime curDate)
		{
			using (DrivingSchoolDataContext db = DataAccess.GetDataContext())
			{
				var scheduledTeachersForOtherCourses = (from sche in db.Schedules
														where sche.SessionID == sessionID
															  && sche.SessionDate == curDate
															  && sche.Enrollment.CourseID != courseID
														select sche.TeacherID).Distinct().ToList();

				// Lấy LicenseID của khóa học
				var courseLicenseID = (from course in db.Courses
									   where course.CourseID == courseID
									   select course.LicenseID).FirstOrDefault();

				// Lọc danh sách giáo viên
				var availableTeachers = (from teacher in db.Teachers
										 where (teacher.LicenseID >= courseLicenseID)
											   // Giáo viên không nằm trong `scheduledTeachersForOtherCourses` 
											   // hoặc là giáo viên đang dạy cho `courseID` hiện tại
											   && (!scheduledTeachersForOtherCourses.Contains(teacher.TeacherID)
												   || (from sche in db.Schedules
													   where sche.TeacherID == teacher.TeacherID
															 && sche.SessionID == sessionID
															 && sche.SessionDate == curDate
															 && sche.Enrollment.CourseID == courseID
													   select sche.TeacherID).Any())
										 select teacher).ToList();

				return availableTeachers ?? new List<Teacher>();
			}
		}
        #endregion

        #region Map to teacher
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
        #endregion

		public DataTable GetTeachersDTO()
		{
            using (var db = DataAccess.GetDataContext())
            {
                var data = from teacher in db.Teachers
                           join license in db.Licenses on teacher.LicenseID equals license.LicenseID
                           where teacher.StatusID == 1
                           orderby teacher.TeacherID descending
                           select new
                           {
                               FullName = teacher.FullName,
                               CitizenID = teacher.CitizenID,
                               DateOfBirth = teacher.DateOfBirth.Value,
                               Gender = teacher.Gender,
                               PhoneNumber = teacher.PhoneNumber,
                               Email = teacher.Email,
                               Nationality = teacher.Nationality,
                               Address = teacher.Address,
                               EmploymentDate = teacher.EmploymentDate.Value,
                               LicenseName = license.LicenseName,
                               LicenseNumber = teacher.LicenseNumber,
                               BeginningDate = teacher.BeginningDate.Value
                           };

				DataTable dt = this.CreateDataTable();

                foreach (var item in data)
                {
                    dt.Rows.Add(item.FullName, item.CitizenID, item.DateOfBirth.ToString("dd/MM/yyyy"), item.Gender, 
                         item.PhoneNumber, item.Email, item.Nationality,
                         item.Address, item.EmploymentDate.ToString("dd/MM/yyyy"), 
						 item.LicenseName, item.LicenseNumber, item.BeginningDate.ToString("dd/MM/yyyy"));
                }
				return dt;
            }
        }

        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FullName", typeof(string));
            dt.Columns.Add("CitizenID", typeof(string));
            dt.Columns.Add("DateOfBirth", typeof(string));
            dt.Columns.Add("Gender", typeof(string));
            dt.Columns.Add("PhoneNumber", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Nationality", typeof(string));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("EmploymentDate", typeof(string));
            dt.Columns.Add("LicenseName", typeof(string));
            dt.Columns.Add("LicenseNumber", typeof(string));
            dt.Columns.Add("BeginningDate", typeof(string));
            return dt;
        }
    }
}
