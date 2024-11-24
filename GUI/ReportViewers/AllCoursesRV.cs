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
    public partial class AllCoursesRV : Form
    {
        public AllCoursesRV()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
            shadowForm.SetShadowForm(this);
        }

        private void CourseListRV_Load(object sender, EventArgs e)
        {
            var data = CourseService.GetAllCoursesData();
            FormHelper.LoadReport(reportViewer1, "AllCoursesReport", data, "CourseDataSet");
        }
    }
}
