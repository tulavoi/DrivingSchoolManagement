using System;
using System.Collections;
using System.Collections.Generic;
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
                           join sche in db.Schedules on invoice.ScheduleID equals sche.ScheduleID
                           join learner in db.Learners on sche.LearnerID equals learner.LearnerID
                           join course in db.Courses on sche.CourseID equals course.CourseID
                           select new
                           {
                               invoice.InvoiceID,
                               invoice.InvoiceCode,
                               learner.LearnerID,
                               learner.FullName,
                               course.CourseID,
                               course.CourseName,
                               invoice.TotalAmount,
                               invoice.Status,
                               invoice.Created_At,
                               invoice.Updated_At
                           };
                return data.ToList();
            }
        }

        public List<Invoice> GetAllInvoices()
        {
            return GetAll(item => new Invoice
            {
                InvoiceID = item.InvoiceID,
                InvoiceCode = item.InvoiceCode,
                Schedule = new Schedule
                {
                    Learner = new Learner()
                    {
                        LearnerID = item.LearnerID,
                        FullName = item.FullName,
                    },
                    Course = new Course()
                    {
                        CourseID = item.CourseID,
                        CourseName = item.CourseName,
                    }
                },
                TotalAmount = item.TotalAmount,
                Status = item.Status,
                Created_At = item.Created_At,
                Updated_At = item.Updated_At
            });
        }
        #endregion

        #region Search
        protected override IEnumerable<dynamic> QueryDataByKeyword(string keyword)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from invoice in db.Invoices
                           join sche in db.Schedules on invoice.ScheduleID equals sche.ScheduleID
                           join learner in db.Learners on sche.LearnerID equals learner.LearnerID
                           join course in db.Courses on sche.CourseID equals course.CourseID
                           where (invoice.InvoiceCode.Contains(keyword) || learner.FullName.Contains(keyword))
                           select new
                           {
                               invoice.InvoiceID,
                               invoice.InvoiceCode,
                               learner.LearnerID,
                               learner.FullName,
                               course.CourseID,
                               course.CourseName,
                               invoice.TotalAmount,
                               invoice.Status,
                               invoice.Created_At,
                               invoice.Updated_At
                           };
                return data.ToList();
            }
        }

        public List<Invoice> SearchInvoices(string keyword)
        {
            return SearchData(keyword, item => new Invoice
            {
                InvoiceID = item.InvoiceID,
                InvoiceCode = item.InvoiceCode,
                Schedule = new Schedule
                {
                    Learner = new Learner()
                    {
                        LearnerID = item.LearnerID,
                        FullName = item.FullName,
                    },
                    Course = new Course()
                    {
                        CourseID = item.CourseID,
                        CourseName = item.CourseName,
                    }
                },
                TotalAmount = item.TotalAmount,
                Status = item.Status,
                Created_At = item.Created_At,
                Updated_At = item.Updated_At
            });
        }
        #endregion
         
        #region Filter by status
        public List<Invoice> FilterInvoicesByStatus(string status)
        {
            return FilterData(status, item => new Invoice
            {
                InvoiceID = item.InvoiceID,
                InvoiceCode = item.InvoiceCode,
                Schedule = new Schedule
                {
                    Learner = new Learner()
                    {
                        LearnerID = item.LearnerID,
                        FullName = item.FullName,
                    },
                    Course = new Course()
                    {
                        CourseID = item.CourseID,
                        CourseName = item.CourseName,
                    }
                },
                TotalAmount = item.TotalAmount,
                Status = item.Status,
                Created_At = item.Created_At,
                Updated_At = item.Updated_At
            });
        }

        protected override IEnumerable<dynamic> QueryDataByFilter(string filterString)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from invoice in db.Invoices
                           join sche in db.Schedules on invoice.ScheduleID equals sche.ScheduleID
                           join learner in db.Learners on sche.LearnerID equals learner.LearnerID
                           join course in db.Courses on sche.CourseID equals course.CourseID
                           where invoice.Status == filterString
                           select new
                           {
                               invoice.InvoiceID,
                               invoice.InvoiceCode,
                               learner.LearnerID,
                               learner.FullName,
                               course.CourseID,
                               course.CourseName,
                               invoice.TotalAmount,
                               invoice.Status,
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
    }
}
