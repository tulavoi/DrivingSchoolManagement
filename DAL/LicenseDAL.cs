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
                var data = db.Licenses.Select(li => li);

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
