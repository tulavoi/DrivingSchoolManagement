using Guna.Charts.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public static class CreatorChart
    {
        public static bool checkEmpty(DataTable dataTable)
        {
            return dataTable.Rows.Count > 0;
        }

        public static void ChartPie(GunaChart chart, DataTable data, string nameChart)
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

        public static void ChartBar(GunaChart chart, DataTable data, string nameChart)
        {
            if (checkEmpty(data))
            {
                chart.Datasets.Clear();
                //Chart configuration 
                chart.Legend.Display = false;
                chart.YAxes.GridLines.Display = false;
                chart.XAxes.Display = true;
                chart.YAxes.Display = true;
                chart.Title.Text = nameChart;
                
                var dataset = new GunaBarDataset();
                foreach (DataRow row in data.Rows)
                {
                    string monthYearLabel = Convert.ToDateTime(row["MonthYear"]).ToString("MM/yyyy");
                    double totalAmount = Convert.ToDouble(row["TotalAmount"]);
                    dataset.DataPoints.Add(monthYearLabel, totalAmount);
                }
                chart.Datasets.Add(dataset);
            }
            else
                MessageBox.Show("Not enough data.", "Error");
        }

        public static void ChartHorizontalBar(GunaChart chart, DataTable data, string nameChart)
        {
            if (checkEmpty(data))
            {
                chart.Datasets.Clear();
                //Chart configuration 
                chart.Legend.Display = false;
                chart.XAxes.Display = true;
                chart.YAxes.Display = true;
                chart.Title.Text = nameChart;

                var dataset = new GunaHorizontalBarDataset();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    dataset.DataPoints.Add(
                        Convert.ToString(data.Rows[i][0]),
                        Convert.ToInt32(data.Rows[i][1])
                    );
                }
                chart.Datasets.Add(dataset);
            }
            else
                MessageBox.Show("Insufficient data.", "Error");
        }
    }
}
