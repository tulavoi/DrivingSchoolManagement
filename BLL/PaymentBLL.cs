using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;

namespace BLL
{
	public class PaymentBLL
	{
		#region Singleton
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

		public void FilterPaymentsByPaymentID(Guna2DataGridView dgv, int paymentID)
		{
			List<Payment> payments = PaymentDAL.Instance.GetAllPayments();
			var filteredPayments = payments.FindAll(p => p.PaymentID == paymentID);
			AddPaymentsToDataGridView(dgv, filteredPayments);
		}

		public void AssignPaymentsToCombobox(Guna2ComboBox cbo)
		{
			List<Payment> payments = PaymentDAL.Instance.GetAllPayments();
			cbo.Items.Clear();
			cbo.Items.Add("Selected All");
			foreach (var payment in payments)
			{
				cbo.Items.Add(new KeyValuePair<string, int>(payment.Invoice.Enrollment.Learner.FullName, payment.PaymentID));
			}
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
					dgv.Rows[rowIndex].Cells["InvoiceCode"].Value = payment.InvoiceID;
					dgv.Rows[rowIndex].Cells["PaymentDate"].Value = payment.PaymentDate?.ToString("yyyy-MM-dd");
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
