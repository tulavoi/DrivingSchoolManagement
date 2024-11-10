using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartGunaApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private DataTable dataSet()
        {
            // Create a DataTable
            DataTable dataTable = new DataTable("MyTable");

            // Adding columns
            dataTable.Columns.Add("Employee", typeof(string));
            dataTable.Columns.Add("Salary", typeof(double));

            // Adding lines
            dataTable.Rows.Add("Petya", 100);
            dataTable.Rows.Add("Vasya", 200);
            dataTable.Rows.Add("Oleg", 150);
            dataTable.Rows.Add("Denis", 170);

            return dataTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var creatorChart = new CreatorChart();
            creatorChart.ChartPie(gunaChart1, dataSet(), "Statistics of employee salaries (ChartPie)");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var creatorChart = new CreatorChart();
            creatorChart.ChartPolar(gunaChart1, dataSet(), "Statistics of employee salaries (ChartPolar)");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var creatorChart = new CreatorChart();
            creatorChart.ChartBar(gunaChart1, dataSet(), "Statistics of employee salaries (ChartBar)");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var creatorChart = new CreatorChart();
            creatorChart.ChartHorizontalBar(gunaChart1, dataSet(), "Statistics of employee salaries (ChartHorizontalBar)");
        }
    }
}
