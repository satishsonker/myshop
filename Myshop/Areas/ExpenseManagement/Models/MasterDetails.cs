using DataLayer;
using Myshop.App_Start;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Myshop.Areas.ExpenseManagement.Models
{
    public class MasterDetails
    {
        MyshopDb myshop = null;

        public Enums.CrudStatus SetExpType(ExpTypeModel model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();

                var oldexp = myshop.Gbl_Master_ExpenseType.Where(exp => (exp.Id.Equals(model.ExpTypeId) || (exp.ExpenseType.ToLower().Equals(model.ExpType) || exp.ExpenseType.ToLower().Contains(model.ExpType))) && exp.IsDeleted == false && exp.ShopId.Equals(WebSession.ShopId)).FirstOrDefault();
                if (oldexp != null)
                {
                    if(oldexp.ExpenseType.ToLower()==model.ExpType)
                    {
                        return Enums.CrudStatus.AlreadyExistForSameShop;
                    }

                    var isUsed = myshop.Gbl_Master_ExpenseItem.Where(x => x.IsDeleted == false && x.ExpTypeId.Equals(model.ExpTypeId) && x.ShopId.Equals(WebSession.ShopId)).Count() > 0 ? true : false;
                    if(isUsed)
                    {
                        return Enums.CrudStatus.AlreadyInUse;
                    }
                    oldexp.IsSync = false;
                    oldexp.ModifiedBy = WebSession.UserId;
                    oldexp.ModifiedDate = DateTime.Now;
                    if (crudType == Enums.CrudType.Update)
                    {
                        oldexp.ExpenseType = model.ExpType;
                        oldexp.Description = model.ExpTypeDesc;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {   
                            oldexp.IsDeleted = true;
                    }

                    myshop.Entry(oldexp).State = EntityState.Modified;
                }
                else if (crudType == Enums.CrudType.Insert)
                {
                    Gbl_Master_ExpenseType newexp = new Gbl_Master_ExpenseType();
                    newexp.ExpenseType = model.ExpType;
                    newexp.CreatedBy = WebSession.UserId;
                    newexp.CreatedDate = DateTime.Now;
                    newexp.Description = model.ExpTypeDesc;
                    newexp.IsDeleted = false;
                    newexp.IsSync = false;
                    newexp.ShopId = WebSession.ShopId;
                    newexp.ModifiedBy = WebSession.UserId;
                    newexp.ModifiedDate = DateTime.Now;
                    myshop.Entry(newexp).State = EntityState.Added;
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
        public List<ExpTypeModel> GetExpTypeJson()
        {
            try
            {
                myshop = new MyshopDb();
                var expList = (from exp in myshop.Gbl_Master_ExpenseType.Where(x => x.IsDeleted == false && x.ShopId.Equals(WebSession.ShopId))
                               orderby exp.ExpenseType
                               select new ExpTypeModel
                               {
                                   ExpType = exp.ExpenseType,
                                   ExpTypeId = exp.Id,
                                   CreatedDate = exp.CreatedDate,
                                   ExpTypeDesc = exp.Description ?? "No Description",
                               }).ToList();
                return expList;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (myshop != null)
                {
                    myshop = null;
                }
            }
        }
        public Enums.CrudStatus SetExpItem(ExpItemModel model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();

                var oldexp = myshop.Gbl_Master_ExpenseItem.Where(exp => exp.Id.Equals(model.ExpItemId) && !exp.IsDeleted && exp.ShopId.Equals(WebSession.ShopId)).FirstOrDefault();
                if (oldexp != null)
                {
                    if(oldexp.Name.ToLower()==model.ExpItem)
                    {
                        return Enums.CrudStatus.AlreadyExistForSameShop;
                    }

                    var isUsed = myshop.Exp_Dtl_New.Where(x => !x.IsDeleted && x.ShopId.Equals(WebSession.ShopId) && x.ExpItemId.Equals(model.ExpItemId)).Count() > 0 ? true : false;

                    if (!isUsed)
                    {
                        oldexp.IsSync = false;
                        oldexp.ModifiedBy = WebSession.UserId;
                        oldexp.ModifiedDate = DateTime.Now;
                        if (crudType == Enums.CrudType.Update)
                        {
                            oldexp.Name = model.ExpItem;
                            oldexp.Description = model.ExpItemDesc;
                            oldexp.ExpTypeId = model.ExpTypeId;
                            oldexp.Price = model.ExpItemPrice;
                            oldexp.UnitId = model.UnitId;
                        }
                        else if (crudType == Enums.CrudType.Delete)
                        {
                            oldexp.IsDeleted = true;
                        }

                        myshop.Entry(oldexp).State = EntityState.Modified;
                    }
                    else
                    {
                        return Enums.CrudStatus.AlreadyInUse;
                    }
                }
                else if (crudType == Enums.CrudType.Insert)
                {
                    Gbl_Master_ExpenseItem newexp = new Gbl_Master_ExpenseItem
                    {
                        Name = model.ExpItem,
                        ExpTypeId = model.ExpTypeId,
                        CreatedBy = WebSession.UserId,
                        CreatedDate = DateTime.Now,
                        Description = model.ExpItemDesc,
                        IsDeleted = false,
                        IsSync = false,
                        ShopId = WebSession.ShopId,
                        Price = model.ExpItemPrice,
                        UnitId=model.UnitId
                    };
                    myshop.Entry(newexp).State = EntityState.Added;
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
        public List<ExpItemModel> GetExpItemJson()
        {
            try
            {
                myshop = new MyshopDb();
                var expList = (from exp in myshop.Gbl_Master_ExpenseItem.Where(x => x.IsDeleted == false && x.ShopId.Equals(WebSession.ShopId))
                               orderby exp.Name
                               select new ExpItemModel
                               {
                                   ExpItem = exp.Name,
                                   ExpTypeId = exp.ExpTypeId,
                                   ExpTypeName = exp.Gbl_Master_ExpenseType.ExpenseType,
                                   ExpItemId = exp.Id,
                                   CreatedDate = exp.CreatedDate,
                                   ExpItemDesc = exp.Description ?? "No Description",
                                   UnitId=exp.UnitId,
                                   ExpItemPrice=exp.Price,
                                   UnitName=exp.Gbl_Master_Unit.UnitName
                               }).ToList();
                return expList;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (myshop != null)
                {
                    myshop = null;
                }
            }
        }
    }
}