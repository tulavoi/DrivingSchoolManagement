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
    }
}
