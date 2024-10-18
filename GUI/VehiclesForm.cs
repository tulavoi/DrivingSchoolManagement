using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Compilation;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace GUI
{
    public partial class VehiclesForm : Form
    {
		#region Properties
		private bool isEditing_BasicDetails = false;
        private bool isEditing_MaintenanceDetails = false;
		#endregion

		public VehiclesForm()
        {
            InitializeComponent();
			FormHelper.ApplyRoundedCorners(this, 20);
        }

		private void btnEdit_BasicDetail_Click(object sender, EventArgs e)
        {
			if (this.chkTruck.Checked) this.txtWeight.Enabled = true;

			if (this.chkPassengerCar.Checked) this.txtSeats.Enabled = true;

			FormHelper.ToggleEditMode(ref isEditing_BasicDetails, this.btnEdit_BasicDetail, this.txtCarName, this.txtCarNo, this.dtpManuYear, this.chkTruck, this.chkPassengerCar);
		}

		private void btnEdit_Maintenance_Click(object sender, EventArgs e)
        {
			FormHelper.ToggleEditMode(ref this.isEditing_MaintenanceDetails, this.btnEdit_MaintenanceDetail, this.cboStatus, this.txtNotes);
		}

		private void btnOpenAddVehicleForm_Click(object sender, EventArgs e)
		{
			FormHelper.OpenPopupForm(new AddVehicleForm());
		}

		private void chkTruck_CheckedChanged(object sender, EventArgs e)
		{
			this.txtWeight.Enabled = this.chkTruck.Checked;

			if (this.chkTruck.Checked)
			{
				this.chkPassengerCar.Checked = false;
				this.txtSeats.Enabled = false;
			}
		}

		private void chkPassengerCar_CheckedChanged(object sender, EventArgs e)
		{
			txtSeats.Enabled = chkPassengerCar.Checked;

			if (chkPassengerCar.Checked)
			{
				chkTruck.Checked = false;
				txtWeight.Enabled = false;
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (FormHelper.ConfirmDelete())
			{

			}
		}
	}
}
