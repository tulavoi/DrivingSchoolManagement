﻿using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class VehicleBLL
    {
        #region Properties
        private static VehicleBLL instance;

        public static VehicleBLL Instance
        {
            get
            {
                if (instance == null) instance = new VehicleBLL();
                return instance;
            }
        }
        #endregion

        public void AssignVehiclesToCombobox(Guna2ComboBox cbo)
        {
            List<Vehicle> vehicles = VehicleDAL.Instance.GetAllVehicles();
            this.AddVehiclesToCombobox(cbo, vehicles);
        }

        private void AddVehiclesToCombobox(Guna2ComboBox cbo, List<Vehicle> vehicles)
        {
            Vehicle vehicle = new Vehicle();
            vehicle.VehicleName = "Select Vehicle";
            vehicles.Insert(0, vehicle);

            cbo.DataSource = vehicles;
            cbo.ValueMember = "VehicleID";
            cbo.DisplayMember = "VehicleName";
        }

        public void LoadAllVehicles(Guna2DataGridView dgv)
        {
            List<Vehicle> vehicles = VehicleDAL.Instance.GetAllVehicles();
            this.AddVehiclesToDataGridView(dgv, vehicles);
        }

        public void SearchVehicles(Guna2DataGridView dgv, string keyword)
        {
            List<Vehicle> vehicles = VehicleDAL.Instance.SearchVehicles(keyword);
            this.AddVehiclesToDataGridView(dgv, vehicles);
        }

        public void FilterVehiclesByStatus(Guna2DataGridView dgv, string status)
        {
            List<Vehicle> vehicles = VehicleDAL.Instance.FilterVehiclesByStatus(status);
            this.AddVehiclesToDataGridView(dgv, vehicles);
        }

        private void AddVehiclesToDataGridView(Guna2DataGridView dgv, List<Vehicle> vehicles)
        {
            dgv.Rows.Clear();
            foreach (var vehicle in vehicles)
            {
                int rowIndex = dgv.Rows.Add();

                if (rowIndex != -1 && rowIndex < dgv.Rows.Count)
                {
                    dgv.Rows[rowIndex].Tag = vehicle;
                    dgv.Rows[rowIndex].Cells["CarName"].Value = vehicle.VehicleName;
                    dgv.Rows[rowIndex].Cells["CarNumber"].Value = vehicle.VehicleNumber;
                    dgv.Rows[rowIndex].Cells["ManufactureYear"].Value = vehicle.ManufacturerYear;
                    if (vehicle.IsMaintenance == false)
                    {
                        dgv.Rows[rowIndex].Cells["Status"].Value = "Available";
                    }
                    else
                    {
                        dgv.Rows[rowIndex].Cells["Status"].Value = "Maintenance";
                    }

                }
            }
        }

        public bool AddVehicle(Vehicle vehicle)
        {
            return VehicleDAL.Instance.AddVehicle(vehicle);
        }

        public bool EditVehicle(Vehicle vehicle)
        {
            return VehicleDAL.Instance.EditVehicle(vehicle);
        }

        public bool DeleteVehicle(int vehicleID)
        {
            return VehicleDAL.Instance.DeleteVehicle(vehicleID);
        }
    }
}
