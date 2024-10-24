using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.SendEmail
{
    public class CourseService
    {
        public static void SearchCourse(Guna2ComboBox cbo, string keyword)
        {
            CourseBLL.Instance.SearchCourse(cbo, keyword);
        }
    }
}
