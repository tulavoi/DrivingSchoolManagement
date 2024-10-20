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
    }
}
