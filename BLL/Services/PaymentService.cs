using BLL;
using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;

namespace BLL.Services
{
	public class PaymentService
	{
		// Phương thức thêm một Payment mới
		public static bool AddPayment(Payment payment)
		{
			return PaymentBLL.Instance.AddPayment(payment); // Gọi phương thức BLL để thêm Payment
		}

		// Phương thức chỉnh sửa thông tin Payment
		public static bool EditPayment(Payment payment)
		{
			return PaymentBLL.Instance.EditPayment(payment); // Gọi phương thức BLL để chỉnh sửa Payment
		}

		// Phương thức xóa Payment theo PaymentID
		public static bool DeletePayment(int paymentID)
		{
			return PaymentBLL.Instance.DeletePayment(paymentID); // Gọi phương thức BLL để xóa Payment
		}

		// Phương thức tải tất cả các Payment và hiển thị trên DataGridView
		public static void LoadAllPayments(Guna2DataGridView dgv)
		{
			PaymentBLL.Instance.LoadAllPayments(dgv); // Gọi phương thức BLL để tải tất cả Payments và hiển thị trên DataGridView
		}

		// Phương thức tìm kiếm Payment theo từ khóa
		public static void SearchPayments(Guna2DataGridView dgv, string keyword)
		{
			PaymentBLL.Instance.SearchPayments(dgv, keyword); // Gọi phương thức BLL để tìm kiếm Payment theo từ khóa
		}

		// Phương thức lọc Payment theo PaymentID
		public static void FilterPaymentsByPaymentID(Guna2DataGridView dgv, int paymentID)
		{
			PaymentBLL.Instance.FilterPaymentsByPaymentID(dgv, paymentID); // Gọi phương thức BLL để lọc Payment theo PaymentID
		}

		// Phương thức lọc Payment theo ngày thanh toán (ví dụ: DateTime)
		public static void FilterPaymentsByDate(Guna2DataGridView dgv, DateTime selectedDate)
		{
			PaymentBLL.Instance.FilterPaymentsByDate(dgv, selectedDate); // Gọi phương thức BLL để lọc Payment theo ngày
		}

		// Phương thức lọc Payment theo phương thức thanh toán (PaymentMethod)
		//public static void FilterPaymentsByMethod(Guna2DataGridView dgv, string paymentMethod)
		//{
		//	PaymentBLL.Instance.FilterPaymentsByMethod(dgv, paymentMethod); // Gọi phương thức BLL để lọc Payment theo phương thức thanh toán
		//}
	}
}
