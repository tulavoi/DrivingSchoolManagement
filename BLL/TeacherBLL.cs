using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
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

        private void AddTeachersToDataGridView(Guna2DataGridView dgv, List<Teacher> teachers)
        {
            dgv.Rows.Clear();
            foreach (var teacher in teachers)
            {
                int rowIndex = dgv.Rows.Add();

                if (rowIndex != -1 && rowIndex < dgv.Rows.Count)
                {
                    dgv.Rows[rowIndex].Tag = teacher;

                    dgv.Rows[rowIndex].Cells["TeacherID"].Tag = teacher.TeacherID;
                    dgv.Rows[rowIndex].Cells["FullName"].Value = teacher.FullName;
                    dgv.Rows[rowIndex].Cells["CitizenID"].Value = teacher.CitizenID;
                    dgv.Rows[rowIndex].Cells["EmploymentDate"].Value = teacher.EmploymentDate.Value.Year;
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
    }
}
