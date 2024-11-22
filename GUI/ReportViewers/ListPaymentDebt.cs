using BLL.Services;
using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace GUI.ReportViewers
{
    public partial class ListPaymentDebt : Form
    {
        public ListPaymentDebt()
        {
            InitializeComponent();
        }

        private void ListPaymentDebt_Load(object sender, EventArgs e)
        {
            shadowForm.SetShadowForm(this);
            FormHelper.ApplyRoundedCorners(this, 20);

            // Gọi phương thức để lấy dữ liệu nợ và hiển thị báo cáo
            this.DisplayReport();
        }

        private void DisplayReport()
        {
            try
            {
                // Lấy dữ liệu các học viên có nợ từ dịch vụ
                var debtData = PaymentService.GetOutstandingPayments();
                if (debtData.Rows.Count == 0)
                {
                    MessageBox.Show("No data found");
                    return;
                }

                // Thiết lập báo cáo
                reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.Reports.ListPaymentDebt.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                // Tạo và thêm ReportDataSource
                ReportDataSource rds = new ReportDataSource("PaymentDataSet", debtData);
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Làm mới ReportViewer
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
