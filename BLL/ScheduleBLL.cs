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
    }
}
