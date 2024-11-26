using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Contexts;
using static System.Collections.Specialized.BitVector32;

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
                           join enroll in db.Enrollments on schedule.EnrollmentID equals enroll.EnrollmentID
						   join learner in db.Learners on enroll.LearnerID equals learner.LearnerID
                           join course in db.Courses on enroll.CourseID equals course.CourseID
                           join license in db.Licenses on course.LicenseID equals license.LicenseID
                           join teacher in db.Teachers on schedule.TeacherID equals teacher.TeacherID
                           join licenseOfTeacher in db.Licenses on teacher.LicenseID equals licenseOfTeacher.LicenseID
                           join vehicle in db.Vehicles on schedule.VehicleID equals vehicle.VehicleID
                           join session in db.Sessions on schedule.SessionID equals session.SessionID
                           where schedule.StatusID == 1
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
                               TeacherPhone = teacher.PhoneNumber,
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

        public List<Schedule> GetAllSchedulesActive()
        {
            return GetAll(item => this.MapToSchedule(item));
        }
        #endregion

        #region Filter
        protected override IEnumerable<dynamic> QueryDataByFilter(string filterString)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from schedule in db.Schedules
                           join enroll in db.Enrollments on schedule.EnrollmentID equals enroll.EnrollmentID
						   join learner in db.Learners on enroll.LearnerID equals learner.LearnerID
                           join course in db.Courses on enroll.CourseID equals course.CourseID
                           join teacher in db.Teachers on schedule.TeacherID equals teacher.TeacherID
                           join license in db.Licenses on course.LicenseID equals license.LicenseID
                           join licenseOfTeacher in db.Licenses on teacher.LicenseID equals licenseOfTeacher.LicenseID
                           join vehicle in db.Vehicles on schedule.VehicleID equals vehicle.VehicleID
                           join session in db.Sessions on schedule.SessionID equals session.SessionID
                           where session.Session1 == filterString
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
                               TeacherPhone = teacher.PhoneNumber,
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

        public List<Schedule> FilterScheduleBySession(string filterString)
        {
            return FilterData(filterString, item => this.MapToSchedule(item));
        }
        #endregion

        #region Search
        protected override IEnumerable<dynamic> QueryDataByKeyword(string keyword)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from schedule in db.Schedules
						   join enroll in db.Enrollments on schedule.EnrollmentID equals enroll.EnrollmentID
						   join learner in db.Learners on enroll.LearnerID equals learner.LearnerID
						   join course in db.Courses on enroll.CourseID equals course.CourseID
						   join license in db.Licenses on course.LicenseID equals license.LicenseID
                           join teacher in db.Teachers on schedule.TeacherID equals teacher.TeacherID
                           join licenseOfTeacher in db.Licenses on teacher.LicenseID equals licenseOfTeacher.LicenseID
                           join vehicle in db.Vehicles on schedule.VehicleID equals vehicle.VehicleID
                           join session in db.Sessions on schedule.SessionID equals session.SessionID
                           where (learner.FullName.Contains(keyword) ||
                                   teacher.FullName.Contains(keyword) ||
                                   vehicle.VehicleName.Contains(keyword) ||
                                   course.CourseName.Contains(keyword))
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
                               TeacherPhone = teacher.PhoneNumber,
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

        public List<Schedule> SearchSchedules(string keyword)
        {
            return SearchData(keyword, item => this.MapToSchedule(item));
        }

        #endregion

        #region Create
        public bool AddSchedule(Schedule schedule, out string errorMessage)
        {
            return AddData(schedule, out errorMessage);
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
            //return UpdateStatus(sche => sche.ScheduleID == scheduleID, 2); // StatusID = 2, StatusName = "Inactive"
            return DeleteData(sche => sche.ScheduleID == scheduleID);
        }
        #endregion

        #region Lấy ra schedule dựa vào courseID, learnerID nếu learnerID có giá trị
        public Schedule GetScheduleByCourseID(int courseID, int learnerID = 0)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from schedule in db.Schedules
                           join enroll in db.Enrollments on schedule.EnrollmentID equals enroll.EnrollmentID
						   join learner in db.Learners on enroll.LearnerID equals learner.LearnerID
                           join course in db.Courses on enroll.CourseID equals course.CourseID
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
                Enrollment = new Enrollment
                {
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
				}
            };
        }
        #endregion

        #region Map to Schedule
        private Schedule MapToSchedule(dynamic item)
        {
            return new Schedule
            {
                ScheduleID = item.ScheduleID,
                SessionDate = item.SessionDate,
                Enrollment = new Enrollment
                {
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
				},
                Teacher = new Teacher()
                {
                    TeacherID = item.TeacherID,
                    FullName = item.TeacherName,
                    Email = item.TeacherEmail,
					PhoneNumber = item.TeacherPhone,
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
            };
        }
        #endregion

        #region Lấy 1 lịch học dựa vào courseID
        public Schedule GetSchedule(int courseID)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var schedule = db.Schedules.Where(sch => sch.Enrollment.CourseID == courseID).FirstOrDefault();
                if (schedule == null) return null;
                return schedule;
            }
        }
        #endregion

        #region Data table Schedule trong 1 khoảng thời gian
        public DataTable GetScheduleDataByDate(DateTime startDate, DateTime endDate)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from s in db.Schedules
                            join e in db.Enrollments on s.EnrollmentID equals e.EnrollmentID
                            join l in db.Learners on e.LearnerID equals l.LearnerID
                            join t in db.Teachers on s.TeacherID equals t.TeacherID
                            join v in db.Vehicles on s.VehicleID equals v.VehicleID
                            join c in db.Courses on e.CourseID equals c.CourseID
                            join ses in db.Sessions on s.SessionID equals ses.SessionID
                            where s.SessionDate >= startDate.Date && s.SessionDate <= endDate.Date 
                                && s.StatusID == 1
                            select new
                            {
                                ScheduleID = s.ScheduleID,
                                LearnerFullName = l.FullName,
                                LearnerPhoneNumber = l.PhoneNumber,
                                LearnerEmail = l.Email,
                                TeacherFullName = t.FullName,
                                TeacherPhoneNumber = t.PhoneNumber,
                                TeacherEmail = t.Email,
                                VehicleName = v.VehicleName,
                                VehicleNumber = v.VehicleNumber,
                                CourseName = c.CourseName,
                                SessionName = ses.Session1,
                                SessionDate = s.SessionDate
                            };

                DataTable dt = this.CreateDataTable();

                foreach (var item in data)
                {
                    dt.Rows.Add(item.ScheduleID, item.LearnerFullName, item.LearnerPhoneNumber,
                        item.LearnerEmail, item.TeacherFullName, item.TeacherPhoneNumber, item.TeacherEmail,
                        item.VehicleName, item.VehicleNumber, item.CourseName, item.SessionName,
                        item.SessionDate.Value.ToString("dd/MM/yyyy"), startDate.ToString("dd/MM/yyyy"), 
                        endDate.ToString("dd/MM/yyyy"));
                }
                return dt;
            }
        }
        #endregion

        #region Create data table
        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ScheduleID", typeof(int));
            dt.Columns.Add("LearnerFullName", typeof(string));
            dt.Columns.Add("LearnerPhoneNumber", typeof(string));
            dt.Columns.Add("LearnerEmail", typeof(string));
            dt.Columns.Add("TeacherFullName", typeof(string));
            dt.Columns.Add("TeacherPhoneNumber", typeof(string));
            dt.Columns.Add("TeacherEmail", typeof(string));
            dt.Columns.Add("VehicleName", typeof(string));
            dt.Columns.Add("VehicleNumber", typeof(string));
            dt.Columns.Add("CourseName", typeof(string));
            dt.Columns.Add("SessionName", typeof(string));
            dt.Columns.Add("SessionDate", typeof(string));
            dt.Columns.Add("FromDate", typeof(string));
            dt.Columns.Add("ToDate", typeof(string));
            return dt;
        }
        #endregion

        #region Get schudule details data
        public DataTable GetScheduleDetailData(int scheduleID)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from s in db.Schedules
                           join e in db.Enrollments on s.EnrollmentID equals e.EnrollmentID
                           join l in db.Learners on e.LearnerID equals l.LearnerID
                           join t in db.Teachers on s.TeacherID equals t.TeacherID
                           join v in db.Vehicles on s.VehicleID equals v.VehicleID
                           join c in db.Courses on e.CourseID equals c.CourseID
                           join ses in db.Sessions on s.SessionID equals ses.SessionID
                           where s.ScheduleID == scheduleID && s.StatusID == 1
                           select new
                           {
                               ScheduleID = s.ScheduleID,
                               LearnerFullName = l.FullName,
                               LearnerPhoneNumber = l.PhoneNumber,
                               LearnerEmail = l.Email,
                               TeacherFullName = t.FullName,
                               TeacherPhoneNumber = t.PhoneNumber,
                               TeacherEmail = t.Email,
                               VehicleName = v.VehicleName,
                               VehicleNumber = v.VehicleNumber,
                               CourseName = c.CourseName,
                               SessionName = ses.Session1,
                               SessionDate = s.SessionDate
                           };

                DataTable dt = this.CreateDataTable();

                foreach (var item in data)
                {
                    dt.Rows.Add(item.ScheduleID, item.LearnerFullName, item.LearnerPhoneNumber,
                        item.LearnerEmail, item.TeacherFullName, item.TeacherPhoneNumber, item.TeacherEmail,
                        item.VehicleName, item.VehicleNumber, item.CourseName, item.SessionName,
                        item.SessionDate.Value.ToString("dd/MM/yyyy"));
                }
                return dt;
            }
        }
        #endregion

        #region Get schudule of leaner in day
        public DataTable GetScheduleInDayData(int learnerID, DateTime date)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from s in db.Schedules
                           join e in db.Enrollments on s.EnrollmentID equals e.EnrollmentID
                           join l in db.Learners on e.LearnerID equals l.LearnerID
                           join t in db.Teachers on s.TeacherID equals t.TeacherID
                           join v in db.Vehicles on s.VehicleID equals v.VehicleID
                           join c in db.Courses on e.CourseID equals c.CourseID
                           join ses in db.Sessions on s.SessionID equals ses.SessionID
                           where s.Enrollment.LearnerID == learnerID && s.StatusID == 1 
                                && s.SessionDate == date.Date
                           select new
                           {
                               ScheduleID = s.ScheduleID,
                               LearnerFullName = l.FullName,
                               LearnerPhoneNumber = l.PhoneNumber,
                               LearnerEmail = l.Email,
                               TeacherFullName = t.FullName,
                               TeacherPhoneNumber = t.PhoneNumber,
                               TeacherEmail = t.Email,
                               VehicleName = v.VehicleName,
                               VehicleNumber = v.VehicleNumber,
                               CourseName = c.CourseName,
                               SessionName = ses.Session1,
                               SessionDate = s.SessionDate
                           };

                DataTable dt = this.CreateDataTable();

                foreach (var item in data)
                {
                    dt.Rows.Add(item.ScheduleID, item.LearnerFullName, item.LearnerPhoneNumber,
                        item.LearnerEmail, item.TeacherFullName, item.TeacherPhoneNumber, item.TeacherEmail,
                        item.VehicleName, item.VehicleNumber, item.CourseName, item.SessionName,
                        item.SessionDate.Value.ToString("dd/MM/yyyy"));
                }
                return dt;
            }
        }
        #endregion

        #region Get shedules of teacher
        public DataTable GetSchedulesOfTeacher(int teacherID, DateTime startDate, DateTime endDate)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from s in db.Schedules
                           join e in db.Enrollments on s.EnrollmentID equals e.EnrollmentID
                           join l in db.Learners on e.LearnerID equals l.LearnerID
                           join t in db.Teachers on s.TeacherID equals t.TeacherID
                           join v in db.Vehicles on s.VehicleID equals v.VehicleID
                           join c in db.Courses on e.CourseID equals c.CourseID
                           join ses in db.Sessions on s.SessionID equals ses.SessionID
                           where s.TeacherID == teacherID && s.StatusID == 1
                                && s.SessionDate.Value.Date >= startDate.Date
                                && s.SessionDate.Value.Date <= endDate.Date
                           select new
                           {
                               ScheduleID = s.ScheduleID,
                               LearnerFullName = l.FullName,
                               LearnerPhoneNumber = l.PhoneNumber,
                               LearnerEmail = l.Email,
                               TeacherFullName = t.FullName,
                               TeacherPhoneNumber = t.PhoneNumber,
                               TeacherEmail = t.Email,
                               VehicleName = v.VehicleName,
                               VehicleNumber = v.VehicleNumber,
                               CourseName = c.CourseName,
                               SessionName = ses.Session1,
                               SessionDate = s.SessionDate,
                               StartDate = startDate,
                               EndDate = endDate,
                           };
                DataTable dt = this.CreateDataTable();

                foreach (var item in data)
                {
                    dt.Rows.Add(item.ScheduleID, item.LearnerFullName, item.LearnerPhoneNumber,
                        item.LearnerEmail, item.TeacherFullName, item.TeacherPhoneNumber, item.TeacherEmail,
                        item.VehicleName, item.VehicleNumber, item.CourseName, item.SessionName,
                        item.SessionDate.Value.ToString("dd/MM/yyyy"), item.StartDate.ToString("dd/MM/yyyy"),
                        item.EndDate.ToString("dd/MM/yyyy"));
                }
                return dt;
            }
        }
        #endregion
    }
}
