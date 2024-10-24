using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace DAL
{
    public class ScheduleDAL : BaseDAL<Schedule>
    {
        #region Properties
        private static ScheduleDAL instance;

        public static ScheduleDAL Instance
        {
            get
            {
                if (instance == null) instance = new ScheduleDAL();
                return instance;
            }
        }
        #endregion

        #region All Data
        protected override IEnumerable<dynamic> QueryAllData()
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from schedule in db.Schedules
                           join learner in db.Learners on schedule.LearnerID equals learner.LearnerID
                           join course in db.Courses on schedule.CourseID equals course.CourseID
                           join license in db.Licenses on course.LicenseID equals license.LicenseID
                           join teacher in db.Teachers on schedule.TeacherID equals teacher.TeacherID
                           join licenseOfTeacher in db.Licenses on teacher.LicenseID equals licenseOfTeacher.LicenseID
                           join vehicle in db.Vehicles on schedule.VehicleID equals vehicle.VehicleID
                           join session in db.Sessions on schedule.SessionID equals session.SessionID
                           select new
                           {
                               schedule.ScheduleID,
                               schedule.SessionDate,
                               schedule.Created_At,
                               schedule.Updated_At,

                               learner.LearnerID,
                               LearnerName = learner.FullName,
                               LearnerEmail = learner.Email,
                               LearnerPhone = learner.PhoneNumber,

                               course.CourseID,
                               course.CourseName,
                               license.LicenseID,
                               license.LicenseName,

                               teacher.TeacherID,
                               TeacherName = teacher.FullName,
                               TeacherEmail = teacher.Email,
                               TeacherPhone = teacher.Phone,
                               LicenseIDOfTeacher = licenseOfTeacher.LicenseID,
                               LicenseNameOfTeacher = licenseOfTeacher.LicenseName,
                               
                               vehicle.VehicleID,
                               vehicle.VehicleName,
                               vehicle.VehicleNumber,
                               vehicle.IsTruck,
                               vehicle.IsPassengerCar,
                               vehicle.IsMaintenance,

                               session.SessionID,
                               session.Session1,
                           };
                return data.ToList();
            }
        }

        public List<Schedule> GetAllSchedules()
        {
            return GetAll(item => new Schedule
            {
                ScheduleID = item.ScheduleID,
                SessionDate = item.SessionDate,
                Learner = new Learner()
                {
                    LearnerID = item.LearnerID,
                    FullName = item.LearnerName,
                    Email = item.LearnerEmail,
                    PhoneNumber = item.LearnerPhone,
                },
                Course = new Course()
                {
                    CourseID = item.CourseID,
                    CourseName = item.CourseName,
                    License = new License
                    {
                        LicenseID = item.LicenseID,
                        LicenseName = item.LicenseName,
                    }
                },
                Teacher = new Teacher()
                {
                    TeacherID = item.TeacherID,
                    FullName = item.TeacherName,
                    Email = item.TeacherEmail,
                    Phone = item.TeacherPhone,
                    License = new License
                    {
                        LicenseID = item.LicenseIDOfTeacher,
                        LicenseName = item.LicenseNameOfTeacher,
                    }
                },
                Vehicle = new Vehicle()
                {
                    VehicleID = item.VehicleID,
                    VehicleName = item.VehicleName,
                    VehicleNumber = item.VehicleNumber,
                },
                Session = new Session()
                {
                    SessionID = item.SessionID,
                    Session1 = item.Session1
                },
                Created_At = item.Created_At,
                Updated_At = item.Updated_At
            });
        }
        #endregion

        #region Filter
        protected override IEnumerable<dynamic> QueryDataByFilter(string filterString)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Search
        protected override IEnumerable<dynamic> QueryDataByKeyword(string keyword)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Lấy ra schedule dựa vào courseID, learnerID nếu learnerID có giá trị
        public Schedule GetScheduleByCourseID(int courseID, int learnerID = 0)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from schedule in db.Schedules
                           join learner in db.Learners on schedule.LearnerID equals learner.LearnerID
                           join course in db.Courses on schedule.CourseID equals course.CourseID
                           where course.CourseID == courseID && (learnerID <= 0 || learner.LearnerID == learnerID)
                           select new
                           {
                               schedule.ScheduleID,
                               learner.LearnerID,
                               learner.FullName,
                               course.CourseID,
                               course.CourseName
                           };

                var scheduleData = data.FirstOrDefault();
                if (scheduleData == null) return null;
                return this.CreateScheduleFromData(scheduleData);
            }
        }

        private Schedule CreateScheduleFromData(dynamic schedule)
        {
            return new Schedule
            {
                ScheduleID = schedule.ScheduleID,
                Learner = new Learner()
                {
                    LearnerID = schedule.LearnerID,
                    FullName = schedule.FullName,
                },
                Course = new Course()
                {
                    CourseID = schedule.CourseID,
                    CourseName = schedule.CourseName,
                }
            };
        }
        #endregion

        #region Create
        public bool AddSchedule(Schedule schedule, out string errorMessage)
        {
            return AddData(schedule, out errorMessage);
        }
        #endregion

        #region Lấy ra Schedule bằng LearnerID
        public List<Schedule> GetSchedulesByLearnerId(int learnerId)
        {
            using (DrivingSchoolDataContext db = DataAccess.GetDataContext())
            {
                return db.Schedules.Where(s => s.LearnerID == learnerId).ToList();
            }
        }
        #endregion

        #region Edit
        public bool EditSchedule(Schedule schedule, out string errorMessage)
        {
            return EditData(sche => sche.ScheduleID == schedule.ScheduleID,          
                            sche =>                                            
                            {
                                sche.TeacherID = schedule.TeacherID;
                                sche.VehicleID = schedule.VehicleID;
                                sche.SessionID = schedule.SessionID;
                                sche.SessionDate = schedule.SessionDate;
                                sche.Updated_At = DateTime.Now;
                            },
                            out errorMessage);
        }
        #endregion

        #region Delete
        public bool DeleteSchedule(int scheduleID)
        {
            return DeleteData(sche => sche.ScheduleID == scheduleID);
        }
        #endregion
    }
}
