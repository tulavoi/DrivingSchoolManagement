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
            try
            {
                var teachers = TeacherService.GetTeachersDTO();
                if (teachers.Rows.Count == 0)
                {
                    MessageBox.Show("Data null");
                    return;
                }

                reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.Reports.TeacherListReport.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                // Tạo và thêm ReportDataSource
                ReportDataSource rds = new ReportDataSource("TeacherDataSet", teachers);
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Làm mới ReportViewer
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }
    }
}
