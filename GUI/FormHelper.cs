using BLL.Services.SendEmail;
using Guna.UI2.WinForms;
using Microsoft.Extensions.Configuration;
using Microsoft.Reporting.WinForms;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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

        public static void CheckLetterKeyPress(KeyPressEventArgs e, Guna2TextBox txt)
        {
            //if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            //{
            //    // Ngăn người dùng nhập ký tự đó vào TextBox
            //    e.Handled = true;
            //}

            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ngăn không cho nhập ký tự
            }
            // Kiểm tra nếu ký tự vừa nhập là khoảng trắng và ký tự trước đó cũng là khoảng trắng
            else if (char.IsWhiteSpace(e.KeyChar) && txt.Text.EndsWith(" "))
            {
                e.Handled = true; // Ngăn không cho nhập khoảng trắng liên tiếp
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

        public static async Task<bool> SendMailAsync(MailContent mailContent)
        {
            var mailSetting = FormHelper.GetMailSettings();

            if (!FormHelper.IsMailSettingValid(mailSetting))
            {
                FormHelper.ShowError("MailSetting invalid.");
                return false;
            }

            var sendMailService = new SendMailService(mailSetting); // Khởi tạo SendMailService

            return await Task.Run(() => sendMailService.SendMail(mailContent));
        }

        public static void SetDateTimePickerMaxValue(params Guna2DateTimePicker[] dtps)
        {
            foreach (var dtp in dtps)
                dtp.MaxDate = DateTime.Now;
        }

        public static int GetObjectID(string text)
        {
            string[] parts = text.Split(' ');
            int id = Convert.ToInt32(parts[1]);
            return id;
        }

        public static bool HasSelectedRow(Guna2DataGridView dgv)
        {
            // Kiểm tra xem có dòng nào trong datagridview được chọn hay k
            return dgv.SelectedRows.Count > 0;
        }

        public static bool HasSelectedItem(Guna2ComboBox cbo)
        {
            return cbo.SelectedIndex > 0;
        }

		public static void FocusControl(Control control)
		{
            control.Focus();
		}

        public static void PrintReport(System.Data.DataSet ds, string reportPath)
        {
            using (ReportViewer reportViewer = new ReportViewer())
            {
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.LocalReport.ReportPath = reportPath;
                ReportDataSource rds = new ReportDataSource("InvoiceDataSet", ds.Tables[0]);
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(rds);

                // Hiển thị hoặc in báo cáo
                reportViewer.RefreshReport();
            }
        }

        public static void LoadReport(ReportViewer reportViewer, string reportFileName, DataTable data, string dataSetName)
        {
            try
            {
                // Kiểm tra dữ liệu
                if (data == null || data.Rows.Count == 0)
                {
                    MessageBox.Show("No data available to display.");
                    return;
                }

                // Thiết lập ReportViewer
                reportViewer.LocalReport.ReportEmbeddedResource = $"GUI.Reports.{reportFileName}.rdlc";
                reportViewer.LocalReport.DataSources.Clear();

                // Tạo và thêm ReportDataSource
                ReportDataSource rds = new ReportDataSource(dataSetName, data);
                reportViewer.LocalReport.DataSources.Add(rds);

                // Làm mới ReportViewer
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
