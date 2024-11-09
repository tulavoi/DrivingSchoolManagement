using Guna.UI2.WinForms;
using System;

namespace GUI.Validators
{
	public static class PaymentValidator
	{
		// Kiểm tra xem TextBox có được điền không, nếu không hiển thị tooltip
		public static bool CheckRequiredAndShowToolTip(Guna2TextBox txt, Guna2HtmlToolTip toolTip)
		{
			if (string.IsNullOrWhiteSpace(txt.Text))
			{
				FormHelper.ShowToolTip(txt, toolTip, $"Please enter {txt.Tag}");
				return false;
			}
			return true;
		}

		// Kiểm tra InvoiceID (bắt buộc phải điền)
		public static bool ValidateInvoiceID(Guna2TextBox txtInvoiceID, Guna2HtmlToolTip toolTip)
		{
			return CheckRequiredAndShowToolTip(txtInvoiceID, toolTip);
		}

		// Kiểm tra Payment Method (bắt buộc phải chọn)
		public static bool ValidatePaymentMethod(Guna2ComboBox cboMethod, Guna2HtmlToolTip toolTip)
		{
			if (cboMethod.SelectedIndex == 0)
			{
				FormHelper.ShowToolTip(cboMethod, toolTip, "Please select a payment method.");
				return false;
			}
			return true;
		}

		// Kiểm tra Payment Date (không được lớn hơn ngày hiện tại)
		public static bool ValidatePaymentDate(Guna2DateTimePicker dtpPaymentDate, Guna2HtmlToolTip toolTip)
		{
			if (dtpPaymentDate.Value.Date > DateTime.Now.Date)
			{
				FormHelper.ShowToolTip(dtpPaymentDate, toolTip, "Payment date cannot be in the future.");
				return false;
			}
			return true;
		}
		// Kiểm tra Payment Date và Amount (Amount không được lớn hơn TotalAmount của Invoice)
		public static bool ValidatePayment(Guna2DateTimePicker dtpPaymentDate, Guna2TextBox txtAmount, decimal totalAmount, Guna2HtmlToolTip toolTip)
		{
			// Kiểm tra Payment Date (không được lớn hơn ngày hiện tại)
			if (dtpPaymentDate.Value.Date > DateTime.Now.Date)
			{
				FormHelper.ShowToolTip(dtpPaymentDate, toolTip, "Payment date cannot be in the future.");
				return false;
			}

			// Kiểm tra Amount (không được lớn hơn TotalAmount)
			if (!int.TryParse(txtAmount.Text, out int amount))
			{
				FormHelper.ShowToolTip(txtAmount, toolTip, "Invalid amount format.");
				return false;
			}

			if (amount > totalAmount)
			{
				FormHelper.ShowToolTip(txtAmount, toolTip, $"Amount cannot exceed the total amount of {totalAmount:C}.");
				return false;
			}

			return true;
		}


		// Kiểm tra toàn bộ điều kiện của Payment trước khi cho phép thực hiện thêm thanh toán
		public static bool IsPaymentEligible(Guna2TextBox txtInvoiceID, Guna2TextBox txtAmount, Guna2ComboBox cboMethod, Guna2DateTimePicker dtpPaymentDate, Guna2HtmlToolTip toolTip)
		{
			bool isInvoiceIDValid = ValidateInvoiceID(txtInvoiceID, toolTip);
			bool isMethodValid = ValidatePaymentMethod(cboMethod, toolTip);
			bool isDateValid = ValidatePaymentDate(dtpPaymentDate, toolTip);

			return isInvoiceIDValid && isMethodValid && isDateValid;
		}
	}
}
