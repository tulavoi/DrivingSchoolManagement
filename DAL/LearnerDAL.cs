using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LearnerDAL : BaseDAL<Learner>
    {
        #region Properties
        private static LearnerDAL instance;

        public static LearnerDAL Instance
        {
            get
            {
                if (instance == null) instance = new LearnerDAL();
                return instance;
            }
        }
        #endregion

        #region All Learners
        protected override IEnumerable<dynamic> QueryAllData()
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from learner in db.Learners
                           select new
                           {
                               learner.LearnerID,
                               learner.CurrentLicenseID,
                               learner.FullName,
                               learner.DateOfBirth,
                               learner.Gender,
                               learner.PhoneNumber,
                               learner.Email,
                               learner.Address,
                               learner.CitizenID,
                               learner.Status,
                               learner.Created_At,
                               learner.Updated_At
                           };
                return data.ToList();
            }
        }

        public List<Learner> GetAllLearners()
        {
            return GetAll(item => new Learner
            {
                LearnerID = item.LearnerID,
                CurrentLicenseID = item.CurrentLicenseID,
                FullName = item.FullName,
                DateOfBirth = item.DateOfBirth,
                Gender = item.Gender,
                PhoneNumber = item.PhoneNumber,
                Email = item.Email,
                Address = item.Address,
                CitizenID = item.CitizenID,
                Status = item.Status,
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
                var data = from learner in db.Learners
                           where (learner.FullName.Contains(keyword) || learner.Email.Contains(keyword) || learner.CitizenID.Contains(keyword))
                           select new
                           {
                               learner.LearnerID,
                               learner.CurrentLicenseID,
                               learner.FullName,
                               learner.DateOfBirth,
                               learner.Gender,
                               learner.PhoneNumber,
                               learner.Email,
                               learner.Address,
                               learner.CitizenID,
                               learner.Status,
                               learner.Created_At,
                               learner.Updated_At
                           };
                return data.ToList();
            }
        }

        public List<Learner> SearchLearners(string keyword)
        {
            return SearchData(keyword, item => new Learner
            {
                LearnerID = item.LearnerID,
                CurrentLicenseID = item.CurrentLicenseID,
                FullName = item.FullName,
                DateOfBirth = item.DateOfBirth,
                Gender = item.Gender,
                PhoneNumber = item.PhoneNumber,
                Email = item.Email,
                Address = item.Address,
                CitizenID = item.CitizenID,
                Status = item.Status,
                Created_At = item.Created_At,
                Updated_At = item.Updated_At
            });
        }
        #endregion

        #region Filter by status
        public List<Learner> FilterLearnersByStatus(string status)
        {
            return FilterData(status, item => new Learner
            {
                LearnerID = item.LearnerID,
                CurrentLicenseID = item.CurrentLicenseID,
                FullName = item.FullName,
                DateOfBirth = item.DateOfBirth,
                Gender = item.Gender,
                PhoneNumber = item.PhoneNumber,
                Email = item.Email,
                Address = item.Address,
                CitizenID = item.CitizenID,
                Status = item.Status,
                Created_At = item.Created_At,
                Updated_At = item.Updated_At
            });
        }

        protected override IEnumerable<dynamic> QueryDataByFilter(string filterString)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from learner in db.Learners
                           where learner.Status == filterString
                           select new
                           {
                               learner.LearnerID,
                               learner.CurrentLicenseID,
                               learner.FullName,
                               learner.DateOfBirth,
                               learner.Gender,
                               learner.PhoneNumber,
                               learner.Email,
                               learner.Address,
                               learner.CitizenID,
                               learner.Status,
                               learner.Created_At,
                               learner.Updated_At
                           };
                return data.ToList();
            }
        }
        #endregion

        #region Create
        public bool AddLearner(Learner learner)
        {
            return AddData(learner);
        }
        #endregion

        #region Edit
        public bool EditLearner(Learner learner)
        {
            return EditData(lear => lear.LearnerID == learner.LearnerID,      // Điều kiện tìm learner theo ID
                            lear =>                                          // Action cập nhật các thuộc tính
                            {
                                lear.CurrentLicenseID = learner.CurrentLicenseID;
                                lear.FullName = learner.FullName;
                                lear.DateOfBirth = learner.DateOfBirth;
                                lear.Gender = learner.Gender;
                                lear.PhoneNumber = learner.PhoneNumber;
                                lear.Email = learner.Email;
                                lear.Address = learner.Address;
                                lear.CitizenID = learner.CitizenID;
                                lear.Status = learner.Status;
                                lear.Updated_At = DateTime.Now;
                            });
        }
        #endregion

        #region Delete
        public bool DeleteLearner(int learnerID)
        {
            return DeleteData(lear => lear.LearnerID == learnerID); // Điều kiện tìm learner theo ID
        }
        #endregion
    }

}
