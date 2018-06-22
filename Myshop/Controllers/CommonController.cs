﻿using Myshop.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Myshop.App_Start;
using Myshop.GlobalResource;

namespace Myshop.Controllers
{
    public class CommonController : Controller
    {
        internal void SetAlertMessage(string message, Enums.AlertType alert)
        {
            ViewBag.message = message;
            TempData["messages"] = message;
            ViewBag.alert = alert.ToString();
            TempData["alert"] = alert.ToString();
        }

        internal void ReturnAlertMessage(Enums.CrudStatus status)
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

        internal void ReturnFileAlertMessage(Enums.FileValidateStatus status)
        {
            if (status == Enums.FileValidateStatus.SizeTooLow)
            {
                SetAlertMessage(GlobalResource.Resource.SizeTooLow, Enums.AlertType.danger);
            }
            else if (status == Enums.FileValidateStatus.SizeExceeded)
            {
                SetAlertMessage(GlobalResource.Resource.SizeExceeded, Enums.AlertType.danger);
            }
            else if (status == Enums.FileValidateStatus.NoFiles)
            {
                SetAlertMessage(GlobalResource.Resource.NoFiles, Enums.AlertType.danger);
            }
            else if (status == Enums.FileValidateStatus.MaxFilesLimitExceeded)
            {
                SetAlertMessage(GlobalResource.Resource.MaxFilesLimitExceeded, Enums.AlertType.danger);
            }
            else if (status == Enums.FileValidateStatus.InvalidWidth)
            {
                SetAlertMessage(GlobalResource.Resource.InvalidImageWidth, Enums.AlertType.danger);
            }
            else if (status == Enums.FileValidateStatus.InvalidHeight)
            {
                SetAlertMessage(GlobalResource.Resource.InvalidImageHeight, Enums.AlertType.danger);
            }
            else if (status == Enums.FileValidateStatus.InvalidFormat)
            {
                SetAlertMessage(GlobalResource.Resource.InvalidFileFormat, Enums.AlertType.danger);
            }
        }

        public JsonResult GetChequeNoByAccNo(int AccId,bool isAllCheque=false)
        {
            try
            {
                if (AccId > 0)
                {
                    return Json(GlobalMethod.GetChequeNoByAccNo(AccId,isAllCheque));
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

        public JsonResult GetVendorListJosn()
        {
            return Json(GlobalMethod.GetVendors());
        }

        public JsonResult GetCatListJosn()
        {
            return Json(GlobalMethod.GetCatogaries(WebSession.ShopId));
        }
        public JsonResult GetSubCatListJosn(int CatId)
        {
            return Json(GlobalMethod.GetSubCatogaries(CatId,WebSession.ShopId));
        }

        public JsonResult GetBankAccountJosn()
        {
            return Json(GlobalMethod.GetBankAccounts());
        }       

        public JsonResult GetPayModeListJosn()
        {
            return Json(GlobalMethod.GetPayModes());
        }

        public JsonResult GetStateName(string stateName="")
        {
            return Json(GlobalMethod.GetStateName(stateName),JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCityName(string CityName = "")
        {
            return Json(GlobalMethod.GetCityName(CityName), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Validate all posted file
        /// </summary>
        /// <param name="Files">Collection fo files</param>
        /// <param name="FileType">Desire file format to validate</param>
        /// <param name="FileSize">Desire file size(KB) to validate</param>
        /// <returns></returns>
        protected internal Enums.FileValidateStatus ValidateFiles(HttpFileCollectionBase Files,Enums.FileType FileType,int MaxFileSize,int MaxFiles, int MinFileSize=1)
        {
            bool IsInvalidFiles = false;
            bool IsSizeOverflow = false;
            bool IsSizeUnderflow = false;
            int FileCounts = 0;
            if (Files == null || Files.Count < 1)
                return Enums.FileValidateStatus.NoFiles;
            else
            {
                foreach (string file in Files)
                {
                    if (Files[file].FileName==string.Empty)
                    {
                        return Enums.FileValidateStatus.NoFiles;
                    }
                    else if(Enums.FileType.Image==FileType && (Path.GetExtension(Files[file].FileName).ToLower()!=".jpg" && Path.GetExtension(Files[file].FileName).ToLower() != ".jpeg"))
                    {
                        IsInvalidFiles = true;
                    }
                    else if (Enums.FileType.Pdf == FileType && Path.GetExtension(Files[file].FileName).ToLower() != ".pdf")
                    {
                        IsInvalidFiles = true;
                    }
                    else if ((Files[file].ContentLength/1024)>MaxFileSize)
                    {
                        IsSizeOverflow = true;
                    }
                    else if ((Files[file].ContentLength / 1024) < MinFileSize)
                    {
                        IsSizeUnderflow = true;
                    }
                    FileCounts += 1;
                }

                if (IsInvalidFiles)
                    return Enums.FileValidateStatus.InvalidFormat;
                else if (IsSizeOverflow)
                    return Enums.FileValidateStatus.SizeExceeded;
                else if (IsSizeUnderflow)
                    return Enums.FileValidateStatus.SizeTooLow;
                else if (FileCounts > MaxFiles)
                    return Enums.FileValidateStatus.MaxFilesLimitExceeded;
                else
                    return Enums.FileValidateStatus.ValidFile;
            }
        }

        public JsonResult FileSaveOnServer(string ModuleName)
        {
            try
            {
                HttpPostedFileBase Files = Request.Files[0];
                string baseFilePath = AppDomain.CurrentDomain.BaseDirectory + Utility.GetAppSettingsValue("TempFilePath");
                string currentDateFolder = baseFilePath + "\\" + DateTime.Now.Date.ToShortDateString().Replace('/', '-');
                string FileExt= Path.GetExtension(Request.Files[0].FileName).ToLower();
                string fileName = currentDateFolder + "\\" + DateTime.Now.Ticks.ToString() + FileExt;
                if (FileExt == ".jpg" || FileExt == ".jpeg")
                {
                    if (!Directory.Exists(baseFilePath))
                    {
                        Directory.CreateDirectory(baseFilePath);
                        if (Directory.Exists(baseFilePath))
                        {

                        }
                    }
                    if (!Directory.Exists(currentDateFolder))
                    {
                        Directory.CreateDirectory(currentDateFolder);
                    }

                    Request.Files[0].SaveAs(fileName);
                    return Json(new {fileName= Utility.GetAppSettingsValue("TempFilePath")+ "\\" + DateTime.Now.Ticks.ToString() + FileExt, Image = Convert.ToBase64String(GlobalMethod.ReadFile(Files)) });
                }
                else
                    return Json(new { fileName = "/Images/Icons/employee.png",Image= GlobalMethod.ReadFile(Files) });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult IsExist(Enums.validateDataOf DataType,string data)
        {
            return Json(GlobalMethod.isExist(DataType,data), JsonRequestBehavior.AllowGet);
        }
    }
}
