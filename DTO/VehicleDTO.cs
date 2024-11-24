using System;

namespace DTO
{
    public class VehicleDTO
    {
        // Thông tin phương tiện, commnet thừa vl
        public int VehicleID { get; set; }
        public string VehicleName { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleType { get; set; }
        public bool IsTruck { get; set; }
        public bool IsPassengerCar { get; set; }
        public bool IsMaintenance { get; set; }
        public int Seats { get; set; }
        public int Weight { get; set; }
        public int ManufacturerYear { get; set; }
        public string Notes { get; set; } // Ghi chú thêm về phương tiện (nếu có)
        public string MaintenanceStatus {  get; set; }
    }
}
