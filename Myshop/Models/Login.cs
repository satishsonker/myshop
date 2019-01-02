using DataLayer;
using Myshop.App_Start;
using Myshop.Areas.Global.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Myshop.Models
{
    public class LoginModel
    {
        MyshopDb myShop;
        public Enums.LoginStatus getLogin(string username, string password)
        {
            try
            {
                myShop = new MyshopDb();
                string passHash = Utility.getHash(password);

                var isAuthenticated = myShop.Gbl_Master_User.Where(user => user.Username.Equals(username)).FirstOrDefault();

                if (isAuthenticated == null)
                {
                    return Enums.LoginStatus.NotExist;
                }
                else if (isAuthenticated.IsActive == false)
                {
                    return Enums.LoginStatus.Inactive;
                }
                else if (isAuthenticated.IsBlocked == true)
                {
                    return Enums.LoginStatus.UserBlocked;
                }
                else if (isAuthenticated.IsDeleted == true)
                {
                    return Enums.LoginStatus.UserDeleted;
                }
                else
                {
                    var login = myShop.Logins.Where(log => log.UserId.Equals(isAuthenticated.UserId) && log.IsDeleted == false).FirstOrDefault();
                    if (login != null)
                    {
                        if (login.IsLoginBlocked)
                        {
                            return Enums.LoginStatus.LoginBlocked;
                        }
                        else if (isAuthenticated.Password != passHash)
                        {
                            login.LoginAttempt += 1;
                            login.ModificationDate = DateTime.Now;
                            login.ModifiedBy = isAuthenticated.UserId;
                            myShop.Entry(login).State = EntityState.Modified;
                            myShop.SaveChanges();
                            return Enums.LoginStatus.InvalidCredential;
                        }
                        else if (login.LoginAttempt >= 3)
                        {
                            login.IsLoginBlocked = true;
                            login.ModificationDate = DateTime.Now;
                            login.ModifiedBy = isAuthenticated.UserId;
                            myShop.Entry(login).State = EntityState.Modified;
                            myShop.SaveChanges();
                            return Enums.LoginStatus.AttemptExceeded;
                        }
                    }
                    else
                    {
                        Login newLogin = new Login();
                        newLogin.CreationBy = WebSession.UserId;
                        newLogin.UserId = isAuthenticated.UserId;
                        newLogin.ModifiedBy = WebSession.UserId;
                        newLogin.ModificationDate = DateTime.Now;
                        newLogin.LoginDate = DateTime.Now;
                        newLogin.IsSync = false;
                        newLogin.IsReset = false;
                        newLogin.IsLoginBlocked = false;
                        newLogin.IsDeleted = false;
                        newLogin.CreationDate = DateTime.Now;
                        myShop.Entry(newLogin).State = EntityState.Added;
                        myShop.SaveChanges();
                    }
                    var userType = myShop.Gbl_Master_UserType.Where(type => type.UserTypeId.Equals(isAuthenticated.UserTypeId) && type.IsDeleted == false).FirstOrDefault();
                    List<CustomPermission> userPermission = (from permission in myShop.Gbl_Master_User_Permission.Where(x => x.UserId.Equals(isAuthenticated.UserId) && x.IsDeleted == false)
                                                             from page in myShop.Gbl_Master_Page.Where(x => x.PageId.Equals(permission.PageId) && x.IsDeleted == false)
                                                             from module in myShop.Gbl_Master_AppModule.Where(x => x.ModuleId.Equals(page.ModuleId) && x.IsDeleted == false)
                                                             select new CustomPermission
                                                             {
                                                                 Delete = permission.Delete,
                                                                 IsBlockAccess = permission.IsBlockAccess,
                                                                 Read = permission.Read,
                                                                 Update = permission.Update,
                                                                 Write = permission.Write,
                                                                 UserId = permission.UserId,
                                                                 PageId = permission.PageId,
                                                                 PageName = page.PageName,
                                                                 Url = page.Url,
                                                                 ParentId = page.ParentId,
                                                                 ModuleId = page.ModuleId,
                                                                 ModuleName = module.ModuleName
                                                             }).ToList();
                    var shopname =
                        (from shopMap in myShop.User_ShopMapper
                         join shop in myShop.Gbl_Master_Shop on shopMap.ShopId equals shop.ShopId
                         where shopMap.IsDeleted == false && shop.IsDeleted == false && shopMap.UserId == isAuthenticated.UserId
                         select new
                         {
                             shop.ShopId,
                             ShopName = shop.Name,
                             shop.Mobile,
                             shop.Address,
                             shop.Email,
                             State = shop.Gbl_Master_State.StateName,
                             Distict = shop.Gbl_Master_City.CityName,
                             shop.Owner
                         }).ToList();

                    List<ShopCollection> shopCol = new List<ShopCollection>();
                    if (shopname.Count > 0)
                    {
                        WebSession.ShopId = shopname[0].ShopId;
                        WebSession.ShopName = shopname[0].ShopName;
                        foreach (var item in shopname)
                        {
                            ShopCollection newShop = new ShopCollection();
                            newShop.ShopId = item.ShopId;
                            newShop.ShopName = item.ShopName;
                            newShop.ShopCity = item.Distict;
                            newShop.OwnerEmail = item.Email;
                            newShop.OwnerMobile = item.Mobile;
                            newShop.ShopAddress = item.Address;
                            newShop.ShopState = item.State;
                            shopCol.Add(newShop);
                        }
                    }

                    var downtime = myShop.Gbl_AppDowntime.Where(x => x.IsDeleted == false && x.DownTimeEnd >= DateTime.Now).FirstOrDefault();
                    if (downtime != null)
                    {
                        WebSession.DowntimeEnd = downtime.DownTimeEnd;
                        WebSession.DowntimeStart = downtime.DownTimeStart;
                        WebSession.DowntimeMessage = downtime.Message;
                    }
                    else
                    {
                        WebSession.DowntimeMessage = string.Empty;
                    }
                    if (login != null)
                    {
                        login.LoginAttempt = 0;
                        login.LoginDate = DateTime.Now;
                        login.ModificationDate = DateTime.Now;
                        login.IsSync = false;
                        login.ModifiedBy = isAuthenticated.UserId;
                        myShop.Entry(login).State = EntityState.Modified;
                    }

                    WebSession.UserId = isAuthenticated.UserId;
                    WebSession.Firstname = isAuthenticated.Firstname;
                    WebSession.Lastname = isAuthenticated.Lastname;
                    WebSession.UserIsActive = isAuthenticated.IsActive;
                    WebSession.UserIsDeleted = isAuthenticated.IsDeleted;
                    WebSession.UserIsBlocked = isAuthenticated.IsBlocked;
                    WebSession.UserMobile = isAuthenticated.Mobile;
                    WebSession.Username = isAuthenticated.Username;
                    WebSession.ShopList = shopCol;
                    WebSession.UserPhoto = isAuthenticated.Photo == null ? string.Empty : Convert.ToBase64String(isAuthenticated.Photo);
                    WebSession.UserType = isAuthenticated.Gbl_Master_UserType.UserType;
                    WebSession.UserGender = isAuthenticated.Gender.ToUpper();

                    if (userType != null)
                    {
                        WebSession.UserType = userType.UserType;
                    }
                    if (userPermission != null)
                    {
                        WebSession.Permission = userPermission;
                    }

                    var notification = myShop.Gbl_Master_Notification.Where(x =>
                                                                                        x.IsDeleted == false &&
                                                                                        x.IsPushed == true &&
                                                                                        x.IsRead == false &&
                                                                                        x.MessageExpireDate >= DateTime.Now &&
                                                                                        x.ShopId.Equals(WebSession.ShopId) &&
                                                                                        (x.UserId.Equals(WebSession.UserId) || x.IsForAll == true) &&
                                                                                        (x.Gbl_Master_NotificationType.NotificationType.ToLower().IndexOf("push") > -1 || x.Gbl_Master_NotificationType.NotificationType.ToLower().IndexOf("web") > -1)
                                                                                        ).ToList();

                    List<WebSessionNotificationList> _notificationList = new List<WebSessionNotificationList>();

                    foreach (Gbl_Master_Notification item in notification)
                    {
                        WebSessionNotificationList _newItem = new WebSessionNotificationList();
                        _newItem.Message = item.Message;
                        _newItem.Sender = string.Format("{0} {1}", item.Gbl_Master_User.Firstname, item.Gbl_Master_User.Lastname);
                        _newItem.Photo = Convert.ToBase64String(item.Gbl_Master_User.Photo);
                        _newItem.ReceiveDate = Convert.ToDateTime(item.PushedDate);
                        _newItem.NotificationId = item.NotificationId;
                        TimeSpan span = DateTime.Now.Subtract(Convert.ToDateTime(item.PushedDate));
                        if (span.Days == 1)
                        {
                            _newItem.ReceiveTime = string.Format("{0} day ago", span.Days.ToString());
                        }
                        else if (span.Days > 1)
                        {
                            _newItem.ReceiveTime = string.Format("{0} days ago", span.Days.ToString());
                        }
                        else if (span.Hours >= 1 && span.Hours <= 23)
                        {
                            _newItem.ReceiveTime = string.Format("{0} hour ago", span.Hours.ToString());
                        }
                        else //if (span.Minutes < 60)
                        {
                            _newItem.ReceiveTime = string.Format("{0} min ago", span.Minutes.ToString());
                        }

                        _notificationList.Add(_newItem);
                    }
                    WebSession.NotificationList = _notificationList;
                    WebSession.NotificationCount = _notificationList.Count();



                    var taskUser = myShop.Gbl_Master_Task.Where(x =>
                                                                                       x.IsDeleted == false &&
                                                                                       !x.IsCompleted &&
                                                                                       x.ShopId.Equals(WebSession.ShopId) &&
                                                                                       x.AssignedUserId.Equals(WebSession.UserId)).ToList();

                    List<TaskUserModel> _taskList = new List<TaskUserModel>();
                    foreach (Gbl_Master_Task item in taskUser)
                    {
                        TaskUserModel _newItem = new TaskUserModel();
                        _newItem.TaskId = item.TaskId;
                        _newItem.CreatedDate = item.CreatedDate;
                        _newItem.TaskCreatedByName = item.Gbl_Master_User.Firstname + " " + item.Gbl_Master_User.Lastname;
                        _newItem.TaskCreatedById = item.Gbl_Master_User.UserId;
                        _newItem.TaskCreatedByPhoto = Convert.ToBase64String(Utility.GetImageThumbnails(item.Gbl_Master_User.Photo, 30));
                        _newItem.IsImporatant = item.IsImportant;
                        _newItem.TaskAssignedUserId = item.AssignedUserId;
                        _newItem.TaskAssignedUserName = item.Gbl_Master_User1.Firstname + " " + item.Gbl_Master_User1.Lastname; ;
                        _newItem.Priority = item.Priority;
                        _newItem.TaskDetails = item.TaskDetails;
                        TimeSpan span = DateTime.Now.Subtract(Convert.ToDateTime(item.CreatedDate));
                        if (span.Days == 1)
                        {
                            _newItem.TaskAssignedTime = string.Format("{0} day ago", span.Days.ToString());
                        }
                        else if (span.Days > 1)
                        {
                            _newItem.TaskAssignedTime = string.Format("{0} days ago", span.Days.ToString());
                        }
                        else if (span.Hours >= 1 && span.Hours <= 23)
                        {
                            _newItem.TaskAssignedTime = string.Format("{0} hour ago", span.Hours.ToString());
                        }
                        else //if (span.Minutes < 60)
                        {
                            _newItem.TaskAssignedTime = string.Format("{0} min ago", span.Minutes.ToString());
                        }

                        _taskList.Add(_newItem);
                    }
                    WebSession.TaskCount = taskUser.Count();
                    WebSession.TaskList = _taskList;

                    if (isAuthenticated.HasDefaultPassword ?? false)
                    {
                        WebSession.HasDefaultPassword = true;
                        return Enums.LoginStatus.HasDefaultPassword;
                    }
                    else
                    {
                        return shopname.Count > 0 ? Enums.LoginStatus.Authenticate : Enums.LoginStatus.NoShopMapped;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (myShop != null)
                {
                    myShop = null;
                }
            }
        }

        public Enums.ResetLinkStatus sendPasswordRestLink(string username, string _os, string _browser)
        {
            try
            {
                myShop = new MyshopDb();
                var isExist = myShop.Gbl_Master_User.Where(user => user.Username.ToLower().Equals(username.ToLower())).FirstOrDefault();
                if (isExist != null)
                {
                    //Setting session properties
                    WebSession.Firstname = isExist.Firstname;
                    WebSession.Lastname = isExist.Lastname;
                    WebSession.Username = isExist.Username;

                    if (isExist.IsDeleted == true)
                    {
                        return Enums.ResetLinkStatus.UserDeleted;
                    }
                    else if (isExist.IsActive == false)
                    {
                        return Enums.ResetLinkStatus.InactiveUser;
                    }
                    else if (isExist.IsBlocked == true)
                    {
                        return Enums.ResetLinkStatus.BlockedUser;
                    }
                    else if (string.IsNullOrEmpty(isExist.Mobile))
                    {
                        return Enums.ResetLinkStatus.invalidMobile;
                    }
                    else
                    {
                        string otp = string.Empty;
                        int ResetExpireTime = 0;
                        string ResetExpireTimeFromConfig = Utility.GetAppSettingsValue("PasswordResetExpire", "30");
                        int.TryParse(ResetExpireTimeFromConfig, out ResetExpireTime);
                        string messageId = Utility.CreateOTP(isExist.Mobile, out otp);
                        var login = myShop.Logins.Where(log => log.UserId.Equals(isExist.UserId) && log.IsDeleted == false).FirstOrDefault();

                        if (login != null)
                        {
                            login.ModificationDate = DateTime.Now;
                            login.IsSync = false;
                            login.ModifiedBy = isExist.UserId;
                            login.OTPid = messageId;
                            login.ReserExpireTime = DateTime.Now.AddMinutes(30);
                            login.GUID = Guid.NewGuid(); // Create new GUID
                            login.IsReset = true;
                            myShop.Entry(login).State = EntityState.Modified;
                        }
                        else
                        {
                            Login newlogin = new Login();
                            newlogin.CreationBy = isExist.UserId;
                            newlogin.CreationDate = DateTime.Now;
                            newlogin.IsDeleted = false;
                            newlogin.IsSync = false;
                            newlogin.LoginDate = DateTime.Now;
                            newlogin.ModificationDate = DateTime.Now;
                            newlogin.ModifiedBy = isExist.UserId;
                            newlogin.UserId = isExist.UserId;
                            newlogin.OTPid = messageId;
                            newlogin.ReserExpireTime = DateTime.Now.AddMinutes(ResetExpireTime);
                            newlogin.GUID = Guid.NewGuid(); // Create new GUID
                            newlogin.IsReset = true;
                            myShop.Logins.Add(newlogin);
                        }
                        int result = myShop.SaveChanges();
                        if (result > 0)
                        {
                            Utility.EmailSendHtmlFormatted(isExist.Username, "Password Reset Link", Utility.EmailResetBody(isExist.Firstname + " " + isExist.Lastname, isExist.UserId.ToString(), login.GUID.ToString(), _os, _browser, DateTime.Now.AddMinutes(ResetExpireTime), otp));
                        }
                        return Enums.ResetLinkStatus.send;
                    }
                }
                else
                {
                    return Enums.ResetLinkStatus.invalidUser;
                }
            }
            catch (Exception ex)
            {
                return Enums.ResetLinkStatus.exception;
            }
            finally
            {
                if (myShop != null)
                {
                    myShop = null;
                }
            }
        }

        public Enums.OtpStatus VerifyOTP(string username, string otp)
        {
            try
            {
                myShop = new MyshopDb();
                var isExist = myShop.Gbl_Master_User.Where(user => user.Username.ToLower().Equals(username.ToLower()) && user.IsActive == true && user.IsBlocked == false && user.IsDeleted == false).FirstOrDefault();
                if (isExist != null)
                {
                    var login = myShop.Logins.Where(log => log.UserId.Equals(isExist.UserId) && log.IsDeleted == false && log.IsReset == true).FirstOrDefault();
                    if (isExist != null)
                    {
                        Enums.OtpStatus status = Utility.VerifyOTP(otp, login.OTPid);
                        if (status == Enums.OtpStatus.Valid)
                        {
                            login.IsReset = false;
                            login.ReserExpireTime = DateTime.Now.AddHours(-1);
                            login.GUID = null;
                            login.ModificationDate = DateTime.Now;
                            login.ModifiedBy = isExist.UserId;
                            login.IsSync = false;
                            myShop.Entry(login).State = EntityState.Modified;
                            myShop.SaveChanges();
                            return Enums.OtpStatus.Valid;
                        }
                        return status;
                    }

                    return Enums.OtpStatus.InvalidUser;
                }
                else
                {
                    return Enums.OtpStatus.InvalidUser;
                }
            }
            catch (Exception ex)
            {
                return Enums.OtpStatus.Exception;
            }
            finally
            {
                if (myShop != null)
                {
                    myShop = null;
                }
            }
        }

        public Enums.LoginStatus ResetPassword(string username, string password)
        {
            try
            {
                string passHash = Utility.getHash(password);
                myShop = new MyshopDb();
                var IsSet = myShop.Gbl_Master_User.Where(user => user.Username.ToLower().Equals(username.ToLower()) && user.IsActive == true && user.IsBlocked == false && user.IsDeleted == false).FirstOrDefault();
                if (IsSet != null)
                {
                    IsSet.ModificationDate = DateTime.Now;
                    IsSet.ModifiedBy = IsSet.UserId;
                    IsSet.IsSync = false;
                    IsSet.Password = passHash;
                    myShop.Entry(IsSet).State = EntityState.Modified;
                    myShop.SaveChanges();
                    return Enums.LoginStatus.Authenticate;
                }
                else
                {
                    return Enums.LoginStatus.InvalidUser;
                }
            }
            catch (Exception)
            {
                return Enums.LoginStatus.Exception;
            }
        }

        public Enums.LoginStatus ChangePassword(string username, string newPassword, string oldPassword)
        {
            try
            {
                string oldPassHash = Utility.getHash(oldPassword);
                string newPassHash = Utility.getHash(newPassword);
                myShop = new MyshopDb();
                var _user = myShop.Gbl_Master_User.Where(user => user.Username.ToLower().Equals(username.ToLower()) && user.IsActive == true && user.IsBlocked == false && user.IsDeleted == false && user.Password.Equals(oldPassHash)).FirstOrDefault();
                if (_user != null)
                {
                    _user.ModificationDate = DateTime.Now;
                    _user.ModifiedBy = WebSession.UserId;
                    _user.IsSync = false;
                    _user.HasDefaultPassword = false;
                    _user.Password = newPassHash;
                    myShop.Entry(_user).State = EntityState.Modified;
                    int count = myShop.SaveChanges();
                    if (count > 0)
                    {
                        string _userFullname = string.Format("{0} {1}", _user.Firstname, _user.Lastname);
                        Utility.EmailSendHtmlFormatted(_user.Username, "Password Changed", Utility.ChangePasswordEmailBody(_userFullname));
                        return Enums.LoginStatus.Authenticate;
                    }
                    else
                    {
                        return Enums.LoginStatus.Failed;
                    }
                }
                else
                {
                    return Enums.LoginStatus.InvalidUser;
                }
            }
            catch (Exception ex)
            {
                return Enums.LoginStatus.Exception;
            }
        }

        public Enums.ResetLinkStatus CheckResetLink(string guid, string userid)
        {
            try
            {
                int UserId = Convert.ToInt32(userid);
                myShop = new MyshopDb();
                var isValid = myShop.Logins.Where(log => log.UserId.Equals(UserId) && log.IsReset == true && log.IsDeleted == false && log.GUID.ToString().Equals(guid)).FirstOrDefault();
                if (isValid != null)
                {
                    var user = myShop.Gbl_Master_User.Where(log => log.UserId.Equals(UserId) && log.IsDeleted == false && log.IsActive == true && log.IsBlocked == false).FirstOrDefault();
                    if (user != null)
                    {
                        if (isValid.ReserExpireTime.Value < DateTime.Now)
                        {
                            return Enums.ResetLinkStatus.LinkExpire;
                        }
                        else
                        {
                            WebSession.Username = user.Username;
                            return Enums.ResetLinkStatus.send;
                        }
                    }
                    else
                    {
                        return Enums.ResetLinkStatus.InactiveUser;
                    }
                }
                else
                {
                    return Enums.ResetLinkStatus.InvalidLink;
                }
            }
            catch (Exception ex)
            {
                return Enums.ResetLinkStatus.exception;
            }
        }

        public List<ShopListModel> ShopList()
        {
            myShop = new MyshopDb();
            var shopList = (from shopMap in myShop.User_ShopMapper.Where(x => x.UserId.Equals(WebSession.UserId) && x.IsDeleted == false)
                            from shop in myShop.Gbl_Master_Shop.Where(x => x.ShopId.Equals(shopMap.ShopId) && x.IsDeleted == false)
                            from city in myShop.Gbl_Master_City.Where(x => x.CityId.Equals(shop.Distict) && x.IsDeleted == false)
                            from state in myShop.Gbl_Master_State.Where(x => x.StateId.Equals(shop.State) && x.IsDeleted == false)
                            orderby shop.Name
                            select new ShopListModel
                            {
                                ShopId = shop.ShopId,
                                ShopName = shop.Name,
                                GSTIN = shop.GSTIN,
                                Address = shop.Address+", "+city.CityName+", "+state.StateName,
                                Email = shop.Email??"No Email Address",
                                Mobile = shop.Mobile,
                                IsPrimary=shop.IsPrimary
                            }).ToList();
            return shopList;
        }

        public bool ValidateShopId(int shopId)
        {
            myShop = new MyshopDb();
            var shopList = (from shopMap in myShop.User_ShopMapper.Where(x => x.UserId.Equals(WebSession.UserId) && x.IsDeleted == false)
                            from shop in myShop.Gbl_Master_Shop.Where(x => x.ShopId.Equals(shopId) && x.IsDeleted == false)
                            orderby shop.Name
                            select new ShopListModel
                            {
                                ShopId = shop.ShopId,
                                ShopName = shop.Name,
                                GSTIN = shop.GSTIN
                            }).FirstOrDefault();
            if (shopList != null)
            {
                WebSession.GSTIN = shopList.GSTIN;
                return true;
            }
            return false;
        }

        public Enums.LoginStatus GetUsername(string _mobile)
        {
            try
            {
                myShop = new MyshopDb();
                var IsExist = myShop.Gbl_Master_User.Where(user => user.Mobile.ToLower().Equals(_mobile.ToLower()) && user.IsActive == true && user.IsBlocked == false && user.IsDeleted == false).FirstOrDefault();
                if (IsExist != null)
                {
                    string _body = string.Format("Your username for application {0} is {1}", Utility.GetAppSettingsValue("Shopname", string.Empty), IsExist.Username);
                    string _to = IsExist.Mobile.Contains("+91") ? IsExist.Mobile : string.Format("+91{0}", IsExist.Mobile);
                    Utility._SendSMS(_body, _to);
                    return Enums.LoginStatus.SmsSend;
                }
                else
                {
                    return Enums.LoginStatus.MobileNoExist;
                }
            }
            catch (Exception ex)
            {
                return Enums.LoginStatus.Exception;
            }
        }
    }
}