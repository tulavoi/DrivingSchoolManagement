using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LicenseEnrollmentCountBLL
    {
        #region Properties
        private static LicenseEnrollmentCountBLL instance;
        public static LicenseEnrollmentCountBLL Instance
        {
            get
            {
                if (instance == null) instance = new LicenseEnrollmentCountBLL();
                return instance;
            }
        }
        #endregion

        public List<LicenseEnrollmentCount> GetEnrollmentsByLicense()
        {
            return LicenseEnrollmentCountDAL.Instance.GetEnrollmentsByLicense();
        }
    }
}
