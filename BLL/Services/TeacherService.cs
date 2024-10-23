using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TeacherService
    {
        public static void LoadAllTeachers(Guna2DataGridView dgv)
        {
            TeacherBLL.Instance.LoadAllTeachers(dgv);
        }

        public static void SearchTeachers(Guna2DataGridView dgv, string keyword)
        {
            TeacherBLL.Instance.SearchTeachers(dgv, keyword);
        }

        public static bool AddTeacher(Teacher teacher)
        {
            return TeacherBLL.Instance.AddTeacher(teacher);
        }

        public static bool EditTeacher(Teacher teacher)
        {
            return TeacherBLL.Instance.EditTeacher(teacher);
        }

        public static bool DeleteTeacher(int teacherID)
        {
            return TeacherBLL.Instance.DeleteTeacher(teacherID);
        }
    }
}
