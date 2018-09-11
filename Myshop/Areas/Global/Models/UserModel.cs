using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myshop.Areas.Global.Models
{
    public class UserModel
    {
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int UserTypeId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsBlocked { get; set; }
        public byte[] Img { get; set; }
        public string Photo { get; set; } = string.Empty;
    }
}