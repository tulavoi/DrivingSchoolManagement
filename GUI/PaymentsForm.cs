using BLL.Services;
using DAL;
using GUI.Validators;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GUI
{
	public partial class PaymentsForm : Form
	{
		#region Properties
		private bool isEditing = false;
		private static PaymentsForm instance;
		public static PaymentsForm Instance
		{
			get
			{
				if (instance == null) instance = new PaymentsForm();
				return instance;
			}
		}
		#endregion

		public PaymentsForm()
		{
			InitializeComponent();
			FormHelper.ApplyRoundedCorners(this, 20);
		}

		private void PaymentsForm_Load(object sender, EventArgs e)
		{
			dtpPaymentDate.Value = DateTime.Now;
			this.LoadAllPayments();
			LoadComboboxes();
		}

		private void LoadComboboxes()
		{
			ComboboxService.AssignPaymentsToCombobox(cboLearners_Filter);
		}

		public void LoadAllPayments()
		{
			PaymentService.LoadAllPayments(dgvPayments);
			this.UpdateControlsWithSelectedRowData();
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (!this.InSaveMode())
			{
				this.ToggleEditMode();
				return;
			}

			if (!this.ValidateFields()) return;

			if (this.ConfirmAction($"Are you sure to edit payment for '{txtLearnerName.Text}'?"))
			{
				Payment payment = this.GetPayment();
				var result = PaymentService.EditPayment(payment);
				FormHelper.ShowActionResult(result, "Payment edited successfully.", "Failed to edit payment.");
			}
			this.ToggleEditMode();
			this.LoadAllPayments();
		}

		private bool ValidateFields()
		{
			if (!LearnerValidator.ValidateFullName(txtLearnerName, toolTip)) return false;
			if (!PaymentValidator.CheckRequiredAndShowToolTip(txtAmount, toolTip)) return false;

			int invoiceID = Convert.ToInt32(txtInvoiceName.Text);
			decimal paymentAmount = Convert.ToDecimal(txtAmount.Text);

			if (!ValidateTotalAmountForInvoice(invoiceID, paymentAmount, dgvPayments))
			{
				return false;
			}

			if (!PaymentValidator.ValidatePaymentDate(dtpPaymentDate, toolTip)) return false;
			if (!PaymentValidator.ValidatePaymentMethod(cboMethods, toolTip)) return false;

			return true;
		}

		private decimal GetTotalAmountForInvoice()
		{
			if (int.TryParse(txtInvoiceName.Text, out int invoiceID))
			{
				var invoice = InvoiceService.GetInvoice(invoiceID);
				return invoice?.TotalAmount ?? 0;
			}

			return 0;
		}

		public bool ValidateTotalAmountForInvoice(int invoiceID, decimal newPaymentAmount, DataGridView dgv)
		{
			var invoice = InvoiceService.GetInvoice(invoiceID);
			if (invoice == null)
			{
				FormHelper.ShowNotify("Invoice not found.");
				return false;
			}

			var payments = PaymentService.GetPaymentsByInvoiceID(invoiceID);
			var selectedPaymentID = dgv.SelectedRows.Count > 0 ? (dgv.SelectedRows[0].Tag as Payment)?.PaymentID : null;

			if (selectedPaymentID.HasValue)
			{
				payments = payments.Where(p => p.PaymentID != selectedPaymentID.Value).ToList();
			}

			decimal currentTotalAmount = payments.Sum(p => p.Amount.GetValueOrDefault());
			decimal invoiceTotalAmount = invoice.TotalAmount ?? 0;

			if (currentTotalAmount + newPaymentAmount > invoiceTotalAmount)
			{
				FormHelper.ShowNotify("The total payment amount exceeds the allowed invoice amount.");
				return false;
			}

			return true;
		}

		public decimal CalculateRemainingAmount(int invoiceID)
		{
			var invoice = InvoiceService.GetInvoice(invoiceID);
			if (invoice == null)
			{
				FormHelper.ShowNotify("Invoice not found.");
				return 0;
			}

			var payments = PaymentService.GetPaymentsByInvoiceID(invoiceID);
			decimal totalPayments = payments.Sum(p => p.Amount.GetValueOrDefault());
			decimal invoiceTotalAmount = invoice.TotalAmount ?? 0;

			return invoiceTotalAmount - totalPayments;
		}

		private void ToggleEditMode()
		{
			FormHelper.ToggleEditMode(ref this.isEditing, this.btnEdit, txtAmount, cboMethods, dtpPaymentDate);
		}

		private bool InSaveMode()
		{
			return btnEdit.Text == Constant.SAVE_MODE;
		}

		private bool ConfirmAction(string message)
		{
			DialogResult result = FormHelper.ShowConfirm(message);
			return result == DialogResult.Yes;
		}

		private Payment GetPayment()
		{
			return new Payment
			{
				PaymentID = Convert.ToInt32(FormHelper.GetObjectID(lblPaymentID.Text)),
				InvoiceID = int.Parse(txtInvoiceName.Text),
				PaymentDate = dtpPaymentDate.Value,
				Amount = int.Parse(txtAmount.Text),
				PaymentMethod = cboMethods.Text,
				Updated_At = DateTime.Now
			};
		}

		private void btnOpenAddPaymentForm_Click(object sender, EventArgs e)
		{
			FormHelper.OpenFormDialog(new AddPaymentForm());
			this.LoadAllPayments();
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			FormHelper.ClearDataGridViewRow(dgvPayments);
			string keyword = txtSearch.Text.ToLower();
			PaymentService.SearchPayments(dgvPayments, keyword);
			this.UpdateControlsWithSelectedRowData();
		}

		private void dgvPayments_SelectionChanged(object sender, EventArgs e)
		{
			this.UpdateControlsWithSelectedRowData();
		}

		private void UpdateControlsWithSelectedRowData()
		{
			var payment = this.GetSelectedPayment();
			this.AssignDataToControls(payment);
		}

		private Payment GetSelectedPayment()
		{
			if (!FormHelper.HasSelectedRow(dgvPayments)) return null;
			var selectedRow = dgvPayments.SelectedRows[0];
			return selectedRow.Tag as Payment;
		}

		private void AssignDataToControls(Payment payment)
		{
			if (payment == null) return;

			string paymentID = $"ID: {payment.PaymentID.ToString()}";
			FormHelper.SetLabelID(lblPaymentID, paymentID);
			txtLearnerName.Text = payment.Invoice?.Enrollment?.Learner?.FullName ?? payment.InvoiceID.ToString();
			txtInvoiceName.Text = payment.InvoiceID.ToString();
			txtAmount.Text = payment.Amount.ToString();
			dtpPaymentDate.Value = payment.PaymentDate ?? DateTime.Now;
			cboMethods.Text = payment.PaymentMethod;
			int invoiceID = payment.InvoiceID ?? 0;
			decimal remainingAmount = CalculateRemainingAmount(invoiceID);
			txtOwed.Text = remainingAmount.ToString();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (!FormHelper.HasSelectedRow(dgvPayments)) return;
			if (string.IsNullOrEmpty(lblPaymentID.Text)) return;

			if (this.ConfirmAction($"Are you sure to delete payment for '{txtLearnerName.Text}'?"))
			{
				var result = PaymentService.DeletePayment(int.Parse(lblPaymentID.Text));
				FormHelper.ShowActionResult(result, "Payment deleted successfully.", "Failed to delete payment.");
				this.LoadAllPayments();
			}
		}

		private void cboLearners_Filter_SelectedIndexChanged(object sender, EventArgs e)
		{
			FormHelper.ClearDataGridViewRow(dgvPayments);

			if (cboLearners_Filter.SelectedItem.ToString() == "Selected All")
			{
				LoadAllPayments();
			}
			else
			{
				int selectedPaymentID = ((KeyValuePair<string, int>)cboLearners_Filter.SelectedItem).Value;
				PaymentService.FilterPaymentsByInvoiceID(dgvPayments, selectedPaymentID);
				UpdateControlsWithSelectedRowData();
			}
		}

		private void dtpPaymentDate_Filter_ValueChanged(object sender, EventArgs e)
		{
			DateTime selectedPaymentDate = dtpPaymentDate_Filter.Value.Date;
			PaymentService.FilterPaymentsByDate(dgvPayments, selectedPaymentDate);
			this.UpdateControlsWithSelectedRowData();
		}

		private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
		{
			FormHelper.CheckNumericKeyPress(e);
		}
	}
}
