using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;

namespace DAL
{
	public class CourseDAL : BaseDAL<Course>
	{
		#region Properties
		private static CourseDAL instance;
		public static CourseDAL Instance
		{
			get
			{
				if (instance == null) instance = new CourseDAL();
				return instance;
			}
		}
		#endregion

		#region All Courses
		protected override IEnumerable<dynamic> QueryAllData()
		{
			using (var db = DataAccess.GetDataContext())
			{
				var data = from course in db.Courses
						   join license in db.Licenses on course.LicenseID equals license.LicenseID
						   join status in db.Status on course.StatusID equals status.StatusID
						   orderby course.CourseName
						   select new
						   {
							   course.CourseID,
							   course.CourseName,
							   course.LicenseID,
							   license.LicenseName,
							   status.StatusID,
							   status.StatusName,
							   course.Fee,
							   course.DurationInHours,
							   course.HoursStudied,
							   course.StartDate,
							   course.EndDate,
							   course.Created_At,
							   course.Updated_At
						   };

				return data.ToList();
			}
		}

		public List<Course> GetAllCourses()
		{
			return GetAll(item => this.MapToCourse(item));
		}
		#endregion

		#region Filter
		protected override IEnumerable<dynamic> QueryDataByFilter(string statusName)
		{
			using (var db = DataAccess.GetDataContext())
			{
				var data = from course in db.Courses
						   join license in db.Licenses on course.LicenseID equals license.LicenseID
						   join status in db.Status on course.StatusID equals status.StatusID
						   where status.StatusName == statusName
						   orderby course.CourseName
						   select new
                           {
							   course.CourseID,
							   course.CourseName,
							   course.LicenseID,
							   license.LicenseName,
							   status.StatusID,
							   status.StatusName,
							   course.Fee,
							   course.DurationInHours,
							   course.HoursStudied,
							   course.StartDate,
							   course.EndDate,
							   course.Created_At,
							   course.Updated_At
						   };

				return data.ToList();
			}
		}

		public List<Course> FilterCoursesByStatus(string status)
		{
			return FilterData(status, item => this.MapToCourse(item));
		}
		#endregion

		#region Search
		protected override IEnumerable<dynamic> QueryDataByKeyword(string keyword)
		{
			using (var db = DataAccess.GetDataContext())
			{
				var data = from course in db.Courses
						   join license in db.Licenses on course.LicenseID equals license.LicenseID
						   join status in db.Status on course.StatusID equals status.StatusID
						   where course.CourseName.Contains(keyword)
						   orderby course.CourseName
						   select new
                           {
							   course.CourseID,
							   course.CourseName,
							   course.LicenseID,
							   license.LicenseName,
							   status.StatusID,
							   status.StatusName,
							   course.Fee,
							   course.DurationInHours,
							   course.HoursStudied,
							   course.StartDate,
							   course.EndDate,
							   course.Created_At,
							   course.Updated_At
						   };

				return data.ToList();
			}
		}

		public List<Course> SearchCourses(string keyword)
		{
			return SearchData(keyword, item => this.MapToCourse(item));
		}
		#endregion

		#region Create
		public bool AddCourse(Course course)
		{
			return AddData(course);
		}
		#endregion

		#region Edit
		public bool EditCourse(Course course)
		{
			return EditData(c => c.CourseID == course.CourseID,           // Điều kiện tìm course theo id
							c =>                                          // Action cập nhật các thuộc tính
							{
								c.CourseName = course.CourseName;
								c.LicenseID = course.LicenseID;
								c.Fee = course.Fee;
								c.StatusID = course.StatusID;
								c.DurationInHours = course.DurationInHours;
								c.Updated_At = DateTime.Now;
							});
		}
		#endregion

		#region Delete
		public bool DeleteCourse(int courseID)
		{
			return UpdateStatus(c => c.CourseID == courseID, StatusID_Inactive); // Điều kiện tìm course theo id
		}
		#endregion

		#region Map to Course
		private Course MapToCourse(dynamic item)
		{
			return new Course
			{
				CourseID = item.CourseID,
				CourseName = item.CourseName,
				License = new License
				{
					LicenseID = item.LicenseID,
					LicenseName = item.LicenseName
				},
				Status = new Status
				{
					StatusID = item.StatusID,
					StatusName = item.StatusName,
				},
				Fee = item.Fee,
				DurationInHours = item.DurationInHours,
				HoursStudied = item.HoursStudied,
				StartDate = item.StartDate,
				EndDate = item.EndDate,
				Created_At = item.Created_At,
				Updated_At = item.Updated_At
			};
		}
		#endregion

