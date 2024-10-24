using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

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
                           select new
                           {
                               teacher.TeacherID,
                               teacher.FullName,
                               teacher.CitizenID,
                               license.LicenseID,
                               license.LicenseName,
                               teacher.DateOfBirth,
                               teacher.Gender,
                               teacher.Phone,
                               teacher.Email,
                               teacher.Nationality,
                               teacher.Address,
                               teacher.EmploymentDate,
                               teacher.Status,
                               teacher.GraduatedDate,
                               teacher.Created_At,
                               teacher.Updated_At,
                           };

                return data.ToList();
            }
        }

        public List<Teacher> GetAllTeachers()
        {
            return GetAll(item => new Teacher
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
                Phone = item.Phone,
                Email = item.Email,
                Nationality = item.Nationality,
                Address = item.Address,
                EmploymentDate = item.EmploymentDate,
                Status = item.Status,
                GraduatedDate = item.GraduatedDate,
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
                var data = from teacher in db.Teachers
                           join license in db.Licenses on teacher.LicenseID equals license.LicenseID
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
                               teacher.Phone,
                               teacher.Email,
                               teacher.Nationality,
                               teacher.Address,
                               teacher.EmploymentDate,
                               teacher.Status,
                               teacher.GraduatedDate,
                               teacher.Created_At,
                               teacher.Updated_At,
                           };

                return data.ToList();
            }
        }

        public List<Teacher> SearchTeachers(string keyword)
        {
            return SearchData(keyword, item => new Teacher
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
                Phone = item.Phone,
                Email = item.Email,
                Nationality = item.Nationality,
                Address = item.Address,
                EmploymentDate = item.EmploymentDate,
                Status = item.Status,
                GraduatedDate = item.GraduatedDate,
                Created_At = item.Created_At,
                Updated_At = item.Updated_At
            });
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
                                t.Phone = teacher.Phone;
                                t.Email = teacher.Email;
                                t.Nationality = teacher.Nationality;
                                t.Address = teacher.Address;
                                t.LicenseID = teacher.LicenseID;
                                t.GraduatedDate = teacher.GraduatedDate;
                                t.Updated_At = DateTime.Now;
                            });
        }
        #endregion

        #region Delete
        public bool DeleteTeacher(int teacherID)
        {
            return DeleteData(t => t.TeacherID == teacherID); // Điều kiện tìm teacher theo id
        }
        #endregion

        #region Get teachers for course
        public List<Teacher> GetTeacherForCourse(int courseId)
        {
            var allTeachers = TeacherDAL.Instance.GetAllTeachers();

            var course = CourseDAL.Instance.GetCourseById(courseId);
            if (course == null)
                return new List<Teacher>();

            // Lấy ra các giáo viên đủ điều kiện với bằng lái của khóa học
            // (vẫn chưa lấy được các gv dư điều kiện dạy), VD: gv bằng C có thể dạy bằng B
            var qualifiedTeachers = allTeachers
                                    .Where(t => t.LicenseID == course.LicenseID)
                                    .ToList();

            return qualifiedTeachers;
        }
        #endregion
    }
}
