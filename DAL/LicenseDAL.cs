using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class LicenseDAL : BaseDAL<License>
    {
        #region Properties
        private static LicenseDAL instance;
        public static LicenseDAL Instance
        {
            get
            {
                if (instance == null) instance = new LicenseDAL();
                return instance;
            }
        }
        #endregion

        #region All license
        protected override IEnumerable<dynamic> QueryAllData()
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = db.Licenses.Select(li => li).OrderByDescending(li => li.LicenseID);

                return data.ToList();
            }
        }

        public List<License> GetAllLicenses()
        {
            return GetAll(item => new License
            {
                LicenseID = item.LicenseID,
                LicenseName = item.LicenseName
            });
        }
        #endregion

        public List<License> GetLicensesForLearner(int age)
        {
            using (var db = DataAccess.GetDataContext())
            {
                IQueryable<License> data = db.Licenses.Select(li => li).Where(li => li.LicenseName != "E")
                                    .OrderByDescending(li => li.LicenseID);

                if (age >= 24)
                {
                    // Đủ 24 tuổi trở lên, lấy tất cả license ngoại trừ "E" (đã lọc từ đầu)
                }

                else if (age >= 21)
                    data = data.Where(l => l.LicenseName != "D");

                else if (age >= 18)
                    data = data.Where(l => l.LicenseName == "B" || l.LicenseName == "None");

                return data.ToList();
            }
        }

        #region Filter
        protected override IEnumerable<dynamic> QueryDataByFilter(string filterString)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Search
        protected override IEnumerable<dynamic> QueryDataByKeyword(string keyword)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
