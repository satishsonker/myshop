using System;
using System.Collections.Generic;

namespace Myshop.Areas.SalesManagement.Models
{
    public class SalesModel
    {
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public decimal SalePrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public string BrandName { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Unit { get; set; }
    }

    public class InvoiceProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public decimal SalePrice { get; set; }
        public int Discount { get; set; }
        public string Remark { get; set; }
    }

    public class InvoiceDetails
    {
        public List<InvoiceProduct> Products { get; set; }
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int PayModeId { get; set; }
        public decimal SubTotalAmount { get; set; }
        public decimal GstAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public string PayModeRefNo { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal BalanceAmount { get; set; }
    }
}