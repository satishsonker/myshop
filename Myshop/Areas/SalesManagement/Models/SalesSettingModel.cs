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

        [StringLength(10, MinimumLength = 0, ErrorMessage = "Weekly Closing Day should be max 10 chars")]
        public string WeeklyClosingDay { get; set; }

        [StringLength(50, MinimumLength = 0, ErrorMessage = "Exchange Day & Time should be max 50 chars")]
        public string ExchangeDayTime { get; set; }

        public decimal GstRate { get; set; } = 12.00M;

    }
}