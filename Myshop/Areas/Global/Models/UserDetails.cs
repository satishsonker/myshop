using DataLayer;
using Myshop.App_Start;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.IO;

namespace Myshop.Areas.Global.Models
{
    public class UserDetails
    {
        MyshopDb myshop;
        public Enums.CrudStatus SetUser(CreateUserModel model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();
                byte[] picture = null;
                if(!string.IsNullOrEmpty(model.Picturename))
                {
                    if (Directory.Exists(Utility.AppBaseDirectory+ model.Picturename.Split(new char[] {'\\'})[1]))
                    {
                        picture = File.ReadAllBytes(model.Picturename);
                    }
                }
                Gbl_Master_User newUser;
                var oldUser = myshop.Gbl_Master_User.Where(user => (user.UserId.Equals(model.UserId) || (user.Username.ToLower().Equals(model.Username))) && user.IsDeleted == false).FirstOrDefault();
                if (oldUser != null)
                {
                    if (crudType == Enums.CrudType.Update)
                    {
                        oldUser.Firstname = model.Firstname;
                        oldUser.Lastname = model.Lastname;
                        oldUser.Gender = model.Gender;
                        oldUser.Mobile = model.Mobile;
                        oldUser.UserType = model.UserTypeId;
                        oldUser.IsDeleted = false;
                        oldUser.IsSync = false;
                        oldUser.ModifiedBy = WebSession.UserId;
                        oldUser.ModificationDate = DateTime.Now;
                        oldUser.Photo = picture;
                        myshop.Entry(oldUser).State = EntityState.Modified;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        var stock = myshop.User_ShopMapper.Where(x => x.IsDeleted == false && x.UserId.Equals(model.UserId)).FirstOrDefault();
                        if (stock == null)
                        {
                            oldUser.IsDeleted = true;
                            oldUser.IsSync = false;
                            oldUser.ModifiedBy = WebSession.UserId;
                            oldUser.ModificationDate = DateTime.Now;
                            myshop.Entry(oldUser).State = EntityState.Modified;
                        }
                        else
                        {
                            return Enums.CrudStatus.AlreadyInUse;
                        }
                    }
                    else
                    {
                        return Enums.CrudStatus.AlreadyExistForSameShop;
                    }
                }
                else if (crudType == Enums.CrudType.Insert)
                {
                    newUser = new Gbl_Master_User();
                    newUser.Username = model.Username;
                    newUser.Mobile = model.Mobile;
                    newUser.Firstname = model.Firstname;
                    newUser.Lastname = model.Lastname;
                    newUser.Gender = model.Gender;
                    newUser.UserType = model.UserTypeId;
                    newUser.IsActive = true;
                    newUser.IsBlocked = false;
                    newUser.CreationBy = WebSession.UserId;
                    newUser.CreationDate = DateTime.Now;
                    newUser.Password = Utility.getHash(model.Password); // Get String Hash Value
                    newUser.IsDeleted = false;
                    newUser.IsSync = false;
                    newUser.ModifiedBy = WebSession.UserId;
                    newUser.ModificationDate = DateTime.Now;
                    newUser.Photo = picture;
                    myshop.Entry(newUser).State = EntityState.Added;
                }

                int result = myshop.SaveChanges();
                return Utility.CrudStatus(result, crudType);
            }
            catch (Exception ex)
            {
                return Enums.CrudStatus.Exception;
            }
            finally
            {

            }
        }

