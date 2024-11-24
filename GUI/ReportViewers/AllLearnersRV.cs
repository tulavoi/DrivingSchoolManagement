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
    public partial class AllLearnersRV : Form
    {
        public AllLearnersRV()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
            shadowForm.SetShadowForm(this);
        }

        private void LearnerRV_Load(object sender, EventArgs e)
        {
            var data = LearnerService.GetAllLearnersData();
            FormHelper.LoadReport(reportViewer1, "AllLearnersReport", data, "LearnerDataSet");
        }
    }
}
