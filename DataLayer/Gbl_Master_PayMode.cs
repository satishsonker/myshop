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
    
    public partial class Gbl_Master_PayMode
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gbl_Master_PayMode()
        {
            this.Sale_Tr_Invoice = new HashSet<Sale_Tr_Invoice>();
            this.Sale_Tr_Invoice1 = new HashSet<Sale_Tr_Invoice>();
        }
    
        public int PayModeId { get; set; }
        public string PayMode { get; set; }
        public string Description { get; set; }
        public bool IsSync { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModificationDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale_Tr_Invoice> Sale_Tr_Invoice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale_Tr_Invoice> Sale_Tr_Invoice1 { get; set; }
    }
}
