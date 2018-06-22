using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using Myshop.App_Start;
using System.Data.Entity;

namespace Myshop.Areas.Global.Models
{
    public class SettingDetails
    {
        public Enums.CrudStatus AddDowntime(DowntimeModel model, Enums.CrudType crudType)
        {
            MyshopDb db = null;
            if (crudType == Enums.CrudType.Insert)
            {
                Gbl_AppDowntime newDown = new Gbl_AppDowntime();
                newDown.CreatedBy = WebSession.UserId;
                newDown.ModifiedBy = WebSession.UserId;
                newDown.CreatedDate = DateTime.Now;
                newDown.ModifiedDate = DateTime.Now;
                newDown.IsDeleted = false;
                newDown.IsSync = false;
                newDown.Message = model.Message;
                newDown.DownTimeEnd = model.DownTimeEndDate;
                newDown.DownTimeStart = model.DownTimeStartDate;
                db = new MyshopDb();
                db.Gbl_AppDowntime.Add(newDown);
                int result = db.SaveChanges();// DbRepo.InsertRecord<Gbl_AppDowntime>(model);
                return Utility.CrudStatus(result, crudType);
            }
            else if (crudType == Enums.CrudType.Update)
            {
                db = new MyshopDb();
                int result = 0;
                int id = Convert.ToInt32(model.Id);
                var oldDown = db.Gbl_AppDowntime.Where(x => x.Id.Equals(id) && x.IsDeleted == false).FirstOrDefault();
                if (oldDown != null)
                {
                    oldDown.ModifiedBy = WebSession.UserId;
                    oldDown.ModifiedDate = DateTime.Now;
                    oldDown.IsSync = false;
                    oldDown.Message = model.Message;
                    oldDown.DownTimeEnd = model.DownTimeEndDate;
                    oldDown.DownTimeStart = model.DownTimeStartDate;
                    db.Entry(oldDown).State = EntityState.Modified;
                    result = db.SaveChanges();
                }
                return Utility.CrudStatus(result, crudType);
            }
            else
            {
                db = new MyshopDb();
                int result = 0;
                int id = Convert.ToInt32(model.Id);
                var oldDown = db.Gbl_AppDowntime.Where(x => x.Id.Equals(id) && x.IsDeleted == false).FirstOrDefault();
                if (oldDown != null)
                {
                    oldDown.ModifiedBy = WebSession.UserId;
                    oldDown.ModifiedDate = DateTime.Now;
                    oldDown.IsSync = false;
                    oldDown.IsDeleted = false;
                    db.Entry(oldDown).State = EntityState.Modified;
                    result = db.SaveChanges();
                }
                return Utility.CrudStatus(result, crudType);
            }
        }
        public IEnumerable<DowntimeModel> DowntimeList()
        {
            MyshopDb db = new MyshopDb();
            IEnumerable<DowntimeModel> list = (from down in db.Gbl_AppDowntime.Where(x => x.IsDeleted == false && x.DownTimeEnd >= DateTime.Now)
                                               from user in db.Gbl_Master_User.Where(y => y.Id.Equals(down.CreatedBy))
                                               orderby down.DownTimeStart descending
                                               select new DowntimeModel
                                               {
                                                   Id=down.Id,
                                                   Message=down.Message,
                                                   DownTimeEndDate=down.DownTimeEnd,
                                                   DownTimeStartDate=down.DownTimeStart,
                                                   UserName=user.Name,
                                                   CreatedDate=down.CreatedDate
                                               }                                               
                                              ).ToList();
            return list;
        }
    }
}