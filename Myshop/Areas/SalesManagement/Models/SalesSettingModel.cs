using System.ComponentModel.DataAnnotations;

namespace Myshop.Areas.SalesManagement.Models
{
    public class SalesSettingModel
    {
        [Range(0, int.MaxValue, ErrorMessage = "Sale Setting Id should be greater than 0")]
        public int Id { get; set; }

        [StringLength(15, ErrorMessage = "GSTIN should be 15 char")]
        public string GSTIN { get; set; }

        [Range(8, 23, ErrorMessage = "Sales Opening Time should be 8-23")]
        public int SalesOpeningTime { get; set; }

        [Range(12, 23, ErrorMessage = "SalesClosingTime should be 12-23")]
        public int SalesClosingTime { get; set; }

        [StringLength(500, MinimumLength = 0, ErrorMessage = "ReturnPolicy should be max 500 chars")]
        public string ReturnPolicy { get; set; }

    }
}