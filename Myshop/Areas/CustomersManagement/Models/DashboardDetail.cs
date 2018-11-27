using DataLayer;
using Myshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Myshop.Areas.CustomersManagement.Models
{
    public class DashboardDetail
    {
        MyshopDb myshop;
        public MorrisChartModel.LineChart GetCustomesChartData(int[] CustType=null, int Duration = 30)
        {
            CustType = CustType ?? new int[5] { 0, 0, 0, 0, 0 };
            int[] CustTypeId = new int[5] { 0, 0, 0, 0, 0 };
            for (int i = 0; i < CustType.Length; i++)
            {
                CustTypeId[i] = CustType[i];
            }

            myshop = new MyshopDb();
            MorrisChartModel.LineChart chart = new MorrisChartModel.LineChart();
            DateTime lastDate = DateTime.Now.AddDays(-Duration);
            Array.Sort(CustTypeId);
            int count = 1,Seed = Duration / 10,id=0;
            StringBuilder sb = new StringBuilder();

            var customers = myshop.Gbl_Master_Customer.Where(x => CustTypeId.Contains(x.CustomerTypeId) && x.IsDeleted == false).ToList();
            chart.Labels = new string[CustTypeId.Length];
            for (int i = 0; i < CustTypeId.Length; i++)
            {
                id = CustTypeId[i];

                var proName = myshop.Gbl_Master_CustomerType.Where(x => x.CustomerTypeId.Equals(id)).FirstOrDefault();
                chart.Labels[i] = proName == null ? string.Empty : proName.CustomerType;
            }
            if (customers != null)
            {
                sb.Append("|[");
                if (Duration % 10 == 0)
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        sb.Append("{#y#:#" + (Seed * i) + "D#,");
                        sb.Append("#d" + count + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[0]) && x.CreatedDate.Date >= lastDate.Date && x.CreatedDate.Date <= lastDate.AddDays(i * Seed)).Count() + ",");
                        sb.Append("#d" + (count + 1) + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[1]) && x.CreatedDate.Date >= lastDate.AddDays((i-1) * Seed) && x.CreatedDate.Date <= lastDate.AddDays(i * Seed)).Count() + ",");
                        sb.Append("#d" + (count + 2) + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[2]) && x.CreatedDate.Date >= lastDate.AddDays((i - 1) * Seed) && x.CreatedDate.Date <= lastDate.AddDays(i * Seed)).Count() + ",");
                        sb.Append("#d" + (count + 3) + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[3]) && x.CreatedDate.Date >= lastDate.AddDays((i - 1) * Seed) && x.CreatedDate.Date <= lastDate.AddDays(i * Seed)).Count() + ",");
                        sb.Append("#d" + (count + 4) + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[4]) && x.CreatedDate.Date >= lastDate.AddDays((i - 1) * Seed) && x.CreatedDate.Date <= lastDate.AddDays(i * Seed)).Count() + "},");
                    }
                }
                if (Duration % 7 == 0)
                {
                    Seed = Duration / 7;
                    for (int i = 1; i <= 7; i++)
                    {
                        sb.Append("{#y#:#" + (Seed * i) + "D#,");
                        sb.Append("#d" + count + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[0]) && x.CreatedDate.Date >= lastDate.Date && x.CreatedDate.Date <= lastDate.AddDays(i * Seed)).Count() + ",");
                        sb.Append("#d" + (count + 1) + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[1]) && x.CreatedDate.Date >= lastDate.AddDays((i - 1) * Seed) && x.CreatedDate.Date <= lastDate.AddDays(i * Seed)).Count() + ",");
                        sb.Append("#d" + (count + 2) + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[2]) && x.CreatedDate.Date >= lastDate.AddDays((i - 1) * Seed) && x.CreatedDate.Date <= lastDate.AddDays(i * Seed)).Count() + ",");
                        sb.Append("#d" + (count + 3) + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[3]) && x.CreatedDate.Date >= lastDate.AddDays((i - 1) * Seed) && x.CreatedDate.Date <= lastDate.AddDays(i * Seed)).Count() + ",");
                        sb.Append("#d" + (count + 4) + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[4]) && x.CreatedDate.Date >= lastDate.AddDays((i - 1) * Seed) && x.CreatedDate.Date <= lastDate.AddDays(i * Seed)).Count() + "},");
                    }
                }
                if (Duration ==1)
                {
                    Seed = 24 / 8;
                    for (int i = 1; i <= 8; i++)
                    {
                        sb.Append("{#y#:#" + (Seed * i) + "H#,");
                        sb.Append("#d" + count + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[0]) && x.CreatedDate.Date >= lastDate.Date && x.CreatedDate.Date <= lastDate.AddDays(i * Seed)).Count() + ",");
                        sb.Append("#d" + (count + 1) + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[1]) && x.CreatedDate.Date >= lastDate.AddHours((i - 1) * Seed) && x.CreatedDate.Date <= lastDate.AddHours(i * Seed)).Count() + ",");
                        sb.Append("#d" + (count + 2) + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[2]) && x.CreatedDate.Date >= lastDate.AddHours((i - 1) * Seed) && x.CreatedDate.Date <= lastDate.AddHours(i * Seed)).Count() + ",");
                        sb.Append("#d" + (count + 3) + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[3]) && x.CreatedDate.Date >= lastDate.AddHours((i - 1) * Seed) && x.CreatedDate.Date <= lastDate.AddHours(i * Seed)).Count() + ",");
                        sb.Append("#d" + (count + 4) + "#:" + customers.Where(x => x.CustomerTypeId.Equals(CustTypeId[4]) && x.CreatedDate.Date >= lastDate.AddHours((i - 1) * Seed) && x.CreatedDate.Date <= lastDate.AddHours(i * Seed)).Count() + "},");
                    }
                }
                sb.Append("]|");
                chart.Data = sb.ToString().Remove(sb.ToString().LastIndexOf(','), 1);
            }
            return chart;
        }
        public List<MorrisChartModel.DonutChart> GetTotalCustomerTypeChartData(int[] CustType = null, int Duration = 30)
        {
            CustType = CustType ?? new int[5] { 0, 0, 0, 0, 0 };
            int[] CustTypeId = new int[5] { 0, 0, 0, 0, 0 };
            for (int i = 0; i < CustType.Length; i++)
            {
                CustTypeId[i] = CustType[i];
            }
            List<MorrisChartModel.DonutChart> data = new List<MorrisChartModel.DonutChart>();
            myshop = new MyshopDb();
            DateTime lastDate = DateTime.Now.AddDays(-Duration);
            Array.Sort(CustTypeId);
            foreach (int custid in CustTypeId)
            {
                MorrisChartModel.DonutChart item = new MorrisChartModel.DonutChart();
                var CustCollection = myshop.Gbl_Master_Customer.Where(x => x.CustomerTypeId.Equals(custid) && x.IsDeleted == false && x.CreatedDate > lastDate).ToList();
                if(CustCollection != null && CustCollection.Count>0)
                {
                    item.label = myshop.Gbl_Master_CustomerType.Where(x => x.CustomerTypeId == custid && x.IsDeleted == false).FirstOrDefault().CustomerType;
                    item.value = CustCollection.Count();
                    data.Add(item);
                }
            }
            //var products = myshop.Stk_Dtl_Entry.Where(x => CustTypeId.Contains(x.ProductId) && x.IsDeleted == false).ToList();
            return data;
        }
        public List<MorrisChartModel.DonutChart> GetStockEntryTotalQuantityChartData(int[] ProArry = null, int Duration = 30)
        {
            ProArry = ProArry ?? new int[5] { 0, 0, 0, 0, 0 };
            int[] ProductId = new int[5] { 0, 0, 0, 0, 0 };
            for (int i = 0; i < ProArry.Length; i++)
            {
                ProductId[i] = ProArry[i];
            }
            List<MorrisChartModel.DonutChart> data = new List<MorrisChartModel.DonutChart>();
            myshop = new MyshopDb();
            DateTime lastDate = DateTime.Now.AddDays(-Duration);
            Array.Sort(ProductId);
            foreach (int proid in ProductId)
            {
                MorrisChartModel.DonutChart item = new MorrisChartModel.DonutChart();
                var proColl = myshop.Stk_Dtl_Entry.Where(x => x.ProductId.Equals(proid) && x.IsDeleted == false && x.CreatedDate>lastDate).ToList();
                if (proColl != null && proColl.Count > 0)
                {
                    item.label = myshop.Gbl_Master_Product.Where(x => x.ProductId == proid && x.IsDeleted == false).FirstOrDefault().ProductName;
                    item.value = proColl.Sum(x => x.Qty);
                    data.Add(item);
                }
            }
            var products = myshop.Stk_Dtl_Entry.Where(x => ProductId.Contains(x.ProductId) && x.IsDeleted == false).ToList();
            return data;
        }
        /// <summary>  
        /// Dispose DataAccessLayer instance and closes database connection  
        /// </summary>  
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (myshop!= null)
                {
                    myshop=null;
                    myshop.Dispose();
                }
            }
        }

    }
}