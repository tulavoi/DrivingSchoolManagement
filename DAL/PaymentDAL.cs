using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DAL
{
	public class PaymentDAL : BaseDAL<Payment>
	{
		#region Properties
		private static PaymentDAL instance;

		public static PaymentDAL Instance
		{
			get
			{
				if (instance == null) instance = new PaymentDAL();
				return instance;
			}
		}
		#endregion

		#region All Payments
		protected override IEnumerable<dynamic> QueryAllData()
		{
			using (var db = DataAccess.GetDataContext())
			{
				var data = from payment in db.Payments
						   join invoice in db.Invoices on payment.InvoiceID equals invoice.InvoiceID
						   join enrollment in db.Enrollments on invoice.EnrollmentID equals enrollment.EnrollmentID
						   join learner in db.Learners on enrollment.LearnerID equals learner.LearnerID
						   select new
						   {
							   payment.PaymentID,
							   payment.InvoiceID,
							   invoice.InvoiceCode,
							   payment.PaymentDate,
							   payment.Amount,
							   payment.PaymentMethod,
							   payment.Created_At,
							   payment.Updated_At,
							   invoice.TotalAmount,
							   learner.FullName,
							   learner.PhoneNumber,
							   learner.Email
						   };
				return data.ToList();
			}
		}
		public List<Payment> GetAllPayments()
		{
			return GetAll(item => this.MapToPayment(item));
		}
		#endregion

		#region Map to payment
		private Payment MapToPayment(dynamic item)
		{
			return new Payment
			{

				PaymentID = item.PaymentID,
				InvoiceID = item.InvoiceID,
				PaymentDate = item.PaymentDate,
				Amount = item.Amount,
				PaymentMethod = item.PaymentMethod,
				Invoice = new Invoice
				{
					InvoiceID = item.InvoiceID,
					InvoiceCode = item.InvoiceCode,
					TotalAmount = item.TotalAmount,
					Enrollment = new Enrollment
					{
						Learner = new Learner()
						{
							FullName = item.FullName,
							PhoneNumber = item.PhoneNumber,
							Email = item.Email,
						}
					}
				},
				Created_At = item.Created_At,
				Updated_At = item.Updated_At

			};
		}
		#endregion

		#region Search
		protected override IEnumerable<dynamic> QueryDataByKeyword(string keyword)
		{
			using (var db = DataAccess.GetDataContext())
			{
				var data = from payment in db.Payments
						   join invoice in db.Invoices on payment.InvoiceID equals invoice.InvoiceID
						   join enrollment in db.Enrollments on invoice.EnrollmentID equals enrollment.EnrollmentID
						   join learner in db.Learners on enrollment.LearnerID equals learner.LearnerID
						   where payment.InvoiceID.ToString().Contains(keyword)
								 || payment.PaymentMethod.Contains(keyword)
								 || learner.FullName.Contains(keyword)
						   select new
						   {
							   payment.PaymentID,
							   payment.InvoiceID,
							   invoice.InvoiceCode,
							   payment.PaymentDate,
							   payment.Amount,
							   payment.PaymentMethod,
							   payment.Created_At,
							   payment.Updated_At,
							   learner.FullName
						   };
				return data.ToList();
			}
		}

		public List<Payment> SearchPayments(string keyword)
		{
			return SearchData(keyword, item => new Payment
			{
				PaymentID = item.PaymentID,
				InvoiceID = item.InvoiceID,
				PaymentDate = item.PaymentDate,
				Amount = item.Amount,
				PaymentMethod = item.PaymentMethod,
				Created_At = item.Created_At,
				Updated_At = item.Updated_At,
				Invoice = new Invoice
				{
					InvoiceID = item.InvoiceID,
					Enrollment = new Enrollment
					{
						Learner = new Learner
						{
							FullName = item.FullName
						}
					}
				}
			});
		}
		#endregion

		#region Filter by Date
		public List<Payment> FilterPaymentsByDate(DateTime paymentDate)
		{
			DateTime filterDate = paymentDate.Date;

			using (var db = DataAccess.GetDataContext())
			{
				var data = from payment in db.Payments
						   join invoice in db.Invoices on payment.InvoiceID equals invoice.InvoiceID
						   join enrollment in db.Enrollments on invoice.EnrollmentID equals enrollment.EnrollmentID
						   join learner in db.Learners on enrollment.LearnerID equals learner.LearnerID
						   where payment.PaymentDate.HasValue && payment.PaymentDate.Value.Date == filterDate
						   select new
						   {
							   payment.PaymentID,
							   payment.InvoiceID,
							   invoice.InvoiceCode,
							   payment.PaymentDate,
							   payment.Amount,
							   payment.PaymentMethod,
							   payment.Created_At,
							   payment.Updated_At,
							   learner.FullName
						   };
				return data.Select(item => MapToPayment(item)).ToList();
			}
		}
		#endregion

		#region Create
		public bool AddPayment(Payment payment)
		{
			return AddData(payment);
		}
		#endregion

		#region Edit
		public bool EditPayment(Payment payment)
		{
			return EditData(pay => pay.PaymentID == payment.PaymentID,
							pay =>
							{
								pay.InvoiceID = pay.InvoiceID;
								pay.PaymentDate = payment.PaymentDate;
								pay.Amount = payment.Amount;
								pay.PaymentMethod = payment.PaymentMethod;
								pay.Updated_At = DateTime.Now;
							});
		}
		#endregion

		#region Payment Methods
		// Lấy danh sách các phương thức thanh toán
		public List<string> GetPaymentMethods()
		{
			using (var db = DataAccess.GetDataContext())
			{
				return db.Payments.Select(p => p.PaymentMethod).Distinct().ToList();
			}
		}
		#endregion

		#region Delete
		public bool DeletePayment(int paymentID)
		{
			return DeleteData(pay => pay.PaymentID == paymentID);
		}

		protected override IEnumerable<dynamic> QueryDataByFilter(string filterString)
		{
			throw new NotImplementedException();
		}
		#endregion

		// Trong lớp PaymentDAL
		public List<Payment> GetPaymentsByInvoiceID(int invoiceID)
		{
			using (var db = DataAccess.GetDataContext())
			{
				// Truy vấn cơ sở dữ liệu với InvoiceID và trả về danh sách Payment tương ứng
				return db.Payments.Where(p => p.InvoiceID == invoiceID).ToList();
			}
		}

        #region Get data payment by InvoiceId
        public DataTable GetPaymentData(int paymentID)
        {
            using (var db = DataAccess.GetDataContext())
            {
                // Lấy dữ liệu thanh toán từ các bảng liên kết
                var data = from payment in db.Payments
                           join invoice in db.Invoices on payment.InvoiceID equals invoice.InvoiceID
                           join enroll in db.Enrollments on invoice.EnrollmentID equals enroll.EnrollmentID
                           join learner in db.Learners on enroll.LearnerID equals learner.LearnerID
                           join course in db.Courses on enroll.CourseID equals course.CourseID
                           where payment.PaymentID == paymentID
                           select new
                           {
                               payment.PaymentID,
                               invoice.InvoiceCode,
                               invoice.Created_At,
                               learner.FullName,
                               enroll.EnrollmentDate,
                               learner.Address,
                               learner.PhoneNumber,
                               learner.Email,
                               course.CourseName,
                               course.DurationInHours,
                               course.StartDate,
                               course.EndDate,
                               invoice.TotalAmount,
                               payment.Amount, // Số tiền thanh toán từ bảng Payments
                               invoice.Notes,
                               payment.PaymentDate,
                               payment.PaymentMethod
                           };

                // Tạo DataTable để chứa dữ liệu trả về
                DataTable dt = CreatePaymentDatatable();

                // Thêm các dòng dữ liệu vào DataTable
                foreach (var item in data)
                {
                    // Tính toán RemainingDebt (Số tiền còn nợ)
                    decimal remainingDebt = (item.TotalAmount ?? 0) - (item.Amount ?? 0);

                    // Thêm dữ liệu vào DataTable
                    dt.Rows.Add(
                        item.PaymentID,
                        item.InvoiceCode,
                        item.Created_At,
                        item.FullName,
                        item.EnrollmentDate,
                        item.Address,
                        item.PhoneNumber,
                        item.Email,
                        item.CourseName,
                        item.DurationInHours,
                        item.StartDate.Value.ToString("dd/MM/yyyy"),
                        item.EndDate.Value.ToString("dd/MM/yyyy"),
                        item.TotalAmount,
                        item.Amount, // Giá trị Amount từ bảng Payments
                        remainingDebt,
                        item.Notes,
                        item.PaymentDate,
                        item.PaymentMethod
                    );
                }

                return dt;
            }
        }
        public DataTable GetOutstandingPayments()
        {
            using (var db = DataAccess.GetDataContext())
            {
                // Lấy dữ liệu thanh toán từ các bảng liên kết và lọc ra các khoản nợ
                var data = from payment in db.Payments
                           join invoice in db.Invoices on payment.InvoiceID equals invoice.InvoiceID
                           join enroll in db.Enrollments on invoice.EnrollmentID equals enroll.EnrollmentID
                           join learner in db.Learners on enroll.LearnerID equals learner.LearnerID
                           join course in db.Courses on enroll.CourseID equals course.CourseID
                           where (invoice.TotalAmount ?? 0) > (payment.Amount ?? 0) // Điều kiện lọc nợ
                           orderby learner.FullName
                           select new
                           {
                               payment.PaymentID,
                               invoice.InvoiceCode,
                               invoice.Created_At,
                               learner.FullName,
                               enroll.EnrollmentDate,
                               learner.Address,
                               learner.PhoneNumber,
                               learner.Email,
                               course.CourseName,
                               course.DurationInHours,
                               course.StartDate,
                               course.EndDate,
                               invoice.TotalAmount,
                               payment.Amount, // Số tiền thanh toán từ bảng Payments
                               payment.PaymentDate,
                               payment.PaymentMethod,
                               RemainingDebt = (invoice.TotalAmount ?? 0) - (payment.Amount ?? 0) // Số tiền còn nợ
                           };

                // Tạo DataTable để chứa dữ liệu trả về
                DataTable dt = CreatePaymentDatatable();

                // Thêm các dòng dữ liệu vào DataTable
                foreach (var item in data)
                {
                    // Thêm dữ liệu vào DataTable, chuyển các trường DateTime thành string với định dạng "dd/MM/yyyy"
                    dt.Rows.Add(
                        item.PaymentID,
                        item.InvoiceCode,
                        item.Created_At.Value.ToString("dd/MM/yyyy"),  // Chuyển Created_At thành string
                        item.FullName,
                        item.EnrollmentDate.Value.ToString("dd/MM/yyyy"),  // Chuyển EnrollmentDate thành string
                        item.Address,
                        item.PhoneNumber,
                        item.Email,
                        item.CourseName,
                        item.DurationInHours,
                        item.StartDate.Value.ToString("dd/MM/yyyy"),  // Chuyển StartDate thành string
                        item.EndDate.Value.ToString("dd/MM/yyyy"),    // Chuyển EndDate thành string
                        item.TotalAmount,
                        item.Amount, // Giá trị Amount từ bảng Payments
                        item.RemainingDebt, // Số tiền còn nợ
                        item.PaymentDate?.ToString("dd/MM/yyyy"), // Chuyển PaymentDate thành string, có thể null
                        item.PaymentMethod
                    );
                }

                return dt;
            }
        }

        private DataTable CreatePaymentDatatable()
        {
            DataTable dt = new DataTable();

            // Các cột kiểu dữ liệu string, int, decimal
            dt.Columns.Add("PaymentID", typeof(string));      // Mã thanh toán
            dt.Columns.Add("InvoiceCode", typeof(string));    // Mã hóa đơn
            dt.Columns.Add("Created_At", typeof(string));     // Ngày tạo hóa đơn (chuyển thành string)
            dt.Columns.Add("FullName", typeof(string));       // Tên học viên
            dt.Columns.Add("EnrollmentDate", typeof(string)); // Ngày nhập học (chuyển thành string)
            dt.Columns.Add("Address", typeof(string));        // Địa chỉ
            dt.Columns.Add("PhoneNumber", typeof(string));    // Số điện thoại
            dt.Columns.Add("Email", typeof(string));          // Email
            dt.Columns.Add("CourseName", typeof(string));     // Tên khóa học
            dt.Columns.Add("DurationInHours", typeof(int));   // Số giờ khóa học
            dt.Columns.Add("StartDate", typeof(string));      // Ngày bắt đầu khóa học (chuyển thành string)
            dt.Columns.Add("EndDate", typeof(string));        // Ngày kết thúc khóa học (chuyển thành string)
            dt.Columns.Add("TotalAmount", typeof(decimal));   // Tổng số tiền hóa đơn
            dt.Columns.Add("Amount", typeof(decimal));        // Số tiền thanh toán (Amount từ bảng Payments)
            dt.Columns.Add("RemainingDebt", typeof(decimal)); // Số tiền còn nợ
            dt.Columns.Add("Notes", typeof(string));          // Ghi chú
            dt.Columns.Add("PaymentDate", typeof(string));    // Ngày thanh toán (chuyển thành string)
            dt.Columns.Add("PaymentMethod", typeof(string));  // Phương thức thanh toán

            return dt;
        }

        #endregion
    }
}
