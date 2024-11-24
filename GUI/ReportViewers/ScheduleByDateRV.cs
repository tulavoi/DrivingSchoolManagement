using BLL.Services;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.ReportViewers
{
    public partial class ScheduleByDateRV : Form
    {
        public ScheduleByDateRV()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
            shadowForm.SetShadowForm(this);
        }

        private void ScheduleByDateRV_Load(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now;
            this.reportViewer1.RefreshReport();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var data = ScheduleService.GetScheduleDataByDate(dtpStartDate.Value, dtpEndDate.Value);
            FormHelper.LoadReport(reportViewer1, "ScheduleByDateReport", data, "ScheduleDataSet");
        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpEndDate.Value.Date < dtpStartDate.Value.Date)
                dtpEndDate.Value = dtpStartDate.Value;
        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpEndDate.Value.Date < dtpStartDate.Value.Date)
                dtpStartDate.Value = dtpEndDate.Value;
        }
    }
}
