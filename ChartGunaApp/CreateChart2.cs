using Guna.Charts.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartGunaApp
{
    public class CreateChart2
    {
        public bool checkEmpty(DataTable dataTable)
        {
            return dataTable.Rows.Count > 0;
        }

        public void ChartPie(GunaChart chart, DataTable data, string nameChart)
        {
            if (checkEmpty(data))
            {
                chart.Datasets.Clear();

                //config chart
                chart.Legend.Position = LegendPosition.Right;
                chart.Legend.Display = true;
                chart.XAxes.Display = false;
                chart.YAxes.Display = false;
                chart.Title.Text = nameChart;

                var dataset = new GunaPieDataset();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    dataset.DataPoints.Add(
                        Convert.ToString(data.Rows[i][0]),
                        Convert.ToDouble(data.Rows[i][1])
                    );
                }
                chart.Datasets.Add(dataset);
            }
            else
                MessageBox.Show("There is not enough data.", "Error");
        }
    }
}
