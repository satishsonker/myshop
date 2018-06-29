using Myshop.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Myshop.Areas.StockManagement.Models;
using Myshop.Filters;
using Myshop.GlobalResource;
using Myshop.Controllers;

namespace Myshop.Areas.StockManagement.Controllers
{
    [MyshopAuthorize]
    [MyShopPermission]
    public class MastersController : CommonController
    {
        // GET: StockManagement/Masters
        public ActionResult GetBrand()
        {
            ViewBag.ShopList = WebSession.ShopList;
            return View();
        }
        public ActionResult SetBrand(BrandModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MastersModel insertBrand = new MastersModel();
                Enums.CrudStatus status = insertBrand.SetBrand(model, Enums.CrudType.Insert);
                ReturnAlertMessage(status);
            }

            return RedirectToAction("GetBrand");
        }
        public ActionResult UpdateBrand(BrandModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MastersModel insertBrand = new MastersModel();
                Enums.CrudStatus status = insertBrand.SetBrand(model, Enums.CrudType.Update);
                ReturnAlertMessage(status);
            }

            return RedirectToAction("GetBrand");
        }
        public ActionResult DeleteBrand(BrandModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MastersModel insertBrand = new MastersModel();
                Enums.CrudStatus status = insertBrand.SetBrand(model, Enums.CrudType.Delete);
                ReturnAlertMessage(status);
            }

            return RedirectToAction("GetBrand");
        }

        public ActionResult GetCategory()
        {
            ViewBag.ShopList = WebSession.ShopList;
            return View();
        }
        public ActionResult SetCategory(CategoryModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MastersModel insertCat = new MastersModel();
                Enums.CrudStatus status = insertCat.SetCatogary(model, Enums.CrudType.Insert);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetCategory");
        }
        public ActionResult UpdateCategory(CategoryModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MastersModel insertCat = new MastersModel();
                Enums.CrudStatus status = insertCat.SetCatogary(model, Enums.CrudType.Update);
                ReturnAlertMessage(status);
            }

            return RedirectToAction("GetCategory");
        }
        public ActionResult DeleteCategory(CategoryModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MastersModel insertCat = new MastersModel();
                Enums.CrudStatus status = insertCat.SetCatogary(model, Enums.CrudType.Delete);
                ReturnAlertMessage(status);
            }

            return RedirectToAction("GetCategory");
        }

        public ActionResult GetSubCategory()
        {
            ViewBag.ShopList = WebSession.ShopList;
            return View();
        }
        public ActionResult SetSubCategory(SubCategoryModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MastersModel insertCat = new MastersModel();
                Enums.CrudStatus status = insertCat.SetSubCatogary(model, Enums.CrudType.Insert);
                ReturnAlertMessage(status);
            }

            return RedirectToAction("GetSubCategory");
        }
        public ActionResult UpdateSubCategory(SubCategoryModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MastersModel insertCat = new MastersModel();
                Enums.CrudStatus status = insertCat.SetSubCatogary(model, Enums.CrudType.Update);
                ReturnAlertMessage(status);
            }

            return RedirectToAction("GetSubCategory");
        }
        public ActionResult DeleteSubCategory(SubCategoryModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MastersModel insertCat = new MastersModel();
                Enums.CrudStatus status = insertCat.SetSubCatogary(model, Enums.CrudType.Delete);
                ReturnAlertMessage(status);
            }

            return RedirectToAction("GetSubCategory");
        }

        public ActionResult GetUnit()
        {
            ViewBag.ShopList = WebSession.ShopList;
            return View();
        }
        public ActionResult SetUnit(UnitModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MastersModel insertCat = new MastersModel();
                Enums.CrudStatus status = insertCat.SetUnit(model, Enums.CrudType.Insert);
                ReturnAlertMessage(status);
            }

            return RedirectToAction("GetUnit");
        }
        public ActionResult UpdateUnit(UnitModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MastersModel insertCat = new MastersModel();
                Enums.CrudStatus status = insertCat.SetUnit(model, Enums.CrudType.Update);
                ReturnAlertMessage(status);
            }

            return RedirectToAction("GetUnit");
        }
        public ActionResult DeleteUnit(UnitModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MastersModel insertCat = new MastersModel();
                Enums.CrudStatus status = insertCat.SetUnit(model, Enums.CrudType.Delete);
                ReturnAlertMessage(status);
            }

            return RedirectToAction("GetUnit");
        }

        public ActionResult GetProduct()
        {
            ViewBag.ShopList = WebSession.ShopList;
            return View();
        }
        public ActionResult SetProduct(ProductModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MastersModel insertCat = new MastersModel();
                Enums.CrudStatus status = insertCat.SetProduct(model, Enums.CrudType.Insert);
                ReturnAlertMessage(status);
            }

            return RedirectToAction("GetProduct");
        }
        public ActionResult UpdateProduct(ProductModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MastersModel insertCat = new MastersModel();
                Enums.CrudStatus status = insertCat.SetProduct(model, Enums.CrudType.Update);
                ReturnAlertMessage(status);
            }

            return RedirectToAction("GetProduct");
        }
        public ActionResult DeleteProduct(ProductModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MastersModel insertCat = new MastersModel();
                Enums.CrudStatus status = insertCat.SetProduct(model, Enums.CrudType.Delete);
                ReturnAlertMessage(status);
            }

            return RedirectToAction("GetProduct");
        }

        public ActionResult GetVendor()
        {
            ViewBag.ShopList = WebSession.ShopList;
            return View();
        }
        public ActionResult SetVendor(VendorModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MastersModel insertBrand = new MastersModel();
                Enums.CrudStatus status = insertBrand.SetVendor(model, Enums.CrudType.Insert);
                ReturnAlertMessage(status);
            }

            return RedirectToAction("GetVendor");
        }
        public ActionResult UpdateVendor(VendorModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MastersModel insertBrand = new MastersModel();
                Enums.CrudStatus status = insertBrand.SetVendor(model, Enums.CrudType.Update);
                ReturnAlertMessage(status);
            }

            return RedirectToAction("GetVendor");
        }
        public ActionResult DeleteVendor(VendorModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MastersModel insertBrand = new MastersModel();
                Enums.CrudStatus status = insertBrand.SetVendor(model, Enums.CrudType.Delete);
                ReturnAlertMessage(status);
            }

            return RedirectToAction("GetVendor");
        }

        // JSon Result Action Methods
        public JsonResult GetCatogaries(int shopId=1)
        {
            try
            {
                if (shopId > 0)
                {
                    return Json(GlobalMethod.GetCatogaries());
                }
                else
                {
                    return Json("Invalid Parameter");
                }
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult GetCatogaryJson(int shopId=1)
        {
            try
            {
                if (shopId > 0)
                {
                    MastersModel model = new MastersModel();
                    return Json(model.GetCatJson());
                }
                else
                {
                    return Json("Invalid Parameter");
                }
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult GetSubCatogaries(int catId,int shopId=1)
        {
            try
            {
                if (shopId > 0 && catId > 0)
                {
                    return Json(GlobalMethod.GetSubCatogaries(catId));
                }
                else
                {
                    return Json("Invalid Parameter");
                }
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult GetSubCatogaryJson(int shopId=1)
        {
            try
            {
                if (shopId > 0)
                {
                    MastersModel model = new MastersModel();
                    return Json(model.GetSubCatJson());
                }
                else
                {
                    return Json("Invalid Parameter");
                }
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult GetBrands(int shopId=1)
        {
            try
            {
                if (shopId > 0)
                {
                    return Json(GlobalMethod.GetBrands());
                }
                else
                {
                    return Json("Invalid Parameter");
                }
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult GetBrandJson(int shopId=1)
        {
            try
            {
                if (shopId > 0)
                {
                    MastersModel model = new MastersModel();
                    return Json(model.GetBrandJson());
                }
                else
                {
                    return Json("Invalid Parameter");
                }
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult GetVendors(int shopId=1)
        {
            try
            {
                if (shopId > 0)
                {
                    return Json(GlobalMethod.GetVendors());
                }
                else
                {
                    return Json("Invalid Parameter");
                }
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult GetVendorJson(int shopId=1)
        {
            try
            {
                if (shopId > 0)
                {
                    MastersModel model = new MastersModel();
                    return Json(model.GetVendorJson());
                }
                else
                {
                    return Json("Invalid Parameter");
                }
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult GetUnits(int shopId=1)
        {
            try
            {
                if (shopId > 0)
                {
                    return Json(GlobalMethod.GetUnit());
                }
                else
                {
                    return Json("Invalid Parameter");
                }
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult GetProductUnits(int subCatId)
        {
            try
            {
                if (subCatId > 0)
                {
                    return Json(GlobalMethod.GetProUnit(subCatId));
                }
                else
                {
                    return Json("Invalid Parameter");
                }
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult GetUnitJson(int shopId=1)
        {
            try
            {
                if (shopId > 0)
                {
                    MastersModel model = new MastersModel();
                    return Json(model.GetUnitJson());
                }
                else
                {
                    return Json("Invalid Parameter");
                }
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult GetProducts(int shopId=1,int subCatId=0)
        {
            try
            {
                if (shopId > 0)
                {
                    MastersModel model = new MastersModel();
                    return Json(model.GetProducts(subCatId));
                }
                else
                {
                    return Json("Invalid Parameter");
                }
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
    }
}