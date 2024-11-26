using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
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
        public static DataTable GetVehiclesTypeB()
        {
            return VehicleBLL.Instance.GetVehiclesTypeB();
        }
        public static DataTable GetVehiclesTypeD()
        {
            return VehicleBLL.Instance.GetVehiclesTypeD();
        }
        public static DataTable GetVehiclesTypeC()
        {
            return VehicleBLL.Instance.GetVehiclesTypeC();
        }
        public static DataTable GetVehiclesTypeE()
        {
            return VehicleBLL.Instance.GetVehiclesTypeE();
        }
        public static DataTable GetVehiclesMT()
        {
            return VehicleBLL.Instance.GetVehiclesMT();
        }

        public static DataTable GetVehicleForLicense(string license)
        {
            return VehicleBLL.Instance.GetVehicleForLicense(license);
        }
        public static DataTable GetVehicleByVehicleID(int vehicleID)
        {
            return VehicleBLL.Instance.GetVehicleByVehicleID(vehicleID);
        }
        public static DataTable GetAllVehiclesData()
        {
            return VehicleBLL.Instance.GetAllVehiclesData();
        }
    }
}
