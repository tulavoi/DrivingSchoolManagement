using BLL.Services.SendEmail;
using Guna.UI2.WinForms;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GUI
{
    public static class FormHelper
    {
        // Import CreateRoundRectRgn từ thư viện Gdi32.dll
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
           int nLeftRect,     // x-coordinate of upper-left corner
           int nTopRect,      // y-coordinate of upper-left corner
           int nRightRect,    // x-coordinate of lower-right corner
           int nBottomRect,   // y-coordinate of lower-right corner
           int nWidthEllipse, // width of ellipse
           int nHeightEllipse // height of ellipse
        );

        // Phương thức tạo border radius cho bất kỳ form nào
        public static void ApplyRoundedCorners(Form form, int radius)
        {
            form.FormBorderStyle = FormBorderStyle.None;
            form.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, form.Width, form.Height, radius, radius));
        }

        public static void ToggleEditMode(ref bool isEditing, Guna2Button button, params Control[] controls)
        {
            isEditing = !isEditing;
            EnableControls(isEditing, controls);
            button.Text = isEditing ? Constant.SAVE_MODE : Constant.EDIT_MODE;
        }

        private static void EnableControls(bool b, params Control[] controls)
        {
            foreach (var control in controls)
                control.Enabled = b;
        }

        public static void OpenFormDialog(Form form)
        {
            form.ShowDialog();
        }

        public static bool ConfirmDelete()
        {
            DialogResult rs = MessageBox.Show("Are you sure to delete?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            return rs == DialogResult.Yes;
        }

        public static void SetLabelID(Guna2Button lblID, string id)
        {
            lblID.Text = id;
        }

        public static void ClearDataGridViewRow(Guna2DataGridView dgv)
        {
            dgv.Rows.Clear();
        }

        public static void ShowError(string message)
        {
            MessageBox.Show($"{message}",
                            "Notify",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }

        public static void ShowNotify(string message)
        {
            MessageBox.Show($"{message}",
                            "Notify",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        public static DialogResult ShowConfirm(string message)
        {
            return MessageBox.Show($"{message}",
                            "Confirm",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
        }

        public static void ShowToolTip(Control control, Guna2HtmlToolTip toolTip, string message)
        {
            toolTip.Show(message, control);
        }

        public static void CheckNumericKeyPress(KeyPressEventArgs e)
        {
            // Kiểm tra nếu ký tự được nhập không phải là số hoặc không phải phím điều khiển (như phím Backspace)
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Ngăn người dùng nhập ký tự đó vào TextBox
                e.Handled = true;
            }
        }

        public static void ShowActionResult(bool result, string successMessage, string errorMessage)
        {
            if (result)
                FormHelper.ShowNotify(successMessage);
            else
                FormHelper.ShowError(errorMessage);
        }

        public static MailSettings GetMailSettings()
        {
            var builder = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            var mailSettings = configuration.GetSection("MailSettings").Get<MailSettings>();

            if (mailSettings == null) return null;

            return mailSettings;
        }

        public static bool IsMailSettingValid(MailSettings mailSetting)
        {
            return mailSetting != null &&
                   !string.IsNullOrEmpty(mailSetting.Mail) &&
                   !string.IsNullOrEmpty(mailSetting.Password);
        }
    }
}
