using BLL;
using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BLL.Services
{
    public class ComboboxService
    {
        public static void AssignLearnersToCombobox(Guna2ComboBox cbo)
        {
            LearnerBLL.Instance.AssignLearnersToCombobox(cbo);
        }

        public static void AssignLearnersToCombobox(Guna2ComboBox cbo, string status)
        {
            LearnerBLL.Instance.AssignLearnersToCombobox(cbo, status);
        }

        public static void AssignCoursesToCombobox(Guna2ComboBox cbo)
        {
            CourseBLL.Instance.AssignCoursesToCombobox(cbo);
        }

        public static void AssignCoursesToCombobox(Guna2ComboBox cbo, int learnerID)
        {
            CourseBLL.Instance.AssignCoursesToCombobox(cbo, learnerID);
        }

        public static void AssignTeachersToCombobox(Guna2ComboBox cbo)
        {
            TeacherBLL.Instance.AssignTeachersToCombobox(cbo);
        }

        public static void AssignTeachersToCombobox(Guna2ComboBox cbo, int courseID)
        {
            TeacherBLL.Instance.AssignTeachersToCombobox(cbo, courseID);
        }

        public static void AssignSessionsToCombobox(Guna2ComboBox cbo)
        {
            SessionBLL.Instance.AssignSessionsToCombobox(cbo);
        }
        
        public static void AssignVehiclesToCombobox(Guna2ComboBox cbo)
        {
            VehicleBLL.Instance.AssignVehiclesToCombobox(cbo);
        }

        public static void AssignVehiclesToCombobox(Guna2ComboBox cbo, int courseID)
        {
            VehicleBLL.Instance.AssignVehiclesToCombobox(cbo, courseID);
        }

        public static void AssignLicensesToCombobox(Guna2ComboBox cbo)
        {
            LicenseBLL.Instance.AssignLicensesToCombobox(cbo);
        }

        public static void AssignStatesToCombobox(Guna2ComboBox cbo)
        {
            StatusBLL.Instance.AssignCoursesToCombobox(cbo);
        }
    }
}
