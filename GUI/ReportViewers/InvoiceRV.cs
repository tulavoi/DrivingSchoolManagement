using BLL.Services;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.ReportViewers
{
    public partial class InvoiceRV : Form
    {
        private string _invoiceCode;

        public InvoiceRV(string invoiceCode)
        {
            InitializeComponent();
            _invoiceCode = invoiceCode;
        }

        private void ReportViewer_Invoice_Load(object sender, EventArgs e)
        {
            shadowForm.SetShadowForm(this);
            FormHelper.ApplyRoundedCorners(this, 20);

            this.DisplayReport();
        }

        private void DisplayReport()
        {
            try
            {   
                var invoiceData = InvoiceService.GetInvoiceData(_invoiceCode);
                if (invoiceData.Rows.Count == 0)
                {
                    MessageBox.Show("data null");
                    return;
                }

                reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.Reports.InvoiceReport.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                // Tạo và thêm ReportDataSource
                ReportDataSource rds = new ReportDataSource("InvoiceDataSet", invoiceData);
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
