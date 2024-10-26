using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GUI.Validators
{
    public static class VehicleValidator
    {
        public static bool ValidateName(Guna2TextBox txt, Guna2HtmlToolTip toolTip)
        {
            return ValidatorHelper.CheckRequiredAndShowToolTip(txt, toolTip);
        }

        public static bool ValidateVehicleNumber(Guna2TextBox txt, Guna2HtmlToolTip toolTip)
        {
            return ValidatorHelper.CheckRequiredAndShowToolTip(txt, toolTip);
        }

        public static bool ValidateManufactureYear(Guna2DateTimePicker dtp, Guna2HtmlToolTip toolTip)
        {
            int currentYear = DateTime.Now.Year;
            if (dtp.Value.Year > currentYear)
            {
                FormHelper.ShowToolTip(dtp, toolTip, "Manufacture year cannot be in the future.");
                dtp.Focus();
                return false;
            }
            return true;
        }

        public static bool ValidateWeightAndSeats(Guna2CustomCheckBox chkPassengerCar, Guna2CustomCheckBox chkTruck, 
            Guna2TextBox txtSeats, Guna2TextBox txtWeight, Guna2HtmlToolTip toolTip)
        {
            if (!IsVehicleTypeSelected(chkPassengerCar, chkTruck, toolTip))
                return false;

            if (chkTruck.Checked)
                if (!ValidateTruck(txtSeats, txtWeight, chkTruck, toolTip)) return false;

            if (chkPassengerCar.Checked)
                if (!ValidatePassengerCar(txtSeats, txtWeight, chkPassengerCar, toolTip)) return false;

            return true;
        }

        private static bool ValidatePassengerCar(Guna2TextBox txtSeats, Guna2TextBox txtWeight, Guna2CustomCheckBox chkPassengerCar, Guna2HtmlToolTip toolTip)
        {
            if (!string.IsNullOrEmpty(txtWeight.Text))
            {
                FormHelper.ShowToolTip(txtWeight, toolTip, $"If you selected {chkPassengerCar.Tag}, {txtWeight.Tag} must be empty.");
                return false;
            }

            if (!ValidatorHelper.CheckRequiredAndShowToolTip(txtSeats, toolTip))
                return false;

            if (Convert.ToInt32(txtSeats.Text) > 70)
            {
                FormHelper.ShowToolTip(txtSeats, toolTip, $"Invalid {txtSeats.Tag}.");
                return false;
            }

            return true;
        }

        private static bool ValidateTruck(Guna2TextBox txtSeats, Guna2TextBox txtWeight, Guna2CustomCheckBox chkTruck, Guna2HtmlToolTip toolTip)
        {
            if (!string.IsNullOrEmpty(txtSeats.Text))
            {
                FormHelper.ShowToolTip(txtSeats, toolTip, $"If you selected {chkTruck.Tag}, {txtSeats.Tag} must be empty.");
                return false;
            }

            if (!ValidatorHelper.CheckRequiredAndShowToolTip(txtWeight, toolTip))
                return false;

            if (Convert.ToInt32(txtWeight.Text) > 4000)
            {
                FormHelper.ShowToolTip(txtWeight, toolTip, $"Invalid {txtWeight.Tag}.");
                return false;
            }

            return true;
        }

        private static bool IsVehicleTypeSelected(Guna2CustomCheckBox chkPassengerCar, Guna2CustomCheckBox chkTruck, Guna2HtmlToolTip toolTip)
        {
            if (!chkTruck.Checked && !chkPassengerCar.Checked)
            {
                FormHelper.ShowToolTip(chkPassengerCar, toolTip, $"Please select {chkTruck.Tag} or {chkPassengerCar.Tag}.");
                return false;
            }
            return true;
        }
    }

}
