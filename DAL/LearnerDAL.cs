using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.PerformanceData;
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
                           join status in db.Status on learner.StatusID equals status.StatusID
                           select new
                           {
                               learner.LearnerID,
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
                           join status in db.Status on learner.StatusID equals status.StatusID
                           where (learner.FullName.Contains(keyword) || learner.Email.Contains(keyword) || learner.CitizenID.Contains(keyword))
                           select new
                           {
                               learner.LearnerID,
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
                           join status in db.Status on learner.StatusID equals status.StatusID
                           where status.StatusName == statusName
                           select new
                           {
                               learner.LearnerID,
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
            return EditData(l => l.LearnerID == learner.LearnerID,        // Điều kiện tìm learner theo ID
                            l =>                                             // Action cập nhật các thuộc tính
                            {
                                l.FullName = learner.FullName;
                                l.DateOfBirth = learner.DateOfBirth;
                                l.Gender = learner.Gender;
                                l.PhoneNumber = learner.PhoneNumber;
                                l.Email = learner.Email;
                                l.Address = learner.Address;
                                l.CitizenID = learner.CitizenID;
                                l.StatusID = learner.StatusID;
                                l.Updated_At = DateTime.Now;
                            });
        }
        #endregion

        #region Delete
        public bool DeleteLearner(int learnerID)
        {
            return UpdateStatus(lear => lear.LearnerID == learnerID, 2); // Điều kiện tìm learner theo ID
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
                Created_At = item.Created_At,
                Updated_At = item.Updated_At
            };
        }

        #region Get learner by learner id
        public Learner GetLearner(int learnerId)
        {
            using (DrivingSchoolDataContext db = DataAccess.GetDataContext())
            {
                var learner = db.Learners.Where(l => l.LearnerID == learnerId).FirstOrDefault();
                if (learner == null) return null;
                return learner;
            }
        }
        #endregion

        #region Update license
        //public void UpdateLicense(int learnerID, int courseID)
        //{
        //    using (DrivingSchoolDataContext db = DataAccess.GetDataContext())
        //    {
        //        var course = CourseDAL.Instance.GetCourse(courseID);

        //        var learner = db.Learners.Where(l => l.LearnerID == learnerID).FirstOrDefault();

        //        if (learner == null || course == null) return;
        //        if (learner.CurrentLicenseID >= 1005) return; // Nếu như learner này có bằng E thì return
        //        learner.CurrentLicenseID = course.LicenseID; // Nâng bằng hiện tại thành bằng của khóa học mà học viênhoàn thành
        //        db.SubmitChanges();
        //    }
        //}
        #endregion
    }
}
