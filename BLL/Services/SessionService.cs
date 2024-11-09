using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class SessionService
	{
		public static List<Session> GetSessionsOfCourseInDay(int courseID, int selectedSessionID, DateTime curDate)
		{
			return SessionBLL.Instance.GetSessionsOfCourseInDay(courseID, selectedSessionID, curDate);
		}
	}
}
