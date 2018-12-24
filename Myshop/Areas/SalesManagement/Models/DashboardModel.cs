using System;
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
        public List<TopCustomersData> TopCustomersData { get; set; }
    }

    public class TopCustomersData
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int TotalPurchase { get; set; }
        public int TotalPurchaseProduct { get; set; }
        public decimal TotalPurchaseAmount { get; set; }
    }

    public class DashboardInvoiceModel
    {
        public int InvoiceNo { get; set; }
        public string CustomerName { get; set; }
        public string PaymentMode { get; set; }
        public decimal Amount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal RefundAmount { get; set; }
        public DateTime InvoiceDate { get; set; }
        public bool IsRefund { get; set; }
        public bool IsCancelled { get; set; }
        public int TotalInvoice { get; set; }
    }
    public class MostSallingProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int TotalQty { get; set; }
        public int TotalRecord { get; set; }
    }
}