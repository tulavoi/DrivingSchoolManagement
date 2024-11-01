using BLL;
using BLL.Services.SendEmail;
using DAL;
using BLL.Services;
using Guna.UI2.WinForms;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class InvoicesForm : Form
    {
        #region Properties
        private bool isEditing = false;

        private static InvoicesForm instance;
        public static InvoicesForm Instance
        {
            get
            {
                if (instance == null) instance = new InvoicesForm();
                return instance;
            }
        }

        #endregion

        public InvoicesForm()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
        }

        public void InvoicesForm_Load(object sender, EventArgs e)
        {
            this.LoadComboboxes();
            this.LoadAllInvoice();

            //FormHelper.SetDateTimePickerMaxValue(dtpInvoiceDate);
        }

        private void LoadComboboxes()
        {
            // Phải load data của Learners, Courses vào combobox trước,
            // nếu không thì sẽ k gán được LearnerName, CourseName từ dgv vào cbo
            ComboboxService.AssignLearnersToCombobox(cboLearners);
            ComboboxService.AssignCoursesToCombobox(cboCourses);
        }

        public void LoadAllInvoice()
        {
            // Lấy tất cả dữ liệu Invoice bỏ vào datagridview
            InvoiceService.LoadAllInvoices(dgvInvoices);
            this.UpdateControlsWithSelectedRowData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!this.InSaveMode())
            {
                this.ToogleEditMode();
                return;
            }

            if (!this.ValidateFields()) return;

            if (this.ConfirmAction($"Are you sure to edit invoice '{lblInvoiceCode.Text}'?"))
            {
                Invoice invoice = this.GetInvoice();
                var result = InvoiceService.EditInvoice(invoice);
                FormHelper.ShowActionResult(result, "Invoice edited successfully.", "Failed to edit invoice.");
            }
            this.ToogleEditMode();
            this.LoadAllInvoice();
        }

        private bool ValidateFields()
        {
            if (cboPaymentStatus.SelectedIndex < 1)
            {
                FormHelper.ShowToolTip(cboPaymentStatus, toolTip, "Please select status.");
                return false;
            }
            return true;
        }

        private void ToogleEditMode()
        {
            FormHelper.ToggleEditMode(ref this.isEditing, this.btnEdit, txtTotalAmount, txtNotes, cboPaymentStatus);
        }

        private bool InSaveMode()
        {
            return btnEdit.Text == Constant.SAVE_MODE;
        }

        private bool ConfirmAction(string message)
        {
            DialogResult result = FormHelper.ShowConfirm(message);
            return result == DialogResult.Yes;
        }

        private Invoice GetInvoice()
        {
            return new Invoice
            {
                InvoiceCode = lblInvoiceCode.Text,
                StatusID = Constant.StatusID_Active,
                //ScheduleID = this.GetSchedule(),
                IsPaid = this.GetStatusPayment(),
                TotalAmount = int.Parse(txtTotalAmount.Text),
                Notes = txtNotes.Text,
                Updated_At = DateTime.Now,
            };
        }

        private int? GetSchedule()
        {
            int courseID = Convert.ToInt32(cboCourses.SelectedValue.ToString());
            var schedule = ScheduleService.GetSchedule(courseID);
            return schedule.ScheduleID;
        }

        private bool? GetStatusPayment()
        {
            if (cboPaymentStatus.Text == "Paid")
                return true;
            if (cboPaymentStatus.Text == "Pending")
                return false;
            return false;
        }

        private void btnOpenAddInvoiceForm_Click(object sender, EventArgs e)
        {
            FormHelper.OpenFormDialog(new CreateInvoiceForm());
            this.LoadAllInvoice(); // Sau khi đóng CreateInvoiceForm sẽ hiển thị lại dữ liệu trong dgv
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Xóa các dòng cũ trong dgv, lấy keyword từ txtSearch
            // Load dữ liệu search được, gán các thông tin của dòng đc chọn trong dgv sang controls
            FormHelper.ClearDataGridViewRow(dgvInvoices);

            string keyword = txtSearch.Text.ToLower();

            InvoiceService.SearchInvoices(dgvInvoices, keyword);
            this.UpdateControlsWithSelectedRowData();
        }

        private void cboStatus_Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Xóa các dòng hiện tại, nếu chọn "Status" load all dữ liệu
            // Lọc các invoice bằng status, cập nhật dữ liệu vào controls
            FormHelper.ClearDataGridViewRow(dgvInvoices);

            if (cboStatus_Filter.SelectedIndex < 1)
                this.LoadAllInvoice();
            else
            {
                string status = cboStatus_Filter.SelectedItem.ToString();
                InvoiceService.FilterInvoicesByStatus(dgvInvoices, status);
                this.UpdateControlsWithSelectedRowData();
            }
        }

        private void dgvInvoices_SelectionChanged(object sender, EventArgs e)
        {
            this.UpdateControlsWithSelectedRowData();
        }

        private void UpdateControlsWithSelectedRowData()
        {
            var invoice = this.GetSelectedInvoice();
            this.AssignDataToControls(invoice);
        }

        private Invoice GetSelectedInvoice()
        {
            if (!FormHelper.HasSelectedRow(dgvInvoices)) return null;

            var selectedRow = dgvInvoices.SelectedRows[0];

            if (selectedRow.Tag is Invoice selectedInvoice) return selectedInvoice;

            return null;
        }

        private void AssignDataToControls(Invoice selectedInvoice)
        {
            if (selectedInvoice == null) return;

            // Gán các trường dữ liệu vào controls
            string invoiceCode = selectedInvoice.InvoiceCode;

            FormHelper.SetLabelID(lblInvoiceCode, invoiceCode);

            //cboLearners.Text = selectedInvoice.Enrollment.Learner.FullName;
            //cboCourses.Text = selectedInvoice.Enrollment.Course.CourseName;
            txtTotalAmount.Text = selectedInvoice.TotalAmount.ToString();
            dtpInvoiceDate.Value = selectedInvoice.Created_At.Value;
            if (selectedInvoice.IsPaid == false)
                cboPaymentStatus.Text = "Pending";
            if (selectedInvoice.IsPaid == true)
                cboPaymentStatus.Text = "Paid";
        }

        private void txtTotalAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormHelper.CheckNumericKeyPress(e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!FormHelper.HasSelectedRow(dgvInvoices)) return;

            if (string.IsNullOrEmpty(lblInvoiceCode.Text)) return;

            if (this.ConfirmAction($"Are you sure to delete invoice '{lblInvoiceCode.Text}'?"))
            {
                var result = InvoiceService.DeleteInvoice(lblInvoiceCode.Text);
                FormHelper.ShowActionResult(result, "Invoice deleted successfully.", "Failed to delete invoice.");
                this.LoadAllInvoice();
            }
        }

        private async void btnSendInvoiceByMail_Click(object sender, EventArgs e)
        {
            var invoice = this.GetSelectedInvoice(); // Lấy ra invoice đang được chọn
            var mailContent = this.CreateMailContent(invoice); // Tạo nội dung email
            var result = await FormHelper.SendMailAsync(mailContent);

            FormHelper.ShowActionResult(result, "Invoice sent successfully.", "Failed to send invoice.");
        }

        private MailContent CreateMailContent(Invoice invoice)
        {
            return new MailContent
            {
                //To = invoice.Enrollment.Learner.Email,
                Subject = $"Course Invoice: {lblInvoiceCode.Text}",
                Body = $"<h1>{txtMessage.Text}</h1>"
            };
        }
    }
}
