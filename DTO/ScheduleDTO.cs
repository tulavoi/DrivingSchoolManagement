using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ScheduleDTO
    {
        public int ScheduleID { get; set; }
        public string LearnerFullName { get; set; }
        public string LearnerPhoneNumber { get; set; }
        public string LearnerEmail { get; set; }
        public string TeacherFullName { get; set; }
        public string TeacherPhoneNumber { get; set; }
        public string TeacherEmail { get; set; }
        public string VehicleName { get; set; }
        public string VehicleNumber { get; set; }
        public string CourseName { get; set; }
        public string SessionName { get; set; }
        public string SessionDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