		#region Get course by id
		public Course GetCourse(int courseID)
		{
			using (DrivingSchoolDataContext db = DataAccess.GetDataContext())
			{
				// Dùng DataLoadOptions để eager load các dữ liệu liên quan
				var loadOptions = new DataLoadOptions();
				loadOptions.LoadWith<Course>(c => c.License);
				db.LoadOptions = loadOptions;

				var course = db.Courses.Where(c => c.CourseID == courseID).FirstOrDefault();
				return course ?? new Course();
			}
		}
		#endregion

		#region Update hours studied
		public void UpdateHoursStudied(int courseID, int hours)
		{
			using (var db = DataAccess.GetDataContext())
			{
				var course = db.Courses.Where(c => c.CourseID == courseID).FirstOrDefault();
				if (course == null) return;
				course.HoursStudied += hours;
				db.SubmitChanges();
			}
		}
		#endregion

		#region Lấy các khóa học ở thời điểm hiện tại trở đi và chưa có người học
		public List<Course> GetAvailableCourses()
		{
			using (var db = DataAccess.GetDataContext())
			{
				var courses = from c in db.Courses
							  where c.StartDate >= DateTime.Now
							  && c.StatusID == 1
							  && !db.Enrollments.Any(e => e.CourseID == c.CourseID) // Chỉ lấy các khóa học chưa có trong Enrollment
							  orderby c.CourseName
							  select c;
                if (courses == null) return new List<Course>();
				return courses.ToList();
			}
		}
		#endregion
		
		#region Lấy các khóa học ở thời điểm hiện tại trở đi, chưa có người học và của 1 learner
		public List<Course> GetAvailableAndLearnerCourses(int learnerID)
		{
			using (var db = DataAccess.GetDataContext())
			{
				var courses = from c in db.Courses
							  where c.StatusID == 1 &&
								  (
									db.Enrollments.Any(e => e.LearnerID == learnerID 
									&& e.CourseID == c.CourseID)
									|| !db.Enrollments.Any(e => e.CourseID == c.CourseID)
									&& c.StartDate >= DateTime.Now	
								  )
							  orderby c.CourseName
							  select c;

                if (courses == null) return new List<Course>();
				return courses.ToList();
			}
		}
		#endregion

		#region Lấy các khóa học đã có người đăng ký
		public List<Course> GetCourseEnrolled(string statusName, DateTime curDate)
		{
			using (var db = DataAccess.GetDataContext())
			{
				var data = from course in db.Courses
						   join license in db.Licenses on course.LicenseID equals license.LicenseID
						   join status in db.Status on course.StatusID equals status.StatusID
						   where status.StatusName == statusName 
								 && db.Enrollments.Any(e => e.CourseID == course.CourseID)
								 && course.StartDate.Value.Date <= curDate.Date
						   orderby course.CourseName
						   select course;
                if (data == null) return new List<Course>();
				return data.ToList();
			}
		}
		#endregion

		#region Search course by status, keyword
		public List<Course> SearchCourses(string keyword, string statusName)
		{
			using (var db = DataAccess.GetDataContext())
			{
				var data = from course in db.Courses
						   join license in db.Licenses on course.LicenseID equals license.LicenseID
						   join status in db.Status on course.StatusID equals status.StatusID
						   where status.StatusName == statusName
								 && db.Enrollments.Any(e => e.CourseID == course.CourseID) 
								 && course.CourseName.Contains(keyword)
						   orderby course.CourseName
						   select course;
                if (data == null) return new List<Course>();
				
				return data.ToList();
			}
		}
        #endregion

        #region Lấy các khóa học chưa được tạo hóa đơn
        public List<Course> GetCoursesInvoiced(string statusName)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from course in db.Courses
                           join license in db.Licenses on course.LicenseID equals license.LicenseID
                           join status in db.Status on course.StatusID equals status.StatusID
                           where status.StatusName == statusName
								 && !db.Invoices.Any(inv => inv.Enrollment.CourseID == course.CourseID)
                                 && db.Enrollments.Any(e => e.CourseID == course.CourseID)
						   orderby course.CourseName
                           select course;
                if (data == null) return new List<Course>();
                return data.ToList();
            }
        }
        #endregion 
    }
}