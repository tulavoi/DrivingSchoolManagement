using BLL.Services;
using DAL;
using GUI.Validators;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
	public partial class AddPaymentForm : Form
	{
		private int? paymentID;
		private int? enrollmentID;
		public AddPaymentForm()
		{
			InitializeComponent();
			FormHelper.ApplyRoundedCorners(this, 20);
		}

		private void AddPaymentForm_Load(object sender, EventArgs e)
		{
			dtpPaymentDate.Value = DateTime.Now;
			shadowForm.SetShadowForm(this);
			LoadComboBoxes();
		}

		private void LoadComboBoxes()
		{
			string status = "Pending";
			ComboboxService.AssignInvoicesToCombobox(cboInvoices, status);   // Gán danh sách hóa đơn vào combobox
		}
		private void AssignLearnerNameToTextBox(Payment payment)
		{
			// Kiểm tra nếu Payment không có dữ liệu Enrollment
			if (payment?.Invoice?.Enrollment == null)
			{
				FormHelper.ShowNotify("This payment is not associated with an enrolled course, please choose another payment.");
				//this.ResetControls(cboPayments, txtLearnerName);
				return;
			}

			// Gán tên học viên vào TextBox và lưu trữ PaymentID
			lblName.Text = payment.Invoice.Enrollment.Learner.FullName;
			this.paymentID = payment.PaymentID;
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			if (!ValidateFields()) return;

			Payment payment = GetPaymentData();
			string errorMessage = "";
			var result = PaymentService.AddPayment(payment);

			if (result)
			{
				FormHelper.ShowNotify("Payment added successfully.");
				this.Close();
			}
			else
			{
				if (!string.IsNullOrEmpty(errorMessage))
					FormHelper.ShowError(errorMessage);
				else
					FormHelper.ShowError("Failed to add payment.");
			}
		}

		private bool ValidateFields()
		{
			if (!PaymentValidator.CheckRequiredAndShowToolTip(txtAmount, toolTip)) return false;
			// Validate số tiền
			if (!decimal.TryParse(txtAmount.Text, out _))
			{
				toolTip.Show("Invalid amount", txtAmount);
				return false;
			}

			return true;
		}

		private Payment GetPaymentData()
		{
			return new Payment
			{
				InvoiceID = Convert.ToInt32(cboInvoices.SelectedValue),
				Amount = int.Parse(txtAmount.Text),
				PaymentMethod = cboMethods.Text,
				PaymentDate = dtpPaymentDate.Value,
				Created_At = DateTime.Now
			};
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void LoadStudentName()
		{
			//if (cboInvoices.SelectedValue is int invoiceID)
			//{
			//	string studentName = ComboboxService.GetStudentNameByInvoiceID(invoiceID); // Giả sử hàm này trả về tên học viên
			//	.Text = studentName; // Hiển thị tên học viên
			//}
		}

		private void cboInvoices_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cboInvoices.SelectedIndex < 1)
			{
				ResetLearnerName();
				ConfigureForm(false);
				return;
			}

			ConfigureForm(true);
			AssignLearnerNameToLabel();
		}

		// Hiển thị tên học viên lên label dựa trên hóa đơn đã chọn
		private void AssignLearnerNameToLabel()
		{
			int invoiceID = Convert.ToInt32(cboInvoices.SelectedValue);
			var invoice = InvoiceService.GetInvoiceID(invoiceID);

			if (invoice?.Enrollment == null || invoice.Enrollment.Learner == null)
			{
				FormHelper.ShowNotify("No learner associated with this invoice. Please choose another invoice.");
				return;
			}

			lblName.Text = invoice.Enrollment.Learner.FullName;
			this.enrollmentID = invoice.EnrollmentID;
		}

		// Đặt lại tên học viên và làm trống các điều khiển liên quan
		private void ResetLearnerName()
		{
			lblName.Text = string.Empty;
			enrollmentID = 0;
		}

		// Điều chỉnh hiển thị của form dựa trên việc có chọn hóa đơn hay không
		private void ConfigureForm(bool showDetails)
		{
			pnlInvoiceTo.Visible = showDetails;
			this.Width = 584;
			this.Height = showDetails ? 423 : 400;
			FormHelper.ApplyRoundedCorners(this, 20);
		}

		//584, 423
		private void cboMethods_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}

