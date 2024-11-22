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
    public partial class EligibleLearnersRV : Form
    {
        public EligibleLearnersRV()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
            shadowForm.SetShadowForm(this);
        }

        private void EligibleLearnersRV_Load(object sender, EventArgs e)
        {
            var data = LearnerService.GetEligibleLearnersData();
            FormHelper.LoadReport(reportViewer1, "EligibleLearnersReport", data, "LearnerDataSet");
        }
    }
}
