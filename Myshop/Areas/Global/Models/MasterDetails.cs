using DataLayer;
using Myshop.App_Start;
using Myshop.GlobalResource;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Myshop.Areas.Global.Models
{
    public class MasterDetails
    {
        MyshopDb myshop;

        public Enums.CrudStatus SetLogo(AttachmentModel model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();

                var oldAttachment = myshop.Gbl_Attachment.Where(atta => atta.ShopId.Equals(model.ShopId) && atta.IsDeleted == false && atta.AttachmentId.Equals(model.AttachmentId)).FirstOrDefault();
                if (oldAttachment != null)
                {
                    if (crudType == Enums.CrudType.Update)
                    {
                        oldAttachment.Attachment = model.Attachment;
                        oldAttachment.FileExtension = model.FileExtension;
                        oldAttachment.FileName = model.FileName;
                        oldAttachment.ModuleName = model.ModuleName;
                        oldAttachment.OriginalFileName = model.OriginalFileName;
                        oldAttachment.IsDeleted = false;
                        oldAttachment.IsSync = false;
                        oldAttachment.ModifiedBy = WebSession.UserId;
                        oldAttachment.ModificationDate = DateTime.Now;
                        myshop.Entry(oldAttachment).State = EntityState.Modified;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        var stock = myshop.User_ShopMapper.Where(x => x.IsDeleted == false && x.ShopId.Equals(model.ShopId)).FirstOrDefault();
                        if (stock == null)
                        {
                            oldAttachment.IsDeleted = true;
                            oldAttachment.IsSync = false;
                            oldAttachment.ModifiedBy = WebSession.UserId;
                            oldAttachment.ModificationDate = DateTime.Now;
                            myshop.Entry(oldAttachment).State = EntityState.Modified;
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
                    Gbl_Attachment newAttachment = new Gbl_Attachment();
                    newAttachment.Attachment = model.Attachment;
                    newAttachment.FileExtension = model.FileExtension;
                    newAttachment.FileName = model.FileName;
                    newAttachment.ModuleName = model.ModuleName;
                    newAttachment.OriginalFileName = model.OriginalFileName;
                    newAttachment.IsDeleted = false;
                    newAttachment.IsSync = false;
                    newAttachment.CreatedBy = WebSession.UserId;
                    newAttachment.CreatedDate = DateTime.Now;
                    newAttachment.ModifiedBy = WebSession.UserId;
                    newAttachment.ModificationDate = DateTime.Now;
                    myshop.Entry(newAttachment).State = EntityState.Added;
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
        public IEnumerable<object> GetLogoson()
        {
            try
            {
                myshop = new MyshopDb();
                var shopList = (from shop in myshop.Gbl_Master_Shop.Where(x => x.IsDeleted == false)
                                from user in myshop.Gbl_Master_User.Where(x => x.IsDeleted == false && x.UserId.Equals(shop.Owner))
                                orderby shop.Name
                                select new
                                {
                                    shop.ShopId,
                                    shop.Name,
                                    OwnerId = shop.Owner,
                                    OwnerName = string.Format("{0} {1}", user.Firstname, user.Lastname),
                                    shop.Mobile,
                                    shop.Email,
                                    shop.Address,
                                    District = shop.Distict,
                                    shop.State
                                }).ToList();
                return shopList;
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

        public Enums.CrudStatus SetShop(ShopModel model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();

                var oldShop = myshop.Gbl_Master_Shop.Where(shop => (shop.ShopId.Equals(model.ShopId) || (shop.Name.ToLower().Equals(model.Name.Trim()) || shop.Name.ToLower().Contains(model.Name.Trim()))) && shop.IsDeleted == false).FirstOrDefault();
                if (oldShop != null)
                {
                    if (crudType == Enums.CrudType.Update)
                    {
                        oldShop.Name = model.Name;
                        oldShop.Mobile = model.Mobile;
                        oldShop.Address = model.Address;
                        oldShop.Distict = model.District;
                        oldShop.Email = model.Email;
                        oldShop.State = model.State;
                        oldShop.Owner = model.OwnerId;
                        oldShop.IsDeleted = false;
                        oldShop.IsSync = false;
                        oldShop.ModifiedBy = WebSession.UserId;
                        oldShop.ModificationDate = DateTime.Now;
                        myshop.Entry(oldShop).State = EntityState.Modified;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        var stock = myshop.User_ShopMapper.Where(x => x.IsDeleted == false && x.ShopId.Equals(model.ShopId)).FirstOrDefault();
                        if (stock == null)
                        {
                            oldShop.IsDeleted = true;
                            oldShop.IsSync = false;
                            oldShop.ModifiedBy = WebSession.UserId;
                            oldShop.ModificationDate = DateTime.Now;
                            myshop.Entry(oldShop).State = EntityState.Modified;
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
                    Gbl_Master_Shop newShop = new Gbl_Master_Shop();
                    newShop.Name = model.Name;
                    newShop.Mobile = model.Mobile;
                    newShop.Address = model.Address;
                    newShop.Distict = model.District;
                    newShop.Email = model.Email;
                    newShop.State = model.State;
                    newShop.Owner = model.OwnerId;
                    newShop.IsDeleted = false;
                    newShop.IsSync = false;
                    newShop.CreatedBy = WebSession.UserId;
                    newShop.CreatedDate = DateTime.Now;
                    newShop.ModifiedBy = WebSession.UserId;
                    newShop.ModificationDate = DateTime.Now;
                    myshop.Entry(newShop).State = EntityState.Added;
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
        public IEnumerable<object> GetShopJson()
        {
            try
            {
                myshop = new MyshopDb();
                var shopList = (from shop in myshop.Gbl_Master_Shop.Where(x => x.IsDeleted == false)
                                from user in myshop.Gbl_Master_User.Where(x => x.IsDeleted == false && x.UserId.Equals(shop.Owner))
                                orderby shop.Name
                                select new
                                {
                                    shop.ShopId,
                                    shop.Name,
                                    OwnerId = shop.Owner,
                                    OwnerName = user.Firstname+" "+user.Lastname,
                                    shop.Mobile,
                                    shop.Email,
                                    shop.Address,
                                    District = shop.Gbl_Master_City.CityName,
                                   State=shop.Gbl_Master_State.StateName
                                }).ToList();
                return shopList;
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

        public Enums.CrudStatus SetBank(BankModel model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();

                var oldBank = myshop.Gbl_Master_Bank.Where(bank => (bank.BankId.Equals(model.BankId) || (bank.BankName.ToLower().Equals(model.BankName) || bank.BankName.ToLower().Contains(model.BankName))) && bank.IsDeleted == false).FirstOrDefault();
                if (oldBank != null)
                {
                    if (crudType == Enums.CrudType.Update)
                    {
                        oldBank.BankName = model.BankName;
                        oldBank.Description = model.BankDesc;
                        oldBank.IsDeleted = false;
                        oldBank.IsSync = false;
                        oldBank.ModifiedBy = WebSession.UserId;
                        oldBank.ModificationDate = DateTime.Now;
                        myshop.Entry(oldBank).State = EntityState.Modified;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        var stock = myshop.Gbl_Master_BankAccount.Where(x => x.IsDeleted == false && x.BankId.Equals(model.BankId)).FirstOrDefault();
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
                    Gbl_Master_Bank newBank = new Gbl_Master_Bank();
                    newBank.BankName = model.BankName;
                    newBank.CreatedBy = WebSession.UserId;
                    newBank.CreatedDate = DateTime.Now;
                    newBank.Description = model.BankDesc;
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
        public IEnumerable<object> GetBankJson()
        {
            try
            {
                myshop = new MyshopDb();
                var bankList = (from bank in myshop.Gbl_Master_Bank.Where(x => x.IsDeleted == false)
                                orderby bank.BankName
                                select new
                                {
                                    bank.BankId,
                                    bank.BankName,
                                    bank.CreatedDate,
                                    Description = bank.Description ?? "No Description",
                                }).ToList();
                return bankList;
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

        public Enums.CrudStatus SetAccType(AccTypeModel model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();

                var oldBank = myshop.Gbl_Master_BankAccountType.Where(bank => (bank.AccountTypeId.Equals(model.AccountTypeId) || (bank.AccountType.ToLower().Equals(model.AccountType) || bank.AccountType.ToLower().Contains(model.AccountType))) && bank.IsDeleted == false).FirstOrDefault();
                if (oldBank != null)
                {
                    if (crudType == Enums.CrudType.Update)
                    {
                        oldBank.AccountType = model.AccountType;
                        oldBank.Description = model.AccountTypeDesc;
                        oldBank.IsDeleted = false;
                        oldBank.IsSync = false;
                        oldBank.ModifiedBy = WebSession.UserId;
                        oldBank.ModificationDate = DateTime.Now;
                        myshop.Entry(oldBank).State = EntityState.Modified;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        var stock = myshop.Gbl_Master_BankAccount.Where(x => x.IsDeleted == false && x.AccTypeId.Equals(model.AccountTypeId)).FirstOrDefault();
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
                    Gbl_Master_Bank newBank = new Gbl_Master_Bank();
                    newBank.BankName = model.AccountType;
                    newBank.CreatedBy = WebSession.UserId;
                    newBank.CreatedDate = DateTime.Now;
                    newBank.Description = model.AccountTypeDesc;
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
        public IEnumerable<object> GetAccTypeJson()
        {
            try
            {
                myshop = new MyshopDb();
                var bankList = (from bank in myshop.Gbl_Master_BankAccountType.Where(x => x.IsDeleted == false)
                                orderby bank.AccountType
                                select new
                                {
                                    bank.AccountTypeId,
                                    bank.AccountType,
                                    bank.CreatedDate,
                                    Description = bank.Description ?? "No Description",
                                }).ToList();
                return bankList;
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
        public Enums.CrudStatus SetPayMode(PayModeModel model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();

                var oldPay = myshop.Gbl_Master_PayMode.Where(pay => (pay.PayModeId.Equals(model.PayModeId) || (pay.PayMode.ToLower().Equals(model.PayMode) || pay.PayMode.ToLower().Contains(model.PayMode))) && pay.IsDeleted == false).FirstOrDefault();
                if (oldPay != null)
                {
                    if (crudType == Enums.CrudType.Update)
                    {
                        oldPay.PayMode = model.PayMode;
                        oldPay.Description = model.PayModeDesc;
                        oldPay.IsDeleted = false;
                        oldPay.IsSync = false;
                        oldPay.ModifiedBy = WebSession.UserId;
                        oldPay.ModificationDate = DateTime.Now;
                        myshop.Entry(oldPay).State = EntityState.Modified;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        var stockEntry = myshop.Stk_Tr_Entry.Where(x => x.IsDeleted == false && x.PayModeId.Equals(model.PayModeId)).FirstOrDefault();
                        if (stockEntry == null)
                        {
                            oldPay.IsDeleted = true;
                            oldPay.IsSync = false;
                            oldPay.ModifiedBy = WebSession.UserId;
                            oldPay.ModificationDate = DateTime.Now;
                            myshop.Entry(oldPay).State = EntityState.Modified;
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
                    Gbl_Master_PayMode newPay = new Gbl_Master_PayMode();
                    newPay.PayMode = model.PayMode;
                    newPay.CreatedBy = WebSession.UserId;
                    newPay.CreatedDate = DateTime.Now;
                    newPay.Description = model.PayModeDesc;
                    newPay.IsDeleted = false;
                    newPay.IsSync = false;
                    newPay.ModifiedBy = WebSession.UserId;
                    newPay.ModificationDate = DateTime.Now;
                    myshop.Entry(newPay).State = EntityState.Added;
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
        public IEnumerable<object> GetPayModeJson()
        {
            try
            {
                myshop = new MyshopDb();
                var payList = (from pay in myshop.Gbl_Master_PayMode.Where(x => x.IsDeleted == false)
                               orderby pay.PayMode
                               select new
                               {
                                   pay.PayMode,
                                   pay.PayModeId,
                                   pay.CreatedDate,
                                   Description = pay.Description ?? "No Description",
                               }).ToList();
                return payList;
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
        public Enums.CrudStatus SetBankAccount(BankAccModel model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();

                var oldBank = myshop.Gbl_Master_BankAccount.Where(bank => (bank.BankAccId.Equals(model.BankAccId) || (bank.AccountNo.ToLower().Equals(model.AccountNo) || bank.AccountNo.ToLower().Contains(model.AccountNo))) && bank.IsDeleted == false).FirstOrDefault();
                if (oldBank != null)
                {
                    if (crudType == Enums.CrudType.Update)
                    {
                        oldBank.AccHolderName = model.AccountHolderName;
                        oldBank.AccountName = model.AccountName;
                        oldBank.AccountNo = model.AccountNo;
                        oldBank.BranchAddress = model.BranchAddress;
                        oldBank.BranchIFSC = model.BranchIFSC;
                        oldBank.BranchName = model.BranchName;
                        oldBank.IsDeleted = false;
                        oldBank.IsSync = false;
                        oldBank.ModifiedBy = WebSession.UserId;
                        oldBank.ModificationDate = DateTime.Now;
                        myshop.Entry(oldBank).State = EntityState.Modified;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        var stock = myshop.Stk_Tr_Entry.Where(x => x.IsDeleted == false && x.DebitAccount.Equals(model.BankAccId)).FirstOrDefault();
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
                    Gbl_Master_BankAccount newBank = new Gbl_Master_BankAccount();
                    newBank.AccHolderName = model.AccountHolderName;
                    newBank.AccountName = model.AccountName;
                    newBank.AccountNo = model.AccountNo;
                    newBank.AccTypeId = model.AccTypeId;
                    newBank.BankId = model.BankId;
                    newBank.BranchAddress = model.BranchAddress;
                    newBank.BranchIFSC = model.BranchIFSC;
                    newBank.BranchName = model.BranchName;
                    newBank.CreatedBy = WebSession.UserId;
                    newBank.CreatedDate = DateTime.Now;
                    newBank.ShopId = WebSession.ShopId;
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
        public IEnumerable<object> GetBankAccountJson()
        {
            try
            {
                myshop = new MyshopDb();
                var bankList = (from bankAcc in myshop.Gbl_Master_BankAccount.Where(x => x.IsDeleted == false && x.ShopId.Equals(WebSession.ShopId))
                                from bankAccType in myshop.Gbl_Master_BankAccountType.Where(x => x.IsDeleted == false && x.AccountTypeId == bankAcc.AccTypeId)
                                from bank in myshop.Gbl_Master_Bank.Where(x => x.IsDeleted == false && x.BankId == bankAcc.BankId)
                                orderby bankAcc.AccountName, bankAcc.AccountNo
                                select new
                                {
                                    bankAcc.AccHolderName,
                                    bankAcc.AccountName,
                                    bankAcc.AccountNo,
                                    bankAcc.AccTypeId,
                                    bankAcc.BankId,
                                    bankAcc.BranchAddress,
                                    bankAcc.BranchIFSC,
                                    bankAcc.BranchName,
                                    bankAccType.AccountType,
                                    bank.BankName
                                }).ToList();
                return bankList;
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
        public Enums.CrudStatus SetDocProofType(Gbl_Master_DocProofType model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();

                var oldProofType = myshop.Gbl_Master_DocProofType.Where(ProofType => (ProofType.DocProofTypeId.Equals(model.DocProofTypeId) || (ProofType.DocProofType.ToLower().Equals(model.DocProofType) || ProofType.DocProofType.ToLower().Contains(model.DocProofType))) && ProofType.IsDeleted == false).FirstOrDefault();
                if (oldProofType != null)
                {
                    if (crudType == Enums.CrudType.Update)
                    {
                        oldProofType.DocProofType = model.DocProofType;
                        oldProofType.Description = model.Description;
                        oldProofType.IsDeleted = false;
                        oldProofType.IsSync = false;
                        oldProofType.ModifiedBy = WebSession.UserId;
                        oldProofType.ModificationDate = DateTime.Now;
                        myshop.Entry(oldProofType).State = EntityState.Modified;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        var docProof = myshop.Gbl_Master_DocProof.Where(x => x.IsDeleted == false && x.DocProofTypeId.Equals(model.DocProofTypeId)).FirstOrDefault();
                        if (docProof == null)
                        {
                            oldProofType.IsDeleted = true;
                            oldProofType.IsSync = false;
                            oldProofType.ModifiedBy = WebSession.UserId;
                            oldProofType.ModificationDate = DateTime.Now;
                            myshop.Entry(oldProofType).State = EntityState.Modified;
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
                    Gbl_Master_DocProofType newBank = new Gbl_Master_DocProofType();
                    newBank.DocProofType = model.DocProofType;
                    newBank.CreatedBy = WebSession.UserId;
                    newBank.CreatedDate = DateTime.Now;
                    newBank.Description = model.Description;
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
        public IEnumerable<object> GetDocProofTypeJson()
        {
            try
            {
                myshop = new MyshopDb();
                var DocProofTypeList = (from DocProofType in myshop.Gbl_Master_DocProofType.Where(x => x.IsDeleted == false)
                                        orderby DocProofType.DocProofType
                                        select new
                                        {
                                            DocProofType.DocProofTypeId,
                                            DocProofType.DocProofType,
                                            DocProofType.CreatedDate,
                                            Description = DocProofType.Description ?? "No Description",
                                        }).ToList();
                return DocProofTypeList;
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
        public IEnumerable<object> GetDocProofJson()
        {
            try
            {
                myshop = new MyshopDb();
                var DocProofList = (from DocProof in myshop.Gbl_Master_DocProof.Where(x => x.IsDeleted == false)
                                    from DocProofType in myshop.Gbl_Master_DocProofType.Where(x => x.IsDeleted == false && x.DocProofTypeId.Equals(DocProof.DocProofTypeId))
                                    orderby DocProof.DocProof
                                    select new
                                    {
                                        DocProof.DocProofTypeId,
                                        DocProof.DocProofId,
                                        DocProofType.DocProofType,
                                        DocProof.DocProof,
                                        DocProof.CreatedDate,
                                        Description = DocProof.Description ?? "No Description",
                                    }).ToList();
                return DocProofList;
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
        public Enums.CrudStatus SetDocProof(Gbl_Master_DocProof model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();

                var oldProof = myshop.Gbl_Master_DocProof.Where(Proof => (Proof.DocProofId.Equals(model.DocProofId) || (Proof.DocProof.ToLower().Equals(model.DocProof) || Proof.DocProof.ToLower().Contains(model.DocProof))) && Proof.IsDeleted == false).FirstOrDefault();
                if (oldProof != null)
                {
                    if (crudType == Enums.CrudType.Update)
                    {
                        oldProof.DocProofTypeId = model.DocProofTypeId;
                        oldProof.DocProof = model.DocProof;
                        oldProof.Description = model.Description;
                        oldProof.IsDeleted = false;
                        oldProof.IsSync = false;
                        oldProof.ModifiedBy = WebSession.UserId;
                        oldProof.ModificationDate = DateTime.Now;
                        myshop.Entry(oldProof).State = EntityState.Modified;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        var emp = myshop.Gbl_Master_Employee.Where(x => x.IsDeleted == false && (x.AddressDocProofId.Equals(model.DocProofId) || x.IdentityDocProofId.Equals(model.DocProofId))).FirstOrDefault();
                        if (emp == null)
                        {
                            oldProof.IsDeleted = true;
                            oldProof.IsSync = false;
                            oldProof.ModifiedBy = WebSession.UserId;
                            oldProof.ModificationDate = DateTime.Now;
                            myshop.Entry(oldProof).State = EntityState.Modified;
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
                    Gbl_Master_DocProof newProof = new Gbl_Master_DocProof();
                    newProof.DocProof = model.DocProof;
                    newProof.DocProofTypeId = model.DocProofTypeId;
                    newProof.CreatedBy = WebSession.UserId;
                    newProof.CreatedDate = DateTime.Now;
                    newProof.Description = model.Description;
                    newProof.IsDeleted = false;
                    newProof.IsSync = false;
                    newProof.ModifiedBy = WebSession.UserId;
                    newProof.ModificationDate = DateTime.Now;
                    myshop.Entry(newProof).State = EntityState.Added;
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
        public Enums.CrudStatus SetCheque(ChequeModel model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();
                Gbl_Master_BankCheque newCheque = null;

                var oldCheque = myshop.Gbl_Master_BankCheque.Where(cheque => cheque.BankAccId.Equals(model.BankAccId) && ((model.PageStartNo >= cheque.PageStartNo && model.PageStartNo <= cheque.PageEndNo) || (model.PageEndNo >= cheque.PageStartNo && model.PageEndNo <= cheque.PageEndNo)) && cheque.IsDeleted == false).FirstOrDefault();
                if (oldCheque != null)
                {
                    var cheque = myshop.Gbl_Master_BankChequeDetails.Where(x => x.IsDeleted == false && x.ChequeBookId.Equals(model.ChequeId) && x.IsUsed).FirstOrDefault();
                    if (cheque == null)
                    {
                        if (crudType == Enums.CrudType.Update)
                        {
                            oldCheque.PageSize = model.PageSize;
                            oldCheque.PageStartNo = model.PageStartNo;
                            oldCheque.PageEndNo = model.PageEndNo;
                            oldCheque.IssueDate = model.IssueDate;
                            oldCheque.Description = model.Desc;
                            oldCheque.IsDeleted = false;
                            oldCheque.IsSync = false;
                            oldCheque.ModifiedBy = WebSession.UserId;
                            oldCheque.ModificationDate = DateTime.Now;
                            myshop.Entry(oldCheque).State = EntityState.Modified;
                        }
                        else if (crudType == Enums.CrudType.Delete)
                        {
                            oldCheque.IsDeleted = true;
                            oldCheque.IsSync = false;
                            oldCheque.ModifiedBy = WebSession.UserId;
                            oldCheque.ModificationDate = DateTime.Now;
                            myshop.Entry(oldCheque).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        return Enums.CrudStatus.AlreadyInUse;
                    }
                }
                else if (crudType == Enums.CrudType.Insert)
                {
                    newCheque = new Gbl_Master_BankCheque();
                    newCheque.PageSize = model.PageSize;
                    newCheque.Description = model.Desc;
                    newCheque.PageStartNo = model.PageStartNo;
                    newCheque.PageEndNo = model.PageEndNo;
                    newCheque.CreatedBy = WebSession.UserId;
                    newCheque.CreatedDate = DateTime.Now;
                    newCheque.BankAccId = model.BankAccId;
                    newCheque.IsDeleted = false;
                    newCheque.IsSync = false;
                    newCheque.ModifiedBy = WebSession.UserId;
                    newCheque.ModificationDate = DateTime.Now;
                    newCheque.IssueDate = model.IssueDate;
                    newCheque.ShopId = WebSession.ShopId;
                    myshop.Entry(newCheque).State = EntityState.Added;
                }

                int result = myshop.SaveChanges();
                if (newCheque != null)
                {
                    for (int i = newCheque.PageStartNo; i <= newCheque.PageEndNo; i++)
                    {
                        Gbl_Master_BankChequeDetails chequeDetails = new Gbl_Master_BankChequeDetails();
                        chequeDetails.ChequeBookId = newCheque.ChequeId;
                        chequeDetails.ChequeNo = i;
                        chequeDetails.CreatedBy = WebSession.UserId;
                        chequeDetails.CreatedDate = DateTime.Now;
                        chequeDetails.Desciption = Resource.AutoGenData;
                        chequeDetails.IsDeleted = false;
                        chequeDetails.IsSync = false;
                        chequeDetails.IsUsed = false;
                        chequeDetails.ModificationDate = DateTime.Now;
                        chequeDetails.ModifiedBy = WebSession.UserId;
                        chequeDetails.ShopId = WebSession.ShopId;
                        myshop.Entry(chequeDetails).State = EntityState.Added;
                    }
                    myshop.SaveChanges();
                }
                return Utility.CrudStatus(result, crudType);
            }
            catch (Exception ex)
            {
                return Enums.CrudStatus.Exception;
            }
            finally
            {
                myshop = null;
            }
        }

        public Enums.CrudStatus DeleteLogo(int ShopId, string ModuleName, int NewAttachmentId)
        {
            try
            {
                myshop = new MyshopDb();

                var oldAttachment = myshop.Gbl_Attachment.Where(att => att.ShopId.Equals(ShopId) && att.IsDeleted == false && att.ModuleName.Equals(ModuleName) && att.AttachmentId != NewAttachmentId).FirstOrDefault();
                if (oldAttachment != null)
                {

                    oldAttachment.IsDeleted = true;
                    oldAttachment.IsSync = false;
                    oldAttachment.ModifiedBy = WebSession.UserId;
                    oldAttachment.ModificationDate = DateTime.Now;
                    myshop.Entry(oldAttachment).State = EntityState.Modified;
                }

                int result = myshop.SaveChanges();
                return Utility.CrudStatus(result, Enums.CrudType.Delete);
            }
            catch (Exception ex)
            {
                return Enums.CrudStatus.Exception;
            }
            finally
            {

            }
        }

        public Enums.CrudStatus SetNotificationType(NotificationTypeModel model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();

                var oldNotiType = myshop.Gbl_Master_NotificationType.Where(noti => (noti.ShopId.Equals(WebSession.ShopId) && (noti.NotificationTypeId.Equals(model.NotificationTypeId) || noti.NotificationType.ToLower().Equals(model.NotificationType.Trim()) || noti.NotificationType.ToLower().Contains(model.NotificationType.Trim()))) && noti.IsDeleted == false).FirstOrDefault();
                if (oldNotiType != null)
                {
                    if (crudType == Enums.CrudType.Update)
                    {
                        oldNotiType.NotificationType = model.NotificationType;
                        oldNotiType.Description = model.Description;
                        oldNotiType.IsDeleted = false;
                        oldNotiType.IsSync = false;
                        oldNotiType.ModifiedBy = WebSession.UserId;
                        oldNotiType.ModificationDate = DateTime.Now;
                        myshop.Entry(oldNotiType).State = EntityState.Modified;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        var stock = myshop.Gbl_Master_Notification.Where(x => x.IsDeleted == false && x.ShopId.Equals(WebSession.ShopId)).FirstOrDefault();
                        if (stock == null)
                        {
                            oldNotiType.IsDeleted = true;
                            oldNotiType.IsSync = false;
                            oldNotiType.ModifiedBy = WebSession.UserId;
                            oldNotiType.ModificationDate = DateTime.Now;
                            myshop.Entry(oldNotiType).State = EntityState.Modified;
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
                    Gbl_Master_NotificationType newNotiType = new Gbl_Master_NotificationType();
                    newNotiType.NotificationType = model.NotificationType;
                    newNotiType.Description = model.Description;
                    newNotiType.IsDeleted = false;
                    newNotiType.IsSync = false;
                    newNotiType.CreatedBy = WebSession.UserId;
                    newNotiType.CreatedDate = DateTime.Now;
                    newNotiType.ModifiedBy = WebSession.UserId;
                    newNotiType.ModificationDate = DateTime.Now;
                    newNotiType.ShopId = WebSession.ShopId;
                    myshop.Entry(newNotiType).State = EntityState.Added;
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
        public Enums.CrudStatus DeleteNotification(int NotificationId)
        {
            myshop = new MyshopDb();

            var _oldNoti = myshop.Gbl_Master_Notification.Where(noti => noti.ShopId.Equals(WebSession.ShopId) && noti.IsDeleted == false && noti.NotificationId.Equals(NotificationId)).FirstOrDefault();
            if (_oldNoti != null)
            {
                _oldNoti.ModifiedBy = WebSession.UserId;
                _oldNoti.ModificationDate = DateTime.Now;
                _oldNoti.IsSync = false;
                _oldNoti.IsDeleted = true;
                myshop.Entry(_oldNoti).State = EntityState.Modified;
                int _result = myshop.SaveChanges();
                return Utility.CrudStatus(_result, Enums.CrudType.Delete);
            }
            else
                return Enums.CrudStatus.NotExist;
        }
        public Enums.CrudStatus ReadNotification(int NotificationId)
        {
            myshop = new MyshopDb();

            var _oldNoti = myshop.Gbl_Master_Notification.Where(noti => noti.ShopId.Equals(WebSession.ShopId) && !noti.IsDeleted && noti.NotificationId.Equals(NotificationId) && noti.PushedDate<=DateTime.Now && noti.IsPushed && noti.UserId.Equals(WebSession.UserId)).FirstOrDefault();
            if (_oldNoti != null)
            {
                _oldNoti.ModifiedBy = WebSession.UserId;
                _oldNoti.ModificationDate = DateTime.Now;
                _oldNoti.IsSync = false;
                _oldNoti.IsRead = true;
                myshop.Entry(_oldNoti).State = EntityState.Modified;
                int _result = myshop.SaveChanges();
                return Utility.CrudStatus(_result, Enums.CrudType.Update);
            }
            else
                return Enums.CrudStatus.NotExist;
        }
        public IEnumerable<object> GetNotificationTypeJson()
        {
            try
            {
                myshop = new MyshopDb();
                var notiTypeList = (from noti in myshop.Gbl_Master_NotificationType.Where(x => x.IsDeleted == false && x.ShopId.Equals(WebSession.ShopId))
                                    orderby noti.NotificationType
                                    select new
                                    {
                                        noti.NotificationType,
                                        noti.Description,
                                        noti.CreatedDate,
                                        noti.NotificationTypeId
                                    }).ToList();
                return notiTypeList;
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

        public Enums.CrudStatus SetNotification(NotificationDbModel model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();

                var oldNotiType = myshop.Gbl_Master_Notification.Where(noti => (noti.NotificationId.Equals(model.NotificationId) || (noti.Message.ToLower().Equals(model.Message.Trim()) && noti.UserId.Equals(model.UserId))) && noti.IsDeleted == false).FirstOrDefault();
                if (oldNotiType != null)
                {
                    if (crudType == Enums.CrudType.Update)
                    {
                        oldNotiType.Message = model.Message;
                        oldNotiType.NotificationTypeId = model.NotificationTypeId;
                        oldNotiType.MessageExpireDate = model.MessageExpireDate;
                        oldNotiType.IsForAll = model.IsForAll;
                        oldNotiType.IsPushed = false;
                        oldNotiType.IsRead = false;
                        oldNotiType.IsDeleted = false;
                        oldNotiType.IsSync = false;
                        oldNotiType.ModifiedBy = WebSession.UserId;
                        oldNotiType.ModificationDate = DateTime.Now;
                        myshop.Entry(oldNotiType).State = EntityState.Modified;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        var stock = myshop.Gbl_Master_Notification.Where(x => x.IsDeleted == false && x.ShopId.Equals(WebSession.ShopId)).FirstOrDefault();
                        if (stock == null)
                        {
                            oldNotiType.IsDeleted = true;
                            oldNotiType.IsSync = false;
                            oldNotiType.ModifiedBy = WebSession.UserId;
                            oldNotiType.ModificationDate = DateTime.Now;
                            myshop.Entry(oldNotiType).State = EntityState.Modified;
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
                    Gbl_Master_Notification newNotification = new Gbl_Master_Notification();
                    newNotification.Message = model.Message;
                    newNotification.NotificationTypeId = model.NotificationTypeId;
                    newNotification.MessageExpireDate = model.MessageExpireDate;
                    newNotification.IsForAll = model.IsForAll;
                    newNotification.UserId = model.UserId;
                    newNotification.IsDeleted = false;
                    newNotification.IsSync = false;
                    newNotification.IsPushed = false;
                    newNotification.IsRead = false;
                    newNotification.CreatedBy = WebSession.UserId;
                    newNotification.CreatedDate = DateTime.Now;
                    newNotification.ShopId = WebSession.ShopId;
                    myshop.Entry(newNotification).State = EntityState.Added;
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
        public Enums.CrudStatus SendNotification(int NotificationId, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();

                var oldNotiType = myshop.Gbl_Master_Notification.Where(noti => (noti.NotificationId.Equals(NotificationId) && noti.ShopId.Equals(WebSession.ShopId)) && noti.IsDeleted == false).FirstOrDefault();
                if (oldNotiType != null)
                {
                    if (oldNotiType.IsPushed)
                    {
                        return Enums.CrudStatus.AlreadySendNotification;
                    }
                    else if (crudType == Enums.CrudType.Update )
                    {
                        if (oldNotiType.MessageExpireDate >= DateTime.Now)
                        {
                            if (oldNotiType.Gbl_Master_NotificationType.NotificationType.ToLower().IndexOf("email") > -1)
                            {
                                string _username = string.Format("{0} {1}", oldNotiType.Gbl_Master_User.Firstname, oldNotiType.Gbl_Master_User.Lastname);
                                if (!oldNotiType.IsForAll)
                                {
                                    Utility.EmailSendHtmlFormatted(oldNotiType.Gbl_Master_User.Username.Trim(), "Notification from " + WebSession.ShopName, Utility.EmailNotificationBody(_username, oldNotiType.Message));
                                }
                                else
                                {
                                    List<string> email = myshop.Gbl_Master_User.Where(x => x.ShopId.Equals(WebSession.ShopId) && x.IsActive == true && !x.IsDeleted).Select(x => x.Username).ToList();
                                    Utility.EmailSendHtmlFormatted(email, "Notification from " + WebSession.ShopName, Utility.EmailNotificationBody(_username, oldNotiType.Message));
                                }

                            }
                            oldNotiType.IsPushed = true;
                            oldNotiType.IsSync = false;
                            oldNotiType.PushedDate = DateTime.Now;
                            oldNotiType.ModifiedBy = WebSession.UserId;
                            oldNotiType.ModificationDate = DateTime.Now;
                            myshop.Entry(oldNotiType).State = EntityState.Modified;
                        }
                        else
                        {
                            return Enums.CrudStatus.NotificationExpired;
                        }
                    }                    
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        oldNotiType.IsDeleted = true;
                        oldNotiType.IsSync = false;
                        oldNotiType.ModifiedBy = WebSession.UserId;
                        oldNotiType.ModificationDate = DateTime.Now;
                        myshop.Entry(oldNotiType).State = EntityState.Modified;
                    }
                    else
                    {
                        return Enums.CrudStatus.AlreadyExistForSameShop;
                    }
                }
                else
                {
                    return Enums.CrudStatus.NotExist;
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
                if (myshop != null)
                    myshop = null;
            }
        }
        public IEnumerable<object> GetNotificationJson(bool fetchPushed=true)
        {
            try
            {
                myshop = new MyshopDb();
                var notiTypeList = (from noti in myshop.Gbl_Master_Notification.Where(x => x.IsDeleted == false && x.ShopId.Equals(WebSession.ShopId) && (fetchPushed || x.IsPushed==false))
                                    orderby noti.CreatedBy descending
                                    select new
                                    {
                                        noti.Message,
                                        noti.MessageExpireDate,
                                        noti.NotificationTypeId,
                                        noti.NotificationId,
                                        noti.Gbl_Master_NotificationType.NotificationType,
                                        noti.UserId,
                                        UserName = noti.Gbl_Master_User.Firstname+" "+noti.Gbl_Master_User.Lastname,
                                        noti.CreatedDate,
                                        noti.IsForAll
                                    }).ToList();
                return notiTypeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (myshop != null)
                    myshop = null;
            }
        }

        public IEnumerable<object> GetChequeJson()
        {
            try
            {
                myshop = new MyshopDb();
                var chequeList = (from cheque in myshop.Gbl_Master_BankCheque.Where(x => x.IsDeleted == false)
                                  from acc in myshop.Gbl_Master_BankAccount.Where(x => x.BankAccId == cheque.BankAccId && x.IsDeleted == false)
                                  from bank in myshop.Gbl_Master_Bank.Where(x => x.BankId == acc.BankId && x.IsDeleted == false)
                                  orderby cheque.PageStartNo
                                  select new
                                  {
                                      cheque.BankAccId,
                                      cheque.PageSize,
                                      cheque.PageStartNo,
                                      cheque.PageEndNo,
                                      Desc = cheque.Description,
                                      acc.AccountNo,
                                      bank.BankName,
                                      cheque.IssueDate,
                                      cheque.ChequeId

                                  }).ToList();
                return chequeList;
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