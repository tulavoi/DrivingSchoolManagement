using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public static class LicenseEnrollmentCountService
    {
        public static List<LicenseEnrollmentCount> GetEnrollmentsByLicense()
        {
            return LicenseEnrollmentCountBLL.Instance.GetEnrollmentsByLicense();
        }
    }
}
