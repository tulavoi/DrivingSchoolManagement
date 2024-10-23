using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Validators
{
    public static class VehicleValidator
    {
        public static bool ValidateName(Guna2TextBox txt, Guna2HtmlToolTip toolTip)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                FormHelper.ShowToolTip(txt, toolTip, "Vehicle name is required.");
                return false;
            }
            return true;
        }

        public static bool ValidateVehicleNumber(Guna2TextBox txt, Guna2HtmlToolTip toolTip)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                FormHelper.ShowToolTip(txt, toolTip, "Vehicle number is required.");
                return false;
            }
            return true;
        }

        public static bool ValidateManufactureYear(Guna2DateTimePicker dtp, Guna2HtmlToolTip toolTip)
        {
            int currentYear = DateTime.Now.Year;
            if (dtp.Value.Year > currentYear)
            {
                FormHelper.ShowToolTip(dtp, toolTip, "Manufacture year cannot be in the future.");
                return false;
            }
            return true;
        }

        public static bool ValidateWeight(Guna2TextBox txtWeight, Guna2HtmlToolTip toolTip)
        {
            // Kiểm tra nếu trường rỗng
            if (string.IsNullOrWhiteSpace(txtWeight.Text))
            {
                return true; // Trả về true nếu không cần kiểm tra
            }

            // Kiểm tra nếu giá trị không phải là số nguyên
            if (!int.TryParse(txtWeight.Text, out int weight) || weight < 0)
            {
                toolTip.SetToolTip(txtWeight, "Please enter a valid weight.");
                txtWeight.Focus();
                return false;
            }

            // Nếu mọi điều kiện đều đạt, trả về true
            return true;
        }

        public static bool ValidateSeats(Guna2TextBox txtSeats, Guna2HtmlToolTip toolTip)
        {
            // Kiểm tra nếu trường rỗng (cho phép null hoặc rỗng)
            if (string.IsNullOrWhiteSpace(txtSeats.Text))
            {
                // Không hiển thị thông báo lỗi vì không cần thiết
                return true; // Trả về true nếu không cần kiểm tra
            }

            // Kiểm tra nếu giá trị không phải là số nguyên
            if (!int.TryParse(txtSeats.Text, out int seats) || seats < 0)
            {
                toolTip.SetToolTip(txtSeats, "Please enter a valid number of seats.");
                txtSeats.Focus();
                return false;
            }

            // Nếu mọi điều kiện đều đạt, trả về true
            return true;
        }


        public static bool ValidateTruck(Guna2CustomCheckBox chk, Guna2HtmlToolTip toolTip)
        {
            // Optional: Add logic to validate truck checkbox if needed
            return true;
        }

        public static bool ValidatePassengerCar(Guna2CustomCheckBox chk, Guna2HtmlToolTip toolTip)
        {
            // Optional: Add logic to validate passenger car checkbox if needed
            return true;
        }
    }

}
