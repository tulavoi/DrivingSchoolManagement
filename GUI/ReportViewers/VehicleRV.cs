using BLL.Services;
using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Windows.Forms;

namespace GUI.ReportViewers
{
    public partial class VehicleRV : Form
    {
        private int _vehicleID;

        public VehicleRV(int vehicleID)
        {
            InitializeComponent();
            _vehicleID = vehicleID;
        }

        private void VehicleRV_Load(object sender, EventArgs e)
        {
            shadowForm.SetShadowForm(this);
            FormHelper.ApplyRoundedCorners(this, 20);

            this.DisplayReport();
        }

        private void DisplayReport()
        {
            try
            {
                // Lấy dữ liệu xe từ dịch vụ
                var vehicleData = VehicleService.GetVehicleByVehicleID(_vehicleID);
                if (vehicleData.Rows.Count == 0)
                {
                    MessageBox.Show("Data is empty");
                    return;
                }

                // Thiết lập báo cáo
                reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.Reports.Vehicle.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                // Tạo và thêm ReportDataSource
                ReportDataSource rds = new ReportDataSource("VehicleDataSet", vehicleData);
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Làm mới ReportViewer
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
