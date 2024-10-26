using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class StatusBLL
    {
        #region Properties
        private static StatusBLL instance;

        public static StatusBLL Instance
        {
            get
            {
                if (instance == null) instance = new StatusBLL();
                return instance;
            }
        }
        #endregion

        public void AssignCoursesToCombobox(Guna2ComboBox cbo)
        {
            List<Status> states = StatusDAL.Instance.GetAllStates();
            this.AddStatusToCombobox(cbo, states);
        }

        private void AddStatusToCombobox(Guna2ComboBox cbo, List<Status> states)
        {
            cbo.DataSource = states;
            cbo.ValueMember = "StatusID";
            cbo.DisplayMember = "StatusName";
        }
    }
}
