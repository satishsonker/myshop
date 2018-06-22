using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using Myshop.App_Start;
using System.Data.Entity;

namespace Myshop.Areas.Global.Models
{
    public class AdminDetails
    {
        MyshopDb myshop;

        public List<ErrorLog> GetErrorLog(bool all)
        {
            try
            {
                myshop = new MyshopDb();
                List<ErrorLog> log = myshop.ErrorLogs.Where(x => x.IsDeleted == false && (all == true || x.IsResolved == false)).ToList();
                return log;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Enums.CrudStatus UpdateErrorLog(int ErrorId)
        {
            try
            {
                myshop = new MyshopDb();
                var log = myshop.ErrorLogs.Where(x => x.IsDeleted == false && x.Id.Equals(ErrorId)).FirstOrDefault();
                if(log==null)
                {
                    return Enums.CrudStatus.NotExist;
                }
                else
                {
                    int result = 0;
                    log.IsResolved = true;
                    log.ModifiedDate = DateTime.Now;
                    myshop.Entry(log).State = EntityState.Modified;
                    result = myshop.SaveChanges();
                    return result > 0 ? Utility.CrudStatus(result, Enums.CrudType.Update):Enums.CrudStatus.NoEffect;
                }
            }
            catch (Exception ex)
            {
                return Enums.CrudStatus.Exception;
            }
        }
    }
}