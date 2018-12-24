using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public decimal AvailableQty { get; set; }
    }

    public class InvoiceProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public int ReturnQty { get; set; }
        public decimal SalePrice { get; set; }
        public int Discount { get; set; }
        public string Remark { get; set; }
        public bool IsReturn { get; set; }
        public decimal ReturnAmount { get; set; }
        public string ReturnRemark { get; set; }
        public DateTime ReturnDate { get; set; }
    }

    public class InvoiceReturnProduct
    {
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Product Id should be minimum 1")]
        public int ProductId { get; set; }

        [Range(minimum:1,maximum:int.MaxValue,ErrorMessage ="Return Qty should be minimum 1")]
        public int ReturnQty { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Return Amount should be minimum 1")]
        public decimal ReturnAmount { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Return Remarks is required")]
        [StringLength(maximumLength:250,MinimumLength =5,ErrorMessage = "Return Remarks should be min 5 & max 250 char")]
        public string ReturnRemark { get; set; }
    }

    public class InvoiceDetails
    {
        public List<InvoiceProduct> Products { get; set; }

        public int InvoiceId { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Customer Id should be minimum 1")]
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }

        public DateTime InvoiceDate { get; set; } = DateTime.Now;

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "PayMode Id should be minimum 1")]
        public int PayModeId { get; set; }
        public decimal SubTotalAmount { get; set; }=0.00M;
        public decimal GstAmount { get; set; } = 0.00M;
        public decimal PaidAmount { get; set; } = 0.00M;
        //[Required(AllowEmptyStrings =true,ErrorMessage ="Pay Mode Referance Number is Required")]
        [StringLength(maximumLength:250,MinimumLength =0,ErrorMessage = "Pay Mode Referance Number Min 0 & max 250 char")]
        public string PayModeRefNo { get; set; }
        public decimal GrandTotal { get; set; } = 0.00M;
        public decimal BalanceAmount { get; set; } = 0.00M;
        public bool IsRefund { get; set; } = false;
        public bool IsCancelled { get; set; } = false;
        public string CancelRemark { get; set; }
        public DateTime CancelDate { get; set; }
        public decimal GstRate { get; set; } = 12.0M;
    }

    public class InvoiceReturnDetails
    {
        public List<InvoiceReturnProduct> Products { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Invoice Id should be minimum 1")]
        public int InvoiceId { get; set; }

        public decimal RefundAmount { get; set; } = 0.00M;
        public decimal BalanceAmount { get; set; } = 0.00M;
        public bool IsAmountRefunded { get; set; } = true;

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Refund PayMode Id should be minimum 1")]
        public int RefundPayModeId { get; set; }
    }
}