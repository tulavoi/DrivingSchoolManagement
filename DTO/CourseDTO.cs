using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CourseDTO
    {
        public string CourseName { get; set; }
        public string LicenseName { get; set; }
        public int Fee { get; set; }
        public int DurationInHours { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
