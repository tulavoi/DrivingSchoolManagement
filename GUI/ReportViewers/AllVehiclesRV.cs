using BLL.Services;
using Guna.Charts.WinForms;
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
    public partial class AllVehiclesRV : Form
    {
        public AllVehiclesRV()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
            shadowForm.SetShadowForm(this);
        }

        private void AllVehiclesRV_Load(object sender, EventArgs e)
        {
            var vehicles_B = VehicleService.GetVehicleForLicense("B");
            var vehicles_C = VehicleService.GetVehicleForLicense("C");
            var vehicles_D = VehicleService.GetVehicleForLicense("D");
            var vehicles_E = VehicleService.GetVehicleForLicense("E");
            var allVehicles = VehicleService.GetAllVehiclesData();

            try
            {
                // Thiết lập ReportViewer
                reportViewer1.LocalReport.ReportEmbeddedResource = $"GUI.Reports.AllVehiclesReport.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                // Tạo và thêm ReportDataSource
                ReportDataSource rds_B = new ReportDataSource("VehicleBDataSet", vehicles_B);
                ReportDataSource rds_C = new ReportDataSource("VehicleCDataSet", vehicles_C);
                ReportDataSource rds_D = new ReportDataSource("VehicleDDataSet", vehicles_D);
                ReportDataSource rds_E = new ReportDataSource("VehicleEDataSet", vehicles_E);
                ReportDataSource rds_All = new ReportDataSource("AllVehiclesDataSet", allVehicles);
                reportViewer1.LocalReport.DataSources.Add(rds_B);
                reportViewer1.LocalReport.DataSources.Add(rds_C);
                reportViewer1.LocalReport.DataSources.Add(rds_D);
                reportViewer1.LocalReport.DataSources.Add(rds_E);
                reportViewer1.LocalReport.DataSources.Add(rds_All);

                // Làm mới ReportViewer
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
