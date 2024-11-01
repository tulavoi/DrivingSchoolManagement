using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;

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
        public List<Invoice> FilterInvoicesByStatus(string status)
        {
            return FilterData(status, item => this.MapToInvoice(item));
        }

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
                               status.StatusID,
                               status.StatusName,
                               invoice.IsPaid,
                               invoice.Created_At,
                               invoice.Updated_At
                           };
                return data.ToList();
            }
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
                                inv.Status = invoice.Status;
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
                Status = new Status
                {
                    StatusID = item.StatusID,
                    StatusName = item.StatusName,
                },
                Created_At = item.Created_At,
                Updated_At = item.Updated_At
            };
        }

    }
}
