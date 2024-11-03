using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SessionDAL : BaseDAL<Session>
    {
        #region Properties
        private static SessionDAL instance;
        public static SessionDAL Instance
        {
            get
            {
                if (instance == null) instance = new SessionDAL();
                return instance;
            }
        }
        #endregion

        #region All teachers
        protected override IEnumerable<dynamic> QueryAllData()
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from session in db.Sessions
                           select new
                           {
                               session.SessionID,
                               session.Session1,
                               session.Created_At,
                               session.Updated_At,
                           };

                return data.ToList();
            }
        }

        public List<Session> GetAllSessions()
        {
            return GetAll(item => new Session
            {
                SessionID = item.SessionID,
                Session1 = item.Session1,
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

		#region Lấy ra các buổi học phù hợp với khóa học trong ngày
        public List<Session> GetAvalableSessionInDay(int courseID, DateTime curDate)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var scheduledSessions = (from sche in db.Schedules
                                        where sche.Enrollment.CourseID == courseID
                                        select new { sche.SessionID, sche.SessionDate }).Distinct();

                var availableSessions = from session in db.Sessions
										where !scheduledSessions.Any(sche => sche.SessionID == session.SessionID
														  && sche.SessionDate == curDate)
										select session;

                return availableSessions.ToList() ?? new List<Session>();
            }
        }
		#endregion
	}
}
