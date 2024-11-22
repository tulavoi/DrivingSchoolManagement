using BLL.Services;
using GUI.ReportViewers;
using System;
using System.Data;
using System.Linq;
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

            this.LoadStatisticInfo();

            this.btnLearnerChart_Click(sender, e);
        }

        private void LoadStatisticInfo()
        {
            this.LoadTotalEarnings();
            this.LoadTotalLearners();
            this.LoadTotalCourse();
        }

        private DataTable GetLearnerData()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Gender", typeof(string));
            dataTable.Columns.Add("Count", typeof(int));

            var learners = LearnerService.GetAllLearners();

            // Lấy ra giới tính và tổng số lượng của mỗi giới tính
            var genderCounts = learners
                               .GroupBy(l => l.Gender)
                               .Select(group => new { Gender = group.Key, Count = group.Count() });

            foreach (var genderCount in genderCounts)
                dataTable.Rows.Add(genderCount.Gender, genderCount.Count);

            return dataTable;
        }

        private void LoadTotalEarnings()
        {
            lblTotalEarnings_Value.Text = this.GetTotalEarnings();
        }

        private string GetTotalEarnings()
        {
            var payments = PaymentService.GetAllPayments();
            int? earnings = 0;

            foreach (var payment in payments)
                earnings += payment.Amount ?? 0;

            if (earnings.HasValue)
                return this.FormatEarning(earnings);

            return "0 VND";
        }

        private string FormatEarning(int? earnings)
        {
            double earningsValue = earnings.Value;

            // Chuyển đổi thành dạng rút gọn (ví dụ: 25M cho 25 triệu)
            if (earningsValue >= 1_000_000) // Nếu tổng lớn hơn hoặc bằng 1 triệu
                return (earningsValue / 1_000_000).ToString("0.#") + "M VND";

            else if (earningsValue >= 1_000) // Nếu tổng lớn hơn hoặc bằng 1 nghìn
                return (earningsValue / 1_000).ToString("0.#") + "K VND";

            else // Nếu tổng nhỏ hơn 1 nghìn
                return earningsValue.ToString("#,0") + " VND";
        }

        private void LoadTotalCourse()
        {
            lblTotalCourses_Value.Text = this.GetTotalCourses();
        }

        private string GetTotalCourses()
        {
            var courses = CourseService.GetAllCourses();

            int totalCourses = courses.Count * 1000;
            if (totalCourses >= 1000)
                return (totalCourses / 1000).ToString("0.#") + "K";

            return totalCourses.ToString();
        }

        private void LoadTotalLearners()
        {
            lblTotalLearners_Value.Text = this.GetTotalLearners();
        }

        private string GetTotalLearners()
        {
            var learners = LearnerService.GetAllLearners();

            int totalLearners = learners.Count * 1000;
            if (totalLearners >= 1000)
                return (totalLearners / 1000).ToString("0.#") + "K";

            return totalLearners.ToString();
        }


        private void LoadAllData()
        {
            CourseService.LoadAllCourses(dgvCourses);
            TeacherService.LoadAllTeachers(dgvTeachers);
            VehicleService.LoadAllVehicles(dgvVehicles);
        }

        private void btnLearnerChart_Click(object sender, EventArgs e)
        {
            CreatorChart.ChartPie(statisticChart, GetLearnerData(), "Learner Gender Ratio");
        }

        private void btnPaymentChart_Click(object sender, EventArgs e)
        {
            CreatorChart.ChartBar(statisticChart, GetPaymentData(), "Payment Revenue History");
        }

        private DataTable GetPaymentData()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("MonthYear", typeof(DateTime));
            dataTable.Columns.Add("TotalAmount", typeof(double));

            var payments = PaymentService.GetAllPayments();

            // Lấy ra tháng/năm và tổng tiền thanh toán trong 1 tháng/năm
            var paymentData = payments
                            .Where(p => p.PaymentDate.HasValue)
                            .GroupBy(p => new { Year = p.PaymentDate.Value.Year, Month = p.PaymentDate.Value.Month })
                            .Select(g => new
                            {
                                MonthYear = new DateTime(g.Key.Year, g.Key.Month, 1), // Định dạng tháng/năm là DateTime
                                TotalAmount = g.Sum(p => p.Amount)
                            })
                            .OrderBy(result => result.MonthYear)
                            .ToList();

            foreach (var payment in paymentData)
                dataTable.Rows.Add(payment.MonthYear, payment.TotalAmount);

            return dataTable;
        }

        private void btnChart3_Click(object sender, EventArgs e)
        {
            CreatorChart.ChartHorizontalBar(statisticChart, GetEnrollmentData(), "Number of Enrollments by License");
        }

        private DataTable GetEnrollmentData()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Lincense", typeof(string));
            dataTable.Columns.Add("Count", typeof(int));

            var enrollments = EnrollmentService.GetAllEnrollments();

            // Lấy ra tên bằng lái và số lượng của mỗi bằng lái
            var enrollmentsData = enrollments
                                    .Where(enr => enr.Course != null && enr.Course.License != null)
                                    .GroupBy(enr => enr.Course.License.LicenseName)
                                    .Select(gr => new
                                    {
                                        LicenseName = gr.Key,
                                        Count = gr.Count()
                                    })
                                    .OrderBy(result => result.LicenseName)
                                    .ToList();

            foreach (var enrollment in enrollmentsData)
                dataTable.Rows.Add(enrollment.LicenseName, enrollment.Count);

            return dataTable;
        }

        private void btnPrintTeachers_Click(object sender, EventArgs e)
        {
            TeacherListRV teacherListRV = new TeacherListRV();
            teacherListRV.Show();
        }

        private void btnPrintCourses_Click(object sender, EventArgs e)
        {
            CourseListRV courseListRV = new CourseListRV();
            courseListRV.Show();
        }
    }
}
