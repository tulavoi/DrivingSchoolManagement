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

        #region Filter
        protected override IEnumerable<dynamic> QueryDataByFilter(string filterString)
        {
            throw new NotImplementedException();
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
            return DeleteData(c => c.CourseID == courseID); // Điều kiện tìm course theo id
        }
        #endregion
    }

}