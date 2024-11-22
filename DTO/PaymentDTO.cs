using System;

namespace DTO
{
    public class PaymentDTO
    {
        // Mã thanh toán
        public string PaymentID { get; set; }

        // Mã hóa đơn
        public string InvoiceCode { get; set; }

        // Ngày tạo hóa đơn (chuyển từ DateTime sang string)
        public string Created_At { get; set; }

        // Thông tin học viên
        public string FullName { get; set; }
        public string EnrollmentDate { get; set; }  // Chuyển từ DateTime sang string
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        // Thông tin khóa học
        public string CourseName { get; set; }
        public int DurationInHours { get; set; }
        public string StartDate { get; set; }  // Chuyển từ DateTime sang string
        public string EndDate { get; set; }    // Chuyển từ DateTime sang string

        // Tổng số tiền của hóa đơn
        public decimal? TotalAmount { get; set; }

        // Số tiền đã thanh toán
        public decimal TotalPaid { get; set; }

        // Số tiền còn nợ, tính tự động dựa trên TotalAmount và TotalPaid
        public decimal RemainingDebt => (TotalAmount ?? 0) - TotalPaid;

        // Ghi chú cho hóa đơn
        public string Notes { get; set; }

        // Chi tiết thanh toán (tích hợp trong lớp PaymentDTO)
        public string PaymentDate { get; set; }  // Chuyển từ DateTime sang string
        public decimal? PaymentAmount { get; set; }
        public string PaymentMethod { get; set; }

        // Số tiền thanh toán trong giao dịch cụ thể
        public decimal Amount { get; set; }
    }
}
