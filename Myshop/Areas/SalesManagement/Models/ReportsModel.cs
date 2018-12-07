using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myshop.Areas.SalesManagement.Models
{
    public class StatementModel
    {
       public Dictionary<DateTime,Dictionary<string, List<StatementDetails>>> SalesStatement { get; set; }
    }

    public class StatementDetails
    {
        public int InvoiceId { get; set; }
        public string CustomerName { get; set; }
        public string PayRefNo { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal RefundAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal PaidAmount { get; set; }
    }
}