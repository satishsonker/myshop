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
    
    public partial class Gbl_Master_Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gbl_Master_Customer()
        {
            this.Sale_Tr_Invoice = new HashSet<Sale_Tr_Invoice>();
        }
    
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int CustomerTypeId { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<int> District { get; set; }
        public string PINCode { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSync { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ShopId { get; set; }
    
        public virtual Gbl_Master_City Gbl_Master_City { get; set; }
        public virtual Gbl_Master_CustomerType Gbl_Master_CustomerType { get; set; }
        public virtual Gbl_Master_State Gbl_Master_State { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale_Tr_Invoice> Sale_Tr_Invoice { get; set; }
    }
}
