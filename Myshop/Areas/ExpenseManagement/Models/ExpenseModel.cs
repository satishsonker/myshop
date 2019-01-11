using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using Myshop.App_Start;

namespace Myshop.Areas.ExpenseManagement.Models
{
    public class ExpenseModel
    {
        public Exp_Tr_New ExpTr { get; set; }
        public List<Exp_Dtl_New> ExpDtl { get; set; }
    }

    public class ExpenseJsonModel
    {
        public ExpTrModel ExpTr { get; set; }
        public List<ExpDtlModel> ExpDtl { get; set; }
    }
    public class ExpDtlModel
    {
        public int ExpDtlId { get; set; }
        public int ExpItemId { get; set; }
        public string ExpItemName { get; set; }
        public int ExpTypeId { get; set; }
        public string ExpTypeName { get; set; }
        public int Qty { get; set; }
        public decimal ExpItemPrice { get; set; }
    }
    public class ExpListModel: PagingModel
    {
        public decimal BalanceAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ExpId { get; set; }
        public string PayMode { get; set; }
        public string VendorName { get; set; }
        public decimal PaidAmount { get; set; }
        public string PayModeRefNo { get; set; }
        public decimal TotalAmout { get; set; }
    }

    public class ExpTrModel
    {
        public int ExpTrId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public int PayModeId { get; set; }
        public string PayMode { get; set; }
        public string PayRefNo { get; set; }
        public DateTime Date { get; set; }
    }
}