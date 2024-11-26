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
    public partial class SchedulesOfTeacherRV : Form
    {
        private int _teacherID;
        private string _teacherName;

        public SchedulesOfTeacherRV(int teacherID, string teacherName)
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
            shadowForm.SetShadowForm(this);
            _teacherID = teacherID;
            _teacherName = teacherName;
        }

        private void SchedulesOfTeacherRV_Load(object sender, EventArgs e)
        {
            lblTeacherName.Text = _teacherName;
            dtpStartDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var data = ScheduleService.GetSchedulesOfTeacher(_teacherID, dtpStartDate.Value, dtpEndDate.Value);
            FormHelper.LoadReport(reportViewer1, "SchedulesOfTeacherReport", data, "ScheduleDataSet");
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpEndDate.Value.Date < dtpStartDate.Value.Date)
                dtpEndDate.Value = dtpStartDate.Value;
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpEndDate.Value.Date < dtpStartDate.Value.Date)
                dtpStartDate.Value = dtpEndDate.Value;
        }
    }
}
