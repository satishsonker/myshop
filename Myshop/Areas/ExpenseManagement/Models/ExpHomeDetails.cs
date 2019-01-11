using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using Myshop.App_Start;
using static Myshop.Models.MorrisChartModel;

namespace Myshop.Areas.ExpenseManagement.Models
{
    public class ExpHomeDetails
    {
        MyshopDb myshop = null;
        public List<AreaChart> MonthlyExpChart(int Year, int Month)
        {
            myshop = new MyshopDb();
            var data= myshop.Exp_Tr_New.Where(x => x.IsDeleted == false && x.CreatedDate.Year == Year && x.CreatedDate.Month == Month && x.ShopId.Equals(WebSession.ShopId)).GroupBy(x => x.CreatedDate.Day);
            List<AreaChart> areaCharts = new List<AreaChart>();
            foreach (var item in data)
            {
                areaCharts.Add(new AreaChart
                {
                    Y = item.Key.ToString(),
                    A = item.Sum(x=>x.TotalAmout).ToString()
                });
            }
            return areaCharts;
        }

        public IEnumerable<object> TopExpenses(int Year, int Month)
        {
            myshop = new MyshopDb();
            var monthlyExpense = myshop.Exp_Dtl_New.Where(x => x.IsDeleted == false && x.CreatedDate.Year == Year && x.CreatedDate.Month == Month && x.ShopId.Equals(WebSession.ShopId)).Sum(x => x.Qty * x.Gbl_Master_ExpenseItem.Price);
            return myshop.Exp_Dtl_New.Where(x => x.IsDeleted == false && x.CreatedDate.Year == Year && x.CreatedDate.Month == Month && x.ShopId.Equals(WebSession.ShopId)).GroupBy(x => x.Gbl_Master_ExpenseItem.Name).Select(x=>new {Amount=x.Sum(y=>y.Qty*y.Gbl_Master_ExpenseItem.Price),Item=x.Key,MonthlyExpense= monthlyExpense}).OrderByDescending(x=>x.Amount);
        }
    }
}