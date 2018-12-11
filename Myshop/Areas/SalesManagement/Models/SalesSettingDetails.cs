using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DataLayer;
using Myshop.App_Start;
using static Myshop.App_Start.Enums;

namespace Myshop.Areas.SalesManagement.Models
{
    public class SalesSettingDetails
    {
        MyshopDb myshopDb = null;

        public CrudStatus SaveSetting(SalesSettingModel model,CrudType crudType)
        {
            myshopDb = new MyshopDb();
            int _result = 0;
            if (model.Id > 0 && crudType==CrudType.Insert)
            {
                var _oldSetting = myshopDb.Sale_Setting.Where(x => !x.IsDeleted && x.ShopId.Equals(WebSession.ShopId)).FirstOrDefault();
                if (_oldSetting != null)
                {
                    _oldSetting.IsDeleted = true;
                    _oldSetting.IsSync = false;
                    _oldSetting.ModifiedBy = WebSession.UserId;
                    _oldSetting.ModifiedDate = DateTime.Now;
                    myshopDb.Entry(_oldSetting).State = EntityState.Modified;
                    _result = myshopDb.SaveChanges();
                }

                Sale_Setting _newSetting = new Sale_Setting();
                _newSetting.CreatedBy = WebSession.UserId;
                _newSetting.CreatedDate = DateTime.Now;
                _newSetting.GSTIN = model.GSTIN;
                _newSetting.IsDeleted = false;
                _newSetting.IsSync = false;
                _newSetting.ReturnPolicy = model.ReturnPolicy;
                _newSetting.SalesClosingTime = model.SalesClosingTime;
                _newSetting.SalesOpeningTime = model.SalesOpeningTime;
                _newSetting.ShopId = WebSession.ShopId;
                myshopDb.Entry(_newSetting).State = EntityState.Added;
                _result = myshopDb.SaveChanges();
            }
            else
            {
                var _setting = myshopDb.Sale_Setting.Where(x => !x.IsDeleted && x.Id.Equals(model.Id) && x.ShopId.Equals(WebSession.ShopId)).FirstOrDefault();
                if(_setting!=null)
                {
                    if (crudType == CrudType.Update)
                    {
                        _setting.GSTIN = model.GSTIN;
                        _setting.IsDeleted = false;
                        _setting.IsSync = false;
                        _setting.ReturnPolicy = model.ReturnPolicy;
                        _setting.SalesClosingTime = model.SalesClosingTime;
                        _setting.SalesOpeningTime = model.SalesOpeningTime;
                        _setting.ModifiedBy = WebSession.UserId;
                        _setting.ModifiedDate = DateTime.Now;
                    }
                    else if (crudType == CrudType.Delete)
                    {
                        _setting.IsDeleted = true;
                        _setting.IsSync = false;
                        _setting.ModifiedBy = WebSession.UserId;
                        _setting.ModifiedDate = DateTime.Now;
                       
                    }
                    myshopDb.Entry(_setting).State = EntityState.Modified;
                    _result = myshopDb.SaveChanges();
                }
            }

            return Utility.CrudStatus(_result, crudType);
        }
    }
}