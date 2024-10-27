using System;
using System.Collections.Generic;
using System.Data;
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
                           where course.CourseName.Contains(keyword)
                           select new
                           {
                               course.CourseID,
                               course.CourseName,
                               course.LicenseID,
                               license.LicenseName,
                               course.Fee,
                               course.DurationInHours,
                               course.HoursStudied,
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
                            c =>                                         // Action cập nhật các thuộc tính
                            {
                                c.CourseName = course.CourseName;
                                c.LicenseID = course.LicenseID;
                                c.Fee = course.Fee;
                                c.DurationInHours = course.DurationInHours;
                                c.Updated_At = DateTime.Now;
                            });
        }
        #endregion

        #region Delete
        public bool DeleteCourse(int courseID)
        {
            return UpdateStatus(c => c.CourseID == courseID, 1002); // Điều kiện tìm course theo id
        }
        #endregion

        #region Load Courses by Learner
        public List<Course> GetCoursesForLearner(int learnerId)
        {
            // Lấy ra khóa học mà learner đã tham gia
            var scheduledCourses = this.GetScheduledCoursesForLearner(learnerId);
            if (scheduledCourses.Any()) return scheduledCourses;

            // Lấy các khóa học mà học viên chưa tham gia và chưa hoàn thành
            var courses = this.GetLearnerCourses(learnerId);
            if (courses == null) return new List<Course>();
            return courses;
        }

        private List<Course> GetScheduledCoursesForLearner(int learnerId)
        {
            using (DrivingSchoolDataContext db = DataAccess.GetDataContext())
            {
                var course = from c in db.Courses
                             join sche in db.Schedules on c.CourseID equals sche.CourseID
                             where sche.LearnerID == learnerId && c.HoursStudied < c.DurationInHours
                             select c;
                if (course == null) return null;
                return course.ToList();
            }
        }

        private List<Course> GetLearnerCourses(int learnerId)
        {
            var learner = LearnerDAL.Instance.GetLearner(learnerId);

            if (learner.CurrentLicenseID == 1005)
                return null;

            // Lấy các khóa học chưa hoàn thành
            var incompleteCourses = this.GetIncompleteCourses();

            List<Course> availableCourses = new List<Course>();

            foreach (var course in incompleteCourses)
            {
                if (learner.CurrentLicenseID < course.LicenseID)
                    availableCourses.Add(course);
            }

            return availableCourses;
        }
        #endregion

        #region Lấy các khóa học chưa hoàn thành
        private List<Course> GetIncompleteCourses()
        {
            using (DrivingSchoolDataContext db = DataAccess.GetDataContext())
            {
                var scheduledCourses = db.Schedules.Select(s => s.CourseID).Distinct();

                // Lọc các khóa học chưa hoàn thành, chưa có trong bất kỳ schedule nào, có status là Acitve
                return db.Courses.Where(c => c.DurationInHours > c.HoursStudied
                                            && !scheduledCourses.Contains(c.CourseID)
                                            && c.StatusID == 1001)
                                        .OrderBy(c => c.CourseName).ToList();
            }
        }
        #endregion

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
                Created_At = item.Created_At,
                Updated_At = item.Updated_At
            };
        }

        #region Get course by id
        public Course GetCourse(int courseID)
        {
            using (DrivingSchoolDataContext db = DataAccess.GetDataContext())
            {
                var course = db.Courses.Where(c => c.CourseID == courseID).FirstOrDefault();
                if (course == null) return null;
                return course;
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
    }
}