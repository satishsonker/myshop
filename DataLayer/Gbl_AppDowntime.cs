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
    
    public partial class Gbl_AppDowntime
    {
        public int Id { get; set; }
        public System.DateTime DownTimeStart { get; set; }
        public System.DateTime DownTimeEnd { get; set; }
        public string Message { get; set; }
        public int ShopId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsSync { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Gbl_Master_User Gbl_Master_User { get; set; }
        public virtual Gbl_Master_User Gbl_Master_User1 { get; set; }
    }
}
