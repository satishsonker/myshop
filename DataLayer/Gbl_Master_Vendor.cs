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
    
    public partial class Gbl_Master_Vendor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gbl_Master_Vendor()
        {
            this.Stk_Tr_Entry = new HashSet<Stk_Tr_Entry>();
        }
    
        public int VendorId { get; set; }
        public int ShopId { get; set; }
        public string VendorName { get; set; }
        public string VendorMobile { get; set; }
        public string VendorAddress { get; set; }
        public string Description { get; set; }
        public bool IsSync { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime ModificationDate { get; set; }
        public int ModifiedBy { get; set; }
    
        public virtual Gbl_Master_Shop Gbl_Master_Shop { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stk_Tr_Entry> Stk_Tr_Entry { get; set; }
    }
}
