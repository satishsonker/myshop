﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myshop.Areas.SalesManagement.Models
{
    public class DashboardModel
    {
        public int TotalSales { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalProduct { get; set; }
        public decimal TotalQty { get; set; }
        public decimal MonthlyIncome { get; set; }
        public decimal TotalIncomeTillNow { get; set; }
        public List<DashboardInvoiceModel> InvoiceMDetals { get; set; }
        public List<MostSallingProduct> SallingProducts { get; set; }
    }

    public class DashboardInvoiceModel
    {
        public int InvoiceNo { get; set; }
        public string CustomerName { get; set; }
        public decimal Amount { get; set; }
        public DateTime InvoiceDate { get; set; }
        public bool IsRefund { get; set; }
        public int TotalInvoice { get; set; }
    }
    public class MostSallingProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int TotalQty { get; set; }
    }
}