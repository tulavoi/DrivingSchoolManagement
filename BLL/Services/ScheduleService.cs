using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ScheduleService
    {
        public static bool AddSchedule(Schedule schedule, out string errorMessage)
        {
            return ScheduleBLL.Instance.AddSchedule(schedule, out errorMessage);
        }

        public static bool EditSchedule(Schedule schedule, out string errorMessage)
        {
            return ScheduleBLL.Instance.EditSchedule(schedule, out errorMessage);
        }

        public static bool DeleteSchedule(int scheduleID)
        {
            return ScheduleBLL.Instance.DeleteSchedule(scheduleID);
        }

        public static void LoadAllSchedules(Guna2DataGridView dgv)
        {
            ScheduleBLL.Instance.LoadAllSchedules(dgv);
        }

        public static void SearchSchedules(Guna2DataGridView dgv, string keyword)
        {
            ScheduleBLL.Instance.SearchSchedules(dgv, keyword);
        }

        public static void FilterScheduleBySession(Guna2DataGridView dgv, string filterString)
        {
            ScheduleBLL.Instance.FilterScheduleBySession(dgv, filterString);
        }

        public static Schedule GetSchedule(int courseID)
        {
            return ScheduleBLL.Instance.GetSchedule(courseID);
        }

        public static DataTable GetScheduleDataByDate(DateTime startDate, DateTime endDate)
        {
            return ScheduleBLL.Instance.GetScheduleDataByDate(startDate, endDate);
        }

        public static DataTable GetScheduleDetailData(int scheduleID)
        {
            return ScheduleBLL.Instance.GetScheduleDetailData(scheduleID);
        }

        public static DataTable GetScheduleInDayData(int learnerID, DateTime date)
        {
            return ScheduleBLL.Instance.GetScheduleInDayData(learnerID, date);
        }
    }
}
