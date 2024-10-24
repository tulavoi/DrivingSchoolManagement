using DAL;
using Guna.UI2.WinForms;
using System.Collections.Generic;

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

        public void SearchCourse(Guna2ComboBox cbo, string keyword)
        {
            List<Course> courses = CourseDAL.Instance.SearchCourse(keyword);
            this.AddCoursesToCombobox(cbo, courses);
        }
    }
}
