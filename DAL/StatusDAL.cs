using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class StatusDAL : BaseDAL<Status>
    {
        #region Properties
        private static StatusDAL instance;

        public static StatusDAL Instance
        {
            get
            {
                if (instance == null) instance = new StatusDAL();
                return instance;
            }
        }
        #endregion

        #region All Status
        protected override IEnumerable<dynamic> QueryAllData()
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = db.Status.Take(2).Select(st => st); // Lấy 2 status đầu tiên: Active, Inactive
                return data.ToList();
            }
        }

        public List<Status> GetAllStates()
        {
            return GetAll(item => this.MapToLearner(item));
        }
        #endregion

        protected override IEnumerable<dynamic> QueryDataByFilter(string filterString)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<dynamic> QueryDataByKeyword(string keyword)
        {
            throw new NotImplementedException();
        }

        private Status MapToLearner(dynamic item)
        {
            return new Status
            {
                StatusID = item.StatusID,
                StatusName = item.StatusName,
            };
        }
    }
}
