using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
	public partial class PaymentsForm : Form
	{
		#region Properties
		private bool isEditing = false;

		#endregion

		public PaymentsForm()
		{
			InitializeComponent();
			FormHelper.ApplyRoundedCorners(this, 20);
		}

		private void btnOpenAddPaymentForm_Click(object sender, EventArgs e)
		{
			FormHelper.OpenPopupForm(new AddPaymentForm());
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			FormHelper.ToggleEditMode(ref this.isEditing, this.btnEdit, cboInvoices, dtpPaymentDate, txtAmount, cboMethods, cboLearners, txtSearchLearner);
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (FormHelper.ConfirmDelete())
			{

			}
		}
	}
}
