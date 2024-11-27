using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class RevenueDTO
    {
        public int LicenseID { get; set; }
        public string LicenseName { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
