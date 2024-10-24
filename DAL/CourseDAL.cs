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

        #region All Course
        protected override IEnumerable<dynamic> QueryAllData()
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from course in db.Courses
                           join license in db.Licenses on course.LicenseID equals license.LicenseID
                           select new
                           {
                               course.CourseID,
                               course.CourseName,
                               license.LicenseID,
                               license.LicenseName,
                               course.Fee,
                               course.DurationInHours,
                               course.Created_At,
                               course.Updated_At,
                           };

                return data.ToList();
            }
        }

        public List<Course> GetAllCourses()
        {
            return GetAll(item => new Course
            {
                CourseID = item.CourseID,
                CourseName = item.CourseName,
                License = new License
                {
                    LicenseID = item.LicenseID,
                    LicenseName = item.LicenseName
                },
                Fee = item.Fee,
                DurationInHours = item.DurationInHours,
                Created_At = item.Created_At,
                Updated_At = item.Updated_At
            });
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
                               license.LicenseID,
                               license.LicenseName,
                               course.Fee,
                               course.DurationInHours,
                               course.Created_At,
                               course.Updated_At,
                           };
                return data.ToList();
            }
        }

        public List<Course> SearchCourse(string keyword)
        {
            return SearchData(keyword, item => new Course
            {
                CourseID = item.CourseID,
                CourseName = item.CourseName,
                License = new License
                {
                    LicenseID = item.LicenseID,
                    LicenseName = item.LicenseName
                },
                Fee = item.Fee,
                DurationInHours = item.DurationInHours,
                Created_At = item.Created_At,
                Updated_At = item.Updated_At
            });
        }
        #endregion

        #region Filter
        protected override IEnumerable<dynamic> QueryDataByFilter(string filterString)
        {
            throw new NotImplementedException();
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
                                .Distinct()
                                .ToList();

            // Nếu learner đã tham gia khóa học, trả về danh sách đó
            if (learnerCourses.Any())
            {
                return learnerCourses;
            }

            // Nếu chưa tham gia khóa học nào, trả về toàn bộ khóa học
            return allCourses;
        }
        #endregion

        public Course GetCourseById(int courseId)
        {
            using (DrivingSchoolDataContext db = DataAccess.GetDataContext())
            {
                return db.Courses.Where(c => c.CourseID == courseId).FirstOrDefault();
            }
        }
    }
}
