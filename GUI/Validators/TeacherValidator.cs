using Guna.UI2.WinForms;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace GUI.Validators
{
    public static class TeacherValidator
    {
        public static bool ValidateFullName(Guna2TextBox txt, Guna2HtmlToolTip toolTip)
        {
            return ValidatorHelper.CheckRequiredAndShowToolTip(txt, toolTip);
        }

        public static bool ValidateCitizenID(Guna2TextBox txt, Guna2HtmlToolTip toolTip)
        {
            if (!ValidatorHelper.CheckRequiredAndShowToolTip(txt, toolTip)) return false;

            if (!ValidatorHelper.IsValidCitizenID(txt.Text))
            {
                FormHelper.ShowToolTip(txt, toolTip, $"{txt.Tag} must be 12 digits.");
                txt.Focus();
                return false;
            }
            return true;
        }

        public static bool ValidateEmail(Guna2TextBox txt, Guna2HtmlToolTip toolTip)
        {
            if (!ValidatorHelper.CheckRequiredAndShowToolTip(txt, toolTip)) return false;

            if (!ValidatorHelper.IsValidEmail(txt.Text))
            {
                FormHelper.ShowToolTip(txt, toolTip, $"Invalid {txt.Tag}.");
                txt.Focus();
                return false;
            }
            return true;
        }

        public static bool ValidatePhoneNumber(Guna2TextBox txt, Guna2HtmlToolTip toolTip)
        {
            if (!ValidatorHelper.CheckRequiredAndShowToolTip(txt, toolTip)) return false;

            if (!ValidatorHelper.IsValidPhoneNumber(txt.Text))
            {
                FormHelper.ShowToolTip(txt, toolTip, $"{txt.Tag} start with 0, 10 - 11 digits.");
                txt.Focus();
                return false;
            }
            return true;
        }

        public static bool ValidateAddress(Guna2TextBox txt, Guna2HtmlToolTip toolTip)
        {
            return ValidatorHelper.CheckRequiredAndShowToolTip(txt, toolTip);
        }

        public static bool IsTeacherEligible(Guna2DateTimePicker dtpDOB, Guna2DateTimePicker dtpBeginningDate, string license, Guna2HtmlToolTip toolTip)
        {
            // Kiểm tra độ tuổi
            if (!ValidateDOB(dtpDOB, toolTip)) return false;

            // Kiểm tra ngày nhận bằng
            if (!IsBeginningDateValid(dtpDOB, dtpBeginningDate, toolTip)) return false;

            // Kiểm tra năm kinh nghiệm
            if (!IsExperienceValid(dtpBeginningDate, license, toolTip)) return false;

            return true;
        }

        private static bool IsExperienceValid(Guna2DateTimePicker dtpBeginning, string license, Guna2HtmlToolTip toolTip)
        {
            int experienceYears = GetExperienceYears(dtpBeginning.Value);
            string errorMessage = GetErrorMessage(experienceYears, license);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                FormHelper.ShowToolTip(dtpBeginning, toolTip, errorMessage); // Đảm bảo đây là đúng
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
                    return $"Please select license.";
            }
            return "";
        }

        private static int GetExperienceYears(DateTime beginningDate)
        {
            int experienceYears = DateTime.Now.Year - beginningDate.Year;
            if (beginningDate > DateTime.Now.AddYears(-experienceYears)) experienceYears--;

            return experienceYears;
        }

        public static bool IsBeginningDateValid(Guna2DateTimePicker dtpDOB, Guna2DateTimePicker dtpBeginning, Guna2HtmlToolTip toolTip)
        {
            DateTime dateOf18thBirthday = dtpDOB.Value.AddYears(18);
            // Kiểm tra ngày nhận bằng có sau ngày đủ 18 tuổi không
            if (dtpBeginning.Value < dateOf18thBirthday)
            {
                FormHelper.ShowToolTip(dtpBeginning, toolTip, "The beginning date must be after the teacher turns 18.");
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

		public static bool ValidateLicenseNumber(Guna2TextBox txt, Guna2HtmlToolTip toolTip)
		{
			if (!ValidatorHelper.CheckRequiredAndShowToolTip(txt, toolTip)) return false;

			if (!ValidatorHelper.IsValidLicenseNumber(txt.Text))
			{
				FormHelper.ShowToolTip(txt, toolTip, $"{txt.Tag} must be 12 digits.");
				txt.Focus();
				return false;
			}
			return true;
		}

		public static bool ValidateLicense(Guna2ComboBox cbo, Guna2HtmlToolTip toolTip)
		{
            if (!FormHelper.HasSelectedItem(cbo))
            {
				FormHelper.ShowToolTip(cbo, toolTip, $"Please select the {cbo.Tag}.");
				cbo.Focus();
				return false;
            }

            if(cbo.Text == "None")
            {
                FormHelper.ShowToolTip(cbo, toolTip, $"Teacher's license not 'None'.");
                cbo.Focus();
                return false;
            }
			return true;
		}
	}
}
