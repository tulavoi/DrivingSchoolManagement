using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class AddVehicleForm : Form
    {
        public AddVehicleForm()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
        }

		private void AddVehicleForm_Load(object sender, EventArgs e)
		{
            this.shadowForm.SetShadowForm(this);

			// Nếu như khởi động form mà chkPassengerCar đang được chọn thì bật txtSeats
			if (this.chkPassengerCar.Checked) this.txtSeats.Enabled = true;
		}

		private void chkPassengerCar_CheckedChanged(object sender, EventArgs e)
		{
			// Optimze this code
			if (this.chkPassengerCar.Checked)
			{
				this.txtWeight.Enabled = false;

				this.chkTruck.Checked = false;
				this.txtSeats.Enabled = true;
			}
			else
			{
				if (!this.chkPassengerCar.Checked)
					this.txtSeats.Enabled = false;
			}
		}

		private void chkTruck_CheckedChanged(object sender, EventArgs e)
		{
			// Optimze this code
			if (this.chkTruck.Checked)
			{
				this.txtWeight.Enabled = true;

				this.chkPassengerCar.Checked = false;
				this.txtSeats.Enabled = false;
			}
			else
			{
				if (!this.chkTruck.Checked)
					this.txtWeight.Enabled = false;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
