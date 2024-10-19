using BLL;
using DAL;
using GUI.Services;
using Guna.UI2.WinForms;
using System;
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
            this.LoadComboxes();
            this.LoadAllInvoice();
        }

        private void LoadComboxes()
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
                if (InvoiceService.EditInvoice(invoice))
                {
                    FormHelper.ShowNotify("Invoice edited successfully.");
                    this.LoadAllInvoice();
                }
                else
                    FormHelper.ShowError("Failed to edit invoice.");
            }
            else return;
        }

        private bool ValidateFields()
        {
            if (cboStatus.SelectedIndex < 1)
            {
                FormHelper.ShowToolTip(cboStatus, toolTip, "Please select status.");
                return false;
            }
            return true;
        }

        private void ToogleEditMode()
        {
            FormHelper.ToggleEditMode(ref this.isEditing, this.btnEdit, txtTotalAmount, txtNotes, cboStatus);
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
                Status = cboStatus.Text,
                TotalAmount = decimal.Parse(txtTotalAmount.Text),
                Notes = txtNotes.Text,
                Updated_At = DateTime.Now,
            };
        }

        private void btnOpenAddInvoiceForm_Click(object sender, EventArgs e)
        {
            FormHelper.OpenPopupForm(new CreateInvoiceForm());
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
            // Kiểm tra có dòng được chọn hay k
            // Nếu tag của selectedRow là Invoice thì gán data vào controls
            if (!this.HasSelectedRow()) return;

            var selectedRow = dgvInvoices.SelectedRows[0];

            if (selectedRow.Tag is Invoice selectedInvoice)
                this.AssignDataToControls(selectedInvoice);
        }

        private void AssignDataToControls(Invoice selectedInvoice)
        {
            // Gán các trường dữ liệu vào controls
            string invoiceCode = selectedInvoice.InvoiceCode;

            FormHelper.SetLabelID(lblInvoiceCode, invoiceCode);

            cboLearners.Text = selectedInvoice.Schedule.Learner.FullName;
            cboCourses.Text = selectedInvoice.Schedule.Course.CourseName;
            txtTotalAmount.Text = selectedInvoice.TotalAmount.ToString();
            dtpInvoiceDate.Value = selectedInvoice.Created_At.Value;
            cboStatus.Text = selectedInvoice.Status.ToString();
        }

        private void txtTotalAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormHelper.CheckNumericKeyPress(e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!this.HasSelectedRow()) return;

            if (!string.IsNullOrEmpty(lblInvoiceCode.Text)) return;

            if (this.ConfirmAction($"Are you sure to delete invoice '{lblInvoiceCode.Text}'?"))
            {
                if (InvoiceService.DeleteInvoice(lblInvoiceCode.Text))
                {
                    FormHelper.ShowNotify("Invoice deleted successfully.");
                    this.LoadAllInvoice();
                }
                else
                    FormHelper.ShowError("Failed to delete invoice.");
            }
        }

        private bool HasSelectedRow()
        {
            // Kiểm tra xem có dòng nào trong datagridview được chọn hay k
            return dgvInvoices.SelectedRows.Count > 0;
        }
    }
}
