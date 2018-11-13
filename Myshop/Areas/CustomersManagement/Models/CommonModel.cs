using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Myshop.Areas.CustomersManagement.Models
{
    public class CustomerTypeModel
    {
        public int CustomerTypeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "CustomerType is Required")]
        [StringLength(maximumLength:50,MinimumLength =5,ErrorMessage ="Min:5 chars & max 50 chars are required")]
        public string CustomerType { get; set; }

        public string Description { get; set; }
    } public class CustomerModel
    {
        public int CutomerId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "FirstName is Required")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "FirstName Min:3 chars & max 50 chars are required")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "LastName is Required")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "LastName Min:3 chars & max 50 chars are required")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile is Required")]
        [StringLength(maximumLength: 13, MinimumLength = 10, ErrorMessage = "Mobile Min:10 chars & max:13 chars are required")]
        public string Mobile { get; set; }

        public string Email { get; set; }
        public string Address { get; set; }
        public int District { get; set; }
        public int State { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "PINCode is Required")]
        [StringLength(maximumLength: 6, MinimumLength = 6, ErrorMessage = "PINCode Min:6 chars & max:6 chars are required")]
        public string PINCode { get; set; }

        public int CustomerTypeId { get; set; }
    }
}