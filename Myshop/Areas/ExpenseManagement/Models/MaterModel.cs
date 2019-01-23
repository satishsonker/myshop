using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Myshop.Areas.ExpenseManagement.Models
{
    public class ExpTypeModel
    {
        [Range(0,int.MaxValue,ErrorMessage ="Expense type id should be greater than -1")]
        public int ExpTypeId { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Expense type is required")]
        [StringLength(maximumLength:50,ErrorMessage ="Expense type should be min 3 and max 50 char",MinimumLength =3)]
        public string ExpType { get; set; }

        public string ExpTypeDesc { get; set; }

        public DateTime CreatedDate { get; set; }
    }

    public class ExpItemModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Expense type id should be greater than 0")]
        public int ExpTypeId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Unit id should be greater than 0")]
        public int UnitId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Expense Item id should be greater than -1")]
        public int ExpItemId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Expense type is required")]
        [StringLength(maximumLength: 50, ErrorMessage = "Expense type should be min 3 and max 50 char", MinimumLength = 3)]
        public string ExpItem { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Expense Item Price is required")]
        [Range(1,int.MaxValue,ErrorMessage ="Expense Item Price should be greater than 0")]
        public decimal ExpItemPrice { get; set; }

        public string ExpItemDesc { get; set; }

        public string ExpTypeName { get; set; }

        public string UnitName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}