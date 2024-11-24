using BLL.Services;
using System;
using System.Windows.Forms;

namespace GUI.ReportViewers
{
    public partial class AllTeachersRV : Form
    {
        public AllTeachersRV()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
            shadowForm.SetShadowForm(this);
        }

        private void TeacherListRV_Load(object sender, EventArgs e)
        {
            var teachers = TeacherService.GetTeachersDTO();
            FormHelper.LoadReport(reportViewer1, "AllTeachersReport", teachers, "TeacherDataSet");
        }
    }
}