        public List<SelectListModel> GetUserTypes()
        {
            try
            {
                myshop = new MyshopDb();
                List<SelectListModel> list = new List<SelectListModel>();
                var bankList = myshop.Gbl_Master_UserType.Where(bank => bank.IsDeleted == false).OrderBy(x => x.Type).ToList();
                if (bankList.Count > 0)
                {
                    foreach (Gbl_Master_UserType currentItem in bankList)
                    {
                        SelectListModel newItem = new SelectListModel();
                        newItem.Text = currentItem.Type;
                        newItem.Value = currentItem.Id;
                        list.Add(newItem);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (myshop != null)
                    myshop = null;
            }
        }
        public List<SelectListModel> GetShop()
        {
            try
            {
                myshop = new MyshopDb();
                List<SelectListModel> list = new List<SelectListModel>();
                var bankList = myshop.Gbl_Master_Shop.Where(shop => shop.IsDeleted == false).OrderBy(x => x.Name).ToList();
                if (bankList.Count > 0)
                {
                    foreach (Gbl_Master_Shop currentItem in bankList)
                    {
                        SelectListModel newItem = new SelectListModel();
                        newItem.Text = currentItem.Name;
                        newItem.Value = currentItem.ShopId;
                        list.Add(newItem);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (myshop != null)
                    myshop = null;
            }
        }
        public IEnumerable<object> GetUserJson(bool allList = false)
        {
            try
            {
                myshop = new MyshopDb();
                var userList = (from user in myshop.Gbl_Master_User.Where(x => x.IsDeleted == false)
                                from userType in myshop.Gbl_Master_UserType.Where(x => x.IsDeleted == false && user.UserType.Equals(x.Id))
                                orderby user.Firstname
                                select new
                                {
                                    user.Username,
                                    Name=string.Format("{0} {1}",user.Firstname,user.Lastname),
                                    user.Mobile,
                                    UserType = userType.Type,
                                    UserTypeId = user.UserType,
                                    UserId = user.UserId,
                                    CreatedDate = user.CreationDate,
                                    user.IsActive,
                                    user.IsBlocked
                                }).ToList();
                if (allList)
                {
                    var currentUser = userList.Where(x => x.UserId.Equals(WebSession.UserId)).FirstOrDefault();
                    if (currentUser != null)
                    {
                        userList.Remove(currentUser);
                    }
                }

                return userList;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (myshop != null)
                    myshop = null;
            }
        }
        public IEnumerable<object> GetPermissionJson(int userid, int moduleid)
        {
            try
            {
                myshop = new MyshopDb();
                var userList = (from permission in myshop.Gbl_Master_User_Permission.Where(x => x.UserId.Equals(userid) && x.IsDeleted == false)
                                from page in myshop.Gbl_Master_Page.Where(x => x.PageId.Equals(permission.PageId) && x.IsDeleted == false)
                                from module in myshop.Gbl_Master_AppModule.Where(x => x.ModuleId.Equals(page.ModuleId) && x.IsDeleted == false)
                                where module.ModuleId == moduleid
                                select new
                                {
                                    permission.Delete,
                                    permission.IsBlockAccess,
                                    permission.Read,
                                    permission.Update,
                                    permission.Write,
                                    permission.UserId,
                                    permission.PageId,
                                    page.PageName,
                                    page.Url,
                                    page.ParentId,
                                    page.ModuleId,
                                    module.ModuleName,
                                    PermissionId= permission.Id
                                }).ToList();
                return userList;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (myshop != null)
                    myshop = null;
            }
        }
        public Enums.CrudStatus UpdateSinglePermission(List<PermissionModel> model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();
                if (crudType == Enums.CrudType.Update)
                {
                    foreach (PermissionModel item in model)
                    {
                        if (item.PermissionId != 0)
                        {
                            var oldPermission = myshop.Gbl_Master_User_Permission.Where(x => x.Id.Equals(item.PermissionId) && x.PageId.Equals(item.PageId)).FirstOrDefault();
                            if (oldPermission != null)
                            {
                                oldPermission.IsBlockAccess = item.IsBlockAccess;
                                oldPermission.Read = item.Read;
                                oldPermission.Write = item.Write;
                                oldPermission.Update = item.Update;
                                oldPermission.Delete = item.Delete;
                                oldPermission.ModificationDate = DateTime.Now;
                                oldPermission.ModifiedBy = WebSession.UserId;
                                oldPermission.IsSync = false;
                                myshop.Entry(oldPermission).State = EntityState.Modified;
                            }
                        }
                        else
                        {
                            Gbl_Master_User_Permission newPermission = new Gbl_Master_User_Permission();
                            newPermission.IsBlockAccess = model[0].IsBlockAccess;
                            newPermission.Read = model[0].Read;
                            newPermission.PageId = model[0].PageId;
                            newPermission.Write = model[0].Write;
                            newPermission.Update = model[0].Update;
                            newPermission.Delete = model[0].Delete;
                            newPermission.CreationBy = WebSession.UserId;
                            newPermission.CreationDate = DateTime.Now;
                            newPermission.IsDeleted = false;
                            newPermission.ModificationDate = DateTime.Now;
                            newPermission.ModifiedBy = WebSession.UserId;
                            newPermission.IsSync = false;
                            newPermission.UserId = model[0].UserId;
                            myshop.Entry(newPermission).State = EntityState.Added;
                        }

                        myshop.SaveChanges();
                    }

                    return Enums.CrudStatus.Updated;
                }
                else if (crudType == Enums.CrudType.Insert)
                {
                    Gbl_Master_User_Permission newPermission = new Gbl_Master_User_Permission();
                    newPermission.IsBlockAccess = model[0].IsBlockAccess;
                    newPermission.PageId = model[0].PageId;
                    newPermission.Read = model[0].Read;
                    newPermission.Write = model[0].Write;
                    newPermission.Update = model[0].Update;
                    newPermission.Delete = model[0].Delete;
                    newPermission.CreationBy = WebSession.UserId;
                    newPermission.CreationDate = DateTime.Now;
                    newPermission.IsDeleted = false;
                    newPermission.ModificationDate = DateTime.Now;
                    newPermission.ModifiedBy = WebSession.UserId;
                    newPermission.IsSync = false;
                    newPermission.UserId = model[0].UserId;
                    myshop.Entry(newPermission).State = EntityState.Added;
                    myshop.SaveChanges();
                    return Enums.CrudStatus.Inserted;
                }
                else if (crudType == Enums.CrudType.Delete)
                {
                    foreach (PermissionModel item in model)
                    {
                        var oldPermission = myshop.Gbl_Master_User_Permission.Where(x => x.Id.Equals(item.PermissionId) && x.UserId.Equals(item.UserId)).FirstOrDefault();
                        if (oldPermission != null)
                        {
                            oldPermission.IsDeleted = true;
                            oldPermission.IsSync = false;
                            oldPermission.ModificationDate = DateTime.Now;
                            oldPermission.ModifiedBy = WebSession.UserId;
                            myshop.Entry(oldPermission).State = EntityState.Modified;
                            myshop.SaveChanges();
                        }
                    }

                    return Enums.CrudStatus.Deleted;
                }
                else
                {
                    return Enums.CrudStatus.NoEffect;
                }
            }
            catch (Exception ex)
            {
                return Enums.CrudStatus.Exception;
            }
        }
        public Enums.CrudStatus SetShopMap(int userId, int shopId, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();
                var oldUser = myshop.User_ShopMapper.Where(user => user.ShopId.Equals(shopId) && user.UserId.Equals(userId) && user.IsDeleted == false).FirstOrDefault();
                int result = 0;
                if (crudType == Enums.CrudType.Insert)
                {
                    if (oldUser != null)
                    {
                        return Enums.CrudStatus.AlreadyExist;
                    }
                    else
                    {
                        User_ShopMapper map = new User_ShopMapper();
                        map.UserId = userId;
                        map.ShopId = shopId;
                        map.ModifiedBy = WebSession.UserId;
                        map.ModificationDate = DateTime.Now;
                        map.IsSync = false;
                        map.IsDeleted = false;
                        map.CreationDate = DateTime.Now;
                        map.CreationBy = WebSession.UserId;
                        myshop.User_ShopMapper.Add(map);
                        result = myshop.SaveChanges();
                        return Utility.CrudStatus(result, crudType);
                    }
                }
                else if (crudType == Enums.CrudType.Delete)
                {
                    if (oldUser != null)
                    {
                        oldUser.IsSync = false;
                        oldUser.IsDeleted = true;
                        oldUser.ModificationDate = DateTime.Now;
                        oldUser.ModifiedBy = WebSession.UserId;
                        myshop.Entry(oldUser).State = EntityState.Modified;
                        result = myshop.SaveChanges();
                        return Utility.CrudStatus(result, crudType);
                    }
                    else
                    {
                        return Enums.CrudStatus.NotExist;
                    }
                }
                else
                {
                    return Enums.CrudStatus.NoEffect;
                }
            }
            catch (Exception ex)
            {
                return Enums.CrudStatus.Exception;
            }
            finally
            {

            }
        }
        public Enums.CrudStatus SetAccess(List<UserAccessModel> model)
        {
            try
            {
                int result = 0;
                myshop = new MyshopDb();
                foreach (UserAccessModel item in model)
                {
                    var user = myshop.Gbl_Master_User.Where(x => x.IsDeleted == false && x.UserId.Equals(item.UserId)).FirstOrDefault();
                    if (user != null)
                    {
                        user.IsDeleted = false;
                        user.IsSync = false;
                        user.ModificationDate = DateTime.Now;
                        user.ModifiedBy = WebSession.UserId;
                        user.IsActive = item.IsActive;
                        user.IsBlocked = item.IsBlocked;
                        myshop.Entry(user).State = EntityState.Modified;
                        result += myshop.SaveChanges();
                    }
                }

                return Utility.CrudStatus(result, Enums.CrudType.Update);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<object> GetMapJson(int userid)
        {
            try
            {
                myshop = new MyshopDb();
                var userList = (from map in myshop.User_ShopMapper.Where(x => x.IsDeleted == false && x.UserId.Equals(userid))
                                from user in myshop.Gbl_Master_User.Where(x => x.IsDeleted == false && map.UserId.Equals(x.UserId))
                                from shop in myshop.Gbl_Master_Shop.Where(x => x.IsDeleted == false && map.ShopId.Equals(x.ShopId))
                                orderby user.Firstname
                                select new
                                {
                                    Name= string.Format("{0} {1}", user.Firstname, user.Lastname),
                                    ShopName = shop.Name,
                                    shop.Address,
                                    CreatedDate = user.CreationDate,
                                    MapId = map.Id,
                                    ShopId = shop.ShopId
                                }).ToList();
                return userList;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (myshop != null)
                    myshop = null;
            }
        }
    }
}