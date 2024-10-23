using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ScheduleException : Exception
    {
        public ScheduleException(string message) : base(message) { }

        public ScheduleException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
