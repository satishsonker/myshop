using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Myshop.Areas.Global.Models
{
    public class ShopModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "ShopId is required")]
        [Range(0, int.MaxValue, ErrorMessage = "ShopId should be greater than 0 (Zero)")]
        public int ShopId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "OwnerId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "OwnerId should be greater than 0 (Zero)")]
        public int OwnerId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Shop Name is required")]
        [StringLength(maximumLength: 50, MinimumLength = 10, ErrorMessage = "Invalid Shop Name (10 Min and 50 max chars)")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Shop Address is required")]
        [StringLength(maximumLength: 50, MinimumLength = 10, ErrorMessage = "Invalid Address (10 Min and 50 max chars)")]
        public string Address { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile is required")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Entered Mobile No is not valid.")]
        public string Mobile { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [StringLength(maximumLength: 50, MinimumLength = 10, ErrorMessage = "Invalid Email (10 Min and 50 max chars)")]
        public string Email { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Invalid District (3 Min and 50 max chars)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "District is required")]
        public string District { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Invalid State (2 Min and 50 max chars)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "State is required")]
        public string State { get; set; }
    }

    public class BankModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "BankName is required")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Invalid BankName (3 Min and 50 max chars)")]
        public String BankName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "BankId should be greater or Equal than 0 (Zero)")]
        public int BankId { get; set; }

        [StringLength(maximumLength: 200, MinimumLength = 3, ErrorMessage = "Invalid BankDesc (3 Min and 200 max chars)")]
        public String BankDesc { get; set; } = "";
    }

    public class ChequeModel
    {
        /// <summary>
        /// No. of Pages exist in cheque book
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "PageSize is required")]
        [Range(10, int.MaxValue, ErrorMessage = "PageSize should be greater or equal than 10")]
        public int PageSize { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "PageStartNo is required")]
        [Range(99999, 999999, ErrorMessage = "PageStartNo should be in range of 100000-999999")]
        public int PageStartNo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "PageStartNo is required")]
        [Range(99999, 999999, ErrorMessage = "PageStartNo should be in range of 100000-999999")]
        public int PageEndNo { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "ChequeId should be greater or Equal than 0 (Zero)")]
        public int ChequeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "BankName is required")]
        [Range(1, int.MaxValue, ErrorMessage = "BankAccId should be greater than 0 (Zero)")]
        public int BankAccId { get; set; }

        /// <summary>
        /// Check book Issue Date
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "IssueDate is required")]        
        public DateTime IssueDate { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Invalid Desc (3 Min and 50 max chars)")]
        public string Desc { get; set; }

    }

    public class AppModuleModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "ModuleName is required")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Invalid ModuleName (3 Min and 50 max chars)")]
        public String ModuleName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "ModuleId should be greater or Equal than 0 (Zero)")]
        public int ModuleId { get; set; }

        [StringLength(maximumLength: 200, MinimumLength = 3, ErrorMessage = "Invalid ModuleDesc (3 Min and 200 max chars)")]
        public String ModuleDesc { get; set; } = "";
    }
    public class AppPageModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "PageName is required")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Invalid PageName (3 Min and 50 max chars)")]
        public String PageName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "ModuleId should be greater or Equal than 0 (Zero)")]
        public int ModuleId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "PageId should be greater or Equal than 0 (Zero)")]
        public int PageId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "ParentId should be greater or Equal than 0 (Zero)")]
        public int ParentId { get; set; }

        [StringLength(maximumLength: 200, MinimumLength = 3, ErrorMessage = "Invalid ModuleDesc (3 Min and 200 max chars)")]
        public String PageDesc { get; set; } = "";

        [Required(AllowEmptyStrings = false, ErrorMessage = "Url is required")]
        [StringLength(maximumLength: 200, MinimumLength = 6, ErrorMessage = "Invalid Url (6 Min and 200 max chars)")]
        public String Url { get; set; } = "";
    }

    public class AccTypeModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "AccountType is required")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Invalid AccountType (3 Min and 50 max chars)")]
        public String AccountType { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "AccountTypeId should be greater or Equal than 0 (Zero)")]
        public int AccountTypeId { get; set; }

        [StringLength(maximumLength: 200, MinimumLength = 3, ErrorMessage = "Invalid AccountTypeDesc (3 Min and 200 max chars)")]
        public String AccountTypeDesc { get; set; } = "";
    }

    public class PayModeModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "PayMode is required")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Invalid PayMode (3 Min and 50 max chars)")]
        public String PayMode { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "PayModeId should be greater or Equal than 0 (Zero)")]
        public int PayModeId { get; set; }

        [StringLength(maximumLength: 100, MinimumLength = 3, ErrorMessage = "Invalid PayModeDesc (3 Min and 100 max chars)")]
        public String PayModeDesc { get; set; } = "";
    }
    public class BankAccModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "BankAccId is required")]
        [Range(0, int.MaxValue, ErrorMessage = "BankId should be greater or Equal than 0 (Zero)")]
        public int BankId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Account Type Id  is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Account Type Id should be greater or Equal than 0 (Zero)")]
        public int AccTypeId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "BankId should be greater or Equal than 0 (Zero)")]
        public int BankAccId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "AccountName is required")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Invalid AccountName (3 Min and 50 max chars)")]
        public String AccountName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Account Holder Name is required")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Invalid Account Holder Name (3 Min and 50 max chars)")]
        public String AccountHolderName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "AccountNo is required")]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "Invalid AccountNo (6 Min and 20 max chars)")]
        public String AccountNo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "BranchName is required")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Invalid BranchName (3 Min and 20 max chars)")]
        public String BranchName { get; set; }

        [StringLength(maximumLength: 15, MinimumLength = 6, ErrorMessage = "Invalid BranchIFSC (6 Min and 20 max chars)")]
        public String BranchIFSC { get; set; } = "";

        [StringLength(maximumLength: 100, MinimumLength = 6, ErrorMessage = "Invalid BranchAddress (6 Min and 100 max chars)")]
        public String BranchAddress { get; set; }
    }

    public class CreateUserModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "User name is required")]
        [StringLength(maximumLength: 50, MinimumLength = 6, ErrorMessage = "Invalid User Name (6 Min and 50 max chars)")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid Email Address (6 Min and 50 max chars)")]
        public String Username { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [StringLength(maximumLength: 10, MinimumLength = 6, ErrorMessage = "Invalid Password (6 Min and 10 max chars)")]
        public String Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Invalid  Name (3 Min and 50 max chars)")]
        public String Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile is required")]
        [StringLength(maximumLength: 13, MinimumLength = 10, ErrorMessage = "Invalid Mobile (10 Min and 13 max chars)")]
        public String Mobile { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "User Type Id  is required")]
        [Range(1, int.MaxValue, ErrorMessage = "UserType Id should be greater or Equal than 0 (Zero)")]
        public int UserTypeId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "UserId should be greater or Equal than 0 (Zero)")]
        public int UserId { get; set; }
    }

    public class PermissionModel
    {
        //[Required(AllowEmptyStrings = false, ErrorMessage = "PermissionId  is required")]
        [Range(0, int.MaxValue, ErrorMessage = "PermissionId should be greater or Equal than 0 (Zero)")]
        public int PermissionId { get; set; }=0;

        [Required(AllowEmptyStrings = false, ErrorMessage = "UserId  is required")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId should be greater or Equal than 0 (Zero)")]
        public int UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "PageId  is required")]
        [Range(1, int.MaxValue, ErrorMessage = "PageId should be greater or Equal than 0 (Zero)")]
        public int PageId { get; set; }

        public bool Read { get; set; } = false;
        public bool Write { get; set; } = false;
        public bool Delete { get; set; } = false;
        public bool Update { get; set; } = false;
        public bool IsBlockAccess { get; set; } = false;
    }
    public class UserAccessModel
    {
        [Required(AllowEmptyStrings=false,ErrorMessage="UserId is required")]
        [Range(1,int.MaxValue,ErrorMessage="Invalid userid")]
        public int UserId { get; set; }

        public bool IsActive { get; set; }

        public bool IsBlocked { get; set; }
    }

    public class SelectListModel
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }
    public class PageListModel
    {
        public int Value { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
    }
}