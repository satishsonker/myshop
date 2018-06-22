using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Myshop.Areas.Global.Models
{
    public class ErrorLogModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "ErrorId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "ErrorId should be greater than 0 (Zero)")]
        public int ErrorId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "IsResolved is required")]
        public bool IsResolved { get; set; } = false;
    }
}