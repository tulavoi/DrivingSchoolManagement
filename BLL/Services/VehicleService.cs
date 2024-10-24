using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class VehicleService
    {
        public static void SearchVehicles(Guna2ComboBox cbo, string keyword)
        {
            VehicleBLL.Instance.SearchVehicles(cbo, keyword);
        }
    }
}
