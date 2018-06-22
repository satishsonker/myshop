﻿using DataLayer;
using Myshop.App_Start;
using Myshop.GlobalResource;
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
                    Gbl_Master_DocProof newProof= new Gbl_Master_DocProof();
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