﻿using DAL;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ScheduleService
    {
        public static bool AddInvoice(Invoice invoice)
        {
            return InvoiceBLL.Instance.AddInvoice(invoice);
        }

        public static bool EditInvoice(Invoice invoice)
        {
            return InvoiceBLL.Instance.EditInvoice(invoice);
        }

        public static bool DeleteInvoice(string invoiceCode)
        {
            return InvoiceBLL.Instance.DeleteInvoice(invoiceCode);
        }

        public static void LoadAllSchedules(Guna2DataGridView dgv)
        {
            ScheduleBLL.Instance.LoadAllSchedules(dgv);
        }

        public static void SearchInvoices(Guna2DataGridView dgv, string keyword)
        {
            InvoiceBLL.Instance.SearchInvoices(dgv, keyword);
        }

        public static void FilterInvoicesByStatus(Guna2DataGridView dgv, string status)
        {
            InvoiceBLL.Instance.FilterInvoicesByStatus(dgv, status);
        }
    }
}
