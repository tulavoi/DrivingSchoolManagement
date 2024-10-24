using Guna.UI2.WinForms;
using System;
using System.Text.RegularExpressions;

namespace GUI.Validators
{
    public static class ValidatorHelper
    {
        // Class này chứa các validation các dữ liệu có thể có trong nhiều form
        public static bool IsRequiredFieldFilled(string value)
        {
            return !string.IsNullOrEmpty(value.Trim());
        }

        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, Constant.EmailValidationPattern, RegexOptions.IgnoreCase);
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, Constant.PhoneNumberValidationPattern, RegexOptions.IgnoreCase);
        }

        public static bool IsValidCitizenID(string citizenID)
        {
            return citizenID.Length == 12;
        }

        public static bool IsEligibleAge(DateTime dob)
        {
            int age = DateTime.Now.Year - dob.Year;
            if (dob > DateTime.Now.AddYears(-age)) age--; // Kiểm tra chính xác ngày sinh nhật đã qua chưa

            return age >= Constant.MinAge;
        }

        public static bool CheckRequiredAndShowToolTip(Guna2TextBox txt, Guna2HtmlToolTip toolTip)
        {
            if (!IsRequiredFieldFilled(txt.Text))
            {
                FormHelper.ShowToolTip(txt, toolTip, $"Please enter {txt.Tag}");
                return false;
            }
            return true;
        }
    }
}
