using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
