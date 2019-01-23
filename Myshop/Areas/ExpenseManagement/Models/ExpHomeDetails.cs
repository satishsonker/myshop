using DataLayer;
using Myshop.App_Start;
using System.Collections.Generic;
using System.Linq;
using static Myshop.Models.MorrisChartModel;

namespace Myshop.Areas.ExpenseManagement.Models
{
    public class ExpHomeDetails
    {
        MyshopDb myshop = null;
        public List<AreaChart> MonthlyExpChart(int Year, int Month)
        {
            myshop = new MyshopDb();
            var data = myshop.Exp_Tr_New.Where(x => x.IsDeleted == false && x.CreatedDate.Year == Year && x.CreatedDate.Month == Month && x.ShopId.Equals(WebSession.ShopId)).GroupBy(x => x.CreatedDate.Day);
            List<AreaChart> areaCharts = new List<AreaChart>();
            foreach (var item in data)
            {
                areaCharts.Add(new AreaChart
                {
                    Y = item.Key.ToString(),
                    A = item.Sum(x => x.TotalAmout).ToString()
                });
            }
            return areaCharts;
        }

        public IEnumerable<object> TopExpenses(int Year, int Month)
        {
            myshop = new MyshopDb();
            var monthlyExpense = myshop.Exp_Dtl_New.Where(x => x.IsDeleted == false && x.CreatedDate.Year == Year && x.CreatedDate.Month == Month && x.ShopId.Equals(WebSession.ShopId)).ToList().Sum(x => x.Qty * x.Gbl_Master_ExpenseItem.Price);
            return myshop.Exp_Dtl_New.Where(x => x.IsDeleted == false && x.CreatedDate.Year == Year && x.CreatedDate.Month == Month && x.ShopId.Equals(WebSession.ShopId)).GroupBy(x => x.Gbl_Master_ExpenseItem.Name).Select(x => new { Amount = x.Sum(y => y.Qty * y.Gbl_Master_ExpenseItem.Price), Item = x.Key, MonthlyExpense = monthlyExpense }).OrderByDescending(x => x.Amount);
        }

        public IEnumerable<object> TopBalance(int Year, int Month)
        {
            myshop = new MyshopDb();
            return myshop.Exp_Tr_New.Where(x => x.IsDeleted == false && x.CreatedDate.Year == Year && x.CreatedDate.Month == Month && x.ShopId.Equals(WebSession.ShopId) && x.BalanceAmount>0 ).GroupBy(x=>x.Gbl_Master_Vendor.VendorName).Select(x =>new {VendorName=x.Key,TotalAmount=x.Select(y=>y.TotalAmout).Sum(),BalanceAmount= x.Select(y => y.BalanceAmount).Sum(), PaidAmount = x.Select(y => y.PaidAmount).Sum() }).ToList();
        }

        public OverviewModel Overview(int Year, int Month)
        {
            myshop = new MyshopDb();
            var monthlyExpense = myshop.Exp_Tr_New.Where(x => x.IsDeleted == false && x.ShopId.Equals(WebSession.ShopId)).ToList();
            List<OverviewModel> overview= monthlyExpense.Select(x => new OverviewModel
            {
                TotalExpense = monthlyExpense.ToList().Sum(y => y?.TotalAmout??0.00M),
                MonExp = monthlyExpense.Where(y => y.CreatedDate.Year == Year && y.CreatedDate.Month == Month).DefaultIfEmpty().Sum(y => y?.TotalAmout??0.00M),
                MonBal = monthlyExpense.Where(y => y.CreatedDate.Year == Year && y.CreatedDate.Month == Month).DefaultIfEmpty().Sum(y => y?.BalanceAmount??0.00M),
                BigExp = monthlyExpense.Where(y => y.CreatedDate.Year == Year && y.CreatedDate.Month == Month).DefaultIfEmpty().Max(y => y?.TotalAmout??0.00M)
            }).ToList();

            return overview.Count > 0 ? overview.First() : new OverviewModel();
        }
    }
}