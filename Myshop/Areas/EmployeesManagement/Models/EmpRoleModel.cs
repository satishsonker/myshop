using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myshop.Areas.EmployeesManagement.Models
{
    public class EmpRoleModel
    {
        public int RoleId { get; set; }
        public string RoleType { get; set; }
        public string Description { get; set; }
        public int ShopId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}