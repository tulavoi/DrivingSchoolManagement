using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
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

        public static void LoadAllSchedules(Guna2DataGridView dgv)
        {
            ScheduleBLL.Instance.LoadAllSchedules(dgv);
        }
    }
}
