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
    public partial class ScheduleInDayRV : Form
    {
        private string _learnerName;
        private int _learnerID;
        private DateTime _sessionDate;

        public ScheduleInDayRV(string learnerName, int learnerID, DateTime sessionDate)
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
            shadowForm.SetShadowForm(this);
            _learnerName = learnerName;
            _learnerID = learnerID;
            _sessionDate = sessionDate;
        }

        private void ScheduleInDateRV_Load(object sender, EventArgs e)
        {
            dtpSessionDate.Value = _sessionDate;
            lblLearnerName.Text = _learnerName;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var data = ScheduleService.GetScheduleInDayData(_learnerID, dtpSessionDate.Value);
            FormHelper.LoadReport(reportViewer1, "ScheduleInDayReport", data, "ScheduleDataSet");
        }
    }
}
