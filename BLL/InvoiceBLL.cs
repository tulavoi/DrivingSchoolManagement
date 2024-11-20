using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;

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
		public Invoice GetInvoice(int invoiceID)
		{
			return InvoiceDAL.Instance.GetInvoice(invoiceID);
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
                    dgv.Rows[rowIndex].Cells["Status"].Value = invoice.IsPaid == false ? "Pending" : "Paid";
                    dgv.Rows[rowIndex].Cells["FullName"].Value = invoice.Enrollment.Learner.FullName;
                }
            }
        }

        public bool AddInvoice(Invoice invoice)
        {
            return InvoiceDAL.Instance.AddInvoice(invoice);
        }

        public bool EditInvoice(Invoice invoice)
        {
            return InvoiceDAL.Instance.EditInvoice(invoice);
        }

        public bool DeleteInvoice(string invoiceCode) {
            return InvoiceDAL.Instance.DeleteInvoice(invoiceCode);
        }

		public void AssignInvoicesToCombobox(Guna2ComboBox cbo)
		{
			List<Invoice> invoices = InvoiceDAL.Instance.GetAllInvoices();
			this.AddInvoicesToCombobox(cbo, invoices);
		}
		public void AssignInvoicesToCombobox(Guna2ComboBox cbo, string status)
		{
			List<Invoice> invoices = InvoiceDAL.Instance.FilterInvoicesByStatus(status);
			this.AddInvoicesToCombobox(cbo, invoices);
		}
		private void AddInvoicesToCombobox(Guna2ComboBox cbo, List<Invoice> invoices)
		{
			// Tạo mục "Select Invoice" mặc định
			Invoice defaultInvoice = new Invoice
			{
				InvoiceID = 0, // hoặc null nếu `InvoiceID` là nullable
				InvoiceCode = "Select invoice"
			};

			// Tạo một danh sách mới và thêm mục mặc định vào đầu
			List<Invoice> updatedInvoices = new List<Invoice> { defaultInvoice };
			updatedInvoices.AddRange(invoices); // Thêm tất cả hóa đơn vào danh sách

			// Gán danh sách cập nhật làm DataSource
			cbo.DataSource = updatedInvoices;
			cbo.ValueMember = "InvoiceID";
			cbo.DisplayMember = "InvoiceCode";
			cbo.SelectedIndex = 0; // Đặt mục mặc định là mục đầu tiên
		}

        public DataTable GetInvoiceData(string invoiceCode)
        {
            return InvoiceDAL.Instance.GetInvoiceData(invoiceCode);
        }
    }
}
