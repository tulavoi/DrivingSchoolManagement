﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class VehicleDAL : BaseDAL<Vehicle>
    {
        #region Properties
        private static VehicleDAL instance;

        public static VehicleDAL Instance
        {
            get
            {
                if (instance == null) instance = new VehicleDAL();
                return instance;
            }
        }
        #endregion

        #region All Vehicles
        protected override IEnumerable<dynamic> QueryAllData()
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from vehicle in db.Vehicles
                           select new
                           {
                               vehicle.VehicleID,
                               vehicle.VehicleName,
                               vehicle.VehicleNumber,
                               vehicle.IsTruck,
                               vehicle.IsPassengerCar,
                               vehicle.IsMaintenance,
                               vehicle.ManufacturerYear,
                               vehicle.Weight,
                               vehicle.Seats,
                               vehicle.Notes,
                               vehicle.Created_At,
                               vehicle.Updated_At
                           };
                return data.ToList();
            }
        }

        public List<Vehicle> GetAllVehicles()
        {
            return GetAll(item => new Vehicle
            {
                VehicleID = item.VehicleID,
                VehicleName = item.VehicleName,
                VehicleNumber = item.VehicleNumber,
                IsTruck = item.IsTruck,
                IsPassengerCar = item.IsPassengerCar,
                IsMaintenance = item.IsMaintenance,
                ManufacturerYear = item.ManufacturerYear,
                Weight = item.Weight,
                Seats = item.Seats,
                Notes = item.Notes,
                Created_At = item.Created_At,
                Updated_At = item.Updated_At
            });
        }
        #endregion

        #region All Vehicles Not Maintenance
        public List<Vehicle> GetAllVehiclesNotMaintenance()
        {
            using (var db = DataAccess.GetDataContext())
            {
                var vehicles = db.Vehicles
                   .Where(v => v.IsMaintenance == false)
                   .ToList();

                return vehicles;
            }
        }
        #endregion

        #region Search
        protected override IEnumerable<dynamic> QueryDataByKeyword(string keyword)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from vehicle in db.Vehicles
                           where vehicle.VehicleName.Contains(keyword) || vehicle.VehicleNumber.Contains(keyword)
                           select new
                           {
                               vehicle.VehicleID,
                               vehicle.VehicleName,
                               vehicle.VehicleNumber,
                               vehicle.IsTruck,
                               vehicle.IsPassengerCar,
                               vehicle.IsMaintenance,
                               vehicle.ManufacturerYear,
                               vehicle.Weight,
                               vehicle.Seats,
                               vehicle.Notes,
                               vehicle.Created_At,
                               vehicle.Updated_At
                           };
                return data.ToList();
            }
        }

        public List<Vehicle> SearchVehicles(string keyword)
        {
            return SearchData(keyword, item => new Vehicle
            {
                VehicleID = item.VehicleID,
                VehicleName = item.VehicleName,
                VehicleNumber = item.VehicleNumber,
                IsTruck = item.IsTruck,
                IsPassengerCar = item.IsPassengerCar,
                IsMaintenance = item.IsMaintenance,
                ManufacturerYear = item.ManufacturerYear,
                Weight = item.Weight,
                Seats = item.Seats,
                Notes = item.Notes,
                Created_At = item.Created_At,
                Updated_At = item.Updated_At
            });
        }
        #endregion

        #region Filter by status
        public List<Vehicle> FilterVehiclesByStatus(string status)
        {
            return FilterData(status, item => new Vehicle
            {
                VehicleID = item.VehicleID,
                VehicleName = item.VehicleName,
                VehicleNumber = item.VehicleNumber,
                IsTruck = item.IsTruck,
                IsPassengerCar = item.IsPassengerCar,
                IsMaintenance = item.IsMaintenance,
                ManufacturerYear = item.ManufacturerYear,
                Weight = item.Weight,
                Seats = item.Seats,
                Notes = item.Notes,
                Created_At = item.Created_At,
                Updated_At = item.Updated_At
            });
        }


        protected override IEnumerable<dynamic> QueryDataByFilter(string filterString)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from vehicle in db.Vehicles
                           where vehicle.IsTruck.ToString() == filterString || vehicle.IsPassengerCar.ToString() == filterString
                           select new
                           {
                               vehicle.VehicleID,
                               vehicle.VehicleName,
                               vehicle.VehicleNumber,
                               vehicle.IsTruck,
                               vehicle.IsPassengerCar,
                               vehicle.IsMaintenance,
                               vehicle.ManufacturerYear,
                               vehicle.Weight,
                               vehicle.Seats,
                               vehicle.Notes,
                               vehicle.Created_At,
                               vehicle.Updated_At
                           };
                return data.ToList();
            }
        }
        #endregion

        #region Create
        public bool AddVehicle(Vehicle vehicle)
        {
            return AddData(vehicle);
        }
        #endregion

        #region Edit

        public bool EditVehicle(Vehicle vehicle)
        {
            return EditData(v => v.VehicleID == vehicle.VehicleID,      // Điều kiện tìm vehicle theo ID
                            v =>                                        // Action cập nhật các thuộc tính
                            {
                                v.VehicleName = vehicle.VehicleName;
                                v.VehicleNumber = vehicle.VehicleNumber;
                                v.IsTruck = vehicle.IsTruck;
                                v.IsPassengerCar = vehicle.IsPassengerCar;
                                v.IsMaintenance = vehicle.IsMaintenance;
                                v.ManufacturerYear = vehicle.ManufacturerYear;
                                v.Weight = vehicle.Weight;
                                v.Seats = vehicle.Seats;
                                v.Notes = vehicle.Notes;
                                v.Updated_At = DateTime.Now;
                            });
        }
        public bool EditVehicleNote(Vehicle vehicle)
        {
            return EditData(v => v.VehicleID == vehicle.VehicleID,      // Điều kiện tìm vehicle theo ID
                            v =>                                        // Action cập nhật các thuộc tính
                            {
                                v.IsMaintenance = vehicle.IsMaintenance;
                                v.Notes = vehicle.Notes;
                                v.Updated_At = DateTime.Now;
                            });
        }
        #endregion

        #region Delete
        public bool DeleteVehicle(int vehicleID)
        {
            return DeleteData(v => v.VehicleID == vehicleID); // Điều kiện tìm vehicle theo ID
        }
        #endregion

        #region Get vehicle by course
        public List<Vehicle> GetVehicleForCourse(int courseID)
        {
            using (DrivingSchoolDataContext db = DataAccess.GetDataContext())
            {
                var licenseId = (from course in db.Courses
                                 where course.CourseID == courseID
                                 select course.LicenseID).FirstOrDefault();

                var vehicles = db.Vehicles
                    .Where(v => v.IsMaintenance == false)
                    .Where(v =>
                        (licenseId == 1001 && v.IsPassengerCar == true && v.Seats <= 9) ||                      // B
                        (licenseId == 1002 && v.IsTruck == true && v.Weight >= 3500) ||                         // C
                        (licenseId == 1003 && v.IsPassengerCar == true && v.Seats >= 10 && v.Seats <= 30) ||    // D
                        (licenseId == 1004 && v.IsPassengerCar == true && v.Seats > 30)                         // E
                    )
                    .ToList();

                return vehicles;
            }
        }
        #endregion
    }
}
