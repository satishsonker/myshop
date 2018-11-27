//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sale_Tr_Invoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sale_Tr_Invoice()
        {
            this.Sale_Dtl_Invoice = new HashSet<Sale_Dtl_Invoice>();
        }
    
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public System.DateTime InvoiceDate { get; set; }
        public int PayModeId { get; set; }
        public decimal SubTotalAmount { get; set; }
        public decimal GstAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public string PayModeRefNo { get; set; }
        public decimal GrandTotal { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSync { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public decimal BalanceAmount { get; set; }
        public bool IsCancelled { get; set; }
        public string CancelRemark { get; set; }
        public Nullable<System.DateTime> CancelledDate { get; set; }
        public bool IsAmountRefunded { get; set; }
        public decimal RefundAmount { get; set; }
        public int ShopId { get; set; }
    
        public virtual Gbl_Master_PayMode Gbl_Master_PayMode { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale_Dtl_Invoice> Sale_Dtl_Invoice { get; set; }
        public virtual Gbl_Master_Customer Gbl_Master_Customer { get; set; }
        public virtual Gbl_Master_Shop Gbl_Master_Shop { get; set; }
    }
}
