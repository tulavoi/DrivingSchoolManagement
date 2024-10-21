using BLL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ComboboxService
    {
        public static void AssignLearnersToCombobox(Guna2ComboBox cbo)
        {
            LearnerBLL.Instance.AssignLearnersToCombobox(cbo);
        }

        public static void AssignCoursesToCombobox(Guna2ComboBox cbo)
        {
            CourseBLL.Instance.AssignCoursesToCombobox(cbo);
        }

        public static void AssignTeachersToCombobox(Guna2ComboBox cbo)
        {
            TeacherBLL.Instance.AssignTeachersToCombobox(cbo);
        }
        
        public static void AssignSessionsToCombobox(Guna2ComboBox cbo)
        {
            SessionBLL.Instance.AssignSessionsToCombobox(cbo);
        }
        
        public static void AssignVehiclesToCombobox(Guna2ComboBox cbo)
        {
            VehicleBLL.Instance.AssignVehiclesToCombobox(cbo);
        }

        public static void AssignLicensesToCombobox(Guna2ComboBox cbo)
        {
            LicenseBLL.Instance.AssignLicensesToCombobox(cbo);
        }
    }
}
