using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

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
                           join license in db.Licenses on learner.CurrentLicenseID equals license.LicenseID into licenseGroup
                           from license in licenseGroup.DefaultIfEmpty()
                           join status in db.Status on learner.StatusID equals status.StatusID
                           select new
                           {
                               learner.LearnerID,
                               license.LicenseID,
                               license.LicenseName,
                               learner.FullName,
                               learner.DateOfBirth,
                               learner.Gender,
                               learner.PhoneNumber,
                               learner.Email,
                               learner.Address,
                               learner.CitizenID,
                               status.StatusID,
                               status.StatusName,
                               learner.Created_At,
                               learner.Updated_At
                           };
                return data.ToList();
            }
        }

        public List<Learner> GetAllLearners()
        {
            return GetAll(item => this.MapToLearner(item));
        }
        #endregion

        #region Search
        protected override IEnumerable<dynamic> QueryDataByKeyword(string keyword)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from learner in db.Learners
                           join license in db.Licenses on learner.CurrentLicenseID equals license.LicenseID into licenseGroup
                           from license in licenseGroup.DefaultIfEmpty()
                           join status in db.Status on learner.StatusID equals status.StatusID
                           where (learner.FullName.Contains(keyword) || learner.Email.Contains(keyword) || learner.CitizenID.Contains(keyword))
                           select new
                           {
                               learner.LearnerID,
                               license.LicenseID,
                               license.LicenseName,
                               learner.FullName,
                               learner.DateOfBirth,
                               learner.Gender,
                               learner.PhoneNumber,
                               learner.Email,
                               learner.Address,
                               learner.CitizenID,
                               status.StatusID,
                               status.StatusName,
                               learner.Created_At,
                               learner.Updated_At
                           };
                return data.ToList();
            }
        }

        public List<Learner> SearchLearners(string keyword)
        {
            return SearchData(keyword, item => this.MapToLearner(item));
        }
        #endregion

        #region Filter by status
        protected override IEnumerable<dynamic> QueryDataByFilter(string statusName)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from learner in db.Learners
                           join license in db.Licenses on learner.CurrentLicenseID equals license.LicenseID into licenseGroup
                           from license in licenseGroup.DefaultIfEmpty()
                           join status in db.Status on learner.StatusID equals status.StatusID
                           where status.StatusName == statusName
                           select new
                           {
                               learner.LearnerID,
                               license.LicenseID,
                               license.LicenseName,
                               learner.FullName,
                               learner.DateOfBirth,
                               learner.Gender,
                               learner.PhoneNumber,
                               learner.Email,
                               learner.Address,
                               learner.CitizenID,
                               status.StatusID,
                               status.StatusName,
                               learner.Created_At,
                               learner.Updated_At
                           };
                return data.ToList();
            }
        }

        public List<Learner> FilterLearnersByStatus(string status)
        {
            return FilterData(status, item => this.MapToLearner(item));
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
            return EditData(lear => lear.LearnerID == learner.LearnerID,        // Điều kiện tìm learner theo ID
                            lear =>                                             // Action cập nhật các thuộc tính
                            {
                                lear.CurrentLicenseID = learner.CurrentLicenseID;
                                lear.FullName = learner.FullName;
                                lear.DateOfBirth = learner.DateOfBirth;
                                lear.Gender = learner.Gender;
                                lear.PhoneNumber = learner.PhoneNumber;
                                lear.Email = learner.Email;
                                lear.Address = learner.Address;
                                lear.CitizenID = learner.CitizenID;
                                lear.StatusID = learner.StatusID;
                                lear.Updated_At = DateTime.Now;
                            });
        }
        #endregion

        #region Delete
        public bool DeleteLearner(int learnerID)
        {
            return UpdateStatus(lear => lear.LearnerID == learnerID, 1002); // Điều kiện tìm learner theo ID
        }
        #endregion


        private Learner MapToLearner(dynamic item)
        {
            return new Learner
            {
                LearnerID = item.LearnerID,
                FullName = item.FullName,
                DateOfBirth = item.DateOfBirth,
                Gender = item.Gender,
                PhoneNumber = item.PhoneNumber,
                Email = item.Email,
                Address = item.Address,
                CitizenID = item.CitizenID,
                Status = new Status
                {
                    StatusID = item.StatusID,
                    StatusName = item.StatusName,
                },
                License = new License
                {
                    LicenseID = item.LicenseID,
                    LicenseName = item.LicenseName,
                },
                Created_At = item.Created_At,
                Updated_At = item.Updated_At
            };
        }

        internal Learner GetLearner(int learnerId)
        {
            using (DrivingSchoolDataContext db = new DrivingSchoolDataContext())
            {
                var learner = db.Learners.Where(l => l.LearnerID == learnerId).FirstOrDefault();
                if (learner == null) return null;
                return learner;
            }
        }
    }

}
