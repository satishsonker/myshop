using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    [MetadataType(typeof(StockEntryMeta))]
    public partial class StockEntry
    {
    }
    public class StockEntryMeta
    {
        [RegularExpression(@"^\s*(?=.*[1-9])\d*(?:\.\d{1,2})?\s*$", ErrorMessage = "Invalid Sell Price")]
        public decimal SellPrice { get; set; }
        [RegularExpression(@"^\s*(?=.*[1-9])\d*(?:\.\d{1,2})?\s*$", ErrorMessage = "Invalid Purchase Price")]
        public decimal PurchasePrice { get; set; }
        [RegularExpression(@"^\s*(?=.*[1-9])\d*(?:\.\d{1,2})?\s*$", ErrorMessage = "Invalid Quantity")]
        public decimal Qty { get; set; }
    }

    [MetadataType(typeof(EmpRoleMeta))]
    public partial class Gbl_Master_Employee_Role
    {
    }

    public class EmpRoleMeta
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Role Type is Required")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Invalid Role Type (min char:2 & Max char:50)")]
        public string RoleType { get; set; }
    }

    [MetadataType(typeof(EmpMeta))]
    public partial class Gbl_Master_Employee
    {
        public string UserImage { get; set; }
    }

    public class EmpMeta
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is Required")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Invalid First Name (min char:2 & Max char:50)")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Namee is Required")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Invalid Last Name (min char:2 & Max char:50)")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Father Name is Required")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Invalid Father Name (min char:2 & Max char:50)")]
        public string FatherName { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile No shuold be only 10 digit")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile is Required")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Invalid Mobile (min char:2 & Max char:50)")]
        public string Mobile { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Address is Required")]
        [StringLength(maximumLength: 250, MinimumLength = 30, ErrorMessage = "Invalid Address (min char:30 & Max char:250)")]
        public string Address { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "City is Required")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Invalid City (min char:2 & Max char:50)")]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "District is Required")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "District (min char:2 & Max char:50)")]
        public string Distict { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "State is Required")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Invalid State (min char:2 & Max char:50)")]
        public string State { get; set; }

        [RegularExpression(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*", ErrorMessage = "Email is invailid")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is Required")]
        [StringLength(maximumLength: 50, MinimumLength = 8, ErrorMessage = "Invalid Email (min char:8 & Max char:50)")]
        public string EmailId { get; set; }

        [RegularExpression(@"^\d{12}$", ErrorMessage = "Aadhar Card No shuold be only 12 digit")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Aadhar No is Required")]
        [StringLength(maximumLength: 12, MinimumLength = 12, ErrorMessage = "Invalid AadharNo (Only 12 char)")]
        public string AadharNo { get; set; }

        [RegularExpression(@"[A-Za-z]{5}\d{4}[A-Za-z]{1}", ErrorMessage = "PAN Card No shuold be 5 char, 4 digit, 1 char (ex. ABCDE1234F)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "PAN Card is Required")]
        [StringLength(maximumLength: 10, MinimumLength = 10, ErrorMessage = "Invalid PAN Card No (only 10 char, ex. ABCDE1234F)")]
        public string PANCardNo { get; set; }

        [RegularExpression(@"^\d{6}$",ErrorMessage ="PIN code shuold be only 6 digit")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "PIN Code is Required")]
        [StringLength(maximumLength: 6, MinimumLength = 6, ErrorMessage = "Invalid PIN Code (Only 6 char)")]
        public string PINCode { get; set; }
    }

    [MetadataType(typeof(DocProofTypeMeta))]
    public partial class Gbl_Master_DocProofType
    {
    }

    public class DocProofTypeMeta
    {
        [RegularExpression(@"^[a-zA-Z\s][a-zA-Z0-9\s]*$", ErrorMessage = "Document Proof Type should be alphanumeric")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Document Proof Type is Required")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Invalid Document Proof Type (min char:2 & Max char:50)")]
        public string DocProofType { get; set; }
    }

    [MetadataType(typeof(DocProofMeta))]
    public partial class Gbl_Master_DocProof
    {
    }

    public class DocProofMeta
    {
        [RegularExpression(@"^[a-zA-Z\s][a-zA-Z0-9\s]*$", ErrorMessage = "Document Proof Type should be alphanumeric")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Document Proof Type is Required")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Invalid Document Proof Type (min char:2 & Max char:50)")]
        public string DocProof { get; set; }

        [Range(minimum:1,maximum:int.MaxValue, ErrorMessage = "DocProofTypeId should be greater than 0")]
        public int DocProofTypeId { get; set; }
    }

    [MetadataType(typeof(TaskMeta))]
    public partial class Gbl_Master_Task
    {
    }

    public class TaskMeta
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Task Details is Required")]
        [StringLength(maximumLength: 500, MinimumLength = 10, ErrorMessage = "Invalid TaskDetails (min char:10 & Max char:500)")]
        public string TaskDetails { get; set; }

        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = "TaskId should be greater than -1")]
        public int TaskId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Priority is Required")]
        [Range(minimum: 1, maximum: 7, ErrorMessage = "Priority should be greater than 0")]
        public byte Priority { get; set; }
    }
}
