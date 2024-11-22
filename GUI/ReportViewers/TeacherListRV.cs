using BLL.Services;
using System;
using System.Windows.Forms;

namespace GUI.ReportViewers
{
    public partial class TeacherListRV : Form
    {
        public TeacherListRV()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
            shadowForm.SetShadowForm(this);
        }

        private void TeacherListRV_Load(object sender, EventArgs e)
        {
            var teachers = TeacherService.GetTeachersDTO();
            FormHelper.LoadReport(reportViewer1, "TeacherListReport", teachers, "TeacherDataSet");
        }
    }
}
