using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    }
}
