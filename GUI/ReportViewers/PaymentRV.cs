using BLL.Services;
using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace GUI.ReportViewers
{
    public partial class PaymentRV : Form
    {
        private int _paymentID;

        public PaymentRV(int paymentID)
        {
            InitializeComponent();
            _paymentID = paymentID;
        }

        private void PaymentRV_Load(object sender, EventArgs e)
        {
            shadowForm.SetShadowForm(this);
            FormHelper.ApplyRoundedCorners(this, 20);

            this.DisplayReport();
        }

        private void DisplayReport()
        {
            try
            {
                // Lấy dữ liệu thanh toán từ dịch vụ
                var paymentData = PaymentService.GetPaymentData(_paymentID);
                if (paymentData.Rows.Count == 0)
                {
                    MessageBox.Show("Data is empty");
                    return;
                }

                // Thiết lập báo cáo
                reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.Reports.PaymentReport.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                // Tạo và thêm ReportDataSource
                ReportDataSource rds = new ReportDataSource("PaymentDataSet", paymentData);
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
