using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class InvoiceDTO
    {
        public string InvoiceCode { get; set; }
        public DateTime Created_At { get; set; }
        public string FullName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CourseName { get; set; }
        public int? DurationInHours { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? TotalAmount { get; set; }
        public string Notes { get; set; }
    }
}
