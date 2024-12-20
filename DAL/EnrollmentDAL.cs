﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;

namespace DAL
{
    public class EnrollmentDAL : BaseDAL<Enrollment>
    {
        #region Properties
        private static EnrollmentDAL instance;
        public static EnrollmentDAL Instance
        {
            get
            {
                if (instance == null) instance = new EnrollmentDAL();
                return instance;
            }
        }
        #endregion

        #region Get all
        protected override IEnumerable<dynamic> QueryAllData()
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from enr in db.Enrollments
                           join course in db.Courses on enr.CourseID equals course.CourseID
                           join license in db.Licenses on course.LicenseID equals license.LicenseID
                           join learner in db.Learners on enr.LearnerID equals learner.LearnerID
                           select new
                           {
                               enr.EnrollmentID,
                               course.CourseID,
                               license.LicenseID,
                               license.LicenseName,
                               learner.LearnerID,
                               enr.EnrollmentDate,
                               enr.IsComplete
                           };
                return data.ToList();
            }
        }

        public List<Enrollment> GetAllEnrollments()
        {
            return GetAll(item => this.MapToEnrollment(item));
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

        #region Map to enrollment
        private Enrollment MapToEnrollment(dynamic item)
        {
            return new Enrollment
            {
                EnrollmentID = item.EnrollmentID,
                Course = new Course
                {
                    CourseID = item.CourseID,
                    License = new License
                    {
                        LicenseID = item.LicenseID,
                        LicenseName = item.LicenseName,
                    }
                },
                Learner = new Learner
                {
                    LearnerID = item.LearnerID,
                },
                EnrollmentDate = item.EnrollmentDate,
                IsComplete = item.IsComplete
            };
        }
        #endregion

        #region Create
        public bool AddEnrollment(int learnerID, int courseID)
        {
            var enrollment = new Enrollment
            {
                LearnerID = learnerID,
                CourseID = courseID,
                EnrollmentDate = DateTime.Now,
                IsComplete = false
            };
            return AddData(enrollment);
        }
        #endregion

        #region Edit
        public bool EditEnrollment(Learner learner, int courseID)
        {
            return EditData(e => e.LearnerID == learner.LearnerID,
                            e =>
                            {
                                e.CourseID = courseID;
                                e.LearnerID = learner.LearnerID;
                            });
        }
        #endregion

        #region Lấy ra enrollment dựa vào learnerID
        public Enrollment GetEnrollmentByLearnerID(int learnerId)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Enrollment>(e => e.Course);
                db.LoadOptions = loadOptions;

                var enrollment = db.Enrollments.Where(en => en.LearnerID == learnerId).FirstOrDefault();
                if (enrollment == null) return null;
                return enrollment;
            }
        }
        #endregion

        #region Lấy ra enrollment dựa vào courseID
        public Enrollment GetEnrollmentByCourseID(int courseID)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Enrollment>(e => e.Learner);
                db.LoadOptions = loadOptions;

                var enrollment = db.Enrollments.Where(en => en.CourseID == courseID).FirstOrDefault();
                if (enrollment == null) return null;
                return enrollment;
            }
        }
        #endregion
    }
}
