using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myshop.Areas.ExpenseManagement.Models
{
    public class ReportsModel
    {
    }
    public class BalanceModel
    {
        public decimal BalanceAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ExpId { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime CancelDate { get; set; }
        public string CancelReason { get; set; }
        public List<BalanceDataModel> Data { get; set; }
    }

    public class BalanceDataModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }
        public decimal Amount { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime CancelDate { get; set; }
        public string CancelReason { get; set; }
    }

}