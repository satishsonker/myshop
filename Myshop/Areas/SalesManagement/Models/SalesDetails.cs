namespace Myshop.Areas.SalesManagement.Models
{
    using DataLayer;
    using Myshop.App_Start;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static Myshop.App_Start.Enums;

    public class SalesDetails
    {
        MyshopDb _myshopDb = null;

        public List<SalesModel> SearchProduct(string searchValue)
        {
            _myshopDb = new MyshopDb();
            var _proList = (from pro in _myshopDb.Stk_Dtl_Entry.Where(x => (searchValue.Equals(string.Empty) || x.Gbl_Master_Product.ProductCode.IndexOf(searchValue) > -1 || x.Gbl_Master_Product.ProductName.IndexOf(searchValue) > -1) && !x.IsDeleted && x.Qty > 0)
                            select new SalesModel
                            {
                                BrandName = pro.Gbl_Master_Brand.BrandName,
                                Category = pro.Gbl_Master_Product.Gbl_Master_SubCategory.Gbl_Master_Category.CatName,
                                ProductCode = pro.Gbl_Master_Product.ProductCode,
                                ProductName = pro.Gbl_Master_Product.ProductName,
                                ProductId = pro.Gbl_Master_Product.ProductId,
                                PurchasePrice = pro.PurchasePrice,
                                SalePrice = pro.SellPrice,
                                SubCategory = pro.Gbl_Master_Product.Gbl_Master_SubCategory.SubCatName,
                                Unit = pro.Gbl_Master_Product.Gbl_Master_Unit.UnitName
                            }).ToList();
            return _proList;
        }

        public Tuple<CrudStatus,int> SaveInvoice(InvoiceDetails invoiceDetails)
        {
            if (invoiceDetails!=null && invoiceDetails.Products!=null)
            {
                _myshopDb = new MyshopDb();
                Sale_Tr_Invoice _SaleTrans = new Sale_Tr_Invoice();
                _SaleTrans.BalanceAmount = invoiceDetails.BalanceAmount;
                _SaleTrans.CustomerId = invoiceDetails.CustomerId;
                _SaleTrans.GrandTotal = invoiceDetails.GrandTotal;
                _SaleTrans.GstAmount = invoiceDetails.GstAmount;
                _SaleTrans.InvoiceDate = DateTime.Now;
                _SaleTrans.PaidAmount = invoiceDetails.PaidAmount;
                _SaleTrans.PayModeId = invoiceDetails.PayModeId;
                _SaleTrans.PayModeRefNo = invoiceDetails.PayModeRefNo;
                _SaleTrans.SubTotalAmount = invoiceDetails.SubTotalAmount;
                _SaleTrans.IsSync = false;
                _SaleTrans.IsDeleted = false;
                _SaleTrans.IsCancelled = false;
                _SaleTrans.IsAmountRefunded = false;
                _SaleTrans.CreatedDate = DateTime.Now;
                _SaleTrans.CreatedBy = WebSession.UserId;
                _SaleTrans.ShopId = WebSession.ShopId;
                _myshopDb.Sale_Tr_Invoice.Add(_SaleTrans);
               if(_myshopDb.SaveChanges()>0 )
                {
                    foreach (InvoiceProduct item in invoiceDetails.Products)
                    {
                        Sale_Dtl_Invoice _newDetails = new Sale_Dtl_Invoice();
                        _newDetails.CreatedBy = WebSession.UserId;
                        _newDetails.CreatedDate = DateTime.Now;
                        _newDetails.Discount = item.Discount;
                        _newDetails.InvoiceId = _SaleTrans.InvoiceId;
                        _newDetails.IsDeleted = false;
                        _newDetails.IsSync = false;
                        _newDetails.Price = item.SalePrice;
                        _newDetails.ProductId = item.ProductId;
                        _newDetails.Qty = item.Qty;
                        _newDetails.Remark = item.Remark;
                        _myshopDb.Sale_Dtl_Invoice.Add(_newDetails);                        
                    }

                    int _result=_myshopDb.SaveChanges();
                    return _result>0? new Tuple<CrudStatus, int>(CrudStatus.Inserted, _SaleTrans.InvoiceId): new Tuple<CrudStatus, int>(CrudStatus.NoEffect,0);
                }
               else
                   return new Tuple<CrudStatus, int>(CrudStatus.NoEffect, 0);
            }
            else
            {
                return new Tuple<CrudStatus, int>(CrudStatus.InvalidParameter, 0);
            }
        }

        public CrudStatus AddCustomer(string firstName, string lastName, string custMobile)
        {
            _myshopDb = new MyshopDb();
            Gbl_Master_Customer _newCustomer = new Gbl_Master_Customer();
            _newCustomer.CreatedBy = WebSession.UserId;
            _newCustomer.CreatedDate = DateTime.Now;
            _newCustomer.FirstName = firstName;
            _newCustomer.LastName = lastName;
            _newCustomer.IsDeleted = false;
            _newCustomer.IsSync = false;
            _newCustomer.Mobile = custMobile;
            _newCustomer.CustomerTypeId = 1;
            _newCustomer.ShopId = WebSession.ShopId;
            _myshopDb.Gbl_Master_Customer.Add(_newCustomer);
            return _myshopDb.SaveChanges() > 0 ? CrudStatus.Inserted : CrudStatus.NoEffect;
        }
    }
}