using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var allCourses = CourseDAL.Instance.GetAllCourses();

            // Lấy danh sách khóa học mà học viên đã tham gia
            var schedules = ScheduleDAL.Instance.GetSchedulesByLearnerId(learnerId);
            var learnerCourses = schedules
                                .Join(allCourses, sche => sche.CourseID, course => course.CourseID, (sche, c) => c)
                                .Where(course => course.StatusID == 1001)
                                .Distinct()
                                .ToList();

            // Nếu learner đã tham gia khóa học, trả về danh sách đó
            if (learnerCourses.Any())
                return learnerCourses;

            // Nếu chưa tham gia khóa học nào, trả về toàn bộ khóa học
            return allCourses;
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
                Created_At = item.Created_At,
                Updated_At = item.Updated_At
            };
        }
    }
}