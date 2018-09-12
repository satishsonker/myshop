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
                if (log == null)
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
                    return result > 0 ? Utility.CrudStatus(result, Enums.CrudType.Update) : Enums.CrudStatus.NoEffect;
                }
            }
            catch (Exception ex)
            {
                return Enums.CrudStatus.Exception;
            }
        }

        public Enums.CrudStatus ResetUserPassword(int _userId)
        {
            try
            {
                myshop = new MyshopDb();
                var _user = myshop.Gbl_Master_User.Where(x => x.IsDeleted == false && x.UserId.Equals(_userId) && x.ShopId.Equals(WebSession.ShopId)).FirstOrDefault();
                if (_user == null)
                {
                    return Enums.CrudStatus.NotExist;
                }
                else
                {
                    string _randomPassword = Utility.GetDefaultPassword(10);
                    _user.Password = Utility.getHash(_randomPassword);
                    _user.HasDefaultPassword = true;
                    _user.ModificationDate = DateTime.Now;
                    _user.ModifiedBy = WebSession.UserId;
                    _user.IsSync = false;
                    myshop.Entry(_user).State = EntityState.Modified;
                    int result = myshop.SaveChanges();
                    if (result > 0)
                    {
                        string _userFullname = _user.Firstname + " " + _user.Lastname;
                        string _emailBody = Utility.EmailUserAdminPasswordResetBody(_userFullname, _user.Username, _randomPassword);
                        Utility.EmailSendHtmlFormatted(_user.Username, "Password Reset by Admin", _emailBody);
                        return Utility.CrudStatus(result, Enums.CrudType.Update);
                    }
                    else
                    {
                        return Enums.CrudStatus.NoEffect;
                    }
                }
            }
            catch (Exception ex)
            {
                return Enums.CrudStatus.Exception;
            }
        }


    }
}