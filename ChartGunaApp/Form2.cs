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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var creatorChart = new CreateChart2();
            creatorChart.ChartPie(gunaChart1, dataSet(), "Statistics of employee salaries (ChartPie)");
        }

        private DataTable dataSet()
        {
            DataTable dataTable = new DataTable("MyTable");
            dataTable.Columns.Add("Gender", typeof(string));
            dataTable.Columns.Add("Count", typeof(int));

            dataTable.Rows.Add("Male", 20);
            dataTable.Rows.Add("Female", 12);
            return dataTable;
        }
    }
}
