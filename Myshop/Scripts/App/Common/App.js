﻿/// <reference path="../../jquery-1.10.2.js" />

var app = {};
app.urls = {};
app.const = {};

app.urls.DefaultPagingUrl = "?PageNo=1&PageSize=20";
app.urls.getProducts = '/StockManagement/Masters/GetProducts';
app.urls.GetBrandJson = '/StockManagement/Masters/GetBrandJson';
app.urls.GetBrandListUrl = '/StockManagement/Masters/GetBrands';
app.urls.GetGetProductUnitsListUrl = '/StockManagement/Masters/GetProductUnits';
app.urls.GetVendorJson = '/StockManagement/Masters/GetVendorJson';
app.urls.GetCatogaryJson = '/StockManagement/Masters/GetCatogaryJson';
app.urls.GetSubCatogaryJson = '/StockManagement/Masters/GetSubCatogaryJson';
app.urls.GetUnitJson = '/StockManagement/Masters/GetUnitJson';
app.urls.GetDocProofTypesListJson = '/Global/Masters/GetDocProofTypes';
app.urls.GetDocProofsListJson = '/Global/Masters/GetDocProofs';
app.urls.GetStockJosn = '/StockManagement/stock/GetStockJosn';
app.urls.GetBankAccountJosn = '/StockManagement/stock/GetBankAccountJosn';
app.urls.GetVendorListUrl ="/StockManagement/Stock/GetVendorListJosn";
app.urls.GetBankAccListUrl ="/StockManagement/Stock/GetBankAccountJosn";
app.urls.GetChequeListUrl = "/StockManagement/Stock/GetChequeNoByAccNo";
app.urls.GetPaymodeListUrl = "/StockManagement/Stock/GetPayModeListJosn";
app.urls.GetCatListUrl = "/StockManagement/Stock/GetCatListJosn";
app.urls.GetSubCatListUrl = "/StockManagement/Stock/GetSubCatListJosn";
app.urls.GetCatogariesUrl = "/StockManagement/Masters/GetCatogaries";
app.urls.GetProductsUrl = "/StockManagement/Masters/GetProducts"
app.urls.GetUnitUrl = "/StockManagement/Masters/GetUnits";
app.urls.GetSubCatogariesUrl = "/StockManagement/Masters/GetSubCatogaries";
app.urls.GetAppPagesUrl = "/Global/Menu/GetAppPages";
app.urls.GetPermissionJson = '/Global/user/GetPermissionJson';
app.urls.UpdateSinglePermission = '/Global/user/UpdateSinglePermission';
app.urls.SaveSinglePermission = '/Global/user/SaveSinglePermission';
app.urls.DeletesSinglePermission = '/Global/user/DeletesSinglePermission';
app.urls.GetShopMap = '/Global/user/GetShopMap';
app.urls.SetShopJson = '/Global/user/SetShopJson';
app.urls.DeleteShopMaps = '/Global/user/DeleteShopMap';
app.urls.UpdateUserAccess = '/Global/user/UpdateUserAccess';
app.urls.GetErrorLog = '/Global/Admin/GetErrorLog';
app.urls.UpdateErrorLog = '/Global/Admin/UpdateErrorLog';
app.urls.isUserExist = '/Global/user/isUserExist';
app.urls.GetBanksUrl = '/Global/Masters/GetBanks';
app.urls.GetBankJson = '/Global/Masters/GetBankJson';
app.urls.GetAccTypeJson = '/Global/Masters/GetAccTypeJson';
app.urls.GetAccTypeUrl = '/Global/Masters/GetAccTypes';
app.urls.GetPayModeJson = '/Global/Masters/GetPayModeJson';
app.urls.GetBankAccounJson = '/Global/Masters/GetBankAccounJson';
app.urls.GetChequeJson = '/Global/Masters/GetChequeJson';
app.urls.GetAppModuleJson = '/Global/Menu/GetAppModuleJson';
app.urls.GetAppPageJson = '/Global/Menu/GetAppPageJson';
app.urls.GetUserJson = '/Global/User/GetUserJson';
app.urls.GetDowntimeJson = '/Global/Setting/GetDowntimeJson';
app.urls.GetBankAccUrl = '/Global/Masters/GetBankAccounts';
app.urls.GetModuleUrl = "/Global/Menu/GetAppModules";
app.urls.GetPageUrl = "/Global/Menu/GetAppPages";
app.urls.GetUserUrl = "/Global/User/GetUserJson";
app.urls.GetUserTypeUrl = "/Global/User/GetUserType";
app.urls.GetShopUrl = "/Global/User/GetShopJson";
app.urls.GetStockDetailsJosn = "/StockManagement/stock/GetStockDetailsJosn";
app.urls.GetRoleTypeJson = "/EmployeesManagement/Master/GetRoleTypeJson";
app.urls.GetEmpRoleTypeListJson = "/EmployeesManagement/Master/GetEmpRoleJson";
app.urls.GetEmpListJson = "/EmployeesManagement/Master/GetEmpListJson?PageNo=1&PageSize=20";
app.urls.GetDocProofTypeJson = '/Global/Masters/GetDocProofTypeJson';
app.urls.GetDocProofJson = '/Global/Masters/GetDocProofJson';
app.urls.GetPermissionJson = '/Global/user/GetPermissionJson';
app.urls.UpdateSinglePermission = '/Global/user/UpdateSinglePermission';
app.urls.SaveSinglePermission = '/Global/user/SaveSinglePermission';
app.urls.DeletesSinglePermission = '/Global/user/DeletesSinglePermission';
app.urls.GetShopMap = '/Global/user/GetShopMap';
app.urls.SetShopJson = '/Global/user/SetShopJson';
app.urls.DeleteShopMaps = '/Global/user/DeleteShopMap';
app.urls.GetUserJson = '/Global/user/GetUserJson';
app.urls.UpdateUserAccess = '/Global/user/UpdateUserAccess';
app.urls.GetErrorLog = '/Global/Admin/GetErrorLog';
app.urls.UpdateErrorLog = '/Global/Admin/UpdateErrorLog';
app.urls.isUserExist = '/Global/user/isUserExist';
app.urls.isExist = '/Common/IsExist';

app.urls.GetStockProductJosn = "/StockManagement/stock/GetUniqueStockProducts";

//Chart Urls
app.urls.StockProductLineChart = "/StockManagement/stock/GetStockEntryProductChartData";
app.urls.StockTotalAmountPieChart = "GetStockEntryTotalAmountChartData";
//Constant Declaration
app.const.toastColor = {};
app.const.toastColor.red = '#ff00006e';
app.const.toastColor.green = '#55bf016e';
app.const.toastColor.blue = '#0348bf7d';
app.const.toastColor.gray = '#807e7ecc';

app.const.alertType = {};
app.const.alertType.info = "info";
app.const.alertType.danger = "danger";
app.const.alertType.success = "success";
app.const.alertType.warning = "warning";

app.const.validateDataOf={};
app.const.validateDataOf.panCard = 1;
app.const.validateDataOf.aadharCard = 1;
app.const.validateDataOf.email = 1;
app.const.validateDataOf.mobile = 1;