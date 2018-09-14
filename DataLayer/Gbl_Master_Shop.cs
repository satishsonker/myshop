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
    
    public partial class Gbl_Master_Shop
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gbl_Master_Shop()
        {
            this.Gbl_Attachment = new HashSet<Gbl_Attachment>();
            this.Gbl_Master_BankAccount = new HashSet<Gbl_Master_BankAccount>();
            this.Gbl_Master_BankChequeDetails = new HashSet<Gbl_Master_BankChequeDetails>();
            this.Gbl_Master_Brand = new HashSet<Gbl_Master_Brand>();
            this.Gbl_Master_Category = new HashSet<Gbl_Master_Category>();
            this.Gbl_Master_Customer = new HashSet<Gbl_Master_Customer>();
            this.Gbl_Master_CustomerType = new HashSet<Gbl_Master_CustomerType>();
            this.Gbl_Master_Notification = new HashSet<Gbl_Master_Notification>();
            this.Gbl_Master_NotificationType = new HashSet<Gbl_Master_NotificationType>();
            this.Gbl_Master_Product = new HashSet<Gbl_Master_Product>();
            this.Gbl_Master_User = new HashSet<Gbl_Master_User>();
            this.Gbl_Master_SubCategory = new HashSet<Gbl_Master_SubCategory>();
            this.Gbl_Master_Vendor = new HashSet<Gbl_Master_Vendor>();
            this.Stk_Tr_Entry = new HashSet<Stk_Tr_Entry>();
            this.Stk_Dtl_Entry = new HashSet<Stk_Dtl_Entry>();
            this.User_ShopMapper = new HashSet<User_ShopMapper>();
            this.Gbl_Master_Task = new HashSet<Gbl_Master_Task>();
        }
    
        public int ShopId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Distict { get; set; }
        public string State { get; set; }
        public int Owner { get; set; }
        public Nullable<int> LogoAttachmentId { get; set; }
        public bool IsSync { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime ModificationDate { get; set; }
        public int ModifiedBy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_Attachment> Gbl_Attachment { get; set; }
        public virtual Gbl_Attachment Gbl_Attachment1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_Master_BankAccount> Gbl_Master_BankAccount { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_Master_BankChequeDetails> Gbl_Master_BankChequeDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_Master_Brand> Gbl_Master_Brand { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_Master_Category> Gbl_Master_Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_Master_Customer> Gbl_Master_Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_Master_CustomerType> Gbl_Master_CustomerType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_Master_Notification> Gbl_Master_Notification { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_Master_NotificationType> Gbl_Master_NotificationType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_Master_Product> Gbl_Master_Product { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_Master_User> Gbl_Master_User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_Master_SubCategory> Gbl_Master_SubCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_Master_Vendor> Gbl_Master_Vendor { get; set; }
        public virtual Gbl_Master_User Gbl_Master_User1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stk_Tr_Entry> Stk_Tr_Entry { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Stk_Dtl_Entry> Stk_Dtl_Entry { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User_ShopMapper> User_ShopMapper { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_Master_Task> Gbl_Master_Task { get; set; }
    }
}
