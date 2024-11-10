using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;

namespace BLL
{
	public class PaymentBLL
	{
		#region Properties
		private static PaymentBLL instance;

		public static PaymentBLL Instance
		{
			get
			{
				if (instance == null) instance = new PaymentBLL();
				return instance;
			}
		}
		#endregion

		public List<Payment> GetAllPayments()
		{
			return PaymentDAL.Instance.GetAllPayments();
		}

        public void FilterPaymentsByPaymentID(Guna2DataGridView dgv, int paymentID)
		{
			List<Payment> payments = PaymentDAL.Instance.GetAllPayments();
			var filteredPayments = payments.FindAll(p => p.PaymentID == paymentID);
			AddPaymentsToDataGridView(dgv, filteredPayments);
		}
		public void FilterPaymentsByInvoiceID(Guna2DataGridView dgv, int invoiceID)
		{
			List<Payment> payments = PaymentDAL.Instance.GetAllPayments();
			var filteredPayments = payments.FindAll(p => p.InvoiceID == invoiceID);
			AddPaymentsToDataGridView(dgv, filteredPayments);
		}
		// Trong lớp PaymentBLL
		// Trong lớp PaymentBLL
		public List<Payment> GetPaymentsByInvoiceID(int invoiceID)
		{
			// Lấy dữ liệu thông qua tầng DAL
			List<Payment> payments = PaymentDAL.Instance.GetPaymentsByInvoiceID(invoiceID);
			return payments;
		}


		public void AssignPaymentsToCombobox(Guna2ComboBox cbo)
		{
			List<Payment> payments = PaymentDAL.Instance.GetAllPayments();
			cbo.Items.Clear();
			cbo.Items.Add("Selected All");

			// Tạo một HashSet để lưu các InvoiceID đã thêm
			HashSet<int> addedInvoiceIDs = new HashSet<int>();

			foreach (var payment in payments)
			{
				int invoiceID = payment.InvoiceID ?? 0; // Sử dụng 0 nếu InvoiceID là null

				// Kiểm tra xem InvoiceID đã tồn tại trong HashSet chưa
				if (!addedInvoiceIDs.Contains(invoiceID))
				{
					// Nếu chưa tồn tại, thêm vào ComboBox và HashSet
					cbo.Items.Add(new KeyValuePair<string, int>(
						payment.Invoice.Enrollment.Learner.FullName,
						invoiceID
					));

					addedInvoiceIDs.Add(invoiceID);
				}
			}

			// Thiết lập DisplayMember và ValueMember cho ComboBox
			cbo.DisplayMember = "Key";
			cbo.ValueMember = "Value";
			cbo.SelectedIndex = 0;
		}


		public void LoadAllPayments(Guna2DataGridView dgv)
		{
			List<Payment> payments = PaymentDAL.Instance.GetAllPayments();
			AddPaymentsToDataGridView(dgv, payments);
		}

		public void SearchPayments(Guna2DataGridView dgv, string keyword)
		{
			List<Payment> payments = PaymentDAL.Instance.SearchPayments(keyword);
			AddPaymentsToDataGridView(dgv, payments);
		}

		public void FilterPaymentsByDate(Guna2DataGridView dgv, DateTime selectedDate)
		{
			List<Payment> filteredPayments = PaymentDAL.Instance.FilterPaymentsByDate(selectedDate);
			AddPaymentsToDataGridView(dgv, filteredPayments);
		}

		private void AddPaymentsToDataGridView(Guna2DataGridView dgv, List<Payment> payments)
		{
			dgv.Rows.Clear();

			foreach (var payment in payments)
			{
				int rowIndex = dgv.Rows.Add();

				if (rowIndex != -1 && rowIndex < dgv.Rows.Count)
				{
					dgv.Rows[rowIndex].Tag = payment;
					dgv.Rows[rowIndex].Cells["InvoiceCode"].Value = payment.Invoice.InvoiceCode;
					dgv.Rows[rowIndex].Cells["PaymentDate"].Value = payment.PaymentDate?.ToString("dd-MM-yyyy");
					dgv.Rows[rowIndex].Cells["InvoiceTo"].Value = payment.Invoice?.Enrollment?.Learner?.FullName ?? payment.InvoiceID.ToString();
					dgv.Rows[rowIndex].Cells["Amount"].Value = payment.Amount.ToString();
					dgv.Rows[rowIndex].Cells["Method"].Value = payment.PaymentMethod;
				}
			}
		}

		public bool AddPayment(Payment payment)
		{
			return PaymentDAL.Instance.AddPayment(payment);
		}

		public bool EditPayment(Payment payment)
		{
			return PaymentDAL.Instance.EditPayment(payment);
		}

		public bool DeletePayment(int paymentID)
		{
			return PaymentDAL.Instance.DeletePayment(paymentID);
		}

		public void AssignPaymentMethodsToComboBox(Guna2ComboBox cboMethods)
		{
			List<string> paymentMethods = PaymentDAL.Instance.GetPaymentMethods();
			cboMethods.Items.Clear();

			foreach (var method in paymentMethods)
			{
				cboMethods.Items.Add(method);
			}
		}
	}
}
