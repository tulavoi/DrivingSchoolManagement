using BLL;
using BLL.Services;
using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GUI
{
    public partial class PaymentsForm : Form
    {
        #region Properties
        private string invoiceStatus = "Pending";
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
			// Gán ngày giờ hiện tại cho DateTimePicker
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
            if (cboMethods.SelectedIndex < 1)
            {
                FormHelper.ShowToolTip(cboMethods, toolTip, "Please select payment method.");
                return false;
            }
            return true;
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
                PaymentID = int.Parse(lblPaymentID.Text),
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

            FormHelper.SetLabelID(lblPaymentID, payment.PaymentID.ToString());
            //txtLearnerName.Text = payment.Invoice.Enrollment.Learner.FullName;
            //txtLearnerName.Text=payment.InvoiceID.ToString();
            //txtLearnerName.Text = payment.Invoice?.Enrollment?.Learner?.FullName ?? "N/A";
            txtLearnerName.Text = payment.Invoice?.Enrollment?.Learner?.FullName ?? payment.InvoiceID.ToString();
            txtInvoiceName.Text = payment.InvoiceID.ToString();
            txtAmount.Text = payment.Amount.ToString();
            dtpPaymentDate.Value = payment.PaymentDate ?? DateTime.Now;
            cboMethods.Text = payment.PaymentMethod;
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

			//if (!FormHelper.HasSelectedItem(cboLearners_Filter))
				//         {
				//             this.LoadAllPayments();
				//             return;
				//}
				//int selectedPaymentID = Convert.ToInt32(cboLearners_Filter.SelectedValue);
				//         PaymentService.FilterPaymentsByPaymentID(dgvPayments, selectedPaymentID);
				//         UpdateControlsWithSelectedRowData();
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
	}
}
