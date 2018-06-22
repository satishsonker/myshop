using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myshop.Areas.Global.Models
{
    public class DowntimeModel
    {
        public int? Id { get; set; }
        public DateTime DownTimeStartDate { get; set; }
        public DateTime DownTimeEndDate { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserName { get; set; }
    }
}