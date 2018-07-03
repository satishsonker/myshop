using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using Myshop.App_Start;
using System.Data.Entity;
using System.Text;

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
                    var login = myShop.Logins.Where(log => log.UserId.Equals(isAuthenticated.Id) && log.IsDeleted == false).FirstOrDefault();
                    if(login.IsLoginBlocked)
                    {
                        return Enums.LoginStatus.LoginBlocked;
                    }
                    else if (isAuthenticated.Password != passHash)
                    {
                        login.LoginAttempt += 1;
                        login.ModificationDate = DateTime.Now;
                        login.ModifiedBy = isAuthenticated.Id;
                        myShop.Entry(login).State = EntityState.Modified;
                        myShop.SaveChanges();                        
                        return Enums.LoginStatus.InvalidCredential;                        
                    }
                    else if (login.LoginAttempt >=3)
                    {
                        login.IsLoginBlocked = true;
                        login.ModificationDate = DateTime.Now;
                        login.ModifiedBy = isAuthenticated.Id;
                        myShop.Entry(login).State = EntityState.Modified;
                        myShop.SaveChanges();
                        return Enums.LoginStatus.AttemptExceeded;
                    }

                    var userType = myShop.Gbl_Master_UserType.Where(type => type.Id.Equals(isAuthenticated.UserType) && type.IsDeleted == false).FirstOrDefault();                   
                    List<CustomPermission> userPermission = (from permission in myShop.Gbl_Master_User_Permission.Where(x => x.UserId.Equals(isAuthenticated.Id) && x.IsDeleted == false)
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
                        where shopMap.IsDeleted == false && shop.IsDeleted == false && shopMap.UserId == isAuthenticated.Id
                        select new
                        {
                            ShopId = shop.ShopId,
                            ShopName = shop.Name
                        }).ToList();

                    List<ShopCollection> shopCol = new List<ShopCollection>();
                    if(shopname.Count>0)
                    {
                        WebSession.ShopId = shopname[0].ShopId;
                        WebSession.ShopName= shopname[0].ShopName;
                        foreach (var item in shopname)
                        {
                            ShopCollection newShop = new ShopCollection();
                            newShop.ShopId = item.ShopId;
                            newShop.ShopName = item.ShopName;
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
                        login.ModifiedBy = isAuthenticated.Id;
                        myShop.Entry(login).State = EntityState.Modified;
                    }
                    else
                    {
                        Login newlogin = new Login();
                        newlogin.CreationBy = isAuthenticated.Id;
                        newlogin.CreationDate = DateTime.Now;
                        newlogin.IsDeleted = false;
                        newlogin.IsSync = false;
                        newlogin.LoginDate = DateTime.Now;
                        newlogin.ModificationDate = DateTime.Now;
                        newlogin.ModifiedBy = isAuthenticated.Id;
                        newlogin.UserId = isAuthenticated.Id;
                        myShop.Logins.Add(newlogin);
                    }

                    myShop.SaveChanges();

                    WebSession.UserId = isAuthenticated.Id;
                    WebSession.Name = isAuthenticated.Name;
                    WebSession.UserIsActive = isAuthenticated.IsActive;
                    WebSession.UserIsDeleted = isAuthenticated.IsDeleted;
                    WebSession.UserIsBlocked = isAuthenticated.IsBlocked;
                    WebSession.UserMobile = isAuthenticated.Mobile;
                    WebSession.Username = isAuthenticated.Username;
                    WebSession.ShopList = shopCol;

                    if (userType != null)
                    {
                        WebSession.UserType = userType.Type;
                    }
                    if (userPermission != null)
                    {
                        WebSession.Permission = userPermission;
                    }

                    return Enums.LoginStatus.Authenticate;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (myShop != null)
                    myShop = null;
            }
        }

        public Enums.ResetLinkStatus sendPasswordRestLink(string username)
        {
            try
            {
                myShop = new MyshopDb();
                var isExist = myShop.Gbl_Master_User.Where(user => user.Username.ToLower().Equals(username.ToLower())).FirstOrDefault();
                if (isExist != null)
                {
                    //Setting session properties
                    WebSession.Name = isExist.Name;
                    WebSession.Username = isExist.Username;

                    if (isExist.IsDeleted == true)
                        return Enums.ResetLinkStatus.UserDeleted;
                    else if (isExist.IsActive == false)
                        return Enums.ResetLinkStatus.InactiveUser;
                    else if (isExist.IsBlocked == true)
                        return Enums.ResetLinkStatus.BlockedUser;
                    else if (string.IsNullOrEmpty(isExist.Mobile))
                        return Enums.ResetLinkStatus.invalidMobile;
                    else
                    {
                        string messageId = Utility.CreateOTP(isExist.Mobile);
                        var login = myShop.Logins.Where(log => log.UserId.Equals(isExist.Id) && log.IsDeleted == false).FirstOrDefault();

                        if (login != null)
                        {
                            login.ModificationDate = DateTime.Now;
                            login.IsSync = false;
                            login.ModifiedBy = isExist.Id;
                            login.OTPid = messageId;
                            login.ReserExpireTime = DateTime.Now.AddMinutes(30);
                            login.GUID = Guid.NewGuid(); // Create new GUID
                            login.IsReset = true;
                            myShop.Entry(login).State = EntityState.Modified;
                        }
                        else
                        {
                            Login newlogin = new Login();
                            newlogin.CreationBy = isExist.Id;
                            newlogin.CreationDate = DateTime.Now;
                            newlogin.IsDeleted = false;
                            newlogin.IsSync = false;
                            newlogin.LoginDate = DateTime.Now;
                            newlogin.ModificationDate = DateTime.Now;
                            newlogin.ModifiedBy = isExist.Id;
                            newlogin.UserId = isExist.Id;
                            newlogin.OTPid = messageId;
                            newlogin.ReserExpireTime = DateTime.Now.AddMinutes(30);
                            newlogin.GUID = Guid.NewGuid(); // Create new GUID
                            newlogin.IsReset = true;
                            myShop.Logins.Add(newlogin);
                        }
                        int result = myShop.SaveChanges();
                        if (result > 0)
                        {
                           Utility.SendHtmlFormattedEmail(isExist.Username, "Password Reset Link", Utility.ResetEmailBody(isExist.Name, isExist.Id.ToString(), login.GUID.ToString()));
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
                    myShop = null;
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
                    var login = myShop.Logins.Where(log => log.UserId.Equals(isExist.Id) && log.IsDeleted == false && log.IsReset==true).FirstOrDefault();
                    if (isExist != null)
                    {
                       Enums.OtpStatus status= Utility.VerifyOTP(otp, login.OTPid);
                        if(status==Enums.OtpStatus.Valid)
                        {
                            login.IsReset = false;
                            login.ReserExpireTime = DateTime.Now.AddHours(-1);
                            login.GUID = null;
                            login.ModificationDate = DateTime.Now;
                            login.ModifiedBy = isExist.Id;
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
                    myShop = null;
            }
        }

        public Enums.LoginStatus ResetPassword(string username,string password)
        {
            try
            {
                string passHash = Utility.getHash(password);
                myShop = new MyshopDb();
                var IsSet = myShop.Gbl_Master_User.Where(user => user.Username.ToLower().Equals(username.ToLower()) && user.IsActive == true && user.IsBlocked == false && user.IsDeleted == false).FirstOrDefault();
                if (IsSet != null)
                {
                    IsSet.ModificationDate = DateTime.Now;
                    IsSet.ModifiedBy = IsSet.Id;
                    IsSet.IsSync = false;
                    IsSet.Password = passHash;
                    myShop.Entry(IsSet).State = EntityState.Modified;
                    myShop.SaveChanges();
                    return Enums.LoginStatus.Authenticate;
                }
                else
                    return Enums.LoginStatus.InvalidUser;
            }
            catch (Exception)
            {
               return Enums.LoginStatus.Exception;
            }
        }

        public Enums.LoginStatus ChangePassword(string username, string newPassword,string oldPassword)
        {
            try
            {
                string oldPassHash = Utility.getHash(oldPassword);
                string newPassHash = Utility.getHash(newPassword);
                myShop = new MyshopDb();
                var IsSet = myShop.Gbl_Master_User.Where(user => user.Username.ToLower().Equals(username.ToLower()) && user.IsActive == true && user.IsBlocked == false && user.IsDeleted == false && user.Password.Equals(oldPassHash)).FirstOrDefault();
                if (IsSet != null)
                {
                    IsSet.ModificationDate = DateTime.Now;
                    IsSet.ModifiedBy = IsSet.Id;
                    IsSet.IsSync = false;
                    IsSet.Password = newPassHash;
                    myShop.Entry(IsSet).State = EntityState.Modified;
                    int count= myShop.SaveChanges();
                    if (count > 0)
                    {
                        Utility.SendHtmlFormattedEmail("btech.csit@gmail.com", "Password Changed", Utility.ChangePasswordEmailBody("Satish Kumar Sonkar"));
                        return Enums.LoginStatus.Authenticate;
                    }
                    else
                        return Enums.LoginStatus.Failed;
                }
                else
                    return Enums.LoginStatus.InvalidUser;
            }
            catch (Exception ex)
            {
                return Enums.LoginStatus.Exception;
            }
        }

        public Enums.ResetLinkStatus CheckResetLink(string guid,string userid)
        {
            try
            {
                int UserId = Convert.ToInt32(userid);
                myShop = new MyshopDb();
                var isValid = myShop.Logins.Where(log => log.UserId.Equals(UserId) && log.IsReset == true && log.IsDeleted == false && log.GUID.ToString().Equals(guid)).FirstOrDefault();
                if (isValid != null)
                {
                    var user = myShop.Gbl_Master_User.Where(log => log.Id.Equals(UserId) && log.IsDeleted == false && log.IsActive==true && log.IsBlocked==false).FirstOrDefault();
                    if (user != null)
                    {
                        if (isValid.ReserExpireTime.Value < DateTime.Now)
                            return Enums.ResetLinkStatus.LinkExpire;
                        else
                        {
                            WebSession.Username = user.Username;
                            return Enums.ResetLinkStatus.send;
                        }
                    }
                    else return Enums.ResetLinkStatus.InactiveUser;
                }
                else
                    return Enums.ResetLinkStatus.InvalidLink;
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
                            orderby shop.Name
                            select new ShopListModel
                            {
                                ShopId=shop.ShopId,
                                ShopName=shop.Name
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
                                ShopName = shop.Name
                            }).Count();
            return shopList > 0 ? true : false;
        }
    }
}