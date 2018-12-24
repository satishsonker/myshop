using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DataLayer;
using Myshop.App_Start;

namespace Myshop.Areas.SalesManagement.Models
{
    public class ReportsDetails
    {
        MyshopDb _myshopDb = null;
        public Dictionary<DateTime?, Dictionary<string, List<StatementDetails>>> GetStatement(DateTime FromDate,DateTime ToDate)
        {
            _myshopDb = new MyshopDb();
            Dictionary<DateTime?, Dictionary<string, List<StatementDetails>>> _saleStatement = new Dictionary<DateTime?, Dictionary<string, List<StatementDetails>>>();
          var statement=  _myshopDb.Sale_Tr_Invoice.Where(x => !x.IsDeleted && !x.IsCancelled && x.ShopId.Equals(WebSession.ShopId) && DbFunctions.TruncateTime(x.InvoiceDate) >= FromDate && DbFunctions.TruncateTime(x.InvoiceDate) <= ToDate).ToList().OrderByDescending(x => x.InvoiceDate).GroupBy(x =>x.InvoiceDate.Date);
            var payMode = _myshopDb.Gbl_Master_PayMode.Where(x => !x.IsDeleted).ToList();
            foreach (var statementCollection in statement)
            {
                var payGroup = statementCollection.GroupBy(x => x.PayModeId);
                Dictionary<string, List<StatementDetails>> newDateCollection = new Dictionary<string, List<StatementDetails>>();

                foreach (var item in payGroup)
                {
                    List<StatementDetails> newDetails = new List<StatementDetails>();
                    foreach (var itemNew in item) {
                        StatementDetails statementDetailsItem = new StatementDetails();
                        statementDetailsItem.BalanceAmount = itemNew.BalanceAmount;
                        statementDetailsItem.CustomerName = itemNew.Gbl_Master_Customer.FirstName+" "+ itemNew.Gbl_Master_Customer.LastName;
                        statementDetailsItem.GrandTotal = itemNew.GrandTotal;
                        statementDetailsItem.InvoiceId = itemNew.InvoiceId;
                        statementDetailsItem.PaidAmount = itemNew.PaidAmount;
                        statementDetailsItem.RefundAmount = itemNew.RefundAmount;
                        statementDetailsItem.PayRefNo = itemNew.PayModeRefNo;
                        newDetails.Add(statementDetailsItem);
                    }
                    newDateCollection.Add(payMode.Where(x => x.PayModeId.Equals(item.Key)).Select(x => x.PayMode).FirstOrDefault(), newDetails);
                }
                _saleStatement.Add(statementCollection.Key, newDateCollection);
            }
            return _saleStatement;
        }

        public Dictionary<DateTime?, List<GstStatementDetails>> GetGstStatement(DateTime FromDate, DateTime ToDate)
        {
            _myshopDb = new MyshopDb();
            Dictionary<DateTime?, List<GstStatementDetails>> _saleStatement = new Dictionary<DateTime?, List<GstStatementDetails>>();
            var statement = _myshopDb.Sale_Tr_Invoice.Where(x => !x.IsDeleted && !x.IsCancelled && x.ShopId.Equals(WebSession.ShopId) && DbFunctions.TruncateTime(x.InvoiceDate) >= FromDate && DbFunctions.TruncateTime(x.InvoiceDate) <= ToDate).ToList().OrderByDescending(x=>x.InvoiceDate).GroupBy(x => x.InvoiceDate.Date);
            foreach (var statementCollection in statement)
            {
                Dictionary<string, List<GstStatementDetails>> newDateCollection = new Dictionary<string, List<GstStatementDetails>>();
                List<GstStatementDetails> newDetails = new List<GstStatementDetails>();
                foreach (var item in statementCollection)
                {
                        GstStatementDetails statementDetailsItem = new GstStatementDetails();
                        statementDetailsItem.CustomerName = item.Gbl_Master_Customer.FirstName + " " + item.Gbl_Master_Customer.LastName;
                        statementDetailsItem.GrandTotal = item.GrandTotal;
                        statementDetailsItem.InvoiceId = item.InvoiceId;
                        statementDetailsItem.GstRate = item.GstRate??0.00M;
                        statementDetailsItem.GstAmount = (statementDetailsItem.GrandTotal / 100) * (item.GstRate ?? 0.00M);

                        newDetails.Add(statementDetailsItem);
                    }
                    _saleStatement.Add(statementCollection.Key, newDetails);
            }
            return _saleStatement;
        }

        public  List<MostSallingProduct> ProductsMostSalling(DateTime FromDate, DateTime ToDate, int PageNo, int PageSize)
        {
            _myshopDb = new MyshopDb();
            var saleList = _myshopDb.Sale_Dtl_Invoice.Where(x => DbFunctions.TruncateTime(x.Sale_Tr_Invoice.InvoiceDate) >= FromDate && DbFunctions.TruncateTime(x.Sale_Tr_Invoice.InvoiceDate) <= ToDate && !x.IsDeleted && !(x.IsReturn ?? false) && !x.Sale_Tr_Invoice.IsCancelled && x.ShopId.Equals(WebSession.ShopId)).GroupBy(x=>x.ProductId);
            var products = _myshopDb.Gbl_Master_Product.Where(x => !x.IsDeleted && x.ShopId.Equals(WebSession.ShopId)).ToList();
            List<MostSallingProduct> mostSallingProducts = new List<MostSallingProduct>();
            foreach (var item in saleList)
            {
                MostSallingProduct _InvoicePro = new MostSallingProduct();
                _InvoicePro.ProductId = item.Key;
                _InvoicePro.ProductName = products.Where(x => x.ProductId.Equals(item.Key)).Select(x => x.ProductName).FirstOrDefault();
                _InvoicePro.TotalQty = item.Sum(x => x.Qty);
                _InvoicePro.TotalRecord = saleList.Count();
                mostSallingProducts.Add(_InvoicePro);
            }

            return mostSallingProducts.Skip((PageNo-1)*PageSize).Take(PageSize).OrderByDescending(x=>x.TotalQty).ToList();
        }

    }
}