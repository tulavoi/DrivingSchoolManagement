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
    public partial class VehicleTypeBRV : Form
    {
        public VehicleTypeBRV()
        {
            InitializeComponent();
        }

        private void DisplayReport()
        {
            try
            {
                // Lấy toàn bộ danh sách phương tiện từ dịch vụ
                var Data = VehicleService.GetVehiclesTypeB(); // Lấy tất cả các phương tiện
                if (Data.Rows.Count == 0)
                {
                    MessageBox.Show("No data found");
                    return;
                }

                // Thiết lập báo cáo
                reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.Reports.VehicleReportType.rdlc"; // Đảm bảo báo cáo là loại đúng
                reportViewer1.LocalReport.DataSources.Clear();

                // Tạo và thêm ReportDataSource
                ReportDataSource rds = new ReportDataSource("VehicleDataSet", Data); // "VehicleDataSet" là tên DataSet trong báo cáo
                reportViewer1.LocalReport.DataSources.Add(rds);

                // Làm mới ReportViewer
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void VehicleTypeRV_Load(object sender, EventArgs e)
        {

            shadowForm.SetShadowForm(this);
            FormHelper.ApplyRoundedCorners(this, 20);

            // Gọi phương thức để lấy dữ liệu nợ và hiển thị báo cáo
            this.DisplayReport();
        }
    }
}
