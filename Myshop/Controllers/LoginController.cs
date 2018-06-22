using Myshop.App_Start;
using Myshop.Filters;
using Myshop.GlobalResource;
using Myshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myshop.Controllers
{
    public class LoginController : Controller
    {
        [MyshopDowntime]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult logout()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("login");
        }

       
        public ActionResult GetLogin(string username, string password)
        {
            LoginModel model = new LoginModel();
            Enums.LoginStatus status = model.getLogin(username, password);
            if (status != Enums.LoginStatus.Authenticate)
            {
                if (status == Enums.LoginStatus.InvalidCredential)
                {
                    SetAlertMessage(Resource.InvalidCredential, Enums.AlertType.danger);
                }
                else if (status == Enums.LoginStatus.LoginBlocked)
                {
                    SetAlertMessage(Resource.LoginBlocked, Enums.AlertType.warning);
                }
                else if (status == Enums.LoginStatus.AttemptExceeded)
                {
                    SetAlertMessage(Resource.LoginAttemptExceeded, Enums.AlertType.warning);
                }
                else if (status == Enums.LoginStatus.NotExist)
                {
                    SetAlertMessage(Resource.LoginNotExist, Enums.AlertType.warning);
                }
                else if (status == Enums.LoginStatus.LoginBlocked)
                {
                    SetAlertMessage(Resource.LoginBlocked, Enums.AlertType.warning);
                }
                else if (status == Enums.LoginStatus.Inactive)
                {
                    SetAlertMessage(Resource.UserInactive, Enums.AlertType.warning);
                }
                else if (status == Enums.LoginStatus.UserBlocked)
                {
                    SetAlertMessage(Resource.UserBlocked, Enums.AlertType.warning);
                }
                else if (status == Enums.LoginStatus.UserDeleted)
                {
                    SetAlertMessage(Resource.UserDeleted, Enums.AlertType.warning);
                }
                return RedirectToAction("login");
            }
            else
            {
               return RedirectToAction("index", "Home", model);
                //return RedirectToAction("../Home/dashboard", model);
            }
        }

        public ActionResult SendResetLink(FormCollection collection)
        {
            string username = collection.Get("username");
            LoginModel model = new LoginModel();
            Enums.ResetLinkStatus status = model.sendPasswordRestLink(username);
            if (status != Enums.ResetLinkStatus.send)
            {
                if (status == Enums.ResetLinkStatus.InactiveUser)
                {
                    SetAlertMessage(Resource.UserInactive, Enums.AlertType.warning);
                }
                else if (status == Enums.ResetLinkStatus.BlockedUser)
                {
                    SetAlertMessage(Resource.UserBlocked, Enums.AlertType.warning);
                }
                else if (status == Enums.ResetLinkStatus.UserDeleted)
                {
                    SetAlertMessage(Resource.UserDeleted, Enums.AlertType.warning);
                }
                else if (status == Enums.ResetLinkStatus.exception)
                {
                    SetAlertMessage(Resource.Exception, Enums.AlertType.danger);
                }
                else if (status == Enums.ResetLinkStatus.invalidUser)
                {
                    SetAlertMessage(Resource.UserInvalid, Enums.AlertType.danger);
                }
                return RedirectToAction("ForgetPassword");
            }
            else
            {
               WebSession.Username = username;
                SetAlertMessage(Resource.SentOTP, Enums.AlertType.success);
                return RedirectToAction("InputOTP");
            }
        }

        public ActionResult ValidateOtp(FormCollection coll)
        {
            string otp = coll.Get("otp");
            LoginModel model = new LoginModel();

            if (WebSession.Username == null || WebSession.Username == "")
            {
                return RedirectToAction("ForgetPassword");
            }

            Enums.OtpStatus status = model.VerifyOTP(WebSession.Username.ToString(), otp);
            if (status != Enums.OtpStatus.Valid)
            {
                if (status == Enums.OtpStatus.Invalid)
                {
                    SetAlertMessage(Resource.InvalidOtp, Enums.AlertType.danger);
                }
                else if (status == Enums.OtpStatus.Expire)
                {
                    SetAlertMessage(Resource.ExpireOtp, Enums.AlertType.info);
                }
                else if (status == Enums.OtpStatus.InvalidUser)
                {
                    SetAlertMessage(Resource.UserInvalid, Enums.AlertType.warning);
                }
                else if (status == Enums.OtpStatus.Exception)
                {
                    SetAlertMessage(Resource.Exception, Enums.AlertType.danger);
                }
                return RedirectToAction("InputOtp");
            }
            else
            {
                SetAlertMessage(Resource.ValidOTP, Enums.AlertType.success);
                ViewBag.username = WebSession.Username;
                return RedirectToAction("SetPassword");
            }
        }

        public ActionResult ResetPassword(FormCollection collection)
        {           
            string username = WebSession.Username;
            string password = collection.Get("password");
            string conPassword = collection.Get("confirmpassword");

            if(password!=conPassword)
            {
                SetAlertMessage(Resource.PasswordNotMatch, Enums.AlertType.warning); 
                ViewBag.username = WebSession.Username;
                return RedirectToAction("SetPassword");
            }
            LoginModel model = new LoginModel();
            Enums.LoginStatus status = model.ResetPassword(username, password);
            if (status != Enums.LoginStatus.Authenticate)
            {
                if (status == Enums.LoginStatus.InvalidUser)
                {
                    SetAlertMessage(Resource.UserInvalid, Enums.AlertType.warning);
                }
                else if (status == Enums.LoginStatus.Exception)
                {
                    SetAlertMessage(Resource.Exception, Enums.AlertType.danger);
                }
                ViewBag.username = WebSession.Username;
                return RedirectToAction("SetPassword");
            }
            else
            {
                SetAlertMessage(Resource.PasswordSet, Enums.AlertType.success);
                WebSession.Username = "";
                return RedirectToAction("Login");
            }
        }

        [MyshopAuthorize]
        public ActionResult SetChangePassword(FormCollection collection)
        {
            string username = WebSession.Username;
            string oldPassword = collection.Get("oldpassword");
            string newPassword = collection.Get("newpassword");
            string newConPassword = collection.Get("confirmnewpassword");

            if (newPassword != newConPassword)
            {
                SetAlertMessage(Resource.PasswordNotMatch, Enums.AlertType.warning);
                ViewBag.username = WebSession.Username;
                return RedirectToAction("SetPassword");
            }
            LoginModel model = new LoginModel();
            Enums.LoginStatus status = model.ChangePassword(username, newPassword,oldPassword);
            if (status != Enums.LoginStatus.Authenticate)
            {
                if (status == Enums.LoginStatus.InvalidUser)
                {
                    SetAlertMessage(Resource.UserInvalid, Enums.AlertType.warning);
                }
                else if (status == Enums.LoginStatus.Exception)
                {
                    SetAlertMessage(Resource.Exception, Enums.AlertType.danger);
                }
                ViewBag.username = WebSession.Username;
                return RedirectToAction("changepassword");
            }
            else
            {
                SetAlertMessage(Resource.PasswordSet, Enums.AlertType.success);
                WebSession.Username = "";
                return RedirectToAction("changepassword");
            }
        }

        public ActionResult ForgetPassword()
        {
            WebSession.Username = "";
            return View();
        }

        public ActionResult InputOTP()
        {
            return View();
        }

        public ActionResult SetPassword()
        {
            if (Request.QueryString["txn"] != null && Request.QueryString["txnid"] != null)
            {
                LoginModel model = new LoginModel();
                Enums.ResetLinkStatus status = model.CheckResetLink(Request.QueryString["txn"].ToString(), Request.QueryString["txnid"].ToString());
                if(status!=Enums.ResetLinkStatus.send)
                {
                    if (status == Enums.ResetLinkStatus.InvalidLink)
                    {
                        SetAlertMessage(Resource.InvalidResetLink,Enums.AlertType.danger);
                    }
                    else if (status == Enums.ResetLinkStatus.LinkExpire)
                    {
                        SetAlertMessage(Resource.InvalidResetLink, Enums.AlertType.danger);
                    }
                    else if (status == Enums.ResetLinkStatus.exception)
                    {
                        SetAlertMessage(Resource.Exception, Enums.AlertType.danger);
                    }
                    return RedirectToAction("ForgetPassword");
                }
                else
                {
                    SetAlertMessage(Resource.PasswordSet, Enums.AlertType.success);
                    return View();
                }

            }
            if (WebSession.Username == null || WebSession.Username == "")
            {
                return RedirectToAction("ForgetPassword");
            }
            else
            {
                ViewBag.username = WebSession.Username;
                return View();
            }
        }

        [MyshopAuthorize]
        public ActionResult ChangePassword()
        {
            ViewBag.username = WebSession.Username;
            return View();
        }

        private void SetAlertMessage(string message, Enums.AlertType alert)
        {
            ViewBag.message = message;
            TempData["messages"] = message;
            ViewBag.alert = alert.ToString();
            TempData["alert"] = alert.ToString();
        }
    }
}