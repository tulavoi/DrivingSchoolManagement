using DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;

namespace DAL
{
    public class InvoiceDAL : BaseDAL<Invoice>
    {
        #region Properties
        private static InvoiceDAL instance;

        public static InvoiceDAL Instance
        {
            get
            {
                if (instance == null) instance = new InvoiceDAL();
                return instance;
            }
        }
        #endregion

        #region All Invoices
        protected override IEnumerable<dynamic> QueryAllData()
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from invoice in db.Invoices
                           join enroll in db.Enrollments on invoice.EnrollmentID equals enroll.EnrollmentID
                           join learner in db.Learners on enroll.LearnerID equals learner.LearnerID
                           join course in db.Courses on enroll.CourseID equals course.CourseID
                           join status in db.Status on invoice.StatusID equals status.StatusID
                           orderby invoice.InvoiceID descending
                           select new
                           {
                               invoice.InvoiceID,
                               invoice.InvoiceCode,
                               learner.LearnerID,
                               learner.FullName,
                               learner.Email,
                               learner.PhoneNumber,
                               course.CourseID,
                               course.CourseName,
                               invoice.TotalAmount,
                               invoice.Notes,
                               status.StatusID,
                               status.StatusName,
                               invoice.IsPaid,
                               invoice.Created_At,
                               invoice.Updated_At
                           };
                return data.ToList();
            }
        }

        public List<Invoice> GetAllInvoices()
        {
            return GetAll(item => this.MapToInvoice(item));
        }
        #endregion

        #region Search
        protected override IEnumerable<dynamic> QueryDataByKeyword(string keyword)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from invoice in db.Invoices
						   join enroll in db.Enrollments on invoice.EnrollmentID equals enroll.EnrollmentID
						   join learner in db.Learners on enroll.LearnerID equals learner.LearnerID
						   join course in db.Courses on enroll.CourseID equals course.CourseID
						   join status in db.Status on invoice.StatusID equals status.StatusID
						   where (invoice.InvoiceCode.Contains(keyword) || learner.FullName.Contains(keyword))
						   orderby invoice.InvoiceID descending
                           select new
                           {
                               invoice.InvoiceID,
                               invoice.InvoiceCode,
                               learner.LearnerID,
                               learner.FullName,
                               learner.Email,
                               learner.PhoneNumber,
                               course.CourseID,
                               course.CourseName,
                               invoice.TotalAmount,
                               invoice.Notes,
                               status.StatusID,
							   status.StatusName,
                               invoice.IsPaid,
                               invoice.Created_At,
                               invoice.Updated_At
                           };
                return data.ToList();
            }
        }

        public List<Invoice> SearchInvoices(string keyword)
        {
            return SearchData(keyword, item => this.MapToInvoice(item));
        }
        #endregion
         
        #region Filter by status
        protected override IEnumerable<dynamic> QueryDataByFilter(string filterString)
        {
            using (var db = DataAccess.GetDataContext())
            {
                bool isPaid = false;
                if (filterString == "Paid")
                    isPaid = true;
                if (filterString == "Pending")
                    isPaid = false;

                var data = from invoice in db.Invoices
						   join enroll in db.Enrollments on invoice.EnrollmentID equals enroll.EnrollmentID
						   join learner in db.Learners on enroll.LearnerID equals learner.LearnerID
						   join course in db.Courses on enroll.CourseID equals course.CourseID
						   join status in db.Status on invoice.StatusID equals status.StatusID
						   where invoice.IsPaid == isPaid
						   orderby invoice.InvoiceID descending
                           select new
                           {
                               invoice.InvoiceID,
                               invoice.InvoiceCode,
                               learner.LearnerID,
                               learner.FullName,
                               learner.Email,
                               learner.PhoneNumber,
                               course.CourseID,
                               course.CourseName,
                               invoice.TotalAmount,
                               invoice.Notes,
                               status.StatusID,
							   status.StatusName,
                               invoice.IsPaid,
                               invoice.Created_At,
                               invoice.Updated_At
                           };
                return data.ToList();
            }
        }

        public List<Invoice> FilterInvoicesByStatus(string status)
        {
            return FilterData(status, item => this.MapToInvoice(item));
        }
        #endregion

        #region Create
        public bool AddInvoice(Invoice invoice)
        {
            return AddData(invoice);
        }
        #endregion

        #region Edit
        public bool EditInvoice(Invoice invoice)
        {
            return EditData(inv => inv.InvoiceCode == invoice.InvoiceCode,      // Điều kiện tìm invoice theo code
                            inv =>                                              // Action cập nhật các thuộc tính
                            {
                                inv.TotalAmount = invoice.TotalAmount;
                                inv.Notes = invoice.Notes;
                                inv.StatusID = invoice.StatusID;
                                inv.IsPaid = invoice.IsPaid;
                                inv.Updated_At = DateTime.Now;
                            });
        }
        #endregion

        #region Delete
        public bool DeleteInvoice(string invoiceCode) 
        {
            return DeleteData(inv => inv.InvoiceCode == invoiceCode); // Điều kiện tìm invoice theo code
        }
		#endregion
	
        #region Get invoice by invoice id
		public Invoice GetInvoice(int invoiceId)
		{
			using (DrivingSchoolDataContext db = DataAccess.GetDataContext())
			{
				var invoice = db.Invoices.Where(i => i.InvoiceID == invoiceId).FirstOrDefault();
				if (invoice == null) return null;
				return invoice;
			}
		}
        #endregion

        public Invoice GetInvoiceID(int invoiceId)
		{
			// Tạo một đối tượng DataContext trong phạm vi using
			using (DrivingSchoolDataContext db = DataAccess.GetDataContext())
			{
				// Lấy hóa đơn từ cơ sở dữ liệu
				var invoice = db.Invoices
								.Where(i => i.InvoiceID == invoiceId)
								.FirstOrDefault();

				if (invoice == null)
					return null;  

				// thay vào đó tạo bản sao các đối tượng cần thiết từ DB để trả về.
				var result = new Invoice
				{
					InvoiceID = invoice.InvoiceID,
					InvoiceCode = invoice.InvoiceCode,
					EnrollmentID = invoice.EnrollmentID,
					// Lấy dữ liệu liên quan từ Enrollment và Learner
					Enrollment = new Enrollment
					{
						EnrollmentID = invoice.Enrollment.EnrollmentID,
						Learner = new Learner
						{
							LearnerID = invoice.Enrollment.Learner.LearnerID,
							FullName = invoice.Enrollment.Learner.FullName
							// Có thể thêm các thuộc tính khác nếu cần
						}
					}
				};

				return result;  // Trả về bản sao của đối tượng Invoice và các đối tượng liên quan
			}
		}

        #region Map to invoice
        private Invoice MapToInvoice(dynamic item)
        {
            return new Invoice
            {
                InvoiceID = item.InvoiceID,
                InvoiceCode = item.InvoiceCode,
                Enrollment = new Enrollment
				{
                    Learner = new Learner()
                    {
                        LearnerID = item.LearnerID,
                        FullName = item.FullName,
                        Email = item.Email,
                        PhoneNumber = item.PhoneNumber
                    },
                    Course = new Course()
                    {
                        CourseID = item.CourseID,
                        CourseName = item.CourseName,
                    }
                },
                IsPaid = item.IsPaid,
                TotalAmount = item.TotalAmount,
                Notes = item.Notes,
                Status = new Status
                {
                    StatusID = item.StatusID,
                    StatusName = item.StatusName,
                },
                Created_At = item.Created_At,
                Updated_At = item.Updated_At
            };
        }
        #endregion

        #region Get data invoice by Id
        public DataTable GetInvoiceData(string invoiceCode)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from invoice in db.Invoices
                           join enroll in db.Enrollments on invoice.EnrollmentID equals enroll.EnrollmentID
                           join learner in db.Learners on enroll.LearnerID equals learner.LearnerID
                           join course in db.Courses on enroll.CourseID equals course.CourseID
                           where invoice.InvoiceCode == invoiceCode
                           select new InvoiceDTO
                           {
                               InvoiceCode = invoice.InvoiceCode,
                               Created_At = invoice.Created_At.Value,
                               FullName = learner.FullName,
                               EnrollmentDate = (DateTime)(enroll.EnrollmentDate.HasValue ? enroll.EnrollmentDate.Value.Date : (DateTime?)null),
                               Address = learner.Address,
                               PhoneNumber = learner.PhoneNumber,
                               Email = learner.Email,
                               CourseName = course.CourseName,
                               DurationInHours = course.DurationInHours,
                               StartDate = course.StartDate.Value,
                               EndDate = course.EndDate.Value,
                               TotalAmount = invoice.TotalAmount,
                               Notes = invoice.Notes
                           };

                DataTable dt = this.CreateDataTable();
               
                foreach (var item in data)
                {
                    dt.Rows.Add(item.InvoiceCode, item.Created_At, item.FullName, item.EnrollmentDate,
                       item.Address, item.PhoneNumber, item.Email, item.CourseName,
                       item.DurationInHours, item.StartDate, item.EndDate, item.TotalAmount, item.Notes);
                }
                return dt;
            }
        }
        #endregion

        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("InvoiceCode", typeof(string));
            dt.Columns.Add("Created_At", typeof(DateTime));
            dt.Columns.Add("FullName", typeof(string));
            dt.Columns.Add("EnrollmentDate", typeof(DateTime));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("PhoneNumber", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("CourseName", typeof(string));
            dt.Columns.Add("DurationInHours", typeof(int));
            dt.Columns.Add("StartDate", typeof(DateTime));
            dt.Columns.Add("EndDate", typeof(DateTime));
            dt.Columns.Add("TotalAmount", typeof(decimal));
            dt.Columns.Add("Notes", typeof(string));
            return dt;
        }
    }
}
