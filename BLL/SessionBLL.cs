using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SessionBLL
    {
        #region Properties
        private static SessionBLL instance;

        public static SessionBLL Instance
        {
            get
            {
                if (instance == null) instance = new SessionBLL();
                return instance;
            }
        }
        #endregion

        public void AssignSessionsToCombobox(Guna2ComboBox cbo)
        {
            List<Session> sessions = SessionDAL.Instance.GetAllSessions();
            this.AddSessionsToCombobox(cbo, sessions);
        }

		public void AssignSessionsToCombobox(Guna2ComboBox cbo, int courseID, DateTime curDate)
		{
			List<Session> sessions = SessionDAL.Instance.GetAvalableSessionInDay(courseID, curDate);
			this.AddSessionsToCombobox(cbo, sessions);
		}

		public void AssignSessionsToCombobox(Guna2ComboBox cbo, int courseID, DateTime curDate, int sessionID)
		{
			List<Session> sessions = SessionDAL.Instance.GetAvalableSessionInDay(courseID, curDate);
			this.AddSessionsToCombobox(cbo, sessions, sessionID);
		}

		private void AddSessionsToCombobox(Guna2ComboBox cbo, List<Session> sessions, int sessionID)
		{
			Session session = new Session();
			session.Session1 = "Current Session";
            session.SessionID = sessionID;
			sessions.Insert(0, session);

			cbo.DataSource = sessions;
			cbo.ValueMember = "SessionID";
			cbo.DisplayMember = "Session1";
		}

		public List<Session> GetSessionsOfCourseInDay(int courseID, int selectedSessionID, DateTime curDate)
        {
            return SessionDAL.Instance.GetSessionsOfCourseInDay(courseID, selectedSessionID, curDate);
		}

		private void AddSessionsToCombobox(Guna2ComboBox cbo, List<Session> sessions)
        {
            Session session = new Session();
            session.Session1 = "Select Session";
            sessions.Insert(0, session);

            cbo.DataSource = sessions;
            cbo.ValueMember = "SessionID";
            cbo.DisplayMember = "Session1";
        }
    }
}
