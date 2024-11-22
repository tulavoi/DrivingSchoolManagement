using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;

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

        public void AssignCoursesToCombobox(Guna2ComboBox cbo, string status)
        {
            List<Course> courses = CourseDAL.Instance.GetCoursesInvoiced(status);
            this.AddCoursesToCombobox(cbo, courses);
        }

        public void AssignCoursesToCombobox(Guna2ComboBox cbo, string status, DateTime curDate)
        {
            List<Course> courses = CourseDAL.Instance.GetCourseEnrolled(status, curDate);
            this.AddCoursesToCombobox(cbo, courses);
        }

        public void AssignAvailableCourseToCombobox(Guna2ComboBox cbo)
        {
            List<Course> courses = CourseDAL.Instance.GetAvailableCourses();
            this.AddCoursesToCombobox(cbo, courses);
        }

        public void GetAvailableAndLearnerCourses(Guna2ComboBox cbo, int learnerID)
        {
            List<Course> courses = CourseDAL.Instance.GetAvailableAndLearnerCourses(learnerID);
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

        public List<Course> GetAllCourses()
        {
            return CourseDAL.Instance.GetAllCourses();
        }

        public void SearchCourses(Guna2DataGridView dgv, string keyword)
        {
            List<Course> courses = CourseDAL.Instance.SearchCourses(keyword);
            this.AddCoursesToDataGridView(dgv, courses);
        }

        public void SearchCourses(Guna2ComboBox cbo, string keyword, string status)
        {
            List<Course> courses = CourseDAL.Instance.SearchCourses(keyword, status);
            this.AddCoursesToCombobox(cbo, courses);
        }

        public void FilterCoursesByStatus(Guna2DataGridView dgv, string status)
        {
            List<Course> course = CourseDAL.Instance.FilterCoursesByStatus(status);
            this.AddCoursesToDataGridView(dgv, course);
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
                    dgv.Rows[rowIndex].Cells["Fee"].Value = course.Fee + " VND";
                    if (dgv.Columns.Contains("LicenseType"))
                        dgv.Rows[rowIndex].Cells["LicenseType"].Value = course.License.LicenseName;

                    if (dgv.Columns.Contains("Status"))
                        dgv.Rows[rowIndex].Cells["Status"].Value = course.Status.StatusName;

                    if (dgv.Columns.Contains("StartDate"))
                        dgv.Rows[rowIndex].Cells["StartDate"].Value = course.StartDate.Value.ToString("dd/MM/yyyy");
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

        public Course GetCourse(int courseID)
        {
            return CourseDAL.Instance.GetCourse(courseID);
        }

        public void UpdateHoursStudied(int courseID, int hours)
        {
            CourseDAL.Instance.UpdateHoursStudied(courseID, hours);
        }

        public DataTable GetAllCoursesData()
        {
            return CourseDAL.Instance.GetAllCoursesData();
        }
    }
}
