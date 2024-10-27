using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Validators
{
    public static class ScheduleValidator
    {
        public static bool CheckRequiredAndShowToolTip(Guna2ComboBox cbo, Guna2HtmlToolTip toolTip)
        {
            if (cbo.SelectedIndex < 1)
            {
                FormHelper.ShowToolTip(cbo, toolTip, $"Please select {cbo.Tag}");
                cbo.Focus();
                return false;
            }
            return true;
        }

        public static bool ValidateAgeOfLearner(Guna2ComboBox cbo, int age, string license, Guna2HtmlToolTip toolTip)
        {
            if (license == "C" && age < 21)
            {
                FormHelper.ShowToolTip(cbo, toolTip, $"{cbo.Tag} must be 21 years old for a {license} license.");
                return false;
            }
            
            if (license == "D" && age < 24)
            {
                FormHelper.ShowToolTip(cbo, toolTip, $"{cbo.Tag} must be 24 years old for a {license} license.");
                return false;
            }

            if (license == "E" && age < 27)
            {
                FormHelper.ShowToolTip(cbo, toolTip, $"{cbo.Tag} must be 27 years old for a {license} license.");
                return false;
            }
            return true;
        }

        public static bool IsEligibleForLicenseE(Guna2ComboBox cbo, int? learnerCurrLicenseID, string license, Guna2HtmlToolTip toolTip)
        {
            if (license == "E" && learnerCurrLicenseID < Constant.LicenseID_D)
            {
                FormHelper.ShowToolTip(cbo, toolTip, $"{cbo.Tag} is not eligible for the E license.");
                return false;
            }
            return true;
        }
    }
}
