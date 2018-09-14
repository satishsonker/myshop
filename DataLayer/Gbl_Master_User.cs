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
    
    public partial class Gbl_Master_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gbl_Master_User()
        {
            this.Gbl_AppDowntime = new HashSet<Gbl_AppDowntime>();
            this.Gbl_AppDowntime1 = new HashSet<Gbl_AppDowntime>();
            this.Gbl_Master_Notification = new HashSet<Gbl_Master_Notification>();
            this.Gbl_Master_Notification1 = new HashSet<Gbl_Master_Notification>();
            this.Gbl_Master_Shop1 = new HashSet<Gbl_Master_Shop>();
            this.Logins = new HashSet<Login>();
            this.Gbl_Master_User_Permission = new HashSet<Gbl_Master_User_Permission>();
            this.User_ShopMapper = new HashSet<User_ShopMapper>();
            this.Gbl_Master_Task = new HashSet<Gbl_Master_Task>();
            this.Gbl_Master_Task1 = new HashSet<Gbl_Master_Task>();
        }
    
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Mobile { get; set; }
        public byte[] Photo { get; set; }
        public int UserType { get; set; }
        public System.DateTime CreationDate { get; set; }
        public System.DateTime ModificationDate { get; set; }
        public int CreationBy { get; set; }
        public int ModifiedBy { get; set; }
        public bool IsSync { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsBlocked { get; set; }
        public int ShopId { get; set; }
        public string Gender { get; set; }
        public bool HasDefaultPassword { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_AppDowntime> Gbl_AppDowntime { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_AppDowntime> Gbl_AppDowntime1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_Master_Notification> Gbl_Master_Notification { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_Master_Notification> Gbl_Master_Notification1 { get; set; }
        public virtual Gbl_Master_Shop Gbl_Master_Shop { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_Master_Shop> Gbl_Master_Shop1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Login> Logins { get; set; }
        public virtual Gbl_Master_UserType Gbl_Master_UserType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_Master_User_Permission> Gbl_Master_User_Permission { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User_ShopMapper> User_ShopMapper { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_Master_Task> Gbl_Master_Task { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gbl_Master_Task> Gbl_Master_Task1 { get; set; }
    }
}
