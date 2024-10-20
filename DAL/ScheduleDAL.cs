﻿using System;
using System.Collections.Generic;
using System.Linq;

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
                           join teacher in db.Teachers on schedule.TeacherID equals teacher.TeacherID
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
                               course.CourseID,
                               course.CourseName,
                               teacher.TeacherID,
                               TeacherName = teacher.FullName,
                               vehicle.VehicleID,
                               vehicle.VehicleName,
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
                },
                Course = new Course()
                {
                    CourseID = item.CourseID,
                    CourseName = item.CourseName,
                },
                Teacher = new Teacher()
                {
                    TeacherID = item.TeacherID,
                    FullName = item.TeacherName,
                },
                Vehicle = new Vehicle()
                {
                    VehicleID = item.VehicleID,
                    VehicleName = item.VehicleName
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

        protected override IEnumerable<dynamic> QueryDataByFilter(string filterString)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<dynamic> QueryDataByKeyword(string keyword)
        {
            throw new NotImplementedException();
        }


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
    }
}
