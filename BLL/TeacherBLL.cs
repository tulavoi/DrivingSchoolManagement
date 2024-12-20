﻿using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TeacherBLL
    {
        #region Properties
        private static TeacherBLL instance;

        public static TeacherBLL Instance
        {
            get
            {
                if (instance == null) instance = new TeacherBLL();
                return instance;
            }
        }
        #endregion

        public void AssignTeachersToCombobox(Guna2ComboBox cbo)
        {
            List<Teacher> teachers = TeacherDAL.Instance.GetAllTeachers();
            this.AddTeachersToCombobox(cbo, teachers);
        }

        public void AssignTeachersToCombobox(Guna2ComboBox cbo, int courseID, int sessionID, DateTime curDate)
        {
            List<Teacher> teachers = TeacherDAL.Instance.GetTeacherForCourse(courseID, sessionID, curDate);
            this.AddTeachersToCombobox(cbo, teachers);
        }

		public void AssignTeacherInCourseToCombobox(Guna2ComboBox cbo, int courseID, int sessionID, DateTime curDate)
		{
			List<Teacher> teachers = TeacherDAL.Instance.GetTeacherForCourseAndInCourse(courseID, sessionID, curDate);
			this.AddTeachersToCombobox(cbo, teachers);
		}

		private void AddTeachersToCombobox(Guna2ComboBox cbo, List<Teacher> teachers)
        {
            Teacher teacher = new Teacher();
            teacher.FullName = "Select Teacher";
            teachers.Insert(0, teacher);

            cbo.DataSource = teachers;
            cbo.ValueMember = "TeacherID";
            cbo.DisplayMember = "FullName";
        }

        public void LoadAllTeachers(Guna2DataGridView dgv)
        {
            List<Teacher> teachers = TeacherDAL.Instance.GetAllTeachers();
            this.AddTeachersToDataGridView(dgv, teachers);
        }

        public void SearchTeachers(Guna2DataGridView dgv, string keyword)
        {
            List<Teacher> teachers = TeacherDAL.Instance.SearchTeachers(keyword);
            this.AddTeachersToDataGridView(dgv, teachers);
        }

        public void SearchTeachers(Guna2ComboBox cbo, string keyword)
        {
            List<Teacher> teachers = TeacherDAL.Instance.SearchTeachers(keyword);
            this.AddTeachersToCombobox(cbo, teachers);
        }

        public void FilterTeachersByStatus(Guna2DataGridView dgv, string status)
        {
            List<Teacher> teachers = TeacherDAL.Instance.FilterTeachersByStatus(status);
            this.AddTeachersToDataGridView(dgv, teachers);
        }

        private void AddTeachersToDataGridView(Guna2DataGridView dgv, List<Teacher> teachers)
        {
            dgv.Rows.Clear();
            foreach (var teacher in teachers)
            {
                int rowIndex = dgv.Rows.Add();

                if (rowIndex != -1 && rowIndex < dgv.Rows.Count)
                {
                    dgv.Rows[rowIndex].Tag = teacher;

                    if (dgv.Columns.Contains("TeacherID"))
                        dgv.Rows[rowIndex].Cells["TeacherID"].Tag = teacher.TeacherID;

                    dgv.Rows[rowIndex].Cells["FullName"].Value = teacher.FullName;
                    dgv.Rows[rowIndex].Cells["CitizenID"].Value = teacher.CitizenID;

                    if (dgv.Columns.Contains("License"))
                        dgv.Rows[rowIndex].Cells["License"].Value = teacher.License.LicenseName;

                    if (dgv.Columns.Contains("Status"))
                        dgv.Rows[rowIndex].Cells["Status"].Value = teacher.Status.StatusName;
                }
            }
        }

        public bool AddTeacher(Teacher teacher)
        {
            return TeacherDAL.Instance.AddTeacher(teacher);
        }

        public bool EditTeacher(Teacher teacher)
        {
            return TeacherDAL.Instance.EditTeacher(teacher);
        }

        public bool DeleteTeacher(int teacherID)
        {
            return TeacherDAL.Instance.DeleteTeacher(teacherID);
        }

        public DataTable GetTeachersDTO()
        {
            return TeacherDAL.Instance.GetTeachersDTO();
        }
    }
}
