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
    
    public partial class Gbl_Master_DocProof
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gbl_Master_DocProof()
        {
            this.Gbl_Master_Employee = new HashSet<Gbl_Master_Employee>();
            this.Gbl_Master_Employee1 = new HashSet<Gbl_Master_Employee>();
        }
    
        public int DocProofId { get; set; }
        public int DocProofTypeId { get; set; }
        public string DocProof { get; set; }
        public string Description { get; set; }
        public bool IsSync { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModificationDate { get; set; }
    
        public virtual Gbl_Master_DocProofType Gbl_Master_DocProofType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_Master_Employee> Gbl_Master_Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_Master_Employee> Gbl_Master_Employee1 { get; set; }
    }
}