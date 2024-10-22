using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
