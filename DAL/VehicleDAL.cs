using System;
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

        private int licenseID_B = 1;
        private int licenseID_C = 2;
        private int licenseID_D = 3;
        private int licenseID_E = 4;
        #endregion

        #region All Vehicles
        protected override IEnumerable<dynamic> QueryAllData()
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from vehicle in db.Vehicles
                           select vehicle;
                           
                return data.ToList();
            }
        }

        public List<Vehicle> GetAllVehicles()
        {
            return GetAll(item => this.MapToVehicle(item));
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
                           select vehicle;
                return data.ToList();
            }
        }

        public List<Vehicle> SearchVehicles(string keyword)
        {
            return SearchData(keyword, item => this.MapToVehicle(item));
        }
        #endregion

        #region Filter by status
        public List<Vehicle> FilterVehiclesByStatus(string status)
        {
            return FilterData(status, item => this.MapToVehicle(item));
        }


        protected override IEnumerable<dynamic> QueryDataByFilter(string filterString)
        {
            using (var db = DataAccess.GetDataContext())
            {
                var data = from vehicle in db.Vehicles
                           where vehicle.IsTruck.ToString() == filterString || 
                                 vehicle.IsPassengerCar.ToString() == filterString
                           select vehicle;
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
            return UpdateStatus(v => v.VehicleID == vehicleID, StatusID_Inactive); // Điều kiện tìm vehicle theo ID
        }
        #endregion

        #region Get vehicle by course
        public List<Vehicle> GetVehicleForCourse(int courseID, int sessionID, DateTime curDate)
        {
            using (DrivingSchoolDataContext db = DataAccess.GetDataContext())
            {
				var scheduledVehicles = (from sche in db.Schedules
										 where sche.SessionID == sessionID
											   && sche.SessionDate == curDate
										 select sche.VehicleID).Distinct().ToList();

				var licenseId = (from course in db.Courses
                                 where course.CourseID == courseID
                                 select course.LicenseID).FirstOrDefault();

                var vehicles = db.Vehicles
                    .Where(v => v.IsMaintenance == false)
                    .Where(v =>
                        (licenseId == licenseID_B && v.IsPassengerCar == true && v.Seats <= 9) ||                      // B
                        (licenseId == licenseID_C && v.IsTruck == true && v.Weight >= 3500) ||                         // C
                        (licenseId == licenseID_D && v.IsPassengerCar == true && v.Seats >= 10 && v.Seats <= 30) ||    // D
                        (licenseId == licenseID_E && v.IsPassengerCar == true && v.Seats > 30))                         // E
                    .Where(vehicle => !scheduledVehicles.Contains(vehicle.VehicleID))
					.ToList();

                return vehicles;
            }
        }
		#endregion

		#region Lấy phương tiện hiện có trong khóa học và các phương tiện phù hợp
		public List<Vehicle> GetVehicleForCourseAndInCourse(int courseID, int sessionID, DateTime curDate)
		{
			using (DrivingSchoolDataContext db = DataAccess.GetDataContext())
			{
				var scheduledVehicleForOtherCourses = (from sche in db.Schedules
														where sche.SessionID == sessionID
															  && sche.SessionDate == curDate
															  && sche.Enrollment.CourseID != courseID
														select sche.VehicleID).Distinct().ToList();

				var licenseId = (from course in db.Courses
								 where course.CourseID == courseID
								 select course.LicenseID).FirstOrDefault();

                var availableVehicles = (from vehicle in db.Vehicles
                                         where vehicle.IsMaintenance == false
                                             // Điều kiện lọc theo loại giấy phép và thông số xe
                                             && ((licenseId == licenseID_B && vehicle.IsPassengerCar == true && vehicle.Seats <= 9) ||    // B
                                                 (licenseId == licenseID_C && vehicle.IsTruck == true && vehicle.Weight >= 3500) ||       // C
                                                 (licenseId == licenseID_D && vehicle.IsPassengerCar == true && vehicle.Seats >= 10 && vehicle.Seats <= 30) || // D
                                                 (licenseId == licenseID_E && vehicle.IsPassengerCar == true && vehicle.Seats > 30))     // E
                                                                                                                                         // Phương tiện không nằm trong danh sách đã được lên lịch
                                             && (!scheduledVehicleForOtherCourses.Contains(vehicle.VehicleID)
                                                 || (from sche in db.Schedules
                                                     where sche.VehicleID == vehicle.VehicleID
                                                          && sche.SessionID == sessionID
                                                          && sche.SessionDate == curDate
                                                          && sche.Enrollment.CourseID == courseID
                                                     select sche.VehicleID).Any())
                                         select vehicle).ToList();

                return availableVehicles ?? new List<Vehicle>();
			}
		}
        #endregion

        #region Map to vehicle
        private Vehicle MapToVehicle(dynamic item)
        {
            return new Vehicle {
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
                StartMaintenaceDate = item.StartMaintenaceDate,
                EndMaintenaceDate = item.EndMaintenaceDate,
                Created_At = item.Created_At,
                Updated_At = item.Updated_At
            };
        }
        #endregion
    }
}
