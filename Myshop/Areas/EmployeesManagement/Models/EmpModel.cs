using Myshop.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myshop.Areas.EmployeesManagement.Models
{
    public class EmpModel: PagingModel
    {
        public int EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Distict { get; set; }
        public string State { get; set; }
        public string EmailId { get; set; }
        public string AadharNo { get; set; }
        public string PANCardNo { get; set; }
        public System.DateTime DOJ { get; set; }
        public System.DateTime DOB { get; set; }
        public Nullable<System.DateTime> DOR { get; set; }
        public int ShopId { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<int> ImageId { get; set; }
        public int RoleId { get; set; }
        public string Gender { get; set; }
        public string UserImage { get; set; }
        public bool IsActive { get; set; }
        public string PINCode { get; set; }

    }
}