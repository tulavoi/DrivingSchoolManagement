using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GUI.Validators
{
    public static class CourseValidator
    {
        public static bool ValidateCourseName(Guna2TextBox txtName, Guna2HtmlToolTip toolTip)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                toolTip.Show("Course name is required.", txtName);
                txtName.Focus();
                return false;
            }

            if (txtName.Text.Length < 3)
            {
                toolTip.Show("Course name must be at least 3 characters long.", txtName);
                txtName.Focus();
                return false;
            }

            toolTip.Hide(txtName);
            return true;
        }

        public static bool ValidateFee(Guna2TextBox txtFee, Guna2HtmlToolTip toolTip)
        {
            if (string.IsNullOrWhiteSpace(txtFee.Text))
            {
                toolTip.Show("Fee is required.", txtFee);
                txtFee.Focus();
                return false;
            }

            if (!decimal.TryParse(txtFee.Text, out decimal fee) || fee <= 0)
            {
                toolTip.Show("Fee must be a positive number.", txtFee);
                txtFee.Focus();
                return false;
            }

            toolTip.Hide(txtFee);
            return true;
        }

        public static bool ValidateDuration(Guna2TextBox txtDurationInHours, Guna2HtmlToolTip toolTip)
        {
            if (string.IsNullOrWhiteSpace(txtDurationInHours.Text))
            {
                toolTip.Show("Duration is required.", txtDurationInHours);
                txtDurationInHours.Focus();
                return false;
            }

            if (!int.TryParse(txtDurationInHours.Text, out int duration) || duration <= 0)
            {
                toolTip.Show("Duration must be a positive integer.", txtDurationInHours);
                txtDurationInHours.Focus();
                return false;
            }

            toolTip.Hide(txtDurationInHours);
            return true;
        }
    }
}
