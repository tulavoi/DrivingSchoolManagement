using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Guna.UI2.WinForms;
using BLL.Services;

namespace GUI
{
	public partial class CreateInvoiceForm : Form
	{
        #region Properties
        private int? enrollmentID;

        #endregion

        public CreateInvoiceForm()
		{
			InitializeComponent();
			FormHelper.ApplyRoundedCorners(this, 20);
		}

		private void CreateInvoiceForm_Load(object sender, EventArgs e)
		{
			shadowForm.SetShadowForm(this);
            //string status = "Active";
            //ComboboxService.AssignCoursesToCombobox(cboCourses, status); // Phải gán các khóa học chưa được tạo hóa đơn vào combobox, sửa lại
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!this.ValidateFields()) return;

            Invoice invoice = this.GetInvoice();

            if (InvoiceService.AddInvoice(invoice))
                FormHelper.ShowNotify("Invoice added successfully.");
            else
                FormHelper.ShowError("Failed to add invoice.");
        }

        private bool ValidateFields()
        {
            if (cboCourses.SelectedIndex < 1)
            {
                FormHelper.ShowToolTip(cboCourses, toolTip, "Please select course.");
                return false;
            }
            return true;
        }

        private Invoice GetInvoice()
        {
            return new Invoice()
            {
                EnrollmentID = this.enrollmentID,
                InvoiceCode = this.GetInvoiceCode(),
                TotalAmount = this.GetTotalAmount(),
                IsPaid = Constant.DefaultInvoiceStatus,
                Notes = "",
				StatusID = Constant.StatusID_Active,
                Created_At = DateTime.Now
            };
        }

        private int? GetTotalAmount()
        {
            string courseName = cboCourses.Text;
            string[] splitCourseName = courseName.Split('-');
            string license = splitCourseName[0];

            if (license == "B")
                return Constant.Tuition_B;
            if (license == "C")
                return Constant.Tuition_C;
            if (license == "D")
                return Constant.Tuition_D;
            if (license == "E")
                return Constant.Tuition_E;

            return null;
        }

        private string GetInvoiceCode()
        {
            return "INV-" + DateTime.Now.ToString("HHmmssddMMyy");
        }

        private void cboCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCourses.SelectedIndex < 1) return;
            int courseID = Convert.ToInt32(cboCourses.SelectedValue);

            var enrollment = EnrollmentService.GetEnrollmentByCourseID(courseID);
            this.AssignLearnerNameToTextBox(enrollment);
		}

		private void AssignLearnerNameToTextBox(Enrollment enrollment)
        {
            if (enrollment == null)
            {
                FormHelper.ShowNotify("This course has not been enrolled, please choose another course.");
                this.ResetControls(cboCourses, txtLearnerName);
                return;
            }
            txtLearnerName.Text = enrollment.Learner.FullName;
            this.enrollmentID = enrollment.EnrollmentID;
		}

		private void ResetControls(Guna2ComboBox cboCourses, Guna2TextBox txtLearnerName)
        {
            cboCourses.SelectedIndex = 0;
            txtLearnerName.Text = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
