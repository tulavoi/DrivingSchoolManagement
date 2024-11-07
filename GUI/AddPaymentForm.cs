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
		public AddPaymentForm()
		{
			InitializeComponent();
			FormHelper.ApplyRoundedCorners(this, 20);
		}

		private void AddPaymentForm_Load(object sender, EventArgs e)
		{
			shadowForm.SetShadowForm(this);
			LoadComboBoxes();
		}

		private void LoadComboBoxes()
		{
			ComboboxService.AssignInvoicesToCombobox(cboInvoices);   // Gán danh sách hóa đơn vào combobox
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
	}
}

