using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Services.SendEmail;
using Guna.UI2.WinForms;

namespace GUI
{
    public partial class MainForm : Form
    {
        private List<Guna2Button> menuButtons;

        public MainForm()
        {
            InitializeComponent();

            FormHelper.ApplyRoundedCorners(this, 20);

            this.GenerateButtonList();

            this.btnDashBoard_Click(this.btnDashboard, EventArgs.Empty);
        }

        private void GenerateButtonList()
        {
            this.menuButtons = new List<Guna2Button>
            {
                this.btnDashboard,
                this.btnLearners,
                this.btnTeachers,
                this.btnCourses,
                this.btnInvoice,
                this.btnVehicles,
                this.btnSchedules,
                this.btnAccounts,
				this.btnPayments,
			};
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.shadowMainForm.SetShadowForm(this);
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDashBoard_Click(object sender, EventArgs e)
        {
            Container(new DashBoardForm(), this.btnDashboard.Text, this.btnDashboard);
        }

        private void btnLearners_Click(object sender, EventArgs e)
        {
            Container(new LearnersForm(), this.btnLearners.Text, this.btnLearners);
        }

        private void btnTeachers_Click(object sender, EventArgs e)
        {
            Container(new TeachersForm(), this.btnTeachers.Text, this.btnTeachers);
        }

        private void btnCourses_Click(object sender, EventArgs e)
        {
            Container(new CoursesForm(), this.btnCourses.Text, this.btnCourses);
        }

        private void btnVehicles_Click(object sender, EventArgs e)
        {
            Container(new VehiclesForm(), this.btnVehicles.Text, this.btnVehicles);
        }

		public void btnSchedules_Click(object sender, EventArgs e)
		{
			Container(new ScheduleForm(), this.btnSchedules.Text, this.btnSchedules);
		}

		private void btnInvoice_Click(object sender, EventArgs e)
		{
			Container(new InvoicesForm(), this.btnInvoice.Text, this.btnInvoice);
		}

        private void btnAccounts_Click(object sender, EventArgs e)
        {
            Container(new AccountsForm(), this.btnAccounts.Text, this.btnAccounts);
        }

        private void btnPayments_Click(object sender, EventArgs e)
		{
			Container(new PaymentsForm(), this.btnPayments.Text, this.btnPayments);
		}

		private void btnLogout_Click(object sender, EventArgs e)
		{
            this.Hide();
            LoginForm frm = new LoginForm();
            frm.Show();
		}

		public bool Container(object form, string nameButton, Guna2Button curButton)
        {
            this.UpdateButtonStates(curButton);

			try
			{
				this.SetNameLabel(nameButton);

				if (pnlContainer.Controls.Count > 0)
					pnlContainer.Controls.Clear();

				Form frm = (Form)form;
				frm.TopLevel = false;
				frm.FormBorderStyle = FormBorderStyle.None;
				frm.Dock = DockStyle.Fill;

				pnlContainer.Controls.Add(frm);
				pnlContainer.Tag = frm;
				frm.Show();

				return frm.Dock == DockStyle.Fill;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
		}

        private void UpdateButtonStates(Guna2Button curButton)
        {
			foreach (var button in this.menuButtons)
			{
				// Nếu button là curButton => checked = true
				// Nếu button k là curButton => thì checked = false
				button.Checked = button == curButton;
			}
		}

        private void CheckButtonClicked(Guna2Button curButton)
        {
            curButton.Checked = true;
        }

        private void SetNameLabel(string nameButton)
        {
            this.lblNameForm.Text = nameButton;
        }
	}
}
