using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using System.Data.Entity;

namespace Myshop.App_Start
{
    public static class DbRepo
    {
        public static int InsertRecord<T>(T row) where T : class
        {
            MyshopDb dbContext = new MyshopDb();
            dbContext.Entry(row).State = EntityState.Added;
            return dbContext.SaveChanges();
        }
        public static int UpdateRecord<T>(T row) where T : class
        {
            MyshopDb dbContext = new MyshopDb();
            dbContext.Entry(row).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
        public static int DeleteRecord<T>(T row) where T : class
        {
            MyshopDb dbContext = new MyshopDb();
            dbContext.Entry(row).State = EntityState.Modified;
            return dbContext.SaveChanges();
        }
    }
}