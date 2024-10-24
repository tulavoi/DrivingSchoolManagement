using DAL;
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
        public static bool AddVehicle(Vehicle vehicle)
        {
            return VehicleBLL.Instance.AddVehicle(vehicle);
        }
        
        public static bool EditVehicle(Vehicle vehicle)
        {
            return VehicleBLL.Instance.EditVehicle(vehicle);
        }
      
        public static bool EditVehicleNote(Vehicle vehicle)
        {
            return VehicleBLL.Instance.EditVehicleNote(vehicle);
        }
      
        public static bool DeleteVehicle(int vehicleID)
        {
            return VehicleBLL.Instance.DeleteVehicle(vehicleID);
        }

        public static void LoadAllVehicles(Guna2DataGridView dgv)
        {
            VehicleBLL.Instance.LoadAllVehicles(dgv);
        }

        public static void SearchVehicles(Guna2DataGridView dgv, string keyword)
        {
            VehicleBLL.Instance.SearchVehicles(dgv, keyword);
        }

        public static void FilterVehiclesByStatus(Guna2DataGridView dgv, string type)
        {
            VehicleBLL.Instance.FilterVehiclesByStatus(dgv, type);
        }
   
        public static void SearchVehicles(Guna2ComboBox cbo, string keyword)
        {
            VehicleBLL.Instance.SearchVehicles(cbo, keyword);
        }
    }
}
