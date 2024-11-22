using BLL.Services;
using GUI.ReportViewers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.Forms
{
    public partial class PaymentsDebt : Form
    {
        public PaymentsDebt()
        {
            InitializeComponent();
            FormHelper.ApplyRoundedCorners(this, 20);
            shadowForm.SetShadowForm(this);
        }

        private void PaymentDebtFrom_Load(object sender, EventArgs e)
        {
            this.LoadAllPayments();
        }

        public void LoadAllPayments()
        {
            PaymentService.LoadAllPaymentDebt(dgvPayments);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {    
                // Gọi phương thức lấy danh sách nợ
                ListPaymentDebt paymentDebt = new ListPaymentDebt();
                paymentDebt.Show();            
        }
    }
}
