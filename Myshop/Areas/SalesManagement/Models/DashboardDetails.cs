using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using DataLayer;
using Myshop.App_Start;
using Myshop.Models;

namespace Myshop.Areas.SalesManagement.Models
{
    public class DashboardDetails
    {
        MyshopDb myshop = null;

        public DashboardModel GetDashboardData(int Day)
        {
            myshop = new MyshopDb();
            DashboardModel _newModel = new DashboardModel();
            List<DashboardInvoiceModel> _newInvoiceModel = new List<DashboardInvoiceModel>();

            List<MostSallingProduct> mostSallingProduct = new List<MostSallingProduct>();
            List<TopCustomersData> topCustomersData = new List<TopCustomersData>();
            DateTime salesDate = DateTime.Now.AddDays(-Day);
            DateTime now = DateTime.Now;
            var firstDateOfCurrentMonth = new DateTime(now.Year, now.Month, 1);
            var sales = myshop.Sale_Tr_Invoice.Where(x => !x.IsDeleted && x.ShopId.Equals(WebSession.ShopId) && !x.IsCancelled).ToList();
            var salesDetails= myshop.Sale_Dtl_Invoice.Where(x => !x.IsDeleted && x.ShopId.Equals(WebSession.ShopId)).ToList();
            var monthlyData = sales.Where(x=>x.CreatedDate >= firstDateOfCurrentMonth).ToList();
            var products = myshop.Gbl_Master_Product.Where(x => !x.IsDeleted && x.ShopId.Equals(WebSession.ShopId)).ToList();
            _newModel.TotalSales = sales.Where(x =>x.InvoiceDate >= salesDate.Date).Count();

            _newModel.TotalIncome = sales.Where(x => x.InvoiceDate >= salesDate.Date).Sum(x => x.GrandTotal-x.RefundAmount);

            _newModel.TotalProduct = salesDetails.Where(x => x.CreatedDate >= salesDate.Date).Select(x => x.ProductId).Distinct().Count();

            _newModel.TotalQty = salesDetails.Where(x=>x.CreatedDate >= salesDate.Date).Sum(x => x.Qty);

            _newModel.MonthlyIncome = monthlyData.Sum(x => x.GrandTotal-x.RefundAmount);

            _newModel.TotalIncomeTillNow= sales.Sum(x => x.GrandTotal - x.RefundAmount);
            foreach (var item in sales.Where(x => x.InvoiceDate >= salesDate.Date))
            {
                DashboardInvoiceModel _InvoiceModel = new DashboardInvoiceModel();
                _InvoiceModel.Amount = item.GrandTotal;
                _InvoiceModel.CustomerName =string.Format("{0} {1} {2}", item.Gbl_Master_Customer.FirstName, item.Gbl_Master_Customer.FirstName??string.Empty, item.Gbl_Master_Customer.LastName);
                _InvoiceModel.InvoiceDate = item.InvoiceDate;
                _InvoiceModel.InvoiceNo = item.InvoiceId;
                _InvoiceModel.IsRefund = item.IsAmountRefunded;
                _newInvoiceModel.Add(_InvoiceModel);

                //Top Customers Data
                if (topCustomersData.Where(x => x.CustomerId.Equals(item.CustomerId)).Count() == 0)
                {
                    TopCustomersData _newCustData = new TopCustomersData();
                    _newCustData.CustomerId = item.CustomerId;
                    _newCustData.TotalPurchase = sales.Where(x => x.CustomerId.Equals(item.CustomerId)).Count();
                    _newCustData.TotalPurchaseAmount = sales.Where(x => x.CustomerId.Equals(item.CustomerId)).Sum(x => x.GrandTotal - x.RefundAmount); ;
                    _newCustData.TotalPurchaseProduct = item.CustomerId;
                    _newCustData.CustomerId = item.CustomerId;
                    _newCustData.CustomerName = string.Format("{0} {1} {2}", item.Gbl_Master_Customer.FirstName, item.Gbl_Master_Customer.FirstName ?? string.Empty, item.Gbl_Master_Customer.LastName);
                    topCustomersData.Add(_newCustData);
                }
            }
            _newModel.InvoiceMDetals=_newInvoiceModel.OrderByDescending(x=>x.InvoiceNo).Take(10).ToList();

            foreach (var item in salesDetails.GroupBy(x=>x.ProductId))
            {
                MostSallingProduct _InvoicePro = new MostSallingProduct();
                _InvoicePro.ProductId = item.Key;
                _InvoicePro.ProductName = products.Where(x=>x.ProductId.Equals(item.Key)).Select(x=>x.ProductName).FirstOrDefault();
                _InvoicePro.TotalQty = item.Sum(x=>x.Qty);
                mostSallingProduct.Add(_InvoicePro);

            }
            _newModel.SallingProducts = mostSallingProduct.OrderByDescending(x=>x.TotalQty).Take(10).ToList();
            _newModel.TopCustomersData = topCustomersData.OrderByDescending(x => x.TotalPurchaseAmount).Take(10).ToList();
            return _newModel;
        }
        public MorrisChartModel.LineChart GetSalesChartData(int Duration = 30)
        {
            myshop = new MyshopDb();
            MorrisChartModel.LineChart chart = new MorrisChartModel.LineChart();
            DateTime lastDate =DateTime.Now.AddDays(-Duration).Date;
            int Seed = Duration / 10;
            StringBuilder sb = new StringBuilder();

            var sales = myshop.Sale_Tr_Invoice.Where(x =>x.IsDeleted == false && x.InvoiceDate>=DbFunctions.TruncateTime(lastDate) && x.ShopId.Equals(WebSession.ShopId)).ToList();
           
            if (sales != null)
            {
                sb.Append("|[");
                if (Duration== 0)
                {
                    for (int i = 0; i <= 24; i+=3)
                    {
                        sb.Append("{#y#:#" + (i) + "H#,");
                        sb.Append("#d#:" + sales.Where(x => x.InvoiceDate >=lastDate.AddHours(i) && x.InvoiceDate <= lastDate.AddHours(i + 3)).Count() + ",");
                        sb.Append("#e#:" + sales.Where(x => x.InvoiceDate >= lastDate.AddHours(i) && x.InvoiceDate <= lastDate.AddHours(i + 3)).Sum(x=>x.GrandTotal-x.RefundAmount) + "},");
                        
                    }
                }
                if (Duration !=0)
                {
                    for (int i = 0; i <= Duration; i ++)
                    {
                        sb.Append("{#y#:#" + (i) + "D#,");
                        sb.Append("#d#:" + sales.Where(x =>x.InvoiceDate.Date == lastDate.AddDays(i)).Count() + ",");
                        sb.Append("#e#:" + sales.Where(x =>x.InvoiceDate.Date == lastDate.AddDays(i)).Sum(x => x.GrandTotal - x.RefundAmount) + "},");

                    }
                }
                //if (Duration % 7 == 0)
                //{
                //    Seed = Duration / 7;
                //    for (int i = 1; i <= 7; i++)
                //    {
                //        sb.Append("{#y#:#" + (Seed * i) + "D#,");
                //        sb.Append("#d" + count + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[0]) && x.CreatedDate.Date >= lastDate.Date && x.CreatedDate.Date <= lastDate.AddDays(i * Seed)).Count() + ",");
                //        sb.Append("#d" + (count + 1) + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[1]) && x.CreatedDate.Date >= lastDate.AddDays((i - 1) * Seed) && x.CreatedDate.Date <= lastDate.AddDays(i * Seed)).Count() + ",");
                //        sb.Append("#d" + (count + 2) + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[2]) && x.CreatedDate.Date >= lastDate.AddDays((i - 1) * Seed) && x.CreatedDate.Date <= lastDate.AddDays(i * Seed)).Count() + ",");
                //        sb.Append("#d" + (count + 3) + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[3]) && x.CreatedDate.Date >= lastDate.AddDays((i - 1) * Seed) && x.CreatedDate.Date <= lastDate.AddDays(i * Seed)).Count() + ",");
                //        sb.Append("#d" + (count + 4) + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[4]) && x.CreatedDate.Date >= lastDate.AddDays((i - 1) * Seed) && x.CreatedDate.Date <= lastDate.AddDays(i * Seed)).Count() + "},");
                //    }
                //}
                //if (Duration == 1)
                //{
                //    Seed = 24 / 8;
                //    for (int i = 1; i <= 8; i++)
                //    {
                //        sb.Append("{#y#:#" + (Seed * i) + "H#,");
                //        sb.Append("#d" + count + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[0]) && x.CreatedDate.Date >= lastDate.Date && x.CreatedDate.Date <= lastDate.AddDays(i * Seed)).Count() + ",");
                //        sb.Append("#d" + (count + 1) + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[1]) && x.CreatedDate.Date >= lastDate.AddHours((i - 1) * Seed) && x.CreatedDate.Date <= lastDate.AddHours(i * Seed)).Count() + ",");
                //        sb.Append("#d" + (count + 2) + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[2]) && x.CreatedDate.Date >= lastDate.AddHours((i - 1) * Seed) && x.CreatedDate.Date <= lastDate.AddHours(i * Seed)).Count() + ",");
                //        sb.Append("#d" + (count + 3) + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[3]) && x.CreatedDate.Date >= lastDate.AddHours((i - 1) * Seed) && x.CreatedDate.Date <= lastDate.AddHours(i * Seed)).Count() + ",");
                //        sb.Append("#d" + (count + 4) + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[4]) && x.CreatedDate.Date >= lastDate.AddHours((i - 1) * Seed) && x.CreatedDate.Date <= lastDate.AddHours(i * Seed)).Count() + "},");
                //    }
                //}
                sb.Append("]|");
                chart.Data = sb.ToString().Remove(sb.ToString().LastIndexOf(','), 1);
            }
            return chart;
        }

        public List<MorrisChartModel.DonutChart> GetSalesStatusData(int Duration=30)
        {
            List<MorrisChartModel.DonutChart> donutCharts = new List<MorrisChartModel.DonutChart>();
            myshop = new MyshopDb();
            var sales = myshop.Sale_Tr_Invoice.Where(x => x.ShopId.Equals(WebSession.ShopId) && !x.IsDeleted).ToList();
            MorrisChartModel.DonutChart cancelled = new MorrisChartModel.DonutChart();
            cancelled.label = "Cancelled";
            cancelled.labelColor = "red";
            cancelled.value = sales.Where(x => x.IsCancelled).Count();

            MorrisChartModel.DonutChart billed = new MorrisChartModel.DonutChart();
            billed.label = "Billed";
            billed.labelColor = "green";
            billed.value = sales.Where(x => !x.IsCancelled && !x.IsAmountRefunded).Count();

            MorrisChartModel.DonutChart returned = new MorrisChartModel.DonutChart();
            returned.label = "Returned";
            returned.labelColor = "orange";
            returned.value = sales.Where(x => !x.IsCancelled).Count();

            donutCharts.Add(billed);
            donutCharts.Add(cancelled);
            donutCharts.Add(returned);

            return donutCharts;
        }
    }
}