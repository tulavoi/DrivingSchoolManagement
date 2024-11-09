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

		// Hàm này được gọi khi form được tải, dùng để thiết lập các giá trị ban đầu
		private void AddPaymentForm_Load(object sender, EventArgs e)
		{
			dtpPaymentDate.Value = DateTime.Now;
			shadowForm.SetShadowForm(this);
			LoadComboBoxes();
		}

		// Hàm này dùng để tải danh sách hóa đơn vào combobox
		private void LoadComboBoxes()
		{
			string status = "Pending";
			ComboboxService.AssignInvoicesToCombobox(cboInvoices, status);   // Gán danh sách hóa đơn vào combobox
		}

		// Hàm này gán tên học viên vào Label
		private void AssignLearnerNameToTextBox(Payment payment)
		{
			if (payment?.Invoice?.Enrollment == null)
			{
				FormHelper.ShowNotify("This payment is not associated with an enrolled course, please choose another payment.");
				return;
			}

			lblName.Text = payment.Invoice.Enrollment.Learner.FullName;
			this.paymentID = payment.PaymentID;
		}

		// Hàm này xử lý sự kiện khi nhấn nút Add Payment
		private void btnAdd_Click(object sender, EventArgs e)
		{
			if (!ValidateFields()) return;

			Payment payment = GetPaymentData();

			if (payment == null || !payment.InvoiceID.HasValue || payment.InvoiceID.Value <= 0)
			{
				FormHelper.ShowNotify("Invalid payment or invoice ID.");
				return;
			}

			decimal remainingAmount = GetRemainingAmount(payment.InvoiceID.Value);
			if (payment.Amount > remainingAmount)
			{
				FormHelper.ShowNotify("The payment amount cannot exceed the remaining balance.");
				return;
			}

			bool result = PaymentService.AddPayment(payment);

			if (result)
			{
				FormHelper.ShowNotify("Payment added successfully.");
				this.Close();
			}
			else
			{
				FormHelper.ShowError("Failed to add payment.");
			}
		}

		// Hàm này tính toán số tiền nợ còn lại cho hóa đơn
		private decimal GetRemainingAmount(int invoiceID)
		{
			var invoice = InvoiceService.GetInvoice(invoiceID);
			if (invoice == null)
			{
				FormHelper.ShowNotify("Invoice not found.");
				return 0;
			}

			var payments = PaymentService.GetPaymentsByInvoiceID(invoiceID);
			decimal totalPaid = payments.Sum(p => p.Amount.GetValueOrDefault());

			decimal remainingAmount = (invoice.TotalAmount ?? 0) - totalPaid;

			return remainingAmount;
		}

		// Hàm này dùng để kiểm tra các trường hợp nhập liệu không hợp lệ
		private bool ValidateFields()
		{
			if (!PaymentValidator.CheckRequiredAndShowToolTip(txtAmount, toolTip)) return false;
			if (!decimal.TryParse(txtAmount.Text, out _))
			{
				toolTip.Show("Invalid amount", txtAmount);
				return false;
			}
			if (!PaymentValidator.ValidatePaymentMethod(cboMethods, toolTip)) return false;

			return true;
		}

		// Hàm này lấy dữ liệu Payment từ các điều khiển trên form
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

		// Hàm này xử lý sự kiện khi nhấn nút Cancel
		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		// Hàm này dùng để hiển thị tên học viên của hóa đơn đã chọn
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

		// Hàm này gán tên học viên vào label dựa trên hóa đơn đã chọn
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

		// Hàm này làm trống tên học viên khi không có hóa đơn được chọn
		private void ResetLearnerName()
		{
			lblName.Text = string.Empty;
			enrollmentID = 0;
		}

		// Hàm này điều chỉnh giao diện của form dựa trên việc có chọn hóa đơn hay không
		private void ConfigureForm(bool showDetails)
		{
			pnlInvoiceTo.Visible = showDetails;
			this.Width = 584;
			this.Height = showDetails ? 423 : 400;
			FormHelper.ApplyRoundedCorners(this, 20);
		}

		private void cboMethods_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Có thể thêm logic xử lý nếu cần
		}
	}
}
