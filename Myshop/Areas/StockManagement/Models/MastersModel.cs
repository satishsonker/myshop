using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using Myshop.App_Start;
using System.Data.Entity;

namespace Myshop.Areas.StockManagement.Models
{
    public class MastersModel
    {
        MyshopDb myshop;
        public Enums.CrudStatus SetVendor(VendorModel model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();

                var onldVen = myshop.Gbl_Master_Vendor.Where(ven => (ven.VendorId.Equals(model.VendorId) || (ven.VendorName.ToLower().Equals(model.VendorName) || ven.VendorName.ToLower().Contains(model.VendorName))) && ven.IsDeleted == false).FirstOrDefault();
                if (onldVen != null && onldVen.ShopId.Equals(WebSession.ShopId))
                {
                    if (crudType == Enums.CrudType.Update)
                    {
                        onldVen.VendorName = model.VendorName;
                        onldVen.VendorAddress = model.VendorAddress;
                        onldVen.VendorMobile = model.VendorMobile;
                        onldVen.Description = model.VendorDesc;
                        onldVen.IsDeleted = false;
                        onldVen.IsSync = false;
                        onldVen.ModifiedBy = WebSession.UserId;
                        onldVen.ModificationDate = DateTime.Now;
                        myshop.Entry(onldVen).State = EntityState.Modified;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        var stock = myshop.Stk_Dtl_Entry.Where(x => x.IsDeleted == false && x.BrandId.Equals(model.VendorId) && x.ShopId.Equals(WebSession.ShopId)).FirstOrDefault();
                        if (stock == null)
                        {
                            onldVen.IsDeleted = true;
                            onldVen.IsSync = false;
                            onldVen.ModifiedBy = WebSession.UserId;
                            onldVen.ModificationDate = DateTime.Now;
                            myshop.Entry(onldVen).State = EntityState.Modified;
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
                    Gbl_Master_Vendor newVendor = new Gbl_Master_Vendor();
                    newVendor.VendorName = model.VendorName;
                    newVendor.VendorAddress = model.VendorAddress;
                    newVendor.VendorMobile = model.VendorMobile;
                    newVendor.Description = model.VendorDesc;
                    newVendor.CreatedBy = WebSession.UserId;
                    newVendor.CreatedDate = DateTime.Now;
                    newVendor.IsDeleted = false;
                    newVendor.IsSync = false;
                    newVendor.ModifiedBy = WebSession.UserId;
                    newVendor.ShopId = WebSession.ShopId;
                    newVendor.ModificationDate = DateTime.Now;
                    myshop.Entry(newVendor).State = EntityState.Added;
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
        public Enums.CrudStatus SetBrand(BrandModel model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();

                var oldBrand = myshop.Gbl_Master_Brand.Where(brand => (brand.BrandId.Equals(model.BrandId) || (brand.BrandName.ToLower().Equals(model.BrandName) || brand.BrandName.ToLower().Contains(model.BrandName))) && brand.IsDeleted == false).FirstOrDefault();
                if (oldBrand != null && oldBrand.ShopId.Equals(WebSession.ShopId))
                {
                    if (crudType == Enums.CrudType.Update)
                    {
                        oldBrand.BrandName = model.BrandName;
                        oldBrand.Description = model.BrandDesc;
                        oldBrand.IsDeleted = false;
                        oldBrand.IsSync = false;
                        oldBrand.ModifiedBy = WebSession.UserId;
                        oldBrand.ModificationDate = DateTime.Now;
                        myshop.Entry(oldBrand).State = EntityState.Modified;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        var stock = myshop.Stk_Dtl_Entry.Where(x => x.IsDeleted == false && x.BrandId.Equals(model.BrandId) && x.ShopId.Equals(WebSession.ShopId)).FirstOrDefault();
                        if (stock == null)
                        {
                            oldBrand.IsDeleted = true;
                            oldBrand.IsSync = false;
                            oldBrand.ModifiedBy = WebSession.UserId;
                            oldBrand.ModificationDate = DateTime.Now;
                            myshop.Entry(oldBrand).State = EntityState.Modified;
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
                    Gbl_Master_Brand newBrand = new Gbl_Master_Brand();
                    newBrand.BrandName = model.BrandName;
                    newBrand.CreatedBy = WebSession.UserId;
                    newBrand.CreatedDate = DateTime.Now;
                    newBrand.Description = model.BrandDesc;
                    newBrand.IsDeleted = false;
                    newBrand.IsSync = false;
                    newBrand.ModifiedBy = WebSession.UserId;
                    newBrand.ShopId = WebSession.ShopId;
                    newBrand.ModificationDate = DateTime.Now;
                    myshop.Entry(newBrand).State = EntityState.Added;
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
        public Enums.CrudStatus SetCatogary(CategoryModel model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();

                var oldCat = myshop.Gbl_Master_Category.Where(cat => (cat.CatId.Equals(model.CatId) || (cat.CatName.ToLower().Equals(model.CatName) || cat.CatName.ToLower().Contains(model.CatName))) && cat.IsDeleted == false).FirstOrDefault();
                if (oldCat != null && oldCat.ShopId.Equals(WebSession.ShopId))
                {
                    if (crudType == Enums.CrudType.Update)
                    {
                        oldCat.CatName = model.CatName;
                        oldCat.Description = model.CatDesc;
                        oldCat.IsDeleted = false;
                        oldCat.IsSync = false;
                        oldCat.ModifiedBy = WebSession.UserId;
                        oldCat.ModificationDate = DateTime.Now;
                        myshop.Entry(oldCat).State = EntityState.Modified;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        var pro = myshop.Gbl_Master_SubCategory.Where(x => x.IsDeleted == false && x.CatId.Equals(model.CatId) && x.ShopId.Equals(WebSession.ShopId)).FirstOrDefault();
                        if (pro == null)
                        {
                            oldCat.IsDeleted = true;
                            oldCat.IsSync = false;
                            oldCat.ModifiedBy = WebSession.UserId;
                            oldCat.ModificationDate = DateTime.Now;
                            myshop.Entry(oldCat).State = EntityState.Modified;
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
                    Gbl_Master_Category newCat = new Gbl_Master_Category();
                    newCat.CatName = model.CatName;
                    newCat.CreatedBy = WebSession.UserId;
                    newCat.CreatedDate = DateTime.Now;
                    newCat.Description = model.CatDesc;
                    newCat.IsDeleted = false;
                    newCat.IsSync = false;
                    newCat.ModifiedBy = WebSession.UserId;
                    newCat.ShopId = WebSession.ShopId;
                    newCat.ModificationDate = DateTime.Now;
                    myshop.Entry(newCat).State = EntityState.Added;
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
        public Enums.CrudStatus SetSubCatogary(SubCategoryModel model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();
                var oldSubCat = myshop.Gbl_Master_SubCategory.Where(cat => cat.SubCatName.ToLower().Equals(model.SubCatName) || cat.SubCatName.ToLower().Contains(model.SubCatName) && cat.IsDeleted == false).FirstOrDefault();
                if (oldSubCat != null && oldSubCat.ShopId.Equals(WebSession.ShopId))
                {
                    if (crudType == Enums.CrudType.Update)
                    {
                        oldSubCat.SubCatName = model.SubCatName;
                        oldSubCat.Description = model.SubCatDesc;
                        oldSubCat.CatId = model.CatId;
                        oldSubCat.IsDeleted = false;
                        oldSubCat.IsSync = false;
                        oldSubCat.ModifiedBy = WebSession.UserId;
                        oldSubCat.ModificationDate = DateTime.Now;
                        myshop.Entry(oldSubCat).State = EntityState.Modified;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        var pro = myshop.Gbl_Master_Product.Where(x => x.IsDeleted == false && x.SubCatId.Equals(model.SubCatId) && x.ShopId.Equals(WebSession.ShopId)).FirstOrDefault();
                        if (pro == null)
                        {
                            oldSubCat.IsDeleted = true;
                            oldSubCat.IsSync = false;
                            oldSubCat.ModifiedBy = WebSession.UserId;
                            oldSubCat.ModificationDate = DateTime.Now;
                            myshop.Entry(oldSubCat).State = EntityState.Modified;
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
                    Gbl_Master_SubCategory newSubCat = new Gbl_Master_SubCategory();
                    newSubCat.SubCatName = model.SubCatName;
                    newSubCat.CatId = model.CatId;
                    newSubCat.CreatedBy = WebSession.UserId;
                    newSubCat.CreatedDate = DateTime.Now;
                    newSubCat.Description = model.SubCatDesc;
                    newSubCat.IsDeleted = false;
                    newSubCat.IsSync = false;
                    newSubCat.ModifiedBy = WebSession.UserId;
                    newSubCat.ShopId = WebSession.ShopId;
                    newSubCat.ModificationDate = DateTime.Now;
                    myshop.Entry(newSubCat).State = EntityState.Added;
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
        public Enums.CrudStatus SetUnit(UnitModel model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();

                var oldUnit = myshop.Gbl_Master_Unit.Where(unit => unit.UnitName.ToLower().Equals(model.UnitName) || unit.UnitName.ToLower().Contains(model.UnitName) && unit.IsDeleted == false).FirstOrDefault();
                if (oldUnit != null && oldUnit.ShopId.Equals(WebSession.ShopId))
                {
                    if (crudType == Enums.CrudType.Update)
                    {
                        oldUnit.UnitName = model.UnitName;
                        oldUnit.Description = model.UnitDesc;
                        oldUnit.IsDeleted = false;
                        oldUnit.IsSync = false;
                        oldUnit.ModifiedBy = WebSession.UserId;
                        oldUnit.ModificationDate = DateTime.Now;
                        myshop.Entry(oldUnit).State = EntityState.Modified;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        var pro = myshop.Gbl_Master_Product.Where(x => x.IsDeleted == false && x.UnitId.Equals(model.UnitId) && x.ShopId.Equals(WebSession.ShopId)).FirstOrDefault();
                        if (pro == null)
                        {
                            oldUnit.IsDeleted = false;
                            oldUnit.IsSync = false;
                            oldUnit.ModifiedBy = WebSession.UserId;
                            oldUnit.ModificationDate = DateTime.Now;
                            myshop.Entry(oldUnit).State = EntityState.Modified;
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
                    Gbl_Master_Unit newUnit = new Gbl_Master_Unit();
                    newUnit.UnitName = model.UnitName;
                    newUnit.CreatedBy = WebSession.UserId;
                    newUnit.CreatedDate = DateTime.Now;
                    newUnit.Description = model.UnitDesc;
                    newUnit.IsDeleted = false;
                    newUnit.IsSync = false;
                    newUnit.ModifiedBy = WebSession.UserId;
                    newUnit.ShopId = WebSession.ShopId;
                    newUnit.ModificationDate = DateTime.Now;
                    myshop.Entry(newUnit).State = EntityState.Added;
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
        public Enums.CrudStatus SetProduct(ProductModel model, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();

                var oldPro = myshop.Gbl_Master_Product.Where(pro => (pro.ProductId.Equals(model.ProductId) || (pro.ProductName.ToLower().Equals(model.ProductName.Trim().ToLower()) || pro.ProductName.ToLower().Contains(model.ProductName.Trim().ToLower()))) && pro.IsDeleted == false && pro.SubCatId.Equals(model.SubCatId)).FirstOrDefault();
                if (oldPro != null && oldPro.ShopId.Equals(WebSession.ShopId))
                {
                    if (crudType == Enums.CrudType.Update)
                    {
                        oldPro.MinQuantity = model.MinQuantity;
                        oldPro.ProductName = model.ProductName;
                        oldPro.UnitId = model.UnitId;
                        oldPro.Description = model.Desc;
                        oldPro.IsDeleted = false;
                        oldPro.IsSync = false;
                        oldPro.ModifiedBy = WebSession.UserId;
                        oldPro.ModificationDate = DateTime.Now;
                        myshop.Entry(oldPro).State = EntityState.Modified;
                    }
                    else if (crudType == Enums.CrudType.Delete)
                    {
                        var pro = myshop.Stk_Dtl_Entry.Where(x => x.IsDeleted == false && x.ProductId.Equals(model.ProductId) && x.ShopId.Equals(WebSession.ShopId)).FirstOrDefault();

                        if (pro == null)
                        {
                            oldPro.IsDeleted = true;
                            oldPro.IsSync = false;
                            oldPro.ModifiedBy = WebSession.UserId;
                            oldPro.ModificationDate = DateTime.Now;
                            myshop.Entry(oldPro).State = EntityState.Modified;
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
                    Gbl_Master_Product newPro = new Gbl_Master_Product();
                    newPro.MinQuantity = model.MinQuantity;
                    newPro.ProductCode = model.ProductCode;
                    newPro.ProductName = model.ProductName;
                    newPro.UnitId = model.UnitId;
                    newPro.SubCatId = model.SubCatId;
                    newPro.CreatedBy = WebSession.UserId;
                    newPro.CreatedDate = DateTime.Now;
                    newPro.Description = model.Desc;
                    newPro.IsDeleted = false;
                    newPro.IsSync = false;
                    newPro.ModifiedBy = WebSession.UserId;
                    newPro.ShopId = WebSession.ShopId;
                    newPro.ModificationDate = DateTime.Now;
                    myshop.Entry(newPro).State = EntityState.Added;
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
       
        public IEnumerable<object> GetCatJson()
        {
            try
            {
                myshop = new MyshopDb();
                var catList = (from cat in myshop.Gbl_Master_Category.Where(x => x.IsDeleted == false && x.ShopId.Equals(WebSession.ShopId))
                               orderby cat.CatName
                               select new
                               {
                                   cat.CatId,
                                   cat.CatName,
                                   cat.CreatedDate,
                                   Description = cat.Description ?? "No Description",
                                   cat.ShopId
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
        public IEnumerable<object> GetSubCatJson()
        {
            try
            {
                myshop = new MyshopDb();
                var catList = (from subCat in myshop.Gbl_Master_SubCategory.Where(x => x.IsDeleted == false && x.ShopId.Equals(WebSession.ShopId))
                               from cat in myshop.Gbl_Master_Category.Where(x => x.IsDeleted == false && x.ShopId.Equals(subCat.ShopId) && x.CatId.Equals(subCat.CatId))
                               orderby subCat.SubCatName
                               select new
                               {
                                   cat.CatId,
                                   cat.CatName,
                                   subCat.SubCatId,
                                   subCat.SubCatName,
                                   cat.CreatedDate,
                                   Description = subCat.Description ?? "No Description",
                                   subCat.ShopId
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
        public IEnumerable<object> GetBrandJson()
        {
            try
            {
                myshop = new MyshopDb();
                var brandList = (from brand in myshop.Gbl_Master_Brand.Where(x => x.IsDeleted == false && x.ShopId.Equals(WebSession.ShopId))
                                 orderby brand.BrandName
                                 select new
                                 {
                                     brand.BrandId,
                                     brand.BrandName,
                                     brand.CreatedDate,
                                     Description = brand.Description ?? "No Description",
                                     brand.ShopId
                                 }).ToList();
                return brandList;
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
        public IEnumerable<object> GetVendorJson()
        {
            try
            {
                myshop = new MyshopDb();
                var venList = (from ven in myshop.Gbl_Master_Vendor.Where(x => x.IsDeleted == false && x.ShopId.Equals(WebSession.ShopId))
                               orderby ven.VendorName
                               select new
                               {
                                   ven.VendorId,
                                   ven.VendorName,
                                   ven.VendorMobile,
                                   VendorAddress = ven.VendorAddress ?? "No Description",
                                   ven.CreatedDate,
                                   Description = ven.Description ?? "No Description",
                                   ven.ShopId
                               }).ToList();
                return venList;
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
        public IEnumerable<object> GetUnitJson()
        {
            try
            {
                myshop = new MyshopDb();
                var unitList = (from unit in myshop.Gbl_Master_Unit.Where(x => x.IsDeleted == false && x.ShopId.Equals(WebSession.ShopId))
                                orderby unit.UnitName
                                select new
                                {
                                    unit.UnitId,
                                    unit.UnitName,
                                    unit.CreatedDate,
                                    Description = unit.Description ?? "No Description",
                                    unit.ShopId
                                }).ToList();
                return unitList;
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
        public IEnumerable<object> GetProducts(int subCatId=0)
        {
            try
            {
                myshop = new MyshopDb();
                var proList = (
                    from pro in myshop.Gbl_Master_Product.Where(p => p.IsDeleted == false && p.ShopId.Equals(WebSession.ShopId) && (subCatId==0 ||p.SubCatId==subCatId ))
                    from subCat in myshop.Gbl_Master_SubCategory.Where(s => s.IsDeleted == false && s.ShopId.Equals(WebSession.ShopId) && pro.SubCatId.Equals(s.SubCatId))
                    from cat in myshop.Gbl_Master_Category.Where(c => c.IsDeleted == false && c.ShopId.Equals(WebSession.ShopId) && subCat.CatId.Equals(c.CatId))
                    from unit in myshop.Gbl_Master_Unit.Where(u => u.IsDeleted == false && u.ShopId.Equals(WebSession.ShopId) && pro.UnitId.Equals(u.UnitId))
                    orderby pro.ProductName
                    select new
                    {
                        subCat.CatId,
                        cat.CatName,
                        subCat.SubCatId,
                        subCat.SubCatName,
                        pro.UnitId,
                        unit.UnitName,
                        pro.CreatedDate,
                        pro.Description,
                        pro.MinQuantity,
                        pro.ProductCode,
                        pro.ProductId,
                        pro.ProductName,
                        pro.ShopId
                    }
                    ).ToList();
                return proList;
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