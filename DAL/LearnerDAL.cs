using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

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
                           orderby learner.LearnerID descending
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
                               learner.Nationality,
                               status.StatusID,
                               status.StatusName,
                               learner.IsPass,
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
                           orderby learner.LearnerID descending
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
							   learner.Nationality,
							   status.StatusID,
							   status.StatusName,
                               learner.IsPass,
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
                           orderby learner.LearnerID descending
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
                               learner.Nationality,
                               status.StatusID,
							   status.StatusName,
                               learner.IsPass,
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
        public bool AddLearner(Learner learner, int courseID)
        {
            try
            {
                using (var db = DataAccess.GetDataContext())
                {
                    db.Learners.InsertOnSubmit(learner);
                    db.SubmitChanges();

                    int learnerID = learner.LearnerID;
                    EnrollmentDAL.Instance.AddEnrollment(learnerID, courseID);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return true;
        }
        #endregion

        #region Edit
        public bool EditLearner(Learner learner, int courseID)
        {
            if (!EnrollmentDAL.Instance.EditEnrollment(learner, courseID))
                return false;

            return EditData(l => l.LearnerID == learner.LearnerID,           // Điều kiện tìm learner theo ID
                            l =>                                             // Action cập nhật các thuộc tính
                            {
                                l.FullName = learner.FullName;
                                l.DateOfBirth = learner.DateOfBirth;
                                l.Gender = learner.Gender;
                                l.PhoneNumber = learner.PhoneNumber;
                                l.Email = learner.Email;
                                l.Address = learner.Address;
                                l.Nationality = learner.Nationality;
                                l.CitizenID = learner.CitizenID;
                                l.StatusID = learner.StatusID;
                                l.Updated_At = learner.Updated_At;
                            });
        }
		#endregion

		#region Delete
		public bool DeleteLearner(int learnerID)
        {
            return UpdateStatus(lear => lear.LearnerID == learnerID, StatusID_Inactive); // Điều kiện tìm learner theo ID
        }
        #endregion

        #region Map to learner
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
                Nationality = item.Nationality,
                Status = new Status
                {
                    StatusID = item.StatusID,
                    StatusName = item.StatusName,
                },
                IsPass = item.IsPass,
                Created_At = item.Created_At,
                Updated_At = item.Updated_At
            };
        }
        #endregion

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

        #region Get all learner data
        public DataTable GetAllLearnersData()
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from enr in db.Enrollments
                           join course in db.Courses on enr.CourseID equals course.CourseID
                           join learner in db.Learners on enr.LearnerID equals learner.LearnerID
                           let age = DateTime.Now.Year - learner.DateOfBirth.Value.Year - 
                                    (DateTime.Now.DayOfYear < learner.DateOfBirth.Value.DayOfYear ? 1 : 0)
                           select new
                           {
                               learner.FullName,
                               AgeGroup = this.GetAgeGroup(age),
                               learner.DateOfBirth,
                               learner.Gender,
                               learner.PhoneNumber,
                               learner.Email,
                               learner.Address,
                               learner.CitizenID,
                               learner.Nationality,
                               course.CourseName,
                               learner.Created_At,
                           };
                var dt = this.CreateDataTable();
                foreach (var item in data)
                {

                    dt.Rows.Add(item.FullName, item.DateOfBirth.Value.ToString("dd/MM/yyyy"), item.Gender, 
                        item.PhoneNumber, item.Email, item.Address, item.CitizenID, item.Nationality, 
                        item.Created_At.Value.ToString("dd/MM/yyyy"), "", "", item.CourseName, item.AgeGroup);
                }
                return dt;
            }
        }

        private object GetAgeGroup(int age)
        {
            if (age <= 25)
                return "18-25 Age";
            else if (age <= 35)
                return "26-35 Age";
            else if (age <= 45)
                return "36-45 Age";
            else
                return "46+";
        }

        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FullName", typeof(string));
            dt.Columns.Add("DateOfBirth", typeof(string));
            dt.Columns.Add("Gender", typeof(string));
            dt.Columns.Add("PhoneNumber", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("CitizenID", typeof(string));
            dt.Columns.Add("Nationality", typeof(string));
            dt.Columns.Add("EnrollmentDate", typeof(string));
            dt.Columns.Add("StartDate", typeof(string));
            dt.Columns.Add("EndDate", typeof(string));
            dt.Columns.Add("CourseName", typeof(string));
            dt.Columns.Add("AgeGroup", typeof(string));
            return dt;
        }
        #endregion

        #region Update IsPass
        public bool ConfirmPass(int learnerID)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = db.Learners.FirstOrDefault(l => l.LearnerID == learnerID);
                data.IsPass = true;
                db.SubmitChanges();
                return true;
            }
        }
        #endregion

        #region Get eligible learners
        public DataTable GetEligibleLearnersData()
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from enrollment in db.Enrollments
                           join course in db.Courses on enrollment.CourseID equals course.CourseID
                           join license in db.Licenses on course.LicenseID equals license.LicenseID
                           join learner in db.Learners on enrollment.LearnerID equals learner.LearnerID
                           where course.EndDate <= DateTime.Now
                                 && course.HoursStudied == course.DurationInHours
                                 && learner.IsPass == false
                           select new
                           {
                               learner.FullName,
                               learner.DateOfBirth,
                               learner.Gender,
                               learner.PhoneNumber,
                               learner.Email,
                               learner.Address,
                               learner.CitizenID,
                               learner.Nationality,
                               course.CourseName,
                               course.StartDate,
                               course.EndDate,
                               learner.Created_At,
                           };

                var dt = this.CreateDataTable();

                foreach (var item in data)
                {
                    dt.Rows.Add(item.FullName, item.DateOfBirth.Value.ToString("dd/MM/yyyy"), item.Gender,
                        item.PhoneNumber, item.Email, item.Address, item.CitizenID, item.Nationality,
                        item.Created_At.Value.ToString("dd/MM/yyyy"), 
                        item.StartDate.Value.ToString("dd/MM/yyyy"), item.EndDate.Value.ToString("dd/MM/yyyy"),
                        item.CourseName);
                }
                return dt;
            }
        }
        #endregion
    }
}
