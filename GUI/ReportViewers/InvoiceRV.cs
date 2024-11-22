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
            var invoiceData = InvoiceService.GetInvoiceData(_invoiceCode);
            FormHelper.LoadReport(reportViewer1, "InvoiceReport", invoiceData, "InvoiceDataSet");
        }
    }
}
