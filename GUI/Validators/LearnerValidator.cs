using Guna.UI2.WinForms;
using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace GUI.Validators
{
    public static class LearnerValidator
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

        public static bool IsLearnerEligible(Guna2DateTimePicker dtpDOB, Guna2HtmlToolTip toolTip)
        {
            // Kiểm tra độ tuổi của học viên
            return ValidateDOB(dtpDOB, toolTip);
        }

        public static bool ValidateDOB(Guna2DateTimePicker dtpDOB, Guna2HtmlToolTip toolTip)
        {
            if (!ValidatorHelper.IsEligibleAge(dtpDOB.Value))
            {
                FormHelper.ShowToolTip(dtpDOB, toolTip, "Learners must be at least 18 years old.");
                dtpDOB.Focus();
                return false;
            }
            return true;
        }

		public static bool ValidateLicenseNumber(Guna2TextBox txt, Guna2HtmlToolTip toolTip)
		{
			if (txt.Text.Length != 12)
			{
				FormHelper.ShowToolTip(txt, toolTip, $"{txt.Tag} must be 12 digits.");
				txt.Focus();
				return false;
			}
            return true;
		}

		public static bool ValidateSelectedCourse(Guna2ComboBox cbo, Guna2HtmlToolTip toolTip)
		{
			if (!FormHelper.HasSelectedItem(cbo))
            {
				FormHelper.ShowToolTip(cbo, toolTip, $"Please select {cbo.Tag}.");
				cbo.Focus();
				return false;
            }
            return true;
		}

        // Kiểm tra độ tuổi của học viên có phù hợp với khóa học k
		public static bool ValidateEligibleCourse(Guna2DateTimePicker dtpDOB, string license, Guna2ComboBox cboCourses, Guna2HtmlToolTip toolTip)
		{
            int age = DateTime.Now.Year - dtpDOB.Value.Year;
            string errorMessage = GetErrorMessage(age, license);
            
            if (!string.IsNullOrEmpty(errorMessage))
            {
                FormHelper.ShowToolTip(cboCourses, toolTip, errorMessage);
                return false;
            }
            return true;
		}

		private static string GetErrorMessage(int age, string license)
		{
			switch (license)
			{
				case "B":
					if (age < 18)
						return $"{license} course requires 18 years old.";
					break;

				case "C":
					if (age < 21)
						return $"{license} course requires 21 years old.";
					break;

				case "D":
					if (age < 24)
						return $"{license} course requires 24 years old.";
					break;

				case "E":
					if (age < 27)
						return $"{license} course requires 27 years old.";
					break;
                default:
                    return $"Please select Course";
			}
            return "";
		}

        public static bool IsBeginningDateValid(Guna2DateTimePicker dtpDOB, Guna2DateTimePicker dtpBeginningDate, Guna2HtmlToolTip toolTip)
        {
            DateTime dateOf18thBirthday = dtpDOB.Value.AddYears(18);
            // Kiểm tra ngày nhận bằng có sau ngày đủ 18 tuổi không
            if (dtpBeginningDate.Value < dateOf18thBirthday)
            {
                FormHelper.ShowToolTip(dtpBeginningDate, toolTip, "The beginning date must be after the learner turns 18.");
                return false;
            }
            return true;
        }

        public static bool CheckIfLearnerCanUpgradeLicense(string license, string courseLicense, Guna2DateTimePicker beginningDate, Guna2HtmlToolTip toolTip)
        {
            int experience = GetExperienceYears(beginningDate.Value);
            string key = $"{license}-{courseLicense}";
            if (Constant.UpgradeLicenseRequirements.ContainsKey(key))
            {
                int requiredYears = Constant.UpgradeLicenseRequirements[key];
                if (experience < requiredYears)
                {
                    FormHelper.ShowToolTip(beginningDate, toolTip, $"No driving experience, license {license} to {courseLicense} requires {requiredYears} years of experience.");
                    return false;
                }
            }
            return true;
        }

        private static int GetExperienceYears(DateTime beginningDate)
        {
            var experience = DateTime.Now.Year - beginningDate.Year;
            if (beginningDate.AddYears(experience) > DateTime.Now.Date)
                experience--;
            return experience;
        }
    }
}
