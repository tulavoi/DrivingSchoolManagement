using BLL.Services;
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
    public partial class ScheduleDetailRV : Form
    {
        private int _scheduleID;

        public ScheduleDetailRV(int scheduleID)
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
            shadowForm.SetShadowForm(this);
            _scheduleID = scheduleID;
        }

        private void ScheduleReport_Load(object sender, EventArgs e)
        {
            var data = ScheduleService.GetScheduleDetailData(_scheduleID);
            FormHelper.LoadReport(reportViewer1, "ScheduleDetailReport", data, "ScheduleDataSet");
        }
    }
}
