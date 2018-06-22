using DataLayer;
using Myshop.App_Start;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Myshop.Areas.Global.Models
{
    public class MenuDetails
    {
        MyshopDb myshop;
        public Enums.CrudStatus SetAppModule(AppModuleModel model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();

                var oldBank = myshop.Gbl_Master_AppModule.Where(app => (app.ModuleId.Equals(model.ModuleId) || (app.ModuleName.ToLower().Equals(model.ModuleName) || app.ModuleName.ToLower().Contains(model.ModuleName))) && app.IsDeleted == false).FirstOrDefault();
                if (oldBank != null)
                {
                    if (crudType == Enums.CrudType.Update)
                    {
                        oldBank.ModuleName = model.ModuleName;
                        oldBank.Description = model.ModuleDesc;
                        oldBank.IsDeleted = false;
                        oldBank.IsSync = false;
                        oldBank.ModifiedBy = WebSession.UserId;
                        oldBank.ModificationDate = DateTime.Now;
                        myshop.Entry(oldBank).State = EntityState.Modified;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        var stock = myshop.Gbl_Master_Page.Where(x => x.IsDeleted == false && x.ModuleId.Equals(model.ModuleId)).FirstOrDefault();
                        if (stock == null)
                        {
                            oldBank.IsDeleted = true;
                            oldBank.IsSync = false;
                            oldBank.ModifiedBy = WebSession.UserId;
                            oldBank.ModificationDate = DateTime.Now;
                            myshop.Entry(oldBank).State = EntityState.Modified;
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
                    Gbl_Master_AppModule newBank = new Gbl_Master_AppModule();
                    newBank.ModuleName = model.ModuleName;
                    newBank.CreatedBy = WebSession.UserId;
                    newBank.CreationDate = DateTime.Now;
                    newBank.Description = model.ModuleDesc;
                    newBank.IsDeleted = false;
                    newBank.IsSync = false;
                    newBank.ModifiedBy = WebSession.UserId;
                    newBank.ModificationDate = DateTime.Now;
                    myshop.Entry(newBank).State = EntityState.Added;
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
        
        public IEnumerable<object> GetAppModuleJson()
        {
            try
            {
                myshop = new MyshopDb();
                var appList = (from app in myshop.Gbl_Master_AppModule.Where(x => x.IsDeleted == false)
                                orderby app.ModuleName
                                select new
                                {
                                    app.ModuleId,
                                    app.ModuleName,
                                    CreatedDate=app.CreationDate,
                                    Description = app.Description ?? "No Description",
                                }).ToList();
                return appList;
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

        public Enums.CrudStatus SetAppPage(AppPageModel model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();

                var oldpage = myshop.Gbl_Master_Page.Where(app => (app.ModuleId.Equals(model.PageId) || (app.ModuleId.Equals(model.ModuleId) && (app.PageName.ToLower().Equals(model.PageName) || app.PageName.ToLower().Contains(model.PageName)))) && app.IsDeleted == false).FirstOrDefault();
                if (oldpage != null)
                {
                    if (crudType == Enums.CrudType.Update)
                    {
                        oldpage.PageName = model.PageName;
                        oldpage.Description = model.PageDesc;
                        oldpage.ParentId = model.ParentId;
                        oldpage.Url = model.Url;
                        oldpage.IsDeleted = false;
                        oldpage.IsSync = false;
                        oldpage.ModifiedBy = WebSession.UserId;
                        oldpage.ModificationDate = DateTime.Now;
                        myshop.Entry(oldpage).State = EntityState.Modified;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        var stock = myshop.Gbl_Master_User_Permission.Where(x => x.IsDeleted == false && x.PageId.Equals(model.PageId)).FirstOrDefault();
                        if (stock == null)
                        {
                            oldpage.IsDeleted = true;
                            oldpage.IsSync = false;
                            oldpage.ModifiedBy = WebSession.UserId;
                            oldpage.ModificationDate = DateTime.Now;
                            myshop.Entry(oldpage).State = EntityState.Modified;
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
                    Gbl_Master_Page newPage = new Gbl_Master_Page();
                    newPage.PageName = model.PageName;
                    newPage.ParentId = model.ParentId;
                    newPage.ModuleId = model.ModuleId;
                    newPage.Url = model.Url;
                    newPage.CreatedBy = WebSession.UserId;
                    newPage.CreationDate = DateTime.Now;
                    newPage.Description = model.PageDesc;
                    newPage.IsDeleted = false;
                    newPage.IsSync = false;
                    newPage.ModifiedBy = WebSession.UserId;
                    newPage.ModificationDate = DateTime.Now;
                    myshop.Entry(newPage).State = EntityState.Added;
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
        public List<PageListModel> GetAppPages(int moduleId=0)
        {
            try
            {
                myshop = new MyshopDb();
                List<PageListModel> list = new List<PageListModel>();
                var pageList = myshop.Gbl_Master_Page.Where(page => (moduleId == 0 || page.ModuleId.Equals(moduleId)) && page.IsDeleted == false).OrderBy(x => x.PageName).ToList();
                if (pageList.Count > 0)
                {
                    foreach (Gbl_Master_Page currentItem in pageList)
                    {
                        PageListModel newItem = new PageListModel();
                        newItem.Text = currentItem.PageName;
                        newItem.Value = currentItem.PageId;
                        newItem.Url = currentItem.Url;
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
        public IEnumerable<object> GetAppPageJson(int moduleId = 0)
        {
            try
            {
                myshop = new MyshopDb();
                var appList = (from app in myshop.Gbl_Master_Page//.Where(x=>x.IsDeleted==false)
                               from module in myshop.Gbl_Master_AppModule//.Where(x=> x.IsDeleted == false)
                               from parent in myshop.Gbl_Master_Page.Where(x => x.PageId.Equals(app.ParentId) && x.IsDeleted == false).DefaultIfEmpty()
                               where (moduleId == 0 || app.ModuleId == moduleId) && app.IsDeleted==false && module.IsDeleted==false && parent.IsDeleted==false
                               orderby module.ModuleName, app.PageName
                               select new
                               {
                                   app.ModuleId,
                                   app.PageName,
                                   app.Url,
                                   app.PageId,
                                   app.ParentId,
                                   module.ModuleName,
                                   ParentPageName = parent.PageName??"-NO Value-",
                                   ParentUrl = parent.Url??"-NO Value-",
                                   //ParentModuleId = parent.ModuleId ?? 0,
                                   CreatedDate = app.CreationDate,
                                   Description = app.Description ?? "No Description",
                               }).ToList();
                return appList;
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