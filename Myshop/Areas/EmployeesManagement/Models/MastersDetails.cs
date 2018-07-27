using DataLayer;
using Myshop.App_Start;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Myshop.Areas.EmployeesManagement.Models
{
    public class MastersDetails
    {
        MyshopDb myshop;
        Tuple<int, byte[]> FileStatus;
        public Tuple<Enums.CrudStatus, Gbl_Master_Employee> SetEmployee(Gbl_Master_Employee model, Enums.CrudType crudType, HttpPostedFileBase files = null)
        {
            try
            {
                if (files != null)
                {
                    FileStatus = GlobalMethod.FileUpload(files, files.FileName, GlobalResource.Resource.Module_AddEmployee);
                    if (FileStatus.Item1 < 1)
                        return new Tuple<Enums.CrudStatus, Gbl_Master_Employee>(Enums.CrudStatus.FileNotUploaded, model);
                    else if(FileStatus.Item2.Length>0)
                    {
                        model.UserImage = Convert.ToBase64String(FileStatus.Item2);
                    }

                }
                myshop = new MyshopDb();

                var oldEmp = myshop.Gbl_Master_Employee.Where(emp => emp.EmpId.Equals(model.EmpId) && emp.IsDeleted == false).FirstOrDefault();
                Gbl_Master_Employee newEmp = new Gbl_Master_Employee();
                if (oldEmp != null)
                {
                    if (crudType == Enums.CrudType.Update)
                    {
                        oldEmp.Address = model.Address;
                        oldEmp.City = model.City;
                        oldEmp.Distict = model.Distict;
                        oldEmp.DOB = model.DOB;
                        oldEmp.FatherName = model.FatherName;
                        oldEmp.FirstName = model.FirstName;
                        oldEmp.IsAppAccess = model.IsAppAccess;
                        oldEmp.LastName = model.LastName;
                        oldEmp.Mobile = model.Mobile;
                        oldEmp.PANCardNo = model.PANCardNo;
                        oldEmp.PINCode = model.PINCode;
                        oldEmp.RoleId = model.RoleId;
                        oldEmp.State = model.State;
                        oldEmp.IsDeleted = false;
                        oldEmp.IsSync = false;
                        oldEmp.ImageId = FileStatus.Item1;
                        oldEmp.ModifiedBy = WebSession.UserId;
                        oldEmp.ModificationDate = DateTime.Now;

                        myshop.Entry(oldEmp).State = EntityState.Modified;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        //var stock = myshop.Gbl_Master_BankAccount.Where(x => x.IsDeleted == false && x.BankId.Equals(model.BankId)).FirstOrDefault();
                        //if (stock == null)
                        //{
                        oldEmp.IsDeleted = true;
                        oldEmp.IsSync = false;
                        oldEmp.ModifiedBy = WebSession.UserId;
                        oldEmp.ModificationDate = DateTime.Now;
                        myshop.Entry(oldEmp).State = EntityState.Modified;
                        //}
                        //else
                        //{
                        //    return Enums.CrudStatus.AlreadyInUse;
                        //}
                    }
                    else
                    {
                        return new Tuple<Enums.CrudStatus, Gbl_Master_Employee>(Enums.CrudStatus.AlreadyExistForSameShop, model);
                    }
                }
                else if (crudType == Enums.CrudType.Insert)
                {
                    newEmp.AadharNo = model.AadharNo;
                    newEmp.Address = model.Address;
                    newEmp.City = model.City;
                    newEmp.Distict = model.Distict;
                    newEmp.DOB = model.DOB;
                    newEmp.DOJ = model.DOJ;
                    newEmp.EmailId = model.EmailId;
                    newEmp.ShopId = WebSession.ShopId;
                    newEmp.FatherName = model.FatherName;
                    newEmp.FirstName = model.FirstName;
                    newEmp.IsActive = true;
                    newEmp.IsAppAccess = model.IsAppAccess;
                    newEmp.LastName = model.LastName;
                    newEmp.Mobile = model.Mobile;
                    newEmp.PANCardNo = model.PANCardNo;
                    newEmp.PINCode = model.PINCode;
                    newEmp.RoleId = model.RoleId;
                    newEmp.State = model.State;
                    newEmp.CreatedDate = DateTime.Now;
                    newEmp.CreatedBy = WebSession.UserId;
                    newEmp.IsDeleted = false;
                    newEmp.IsSync = false;
                    newEmp.ModifiedBy = WebSession.UserId;
                    newEmp.ModificationDate = DateTime.Now;
                    newEmp.ImageId = FileStatus.Item1;
                    newEmp.AddressDocProofId = model.AddressDocProofId;
                    newEmp.AddressDocProofNo = model.AddressDocProofNo;
                    newEmp.IdentityDocProofId = model.IdentityDocProofId;
                    newEmp.IdentityDocProofNo = model.IdentityDocProofNo;
                    newEmp.Gender = model.Gender.ToLower() == "male" ? "M" : "F";
                    newEmp.EmergencyContactNo = model.EmergencyContactNo;
                    newEmp.TotalExp = model.TotalExp;
                    newEmp.BloodGroup = model.BloodGroup;
                    newEmp.IsAppAccess = model.IsAppAccess;
                    myshop.Entry(newEmp).State = EntityState.Added;
                }

                int result = myshop.SaveChanges();
                model.EmpId = newEmp.EmpId;
                return new Tuple<Enums.CrudStatus, Gbl_Master_Employee>(Utility.CrudStatus(result, crudType), model);
            }
            catch (Exception ex)
            {
                GlobalMethod.LogError("Masters", "Set Employee", "Employee Management", ex.Message, ex);
                return new Tuple<Enums.CrudStatus, Gbl_Master_Employee>(Enums.CrudStatus.Exception, model);
            }
            finally
            {

            }
        }
        public Enums.CrudStatus SetEmployeeRole(Gbl_Master_Employee_Role model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();

                var oldEmpRole = myshop.Gbl_Master_Employee_Role.Where(emp => emp.RoleId.Equals(model.RoleId) && emp.IsDeleted == false).FirstOrDefault();
                if (oldEmpRole != null)
                {
                    if (crudType == Enums.CrudType.Update)
                    {
                        oldEmpRole.RoleType = model.RoleType;
                        oldEmpRole.Description = model.Description;
                        oldEmpRole.IsDeleted = false;
                        oldEmpRole.IsSync = false;
                        oldEmpRole.ModifiedBy = WebSession.UserId;
                        oldEmpRole.ModificationDate = DateTime.Now;
                        oldEmpRole.ShopId = WebSession.ShopId;
                        myshop.Entry(oldEmpRole).State = EntityState.Modified;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        var emp = myshop.Gbl_Master_Employee.Where(x => x.IsDeleted == false && x.RoleId.Equals(model.RoleId)).FirstOrDefault();
                        if (emp == null)
                        {
                            oldEmpRole.IsDeleted = true;
                            oldEmpRole.IsSync = false;
                            oldEmpRole.ModifiedBy = WebSession.UserId;
                            oldEmpRole.ModificationDate = DateTime.Now;
                            myshop.Entry(oldEmpRole).State = EntityState.Modified;
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
                    Gbl_Master_Employee_Role newEmpRole = new Gbl_Master_Employee_Role();
                    newEmpRole.RoleType = model.RoleType;
                    newEmpRole.Description = model.Description;
                    newEmpRole.CreatedDate = DateTime.Now;
                    newEmpRole.CreatedBy = WebSession.UserId;
                    newEmpRole.IsDeleted = false;
                    newEmpRole.IsSync = false;
                    newEmpRole.ModifiedBy = WebSession.UserId;
                    newEmpRole.ModificationDate = DateTime.Now;
                    newEmpRole.ShopId = WebSession.ShopId;
                    myshop.Entry(newEmpRole).State = EntityState.Added;
                }

                int result = myshop.SaveChanges();
                return Utility.CrudStatus(result, crudType);
            }
            catch (Exception ex)
            {
                GlobalMethod.LogError("Masters", "Set Employee Role", "Employee Management", ex.Message, ex);
                return Enums.CrudStatus.Exception;
            }
            finally
            {

            }
        }
        public List<EmpRoleModel> GetRoleTypeJson()
        {
            try
            {
                myshop = new MyshopDb();
                var roleType = (from role in myshop.Gbl_Master_Employee_Role.Where(role => role.IsDeleted == false)
                                select new EmpRoleModel {RoleType=role.RoleType,
                                    IsActive= role.IsActive,
                                    Description=role.Description,
                                    CreatedDate=role.CreatedDate,
                                    RoleId=role.RoleId                                    
                                }).ToList();
                return roleType;
            }
            catch (Exception ex)
            {
                GlobalMethod.LogError("Masters", "GetRoleTypeJson", "Employee Management", ex.Message, ex);
                throw;
            }
        }

        public JsonResponseModelForPaging<EmpModel> GetEmpJson(int PageNo,int PageSize)
        {
            try
            {
                myshop = new MyshopDb();
                var Allemp= myshop.SpGetEmpList(PageNo, PageSize,WebSession.ShopId);
                List<EmpModel> EmpList = new List<EmpModel>();
                foreach (SpGetEmpList_Result item in Allemp)
                {
                    EmpModel newEmp = new EmpModel();

                    newEmp.AadharNo = item.AadharNo;
                    newEmp.Address = item.Address;
                                  newEmp.City = item.City;
                                  newEmp.CreatedBy = item.CreatedBy;
                                  newEmp.Distict = item.Distict;
                                 newEmp.DOB = item.DOB;
                                  newEmp.DOJ = item.DOJ;
                                  newEmp.DOR = item.DOR;
                                  newEmp.EmailId = item.EmailId;
                                  newEmp.EmpId = item.EmpId;
                                  newEmp.FatherName = item.FatherName;
                                  newEmp.FirstName = item.FirstName;
                                  newEmp.ImageId = item.ImageId;
                                  newEmp.LastName = item.LastName;
                                  newEmp.Mobile = item.Mobile;
                                  newEmp.PANCardNo = item.PANCardNo;
                                 newEmp.RoleId = item.RoleId;
                                  newEmp.State = item.State;
                    newEmp.UserImage = Convert.ToBase64String(item.UserImage);
                                  newEmp.Gender = item.Gender;
                    newEmp.PINCode = item.PINCode;
                    EmpList.Add(newEmp);
                }
                return new JsonResponseModelForPaging<EmpModel>
                {
                    objList = EmpList,
                    TotalRecords = EmpList.Count(),
                    PageNo=PageNo,
                    PageSize=PageSize
                };
            }
            catch (Exception ex)
            {
                GlobalMethod.LogError("Masters", "GetEmpJson", "Employee Management", ex.Message, ex);
                throw;
            }
        }
    }
}