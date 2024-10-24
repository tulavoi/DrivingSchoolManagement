using DAL;
using Guna.UI2.WinForms;
using System;
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
        public void LoadAllCourses(Guna2DataGridView dgv)
        {
            List<Course> courses = CourseDAL.Instance.GetAllCourses();
            this.AddCoursesToDataGridView(dgv, courses);
        }

        public void SearchCourses(Guna2DataGridView dgv, string keyword)
        {
            List<Course> courses = CourseDAL.Instance.SearchCourses(keyword);
            this.AddCoursesToDataGridView(dgv, courses);
        }

        public void SearchCourses(Guna2ComboBox cbo, string keyword)
        {
            List<Course> courses = CourseDAL.Instance.SearchCourses(keyword);
            this.AddCoursesToCombobox(cbo, courses);
        }

        private void AddCoursesToDataGridView(Guna2DataGridView dgv, List<Course> courses)
        {
            dgv.Rows.Clear();
            foreach (var course in courses)
            {
                int rowIndex = dgv.Rows.Add();

                if (rowIndex != -1 && rowIndex < dgv.Rows.Count)
                {
                    dgv.Rows[rowIndex].Tag = course;
                    dgv.Rows[rowIndex].Cells["CourseName"].Value = course.CourseName;
                    dgv.Rows[rowIndex].Cells["Fee"].Value = course.Fee;
                    dgv.Rows[rowIndex].Cells["DurationInHours"].Value = course.DurationInHours;
                    dgv.Rows[rowIndex].Cells["LicenseType"].Value = course.License.LicenseName;
                }
            }
        }

        public bool AddCourse(Course course)
        {
            return CourseDAL.Instance.AddCourse(course);
        }

        public bool EditCourse(Course course)
        {
            return CourseDAL.Instance.EditCourse(course);
        }

        public bool DeleteCourse(int courseID)
        {
            return CourseDAL.Instance.DeleteCourse(courseID);
        }
    }
}
