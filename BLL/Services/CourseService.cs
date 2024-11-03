using BLL;
using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL.Services
{
    public class CourseService
    {

     
        public static void LoadAllCourses(Guna2DataGridView dgv)
        {
            CourseBLL.Instance.LoadAllCourses(dgv);
        }
       
        public static void SearchCourses(Guna2DataGridView dgv, string keyword)
        {
            CourseBLL.Instance.SearchCourses(dgv, keyword);
        }

        public static void SearchCourses(Guna2ComboBox cbo, string keyword, string status)
        {
            CourseBLL.Instance.SearchCourses(cbo, keyword, status);
        }

        public static void FilterLearnersByStatus(Guna2DataGridView dgv, string status)
        {
            CourseBLL.Instance.FilterCoursesByStatus(dgv, status);
        }

        public static bool AddCourse(Course course)
        {
            return CourseBLL.Instance.AddCourse(course);
        }

        public static bool EditCourse(Course course)
        {
            return CourseBLL.Instance.EditCourse(course);
        }

        public static bool DeleteCourse(int courseID)
        {
            return CourseBLL.Instance.DeleteCourse(courseID);
        }

        public static Course GetCourse(int courseID)
        {
            return CourseBLL.Instance.GetCourse(courseID);
        }

        public static void UpdateHoursStudied(int courseID, int hours)
        {
            CourseBLL.Instance.UpdateHoursStudied(courseID, hours);
        }
    }
}
