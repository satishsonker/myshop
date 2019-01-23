using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myshop.Areas.ExpenseManagement.Models
{
    public class OverviewModel
    {
       public decimal TotalExpense { get; set; }
        public decimal MonExp { get; set; }
        public decimal MonBal { get; set; }
        public decimal BigExp { get; set; }
    }
}