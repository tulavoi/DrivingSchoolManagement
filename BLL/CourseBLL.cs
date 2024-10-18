using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CourseBLL
    {
        #region Properties
        private static CourseBLL instance;

        public static CourseBLL Instance
        {
            get
            {
                if (instance == null) instance = new CourseBLL();
                return instance;
            }
        }
        #endregion

        public void AssignCoursesToCombobox(Guna2ComboBox cbo)
        {
            List<Course> courses = CourseDAL.Instance.GetAllCourses();
            this.AddCoursesToCombobox(cbo, courses);
        }

        private void AddCoursesToCombobox(Guna2ComboBox cbo, List<Course> courses)
        {
            Course course = new Course();
            course.CourseName = "Select Course";
            courses.Insert(0, course);

            cbo.DataSource = courses;
            cbo.ValueMember = "CourseID";
            cbo.DisplayMember = "CourseName";
        }
    }
}
