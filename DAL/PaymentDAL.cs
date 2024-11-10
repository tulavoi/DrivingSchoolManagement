using System;
using System.Collections.Generic;
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
							   learner.FullName
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
                    Enrollment = new Enrollment
                    {
                        Learner = new Learner()
                        {
                            FullName = item.FullName,
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

	}
}
