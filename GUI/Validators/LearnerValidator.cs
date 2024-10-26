using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

}
