using Guna.UI2.WinForms;
using System;

namespace GUI.Validators
{
    public static class TeacherValidator
    {
        public static bool CheckRequiredAndShowToolTip(Guna2TextBox txt, Guna2HtmlToolTip toolTip)
        {
            if (!ValidatorHelper.IsRequiredFieldFilled(txt.Text))
            {
                FormHelper.ShowToolTip(txt, toolTip, $"Please enter {txt.Tag}");
                return false;
            }
            return true;
        }

        public static bool ValidateFullName(Guna2TextBox txt, Guna2HtmlToolTip toolTip)
        {
            return CheckRequiredAndShowToolTip(txt, toolTip);
        }

        public static bool ValidateCitizenID(Guna2TextBox txt, Guna2HtmlToolTip toolTip)
        {
            if (!CheckRequiredAndShowToolTip(txt, toolTip)) return false;

            if (!ValidatorHelper.IsValidCitizenID(txt.Text))
            {
                FormHelper.ShowToolTip(txt, toolTip, $"{txt.Tag} must be 12 digits.");
                return false;
            }
            return true;
        }

        public static bool ValidateEmail(Guna2TextBox txt, Guna2HtmlToolTip toolTip)
        {
            if (!CheckRequiredAndShowToolTip(txt, toolTip)) return false;

            if (!ValidatorHelper.IsValidEmail(txt.Text))
            {
                FormHelper.ShowToolTip(txt, toolTip, $"Invalid {txt.Tag}.");
                return false;
            }
            return true;
        }

        public static bool ValidatePhoneNumber(Guna2TextBox txt, Guna2HtmlToolTip toolTip)
        {
            if (!CheckRequiredAndShowToolTip(txt, toolTip)) return false;

            if (!ValidatorHelper.IsValidPhoneNumber(txt.Text))
            {
                FormHelper.ShowToolTip(txt, toolTip, $"{txt.Tag} start with 0, 10 - 11 digits.");
                return false;
            }
            return true;
        }

        public static bool ValidateAddress(Guna2TextBox txt, Guna2HtmlToolTip toolTip)
        {
            return CheckRequiredAndShowToolTip(txt, toolTip);
        }

        public static bool IsTeacherEligible(Guna2DateTimePicker dtpDOB, Guna2DateTimePicker dtpGraduated, string license, Guna2HtmlToolTip toolTip)
        {
            // Kiểm tra độ tuổi
            if (!ValidateDOB(dtpDOB, toolTip)) return false;

            // Kiểm tra ngày tốt nghiệp
            if (!IsGraduationDateValid(dtpDOB, dtpGraduated, toolTip)) return false;

            // Kiểm tra năm kinh nghiệm
            if (!IsExperienceValid(dtpGraduated, license, toolTip)) return false;

            return true;
        }

        public static bool IsExperienceValid(Guna2DateTimePicker dtpGraduated, string license, Guna2HtmlToolTip toolTip)
        {
            int experienceYears = GetExperienceYears(dtpGraduated.Value);
            string errorMessage = GetErrorMessage(experienceYears, license);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                FormHelper.ShowToolTip(dtpGraduated, toolTip, errorMessage); // Đảm bảo đây là đúng
                return false;
            }
            return true;
        }

        private static string GetErrorMessage(int experienceYears, string license)
        {
            switch (license)
            {
                case "B":
                    if (experienceYears < 3)
                        return $"At least 3 years of experience with license {license}.";
                    break;

                case "C":
                case "D":
                case "E":
                    if (experienceYears < 5)
                        return $"At least 5 years of experience with license {license}.";
                    break;
                default:
                    return $"Invalid license type: {license}.";
            }
            return "";
        }

        private static int GetExperienceYears(DateTime graduatedDate)
        {
            int experienceYears = DateTime.Now.Year - graduatedDate.Year;
            if (graduatedDate > DateTime.Now.AddYears(-experienceYears)) experienceYears--;

            return experienceYears;
        }

        public static bool IsGraduationDateValid(Guna2DateTimePicker dtpDOB, Guna2DateTimePicker dtpGraduated, Guna2HtmlToolTip toolTip)
        {
            DateTime dateOf18thBirthday = dtpDOB.Value.AddYears(18);
            // Kiểm tra ngày tốt nghiệp có sau ngày đủ 18 tuổi không
            if (dtpGraduated.Value < dateOf18thBirthday)
            {
                FormHelper.ShowToolTip(dtpGraduated, toolTip, "The graduation date must be after the teacher turns 18.");
                return false;
            }
            return true;
        }

        public static bool ValidateDOB(Guna2DateTimePicker dtpDOB, Guna2HtmlToolTip toolTip)
        {
            if (!ValidatorHelper.IsEligibleAge(dtpDOB.Value))
            {
                FormHelper.ShowToolTip(dtpDOB, toolTip, "Teachers must be at least 18 years old.");
                return false;
            }
            return true;
        }
    }
}
