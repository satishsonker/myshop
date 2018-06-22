using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Myshop.Filters;
using Myshop.Areas.Global.Models;
using Myshop.App_Start;
using DataLayer;
using Myshop.GlobalResource;

namespace Myshop.Areas.Global.Controllers
{
    [MyshopAuthorize]
    [MyShopPermission]
    public class MastersController : Controller
    {
        // GET: Global/Masters
        public ActionResult GetBank()
        {
            return View();
        }
        public ActionResult SetBank(BankModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MasterDetails _details = new MasterDetails();
                Enums.CrudStatus status = _details.SetBank(model, Enums.CrudType.Insert);
                ReturnAlertMessage(status);
            }
                return RedirectToAction("GetBank");
        }
        public ActionResult UpdateBank(BankModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MasterDetails _details = new MasterDetails();
                Enums.CrudStatus status = _details.SetBank(model, Enums.CrudType.Update);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetBank");
        }
        public ActionResult DeleteBank(BankModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MasterDetails _details = new MasterDetails();
                Enums.CrudStatus status = _details.SetBank(model, Enums.CrudType.Delete);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetBank");
        }
        public JsonResult GetBanks()
        {
            try
            {
                   
                    return Json(GlobalMethod.GetBanks());
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult GetBankJson()
        {
            try
            {
                MasterDetails model = new MasterDetails();
                return Json(model.GetBankJson());
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }


        public ActionResult GetBankAccount()
        {
            return View();
        }
        public ActionResult SetBankAccount(BankAccModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MasterDetails _details = new MasterDetails();
                Enums.CrudStatus status = _details.SetBankAccount(model, Enums.CrudType.Insert);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetBankAccount");
        }
        public ActionResult UpdateBankAccount(BankAccModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MasterDetails _details = new MasterDetails();
                Enums.CrudStatus status = _details.SetBankAccount(model, Enums.CrudType.Update);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetBankAccount");
        }
        public ActionResult DeleteBankAccount(BankAccModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MasterDetails _details = new MasterDetails();
                Enums.CrudStatus status = _details.SetBankAccount(model, Enums.CrudType.Delete);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetBankAccount");
        }
        public JsonResult GetBankAccounts()
        {
            try
            {
                    return Json(GlobalMethod.GetBankAccounts());
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult GetBankAccounJson()
        {
            try
            {
                MasterDetails model = new MasterDetails();
                return Json(model.GetBankAccountJson());
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }


        public ActionResult GetPayMode()
        {
            return View();
        }
        public ActionResult SetPayMode(PayModeModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MasterDetails _details = new MasterDetails();
                Enums.CrudStatus status = _details.SetPayMode(model, Enums.CrudType.Insert);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetPayMode");
        }
        public ActionResult UpdatePayMode(PayModeModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MasterDetails _details = new MasterDetails();
                Enums.CrudStatus status = _details.SetPayMode(model, Enums.CrudType.Update);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetPayMode");
        }
        public ActionResult DeletePayMode(PayModeModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MasterDetails _details = new MasterDetails();
                Enums.CrudStatus status = _details.SetPayMode(model, Enums.CrudType.Delete);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetPayMode");
        }
        public JsonResult GetPayModes()
        {
            try
            {               
                    return Json(GlobalMethod.GetPayMode());
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult GetPayModeJson()
        {
            try
            {
                MasterDetails model = new MasterDetails();
                return Json(model.GetPayModeJson());
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }


        public ActionResult GetAccountType()
        {
            return View();
        }
        public ActionResult SetAccountType(AccTypeModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MasterDetails _details = new MasterDetails();
                Enums.CrudStatus status = _details.SetAccType(model, Enums.CrudType.Insert);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetAccountType");
        }
        public ActionResult UpdateAccountType(AccTypeModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MasterDetails _details = new MasterDetails();
                Enums.CrudStatus status = _details.SetAccType(model, Enums.CrudType.Update);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetAccountType");
        }
        public ActionResult DeleteAccountType(AccTypeModel model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MasterDetails _details = new MasterDetails();
                Enums.CrudStatus status = _details.SetAccType(model, Enums.CrudType.Delete);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetAccountType");
        }
        public JsonResult GetAccTypes()
        {
            try
            {
                return Json(GlobalMethod.GetAccTypes());
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult GetAccTypeJson()
        {
            try
            {
                MasterDetails model = new MasterDetails();
                return Json(model.GetAccTypeJson());
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }

        public ActionResult GetCheque()
        {
            return View();
        }
        public ActionResult SetCheque(ChequeModel model)
        {
            if (ModelState.IsValid)
            {
                MasterDetails _details = new MasterDetails();
                Enums.CrudStatus status = _details.SetCheque(model, Enums.CrudType.Insert);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetCheque");
        }
        public ActionResult UpdateCheque(ChequeModel model)
        {            
            if (ModelState.IsValid)
            {
                MasterDetails _details = new MasterDetails();
                Enums.CrudStatus status = _details.SetCheque(model, Enums.CrudType.Update);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetCheque", ModelState.IsValid);
        }
        public ActionResult DeleteCheque(ChequeModel model)
        {
            if (ModelState.IsValid)
            {
                MasterDetails _details = new MasterDetails();
                Enums.CrudStatus status = _details.SetCheque(model, Enums.CrudType.Delete);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetCheque");
        }
        public JsonResult GetCheques()
        {
            try
            {
                return Json(GlobalMethod.GetCheques());
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult GetChequeJson()
        {
            try
            {
                MasterDetails model = new MasterDetails();
                return Json(model.GetChequeJson());
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }

        private void SetAlertMessage(string message, Enums.AlertType alert)
        {
            ViewBag.message = message;
            TempData["messages"] = message;
            ViewBag.alert = alert.ToString();
            TempData["alert"] = alert.ToString();
        }

        private void ReturnAlertMessage(Enums.CrudStatus status)
        {
            if (status == Enums.CrudStatus.Deleted)
            {
                SetAlertMessage(Resource.DataDeleted, Enums.AlertType.success);
            }
            else if (status == Enums.CrudStatus.Inserted)
            {
                SetAlertMessage(Resource.DataSaved, Enums.AlertType.success);
            }
            else if (status == Enums.CrudStatus.Updated)
            {
                SetAlertMessage(Resource.DataUpdated, Enums.AlertType.success);
            }
            else if (status == Enums.CrudStatus.AlreadyExistForSameShop)
            {
                SetAlertMessage(Resource.DataExistWithSameShopName, Enums.AlertType.info);
            }
            else if (status == Enums.CrudStatus.NoEffect)
            {
                SetAlertMessage(Resource.DataNotSaved, Enums.AlertType.warning);
            }
            else if (status == Enums.CrudStatus.Exception)
            {
                SetAlertMessage(Resource.Exception, Enums.AlertType.danger);
            }
            else if (status == Enums.CrudStatus.AlreadyInUse)
            {
                SetAlertMessage(Resource.AlreadyInUse, Enums.AlertType.warning);
            }
        }

        public ActionResult GetDocProofType()
        {
            return View();
        }
        public ActionResult SetDocProofType(Gbl_Master_DocProofType model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MasterDetails _details = new MasterDetails();
                Enums.CrudStatus status = _details.SetDocProofType(model, Enums.CrudType.Insert);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetDocProofType");
        }
        public ActionResult UpdateDocProofType(Gbl_Master_DocProofType model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MasterDetails _details = new MasterDetails();
                Enums.CrudStatus status = _details.SetDocProofType(model, Enums.CrudType.Update);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetDocProofType");
        }
        public ActionResult DeleteDocProofType(Gbl_Master_DocProofType model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MasterDetails _details = new MasterDetails();
                Enums.CrudStatus status = _details.SetDocProofType(model, Enums.CrudType.Delete);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetDocProofType");
        }
        public JsonResult GetDocProofTypes()
        {
            try
            {

                return Json(GlobalMethod.GetDocProofTypes());
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult GetDocProofTypeJson()
        {
            try
            {
                MasterDetails model = new MasterDetails();
                return Json(model.GetDocProofTypeJson());
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
       


        public ActionResult GetDocProof()
        {
            return View();
        }
        public ActionResult SetDocProof(Gbl_Master_DocProof model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MasterDetails _details = new MasterDetails();
                Enums.CrudStatus status = _details.SetDocProof(model, Enums.CrudType.Insert);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetDocProof");
        }
        public ActionResult UpdateDocProof(Gbl_Master_DocProof model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MasterDetails _details = new MasterDetails();
                Enums.CrudStatus status = _details.SetDocProof(model, Enums.CrudType.Update);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetDocProof");
        }
        public ActionResult DeleteDocProof(Gbl_Master_DocProof model)
        {
            ViewBag.ShopList = WebSession.ShopList;
            if (ModelState.IsValid)
            {
                MasterDetails _details = new MasterDetails();
                Enums.CrudStatus status = _details.SetDocProof(model, Enums.CrudType.Delete);
                ReturnAlertMessage(status);
            }
            return RedirectToAction("GetDocProof");
        }
        public JsonResult GetDocProofJson()
        {
            try
            {
                MasterDetails model = new MasterDetails();
                return Json(model.GetDocProofJson());
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
        public JsonResult GetDocProofs(int DocProofTypeId)
        {
            try
            {
                return Json(GlobalMethod.GetDocProofs(DocProofTypeId));
            }
            catch (Exception)
            {
                return Json("Invalid Error");
            }
        }
    }
}