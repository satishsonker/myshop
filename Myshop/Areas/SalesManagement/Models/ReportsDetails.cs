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
          var statement=  _myshopDb.Sale_Tr_Invoice.Where(x => !x.IsDeleted && !x.IsCancelled && x.ShopId.Equals(WebSession.ShopId) && DbFunctions.TruncateTime(x.InvoiceDate) >= FromDate && DbFunctions.TruncateTime(x.InvoiceDate) <= ToDate).ToList().GroupBy(x =>x.InvoiceDate.Date);
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

    }
}