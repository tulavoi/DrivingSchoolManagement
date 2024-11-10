using DAL;
using Guna.UI2.WinForms;
using MailKit;
using System.Collections.Generic;
using System.Security.Principal;

namespace GUI.Validators
{
    public static class AccountValidator
    {
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

        public static bool ValidatePermission(Guna2ComboBox cbo, Guna2HtmlToolTip toolTip)
        {
            if (cbo.SelectedIndex < 1)
            {
                FormHelper.ShowToolTip(cbo, toolTip, $"Please select {cbo.Tag}");
                return false;
            }
            return true;
        }

        public static bool ValidateRequiedPass(List<Guna2TextBox> textBoxes, Guna2HtmlToolTip toolTip)
        {
            foreach (var txt in textBoxes)
                if (!ValidatorHelper.CheckRequiredAndShowToolTip(txt, toolTip)) return false;

            return true;
        }

        public static bool ValidateOldPass(Guna2TextBox txt, Guna2HtmlToolTip toolTip, string oldPass)
        {
            if (txt.Text != oldPass)
            {
                FormHelper.ShowToolTip(txt, toolTip, $"{txt.Tag} is incorrect.");
                return false;
            }
            return true;
        }

        public static bool ValidateNewPass(Guna2TextBox txt, Guna2HtmlToolTip toolTip)
        {
            if (!ValidateLengthPass(txt, toolTip)) return false;

            return true;
        }

        public static bool ValidateConfirmPass(Guna2TextBox txt, Guna2HtmlToolTip toolTip, string newPass)
        {
            if (!ValidateLengthPass(txt, toolTip)) return false;
            
            if (txt.Text != newPass)
            {
                FormHelper.ShowToolTip(txt, toolTip, $"{txt.Tag} does not match");
                return false;
            }
            return true;
        }

        public static bool ValidateLengthPass(Guna2TextBox txt, Guna2HtmlToolTip toolTip)
        {
            if (txt.Text.Length < 6)
            {
                FormHelper.ShowToolTip(txt, toolTip, $"Password at least 6 characters.");
                return false;
            }
            return true;
        }
    }
}
