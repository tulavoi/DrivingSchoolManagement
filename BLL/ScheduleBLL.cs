using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guna.UI2.WinForms;
using System.Windows.Forms;

namespace BLL
{
    public class ScheduleBLL
    {
        #region Properties
        private static ScheduleBLL instance;

        public static ScheduleBLL Instance
        {
            get
            {
                if (instance == null) instance = new ScheduleBLL();
                return instance;
            }
        }
        #endregion

        public Schedule GetLearnerByCourseID(int courseID, int learnerID = 0)
        {
            return ScheduleDAL.Instance.GetScheduleByCourseID(courseID, learnerID);
        }

        public void LoadAllSchedules(Guna2DataGridView dgv)
        {
            List<Schedule> schedules = ScheduleDAL.Instance.GetAllSchedules();
            this.AddSchedulesToDataGridView(dgv, schedules);
        }

        public Schedule GetSchedule(int courseID)
        {
            return ScheduleDAL.Instance.GetSchedule(courseID);
        }

        public void SearchSchedules(Guna2DataGridView dgv, string keyword)
        {
            List<Schedule> schedules = ScheduleDAL.Instance.SearchSchedules(keyword);
            this.AddSchedulesToDataGridView(dgv, schedules);
        }

        public void FilterScheduleBySession(Guna2DataGridView dgv, string filterString)
        {
            List<Schedule> schedules = ScheduleDAL.Instance.FilterScheduleBySession(filterString);
            this.AddSchedulesToDataGridView(dgv, schedules);
        }

        public bool AddSchedule(Schedule schedule, out string errorMessage)
        {
            return ScheduleDAL.Instance.AddSchedule(schedule, out errorMessage);
        }

        public bool EditSchedule(Schedule schedule, out string errorMessage)
        {
            return ScheduleDAL.Instance.EditSchedule(schedule, out errorMessage);
        }

        public bool DeleteSchedule(int scheduleID)
        {
            return ScheduleDAL.Instance.DeleteSchedule(scheduleID);
        }

        private void AddSchedulesToDataGridView(Guna2DataGridView dgv, List<Schedule> schedules)
        {
            dgv.Rows.Clear();
            foreach (var schedule in schedules)
            {
                int rowIndex = dgv.Rows.Add();

                if (rowIndex != -1 && rowIndex < dgv.Rows.Count)
                {
                    dgv.Rows[rowIndex].Tag = schedule;

                    dgv.Rows[rowIndex].Cells["CourseName"].Value = schedule.Enrollment.Course.CourseName;
                    dgv.Rows[rowIndex].Cells["LearnerName"].Value = schedule.Enrollment.Learner.FullName;
                    dgv.Rows[rowIndex].Cells["TeacherName"].Value = schedule.Teacher.FullName;
                    dgv.Rows[rowIndex].Cells["VehicleName"].Value = schedule.Vehicle.VehicleName + " - " + schedule.Vehicle.VehicleNumber;
                    dgv.Rows[rowIndex].Cells["SessionDate"].Value = schedule.SessionDate.Value.ToString("dd-MM-yyyy");
                    dgv.Rows[rowIndex].Cells["Session"].Value = schedule.Session.Session1;
                }
            }
        }
    }
}
