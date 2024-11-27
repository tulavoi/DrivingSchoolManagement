using BLL.Services;
using DAL;
using Guna.Charts.WinForms;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.ReportViewers
{
    public partial class AllCoursesRV : Form
    {
        public AllCoursesRV()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
            shadowForm.SetShadowForm(this);
        }

        private void CourseListRV_Load(object sender, EventArgs e)
        {
            var allCourse = CourseService.GetAllCoursesData();
            var revenue = InvoiceService.GetRevenueByLicense();
            try
            {
                // Kiểm tra dữ liệu
                if (allCourse == null || allCourse.Rows.Count == 0)
                {
                    MessageBox.Show("No data available to display.");
                    return;
                }

                // Thiết lập ReportViewer
                reportViewer1.LocalReport.ReportEmbeddedResource = $"GUI.Reports.AllCoursesReport.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                // Tạo và thêm ReportDataSource
                ReportDataSource rds_AllCourse = new ReportDataSource("CourseDataSet", allCourse);
                ReportDataSource rds_Revenue = new ReportDataSource("RevenueDataSet", revenue);
                reportViewer1.LocalReport.DataSources.Add(rds_AllCourse);
                reportViewer1.LocalReport.DataSources.Add(rds_Revenue);

                // Làm mới ReportViewer
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            //FormHelper.LoadReport(reportViewer1, "AllCoursesReport", allCourse, "CourseDataSet");
        }
    }
}
