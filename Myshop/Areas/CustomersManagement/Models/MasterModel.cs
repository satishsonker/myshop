using DataLayer;
using Myshop.App_Start;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Myshop.Areas.CustomersManagement.Models
{
    public class MasterModel
    {
        MyshopDb myshop = null;
        public Enums.CrudStatus SetCustomerType(CustomerTypeModel model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();

                var oldCustomerType = myshop.Gbl_Master_CustomerType.Where(custType => (custType.CustomerTypeId.Equals(model.CustomerTypeId) || (custType.CustomerType.ToLower().Equals(model.CustomerType.ToLower().Trim()))) && custType.IsDeleted == false).FirstOrDefault();
                if (oldCustomerType != null && oldCustomerType.ShopId.Equals(WebSession.ShopId))
                {
                    if (crudType == Enums.CrudType.Update)
                    {
                        oldCustomerType.CustomerType = model.CustomerType;
                        oldCustomerType.Description = model.Description;
                        oldCustomerType.IsDeleted = false;
                        oldCustomerType.IsSync = false;
                        oldCustomerType.ModifiedBy = WebSession.UserId;
                        oldCustomerType.ModificationDate = DateTime.Now;
                        myshop.Entry(oldCustomerType).State = EntityState.Modified;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        var stock = myshop.Gbl_Master_Customer.Where(x => x.IsDeleted == false && x.CustomerTypeId.Equals(model.CustomerTypeId)).FirstOrDefault();
                        if (stock == null)
                        {
                            oldCustomerType.IsDeleted = true;
                            oldCustomerType.IsSync = false;
                            oldCustomerType.ModifiedBy = WebSession.UserId;
                            oldCustomerType.ModificationDate = DateTime.Now;
                            myshop.Entry(oldCustomerType).State = EntityState.Modified;
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
                    Gbl_Master_CustomerType newCustomerType = new Gbl_Master_CustomerType();
                    newCustomerType.CustomerType = model.CustomerType;
                    newCustomerType.CreatedBy = WebSession.UserId;
                    newCustomerType.CreatedDate = DateTime.Now;
                    newCustomerType.Description = model.Description;
                    newCustomerType.IsDeleted = false;
                    newCustomerType.IsSync = false;
                    newCustomerType.ModifiedBy = WebSession.UserId;
                    newCustomerType.ShopId = WebSession.ShopId;
                    newCustomerType.ModificationDate = DateTime.Now;
                    myshop.Entry(newCustomerType).State = EntityState.Added;
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

        public Enums.CrudStatus SetCustomer(CustomerModel model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();

                var oldCustomer = myshop.Gbl_Master_Customer.Where(cust => (cust.CustomerId.Equals(model.CutomerId) || (cust.Mobile.Equals(model.Mobile.Trim()))) && cust.IsDeleted == false).FirstOrDefault();
                if (oldCustomer != null && oldCustomer.ShopId.Equals(WebSession.ShopId))
                {
                    if (crudType == Enums.CrudType.Update)
                    {
                        oldCustomer.FirstName = model.FirstName;
                        oldCustomer.MiddleName = model.MiddleName;
                        oldCustomer.LastName = model.LastName;
                        oldCustomer.Email = model.Email;
                        oldCustomer.Address = model.Address;
                        oldCustomer.State = model.State;
                        oldCustomer.District = model.City;
                        oldCustomer.PINCode = model.PINCode;
                        oldCustomer.IsDeleted = false;
                        oldCustomer.IsSync = false;
                        oldCustomer.ModifiedBy = WebSession.UserId;
                        oldCustomer.ModificationDate = DateTime.Now;
                        myshop.Entry(oldCustomer).State = EntityState.Modified;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        var stock = myshop.Sale_Tr_Invoice.Where(x => x.IsDeleted == false && x.CustomerId.Equals(model.CustomerTypeId)).FirstOrDefault();
                        if (stock == null)
                        {
                            oldCustomer.IsDeleted = true;
                            oldCustomer.IsSync = false;
                            oldCustomer.ModifiedBy = WebSession.UserId;
                            oldCustomer.ModificationDate = DateTime.Now;
                            myshop.Entry(oldCustomer).State = EntityState.Modified;
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
                    Gbl_Master_Customer newCustomer= new Gbl_Master_Customer();
                    newCustomer.FirstName = model.FirstName;
                    newCustomer.MiddleName = model.MiddleName;
                    newCustomer.LastName = model.LastName;
                    newCustomer.CustomerTypeId = model.CustomerTypeId;
                    newCustomer.Mobile = model.Mobile;
                    newCustomer.Email = model.Email;
                    newCustomer.Address = model.Address;
                    newCustomer.State = model.State;
                    newCustomer.District = model.City;
                    newCustomer.PINCode = model.PINCode;
                    newCustomer.IsDeleted = false;
                    newCustomer.IsSync = false;
                    newCustomer.ModifiedBy = WebSession.UserId;
                    newCustomer.ModificationDate = DateTime.Now;
                    newCustomer.CreatedBy = WebSession.UserId;
                    newCustomer.CreatedDate = DateTime.Now;
                    newCustomer.ShopId = WebSession.ShopId;
                    myshop.Entry(newCustomer).State = EntityState.Added;
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

        public IEnumerable<object> GetCustomerTypeJson()
        {
            try
            {
                myshop = new MyshopDb();
                var catList = (from custType in myshop.Gbl_Master_CustomerType.Where(x => x.IsDeleted == false && x.ShopId.Equals(WebSession.ShopId))
                               orderby custType.CustomerType
                               select new
                               {
                                   custType.CustomerTypeId,
                                   custType.CustomerType,
                                   custType.CreatedDate,
                                   Description = custType.Description ?? "No Description",
                                   custType.ShopId
                               }).ToList();
                return catList;
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

        public IEnumerable<object> GetCustomerJson(string mobile="")
        {
            try
            {
                myshop = new MyshopDb();
                var custList = (from cust in myshop.Gbl_Master_Customer
                               .Where(  x => x.IsDeleted == false && 
                                        x.ShopId.Equals(WebSession.ShopId) && 
                                        (mobile==string.Empty || x.Mobile.IndexOf(mobile)>-1))
                               orderby cust.FirstName
                               select new
                               {
                                   cust.CustomerTypeId,
                                   cust.CustomerId,
                                   cust.CreatedDate,
                                   cust.FirstName,
                                   MiddleName= cust.MiddleName??string.Empty,
                                   cust.LastName,
                                   cust.Mobile,
                                   PINCode= cust.PINCode??string.Empty,
                                   Address=cust.Address??string.Empty,
                                   District=cust.Gbl_Master_City.CityName,
                                   State=cust.Gbl_Master_State.StateName,
                                   Email= cust.Email??string.Empty,
                                   cust.ShopId
                               }).ToList();
                return custList;
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