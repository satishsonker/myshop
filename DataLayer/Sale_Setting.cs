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
    
    public partial class Sale_Setting
    {
        public int Id { get; set; }
        public string GSTIN { get; set; }
        public Nullable<decimal> SalesOpeningTime { get; set; }
        public Nullable<decimal> SalesClosingTime { get; set; }
        public string ReturnPolicy { get; set; }
        public string WeeklyClosingDay { get; set; }
        public string ExchangeDayTime { get; set; }
        public Nullable<decimal> GstRate { get; set; }
        public bool IsSync { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public int ShopId { get; set; }
    
        public virtual Gbl_Master_Shop Gbl_Master_Shop { get; set; }
    }
}
