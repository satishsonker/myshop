using DataLayer;
using Myshop.Areas.Global.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Myshop.App_Start
{
    public static class GlobalMethod
    {
        public static MyshopDb myshop = null;
        public static List<SelectListModel> GetSingleSelectList()
        {
            List<SelectListModel> list = new List<SelectListModel>();
            SelectListModel item = new SelectListModel() { Text = "No Data", Value = 0 };
            list.Add(item);
            return list;
        }
        public static List<SelectListModel> GetAccTypes()
        {
            try
            {
                myshop = new MyshopDb();
                List<SelectListModel> list = new List<SelectListModel>();
                var bankList = myshop.Gbl_Master_BankAccountType.Where(bank => bank.IsDeleted == false).OrderBy(x => x.AccountType).ToList();
                if (bankList.Count > 0)
                {
                    foreach (Gbl_Master_BankAccountType currentItem in bankList)
                    {
                        SelectListModel newItem = new SelectListModel();
                        newItem.Text = currentItem.AccountType;
                        newItem.Value = currentItem.AccountTypeId;
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
        public static List<SelectListModel> GetPayMode()
        {
            try
            {
                myshop = new MyshopDb();
                List<SelectListModel> list = new List<SelectListModel>();
                var PayList = myshop.Gbl_Master_PayMode.Where(pay => pay.IsDeleted == false).OrderBy(x => x.PayMode).ToList();
                if (PayList.Count > 0)
                {
                    foreach (Gbl_Master_PayMode currentItem in PayList)
                    {
                        SelectListModel newItem = new SelectListModel();
                        newItem.Text = currentItem.PayMode;
                        newItem.Value = currentItem.PayModeId;
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
        public static List<SelectListModel> GetBanks()
        {
            try
            {
                myshop = new MyshopDb();
                List<SelectListModel> list = new List<SelectListModel>();
                var bankList = myshop.Gbl_Master_Bank.Where(bank => bank.IsDeleted == false).OrderBy(x => x.BankName).ToList();
                if (bankList.Count > 0)
                {
                    foreach (Gbl_Master_Bank currentItem in bankList)
                    {
                        SelectListModel newItem = new SelectListModel();
                        newItem.Text = currentItem.BankName;
                        newItem.Value = currentItem.BankId;
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
        public static List<SelectListModel> GetDocProofTypes()
        {
            try
            {
                myshop = new MyshopDb();
                List<SelectListModel> list = new List<SelectListModel>();
                var DocTypeList = myshop.Gbl_Master_DocProofType.Where(type => type.IsDeleted == false).OrderBy(x => x.DocProofType).ToList();
                if (DocTypeList.Count > 0)
                {
                    foreach (Gbl_Master_DocProofType currentItem in DocTypeList)
                    {
                        SelectListModel newItem = new SelectListModel();
                        newItem.Text = currentItem.DocProofType;
                        newItem.Value = currentItem.DocProofTypeId;
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
        public static List<SelectListModel> GetDocProofs(int DocProofTypeId)
        {
            try
            {
                myshop = new MyshopDb();
                List<SelectListModel> list = new List<SelectListModel>();
                var DocProofList = myshop.Gbl_Master_DocProof.Where(type => type.IsDeleted == false && type.DocProofTypeId.Equals(DocProofTypeId)).OrderBy(x => x.DocProof).ToList();
                if (DocProofList.Count > 0)
                {
                    foreach (Gbl_Master_DocProof currentItem in DocProofList)
                    {
                        SelectListModel newItem = new SelectListModel();
                        newItem.Text = currentItem.DocProof;
                        newItem.Value = currentItem.DocProofId;
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
        public static List<SelectListModel> GetBankAccounts()
        {
            try
            {
                myshop = new MyshopDb();
                List<SelectListModel> list = new List<SelectListModel>();
                var bankList = myshop.Gbl_Master_BankAccount.Where(bank => bank.IsDeleted == false && bank.ShopId.Equals(WebSession.ShopId)).OrderBy(x => x.AccountName).ToList();
                if (bankList.Count > 0)
                {
                    foreach (Gbl_Master_BankAccount currentItem in bankList)
                    {
                        SelectListModel newItem = new SelectListModel();
                        newItem.Text = currentItem.AccountNo + " - " + currentItem.AccountName;
                        newItem.Value = currentItem.BankAccId;
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
        public static List<SelectListModel> GetCheques()
        {
            try
            {
                myshop = new MyshopDb();
                List<SelectListModel> list = new List<SelectListModel>();
                var chequeList = myshop.Gbl_Master_BankCheque.Where(bank => bank.IsDeleted == false).OrderBy(x => x.PageStartNo).ToList();
                if (chequeList.Count > 0)
                {
                    foreach (Gbl_Master_BankCheque currentItem in chequeList)
                    {
                        SelectListModel newItem = new SelectListModel();
                        newItem.Text = currentItem.PageStartNo.ToString();
                        newItem.Value = currentItem.PageEndNo;
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
        public static List<SelectListModel> GetAppModules()
        {
            try
            {
                myshop = new MyshopDb();
                List<SelectListModel> list = new List<SelectListModel>();
                var appList = myshop.Gbl_Master_AppModule.Where(bank => bank.IsDeleted == false).OrderBy(x => x.ModuleName).ToList();
                if (appList.Count > 0)
                {
                    foreach (Gbl_Master_AppModule currentItem in appList)
                    {
                        SelectListModel newItem = new SelectListModel();
                        newItem.Text = currentItem.ModuleName;
                        newItem.Value = currentItem.ModuleId;
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
        public static List<SelectListModel> GetCatogaries(int ShopId = 0)
        {
            try
            {
                myshop = new MyshopDb();
                List<SelectListModel> list = new List<SelectListModel>();
                var catList = myshop.Gbl_Master_Category.Where(cat => cat.ShopId.Equals(WebSession.ShopId) && cat.IsDeleted == false && (ShopId == 0 || cat.ShopId == ShopId)).OrderBy(x => x.CatName).ToList();
                if (catList.Count > 0)
                {
                    foreach (Gbl_Master_Category currentItem in catList)
                    {
                        SelectListModel newItem = new SelectListModel();
                        newItem.Text = currentItem.CatName;
                        newItem.Value = currentItem.CatId;
                        list.Add(newItem);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                return GlobalMethod.GetSingleSelectList();
            }
            finally
            {
                if (myshop != null)
                    myshop = null;
            }
        }
        public static List<SelectListModel> GetSubCatogaries(int catId, int ShopId = 0)
        {
            try
            {
                myshop = new MyshopDb();
                List<SelectListModel> list = new List<SelectListModel>();
                var subCatList = myshop.Gbl_Master_SubCategory.Where(cat => cat.ShopId.Equals(WebSession.ShopId) && cat.CatId.Equals(catId) && cat.IsDeleted == false && (ShopId == 0 || cat.ShopId == ShopId)).OrderBy(x => x.SubCatName).ToList();
                if (subCatList.Count > 0)
                {
                    foreach (Gbl_Master_SubCategory currentItem in subCatList)
                    {
                        SelectListModel newItem = new SelectListModel();
                        newItem.Text = currentItem.SubCatName;
                        newItem.Value = currentItem.SubCatId;
                        list.Add(newItem);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                return GlobalMethod.GetSingleSelectList();
            }
            finally
            {
                if (myshop != null)
                    myshop = null;
            }
        }
        public static List<SelectListModel> GetBrands()
        {
            try
            {
                myshop = new MyshopDb();
                List<SelectListModel> list = new List<SelectListModel>();
                var brandList = myshop.Gbl_Master_Brand.Where(brand => brand.ShopId.Equals(WebSession.ShopId) && brand.IsDeleted == false).OrderBy(x => x.BrandName).ToList();
                if (brandList.Count > 0)
                {
                    foreach (Gbl_Master_Brand currentItem in brandList)
                    {
                        SelectListModel newItem = new SelectListModel();
                        newItem.Text = currentItem.BrandName;
                        newItem.Value = currentItem.BrandId;
                        list.Add(newItem);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                return GlobalMethod.GetSingleSelectList();
            }
            finally
            {
                if (myshop != null)
                    myshop = null;
            }
        }
        public static List<SelectListModel> GetEmpRole()
        {
            try
            {
                myshop = new MyshopDb();
                List<SelectListModel> list = new List<SelectListModel>();
                var roleList = myshop.Gbl_Master_Employee_Role.Where(role => role.IsDeleted == false && role.ShopId.Equals(WebSession.ShopId)).OrderBy(x => x.RoleType).ToList();
                if (roleList.Count > 0)
                {
                    foreach (Gbl_Master_Employee_Role currentItem in roleList)
                    {
                        SelectListModel newItem = new SelectListModel();
                        newItem.Text = currentItem.RoleType;
                        newItem.Value = currentItem.RoleId;
                        list.Add(newItem);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                return GlobalMethod.GetSingleSelectList();
            }
            finally
            {
                if (myshop != null)
                    myshop = null;
            }
        }
        public static List<SelectListModel> GetDdEmpList()
        {
            try
            {
                myshop = new MyshopDb();
                List<SelectListModel> list = new List<SelectListModel>();
                var empList = myshop.Gbl_Master_Employee.Where(role => role.IsDeleted == false && role.ShopId.Equals(WebSession.ShopId)).OrderBy(x => x.FirstName).ToList();
                if (empList.Count > 0)
                {
                    foreach (Gbl_Master_Employee currentItem in empList)
                    {
                        SelectListModel newItem = new SelectListModel();
                        newItem.Text = currentItem.FirstName+" "+currentItem.LastName;
                        newItem.Value = currentItem.RoleId;
                        list.Add(newItem);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                return GlobalMethod.GetSingleSelectList();
            }
            finally
            {
                if (myshop != null)
                    myshop = null;
            }
        }
        public static List<SelectListModel> GetVendors()
        {
            try
            {
                myshop = new MyshopDb();
                List<SelectListModel> list = new List<SelectListModel>();
                var venList = myshop.Gbl_Master_Vendor.Where(brand => brand.ShopId.Equals(WebSession.ShopId) && brand.IsDeleted == false).OrderBy(x => x.VendorName).ToList();
                if (venList.Count > 0)
                {
                    foreach (Gbl_Master_Vendor currentItem in venList)
                    {
                        SelectListModel newItem = new SelectListModel();
                        newItem.Text = currentItem.VendorName;
                        newItem.Value = currentItem.VendorId;
                        list.Add(newItem);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                return GlobalMethod.GetSingleSelectList();
            }
            finally
            {
                if (myshop != null)
                    myshop = null;
            }
        }
        public static List<SelectListModel> GetUnit()
        {
            try
            {
                myshop = new MyshopDb();
                List<SelectListModel> list = new List<SelectListModel>();
                var unitList = myshop.Gbl_Master_Unit.Where(unit => unit.ShopId.Equals(WebSession.ShopId) && unit.IsDeleted == false).OrderBy(x => x.UnitName).ToList();
                if (unitList.Count > 0)
                {
                    foreach (Gbl_Master_Unit currentItem in unitList)
                    {
                        SelectListModel newItem = new SelectListModel();
                        newItem.Text = currentItem.UnitName;
                        newItem.Value = currentItem.UnitId;
                        list.Add(newItem);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                return GlobalMethod.GetSingleSelectList();
            }
            finally
            {
                if (myshop != null)
                    myshop = null;
            }
        }
        public static List<SelectListModel> GetProUnit(int SubCatId)
        {
            try
            {
                myshop = new MyshopDb();
                List<SelectListModel> list = new List<SelectListModel>();
                var unitList = (from pro in myshop.Gbl_Master_Product.Where(pro => pro.ShopId.Equals(WebSession.ShopId) && pro.IsDeleted == false && pro.SubCatId == SubCatId)
                                from unt in myshop.Gbl_Master_Unit.Where(un => un.UnitId.Equals(pro.UnitId))
                                orderby unt.UnitName
                                select new SelectListModel
                                {
                                    Text = unt.UnitName,
                                    Value = unt.UnitId
                                }
                               ).ToList();
                if (unitList.Count > 0)
                {
                    return list;
                }
                else
                {
                    return GlobalMethod.GetSingleSelectList();
                }
            }
            catch (Exception ex)
            {
                return GlobalMethod.GetSingleSelectList();
            }
            finally
            {
                if (myshop != null)
                    myshop = null;
            }
        }
        public static List<SelectListModel> GetPayModes()
        {
            try
            {
                myshop = new MyshopDb();
                List<SelectListModel> list = new List<SelectListModel>();
                var venList = myshop.Gbl_Master_PayMode.Where(pay => pay.IsDeleted == false).OrderBy(x => x.PayMode).ToList();
                if (venList.Count > 0)
                {
                    foreach (Gbl_Master_PayMode currentItem in venList)
                    {
                        SelectListModel newItem = new SelectListModel();
                        newItem.Text = currentItem.PayMode;
                        newItem.Value = currentItem.PayModeId;
                        list.Add(newItem);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                return GlobalMethod.GetSingleSelectList();
            }
            finally
            {
                if (myshop != null)
                    myshop = null;
            }
        }
        public static List<SelectListModel> GetChequeNoByAccNo(int AccId, bool isAllCheque)
        {
            try
            {
                if (AccId > 0)
                {
                    myshop = new MyshopDb();
                    var result = (
                        from bc in myshop.Gbl_Master_BankCheque.Where(pay => pay.IsDeleted == false && pay.BankAccId.Equals(AccId) && pay.ShopId == WebSession.ShopId)
                        from cb in myshop.Gbl_Master_BankChequeDetails.Where(y => y.IsDeleted == false && (isAllCheque == true || y.IsUsed == false) && bc.ChequeId.Equals(y.ChequeBookId))
                        orderby cb.ChequeNo
                        select new SelectListModel
                        {
                            Text = cb.ChequeNo.ToString(),
                            Value = cb.ChequePageId
                        }
                        ).ToList();
                    var venList = myshop.Gbl_Master_BankCheque.Where(pay => pay.IsDeleted == false && pay.BankAccId.Equals(AccId) && pay.ShopId == WebSession.ShopId).ToList();
                    if (result.Count > 0)
                    {
                        return result;

                    }
                    else
                        return GlobalMethod.GetSingleSelectList();
                }
                else
                    throw new Exception();
            }
            catch (Exception ex)
            {
                return GlobalMethod.GetSingleSelectList();
            }
            finally
            {
                if (myshop != null)
                    myshop = null;
            }
        }
        public static List<SelectListModel> GetCustomerType()
        {
            try
            {
                myshop = new MyshopDb();
                List<SelectListModel> list = new List<SelectListModel>();
                var custTypeList = myshop.Gbl_Master_CustomerType.Where(custType => custType.IsDeleted == false && custType.ShopId==WebSession.ShopId).OrderBy(x => x.CustomerType).ToList();
                if (custTypeList.Count > 0)
                {
                    foreach (Gbl_Master_CustomerType currentItem in custTypeList)
                    {
                        SelectListModel newItem = new SelectListModel();
                        newItem.Text = currentItem.CustomerType;
                        newItem.Value = currentItem.CustomerTypeId;
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
        public static List<SelectListModel> GetUserList()
        {
            try
            {
                myshop = new MyshopDb();
                List<SelectListModel> list = new List<SelectListModel>();
                var userList = myshop.Gbl_Master_User.Where(user => user.IsDeleted == false && user.ShopId == WebSession.ShopId).OrderBy(x => x.Firstname).ToList();
                if (userList.Count > 0)
                {
                    foreach (Gbl_Master_User currentItem in userList)
                    {
                        SelectListModel newItem = new SelectListModel();
                        newItem.Text = string.Format("{0} {1}", currentItem.Firstname, currentItem.Lastname);
                        newItem.Value = currentItem.UserId;
                        list.Add(newItem);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                return new List<SelectListModel>();
            }
            finally
            {
                if (myshop != null)
                    myshop = null;
            }
        }
        public static IEnumerable<object> GetUserJsonWithPhoto(bool allList = false, string searchValue = "")
        {
            try
            {
                myshop = new MyshopDb();
                var userList = (from user in myshop.Gbl_Master_User.Where(x => x.IsDeleted == false
                                && (searchValue == "" || x.Firstname.ToLower().Contains(searchValue))
                                || (searchValue == "" || x.Lastname.ToLower().Contains(searchValue))
                                || (searchValue == "" || x.Username.ToLower().Contains(searchValue))
                                || (searchValue == "" || x.Mobile.ToLower().Contains(searchValue))
                                )
                                from userType in myshop.Gbl_Master_UserType.Where(x => x.IsDeleted == false && user.UserType.Equals(x.Id))
                                orderby user.Firstname
                                select new UserModel
                                {
                                    Username = user.Username,
                                    Name = user.Firstname + " " + user.Lastname,
                                    Mobile = user.Mobile,
                                    Gender = user.Gender,
                                    UserType = userType.Type,
                                    UserTypeId = user.UserType,
                                    UserId = user.UserId,
                                    CreatedDate = user.CreationDate,
                                    IsActive = user.IsActive ?? false,
                                    IsBlocked = user.IsBlocked ?? false,
                                    Img = user.Photo,
                                    Photo = string.Empty
                                }).ToList();
                if (allList)
                {
                    var currentUser = userList.Where(x => x.UserId.Equals(WebSession.UserId)).FirstOrDefault();
                    if (currentUser != null)
                    {
                        userList.Remove(currentUser);
                    }
                }

                foreach (var item in userList)
                {
                    if (item.Img != null)
                    {
                        item.Photo = Convert.ToBase64String(Utility.GetImageThumbnails(item.Img, 100));
                        item.Img = new byte[0];
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
        public static List<SelectListModel> GetNotificationTypeList()
        {
            try
            {
                myshop = new MyshopDb();
                List<SelectListModel> list = new List<SelectListModel>();
                var NotiTypeList = myshop.Gbl_Master_NotificationType.Where(noti => noti.IsDeleted == false && noti.ShopId == WebSession.ShopId).OrderBy(x => x.NotificationType).ToList();
                if (NotiTypeList.Count > 0)
                {
                    foreach (Gbl_Master_NotificationType currentItem in NotiTypeList)
                    {
                        SelectListModel newItem = new SelectListModel();
                        newItem.Text = currentItem.NotificationType;
                        newItem.Value = currentItem.NotificationTypeId;
                        list.Add(newItem);
                    }
                }

                return list;
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
        public static List<SelectListModel> GetShopList()
        {
            try
            {
                myshop = new MyshopDb();
                var shopList = (from map in myshop.User_ShopMapper.Where(map => map.IsDeleted == false && map.UserId == WebSession.UserId)
                                from shop in myshop.Gbl_Master_Shop.Where(shops => shops.IsDeleted == false && shops.ShopId.Equals(map.ShopId))
                                select new SelectListModel
                                {
                                    Text = shop.Name,
                                    Value = shop.ShopId
                                }
                                ).ToList();

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
        public static List<string> GetStateName(string stateName)
        {
            try
            {
                myshop = new MyshopDb();
                List<string> list = new List<string>();
                var stateList = myshop.Gbl_Master_State.Where(state => state.IsDeleted == false && state.StateName.IndexOf(stateName) > -1).OrderBy(x => x.StateName).ToList();
                if (stateList.Count > 0)
                {
                    foreach (Gbl_Master_State currentItem in stateList)
                    {
                        list.Add(currentItem.StateName);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                return new List<string> { "No State" };
            }
            finally
            {
                if (myshop != null)
                    myshop = null;
            }
        }
        public static List<string> GetCityName(string cityName)
        {
            try
            {
                myshop = new MyshopDb();
                List<string> list = new List<string>();
                var cityList = myshop.Gbl_Master_City.Where(city => city.IsDeleted == false && city.CityName.IndexOf(cityName) > -1).OrderBy(x => x.CityName).ToList();
                if (cityList.Count > 0)
                {
                    foreach (Gbl_Master_City currentItem in cityList)
                    {
                        list.Add(currentItem.CityName);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                return new List<string> { "No City" };
            }
            finally
            {
                if (myshop != null)
                    myshop = null;
            }
        }
        public static Tuple<int, byte[]> FileUpload(HttpPostedFileBase Files, string OriginalFileName, string ModuleName,int shopid=0)
        {
            try
            {
                myshop = new MyshopDb();
                string[] ext = OriginalFileName.Split(new char[] { '.' });
                shopid = shopid == 0 ? WebSession.ShopId : shopid;
                Gbl_Attachment newAttachment = new Gbl_Attachment();
                if (!string.IsNullOrEmpty(OriginalFileName))
                {
                    newAttachment.Attachment = ReadFile(Files);
                    newAttachment.CreatedBy = WebSession.UserId;
                    newAttachment.CreatedDate = DateTime.Now;
                    newAttachment.FileExtension = ext[ext.Length - 1];
                    newAttachment.FileName = GetDbFileName(Enums.FileType.Image);
                    newAttachment.ModificationDate = DateTime.Now;
                    newAttachment.ModifiedBy = WebSession.UserId;
                    newAttachment.ModuleName = ModuleName;
                    newAttachment.OriginalFileName = OriginalFileName;
                    newAttachment.ShopId = shopid;
                    newAttachment.IsDeleted = false;
                    newAttachment.IsSync = false;
                    myshop.Gbl_Attachment.Add(newAttachment);
                    myshop.SaveChanges();
                }
                return new Tuple<int, byte[]>(newAttachment.AttachmentId, newAttachment.Attachment);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static byte[] ReadFile(HttpPostedFileBase sPath)
        {
            Stream str = sPath.InputStream;
            BinaryReader Br = new BinaryReader(str);
            Byte[] FileDet = Br.ReadBytes((Int32)str.Length);
            return FileDet;
        }
        public static string GetDbFileName(Enums.FileType ext)
        {
            switch (ext)
            {
                case Enums.FileType.Image:
                    return "Img_" + DateTime.Now.Ticks.ToString();
                case Enums.FileType.MS_Excel:
                    return "Excel_" + DateTime.Now.Ticks.ToString();
                case Enums.FileType.MS_PPT:
                    return "Ppt_" + DateTime.Now.Ticks.ToString();
                case Enums.FileType.MS_Word:
                    return "Word_" + DateTime.Now.Ticks.ToString();
                case Enums.FileType.Pdf:
                    return "Pdf_" + DateTime.Now.Ticks.ToString();
                case Enums.FileType.TextFile:
                    return "Text_" + DateTime.Now.Ticks.ToString();
                default:
                    return "Common_" + DateTime.Now.Ticks.ToString();
            }
        }
        public static bool isExist(Enums.ValidateDataOf DataType, string Data)
        {
            myshop = new MyshopDb();
            int count = 0;
            switch (DataType)
            {
                case Enums.ValidateDataOf.PanCard:
                    count = myshop.Gbl_Master_Employee.Where(x => x.PANCardNo.ToLower().Equals(Data.ToLower())).Count();
                    return count > 0 ? true : false;
                case Enums.ValidateDataOf.AadharCard:
                    count = myshop.Gbl_Master_Employee.Where(x => x.AadharNo.ToLower().Equals(Data.ToLower())).Count();
                    return count > 0 ? true : false;
                case Enums.ValidateDataOf.Email:
                    count = myshop.Gbl_Master_User.Where(x => x.Username.ToLower().Equals(Data.ToLower())).Count();
                    return count > 0 ? true : false;
                case Enums.ValidateDataOf.Mobile:
                    count = myshop.Gbl_Master_User.Where(x => x.Mobile.ToLower().Equals(Data.ToLower())).Count();
                    return count > 0 ? true : false;
                default:
                    return false;
            }
        }
        public static void LogError(string ctrl, string action, string area, string message, Exception exception)
        {
            MyshopDb myshop = new MyshopDb();
            ErrorLog newLog = new ErrorLog();
            newLog.Action = action;
            newLog.Area = area;
            newLog.Controller = ctrl;
            newLog.CreatedDate = DateTime.Now;
            newLog.InnerException = exception.InnerException == null ? "No Inner exception" : exception.InnerException.Message;
            newLog.IsDeleted = false;
            newLog.IsResolved = false;
            newLog.IsSync = false;
            newLog.Message = message;
            newLog.ModifiedDate = DateTime.Now;
            newLog.OuterException = exception.Message;
            myshop.ErrorLogs.Add(newLog);
            myshop.SaveChanges();

            //Fire email notification
            Utility.EmailSendHtmlFormatted(Utility.GetAppSettingsValue("ErrorLogEmail"), Custom.Message.ErrorLogEmailSubject, Utility.EmailErrorLogBody(newLog));
        }
    }

    public class SelectListModel
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }

    public class ShopListModel
    {
        public int ShopId { get; set; }
        public string ShopName { get; set; }
    }

    public class PagingModel
    {
        public int TotalRecords { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }        
    }
    public class JsonResponseModelForPaging<T>:PagingModel
    {
        public List<T> objList { get; set; }
    }
}