using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LicenseEnrollmentCountDAL
    {
        #region Properties
        private static LicenseEnrollmentCountDAL instance;
        public static LicenseEnrollmentCountDAL Instance
        {
            get
            {
                if (instance == null) instance = new LicenseEnrollmentCountDAL();
                return instance;
            }
        }
        #endregion

        #region Lấy ra số lượng đăng ký theo loại bằng
        public List<LicenseEnrollmentCount> GetEnrollmentsByLicense()
        {
            using (var db = DataAccess.GetDataContext())
            {
                var query = from enr in db.Enrollments
                            join course in db.Courses on enr.CourseID equals course.CourseID
                            join license in db.Licenses on course.LicenseID equals license.LicenseID
                            group enr by license.LicenseName into licenseGroup
                            select new
                            {
                                LicenseName = licenseGroup.Key,
                                Count = licenseGroup.Count()
                            };

                var enrollments = new List<LicenseEnrollmentCount>();
                foreach (var item in query)
                {
                    var enrollment = this.GetLicenseEnrollmentCount(item);
                    enrollments.Add(enrollment);
                }

                return enrollments.ToList();
            }
        }
        #endregion

        private LicenseEnrollmentCount GetLicenseEnrollmentCount(dynamic item)
        {
            return new LicenseEnrollmentCount
            {
                LicenseName = item.LicenseName,
                Count = item.Count
            };
        }
    }
}
