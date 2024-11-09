using BLL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class DashBoardForm : Form
    {
        public DashBoardForm()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
        }

        private void DashBoardForm_Load(object sender, EventArgs e)
        {
            this.LoadAllData();

            this.LoadTotalLearners();
            this.LoadTotalCourse();
            //this.LoadTotalEarnings();
        }

        private void LoadTotalEarnings()
        {
            lblTotalEarnings_Value.Text = this.GetTotalEarnings();
        }

        private string GetTotalEarnings()
        {
            var earnings = CourseService.GetAllCourses();
            return earnings.Count.ToString();
        }

        private void LoadTotalCourse()
        {
            lblTotalCourses_Value.Text = this.GetTotalCourses();
        }

        private string GetTotalCourses()
        {
            var courses = CourseService.GetAllCourses();
            return courses.Count.ToString();
        }

        private void LoadTotalLearners()
        {
            lblTotalLearners_Value.Text = this.GetTotalLearners();
        }

        private string GetTotalLearners()
        {
            var learners = LearnerService.GetAllLearners();
            return learners.Count.ToString();
        }

        private void LoadAllData()
        {
            CourseService.LoadAllCourses(dgvCourses);
            TeacherService.LoadAllTeachers(dgvTeachers);
            VehicleService.LoadAllVehicles(dgvVehicles);
        }
    }
}
