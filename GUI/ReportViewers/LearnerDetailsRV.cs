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
    public partial class LearnerDetailsRV : Form
    {
        private int _learnerID;

        public LearnerDetailsRV(int learnerID)
        {
            InitializeComponent();
            _learnerID = learnerID;
            FormHelper.ApplyRoundedCorners(this, 20);
            shadowForm.SetShadowForm(this);
        }

        private void LearnerDetailsRV_Load(object sender, EventArgs e)
        {
            var data = LearnerService.GetLearnerDetailData(_learnerID);
            FormHelper.LoadReport(reportViewer1, "LearnerDetails", data, "LearnerDataSet");
        }
    }
}
