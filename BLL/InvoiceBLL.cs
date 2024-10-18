using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class InvoiceBLL
    {
        #region Properties
        private static InvoiceBLL instance;

        public static InvoiceBLL Instance
        {
            get
            {
                if (instance == null) instance = new InvoiceBLL();
                return instance;
            }
        }
        #endregion

        public void LoadAllInvoices(Guna2DataGridView dgv)
        {
            List<Invoice> invoices = InvoiceDAL.Instance.GetAllInvoices();
            this.AddInvoicesToDataGridView(dgv, invoices);
        }

        public void SearchInvoices(Guna2DataGridView dgv, string keyword)
        {
            List<Invoice> invoices = InvoiceDAL.Instance.SearchInvoices(keyword);
            this.AddInvoicesToDataGridView(dgv, invoices);
        }

        public void FilterInvoicesByStatus(Guna2DataGridView dgv, string status)
        {
            List<Invoice> invoices = InvoiceDAL.Instance.FilterInvoicesByStatus(status);
            this.AddInvoicesToDataGridView(dgv, invoices);
        }

        private void AddInvoicesToDataGridView(Guna2DataGridView dgv, List<Invoice> invoices)
        {
            dgv.Rows.Clear();
            foreach (var invoice in invoices)
            {
                int rowIndex = dgv.Rows.Add();

                if (rowIndex != -1 && rowIndex < dgv.Rows.Count)
                {
                    dgv.Rows[rowIndex].Tag = invoice;

                    dgv.Rows[rowIndex].Cells["InvoiceID"].Tag = invoice.InvoiceID;
                    dgv.Rows[rowIndex].Cells["InvoiceCode"].Value = invoice.InvoiceCode;
                    dgv.Rows[rowIndex].Cells["Status"].Value = invoice.Status;
                    dgv.Rows[rowIndex].Cells["FullName"].Value = invoice.Schedule.Learner.FullName;
                }
            }
        }

        public bool AddInvoice(Invoice invoice)
        {
            return InvoiceDAL.Instance.AddInvoice(invoice);
        }
    }
}
