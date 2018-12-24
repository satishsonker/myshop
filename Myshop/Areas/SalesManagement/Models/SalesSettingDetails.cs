using DataLayer;
using Myshop.App_Start;
using System;
using System.Data.Entity;
using System.Linq;
using static Myshop.App_Start.Enums;

namespace Myshop.Areas.SalesManagement.Models
{
    public class SalesSettingDetails
    {
        MyshopDb myshopDb = null;

        public CrudStatus SaveSetting(SalesSettingModel model, CrudType crudType)
        {
            myshopDb = new MyshopDb();
            int _result = 0;
            if (model.Id < 1 && crudType == CrudType.Insert)
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
                _newSetting.GstRate = model.GstRate;
                _newSetting.WeeklyClosingDay = model.WeeklyClosingDay;
                _newSetting.ExchangeDayTime = model.ExchangeDayTime;
                _newSetting.SalesClosingTime = model.SalesClosingTime;
                _newSetting.SalesOpeningTime = model.SalesOpeningTime;
                _newSetting.ShopId = WebSession.ShopId;
                myshopDb.Entry(_newSetting).State = EntityState.Added;
                _result = myshopDb.SaveChanges();
            }
            else
            {
                var _setting = myshopDb.Sale_Setting.Where(x => !x.IsDeleted && x.Id.Equals(model.Id) && x.ShopId.Equals(WebSession.ShopId)).FirstOrDefault();
                if (_setting != null)
                {
                    _setting.IsSync = false;
                    _setting.ModifiedBy = WebSession.UserId;
                    _setting.ModifiedDate = DateTime.Now;
                    if (crudType == CrudType.Delete)
                    {
                        _setting.IsDeleted = true;
                        goto Save;
                    }

                    _setting.GSTIN = model.GSTIN;
                    _setting.GstRate = model.GstRate;
                    _setting.ReturnPolicy = model.ReturnPolicy;
                    _setting.SalesClosingTime = model.SalesClosingTime;
                    _setting.SalesOpeningTime = model.SalesOpeningTime;
                    _setting.WeeklyClosingDay = model.WeeklyClosingDay;
                    _setting.ExchangeDayTime = model.ExchangeDayTime;

                    #region Set Sales Session
                    WebSession.GstRate = model.GstRate;
                    WebSession.ShopClosingTime = Convert.ToInt32(model.SalesClosingTime);
                    WebSession.ShopOpeningTime = Convert.ToInt32(model.SalesOpeningTime);
                    WebSession.ShopReturnPolicy = model.ReturnPolicy;
                #endregion

                Save:
                    myshopDb.Entry(_setting).State = EntityState.Modified;
                    _result = myshopDb.SaveChanges();
                }
            }

            return Utility.CrudStatus(_result, crudType);
        }

        public SalesSettingModel GetSalesSetting()
        {
            myshopDb = new MyshopDb();
            var _setting = myshopDb.Sale_Setting.Where(x => !x.IsDeleted && x.ShopId.Equals(WebSession.ShopId)).FirstOrDefault();

            SalesSettingModel _newSetting = new SalesSettingModel();

            if (_setting == null)
            {
                return _newSetting;
            }
            else
            {
                _newSetting.GSTIN = _setting.GSTIN;
                _newSetting.SalesOpeningTime = Convert.ToInt32(_setting.SalesOpeningTime);
                _newSetting.SalesClosingTime = Convert.ToInt32(_setting.SalesClosingTime);
                _newSetting.ReturnPolicy = _setting.ReturnPolicy;
                _newSetting.WeeklyClosingDay = _setting.WeeklyClosingDay;
                _newSetting.ExchangeDayTime = _setting.ExchangeDayTime;
                _newSetting.Id = _setting.Id;
                _newSetting.GstRate = Convert.ToDecimal(_setting.GstRate);
                return _newSetting;
            }
        }
    }
}