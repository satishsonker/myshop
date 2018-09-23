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

    public class TaskUserModel
    {
        public string TaskDetails { get; set; }

        public bool IsCompleted { get; set; }

        public bool IsImporatant { get; set; }

        public byte Priority { get; set; }

        public string TaskAssignedUserName { get; set; }

        public string TaskAssignedTime { get; set; }

        public int TaskAssignedUserId { get; set; }

        public int TaskId { get; set; }

        public string TaskCreatedByName { get; set; }

        public string TaskCreatedByPhoto { get; set; } = string.Empty;

        public int TaskCreatedById { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}