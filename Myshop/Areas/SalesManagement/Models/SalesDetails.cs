namespace Myshop.Areas.SalesManagement.Models
{
    using DataLayer;
    using Myshop.App_Start;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using static Myshop.App_Start.Enums;

    public class SalesDetails
    {
        MyshopDb _myshopDb = null;

        public List<SalesModel> SearchProduct(string searchValue)
        {
            _myshopDb = new MyshopDb();
            var sale = _myshopDb.Sale_Dtl_Invoice.Where(x => !x.IsDeleted && x.ShopId.Equals(WebSession.ShopId) && !x.Sale_Tr_Invoice.IsCancelled).ToList();
            var stk = _myshopDb.Stk_Dtl_Entry.Where(x => !x.IsDeleted && x.ShopId.Equals(WebSession.ShopId)).ToList();
            var _proList = (from pro in _myshopDb.Stk_Dtl_Entry.Where(x => (searchValue.Equals(string.Empty) || x.Gbl_Master_Product.ProductCode.IndexOf(searchValue) > -1 || x.Gbl_Master_Product.ProductName.IndexOf(searchValue) > -1) && !x.IsDeleted && x.Qty > 0 && x.ShopId.Equals(WebSession.ShopId))
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
                                Unit = pro.Gbl_Master_Product.Gbl_Master_Unit.UnitName,
                                //AvailableQty = 
                            }).Distinct().ToList();
            //return _proList;
            foreach (var item in _proList)
            {
                decimal StockQty = stk.Where(x => x.ProductId.Equals(item.ProductId)).Sum(q => q.Qty);
                decimal SaleQty = sale.Where(x => x.ProductId.Equals(item.ProductId)).Sum(q => q.Qty - q.ReturnQty ?? 0);
                _proList.Where(x => x.ProductId.Equals(item.ProductId)).FirstOrDefault().AvailableQty = (StockQty - SaleQty);
            }

            return _proList;
        }

        public List<InvoiceDetails> SearchInvoice(string searchValue)
        {
            _myshopDb = new MyshopDb();
            List<InvoiceDetails> _lstInvoice = new List<InvoiceDetails>();
            var _proList = _myshopDb.Sale_Tr_Invoice.Where(x => (searchValue.Equals(string.Empty) || x.InvoiceId.ToString().IndexOf(searchValue) > -1 || x.Gbl_Master_Customer.FirstName.IndexOf(searchValue) > -1 || x.Gbl_Master_Customer.LastName.IndexOf(searchValue) > -1 || x.Gbl_Master_Customer.MiddleName.IndexOf(searchValue) > -1) && !x.IsDeleted && x.ShopId.Equals(WebSession.ShopId))
                           .ToList();
            foreach (Sale_Tr_Invoice _currentInvoice in _proList)
            {
                InvoiceDetails _newInvoice = new InvoiceDetails();
                _newInvoice.BalanceAmount = _currentInvoice.BalanceAmount;
                _newInvoice.CustomerId = _currentInvoice.CustomerId;
                _newInvoice.CustomerName = string.Format("{0} {1} {2}", _currentInvoice.Gbl_Master_Customer.FirstName ?? string.Empty, _currentInvoice.Gbl_Master_Customer.MiddleName ?? string.Empty, _currentInvoice.Gbl_Master_Customer.LastName ?? string.Empty);
                _newInvoice.CustomerAddress = string.Format("{0}\n{1}, {2}", _currentInvoice.Gbl_Master_Customer.Address ?? string.Empty, _currentInvoice.Gbl_Master_Customer.Gbl_Master_City.CityName ?? string.Empty, _currentInvoice.Gbl_Master_Customer.Gbl_Master_State.StateName.ToString() ?? string.Empty);
                _newInvoice.GrandTotal = _currentInvoice.GrandTotal;
                _newInvoice.GstAmount = Convert.ToDecimal(_currentInvoice.GstAmount);
                _newInvoice.InvoiceDate = _currentInvoice.InvoiceDate;
                _newInvoice.PaidAmount = _currentInvoice.PaidAmount;
                _newInvoice.PayModeId = _currentInvoice.PayModeId;
                _newInvoice.PayModeRefNo = _currentInvoice.PayModeRefNo;
                _newInvoice.SubTotalAmount = _currentInvoice.SubTotalAmount;
                _newInvoice.InvoiceId = _currentInvoice.InvoiceId;
                _newInvoice.IsRefund = _currentInvoice.IsAmountRefunded;
                _newInvoice.IsCancelled = _currentInvoice.IsCancelled;
                _newInvoice.CancelDate = _currentInvoice.CancelledDate??default(DateTime);
                _newInvoice.CancelRemark = _currentInvoice.CancelRemark;
                List<InvoiceProduct> _lstProducts = new List<InvoiceProduct>();
                foreach (Sale_Dtl_Invoice _currentDetails in _myshopDb.Sale_Dtl_Invoice.Where(x => x.InvoiceId.Equals(_newInvoice.InvoiceId) && x.IsDeleted == false))
                {
                    InvoiceProduct _newProduct = new InvoiceProduct();
                    _newProduct.Discount = Convert.ToInt32(_currentDetails.Discount);
                    _newProduct.ProductId = _currentDetails.ProductId;
                    _newProduct.ProductName = _currentDetails.Gbl_Master_Product.ProductName;
                    _newProduct.Qty = _currentDetails.Qty;
                    _newProduct.Remark = _currentDetails.Remark;
                    _newProduct.SalePrice = _currentDetails.Price;
                    _newProduct.ReturnAmount = _currentDetails.ReturnAmount??0.00M;
                    _newProduct.ReturnDate = _currentDetails.ReturnDate??default(DateTime);
                    _newProduct.ReturnRemark = _currentDetails.ReturnRemark??string.Empty;
                    _newProduct.ReturnQty = _currentDetails.ReturnQty??0;
                    _newProduct.IsReturn = _currentDetails.IsReturn??false;
                    _lstProducts.Add(_newProduct);
                }
                _newInvoice.Products = _lstProducts;
                _lstInvoice.Add(_newInvoice);
            }
            return _lstInvoice;
        }

        public Tuple<CrudStatus, int> SaveInvoice(InvoiceDetails invoiceDetails)
        {
            if (invoiceDetails != null && invoiceDetails.Products != null)
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
                if (_myshopDb.SaveChanges() > 0)
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
                        _newDetails.ShopId = WebSession.ShopId;
                        _newDetails.Remark = item.Remark;
                        _newDetails.ReturnQty = 0;
                        _myshopDb.Sale_Dtl_Invoice.Add(_newDetails);
                    }

                    int _result = _myshopDb.SaveChanges();
                    return _result > 0 ? new Tuple<CrudStatus, int>(CrudStatus.Inserted, _SaleTrans.InvoiceId) : new Tuple<CrudStatus, int>(CrudStatus.NoEffect, 0);
                }
                else
                {
                    return new Tuple<CrudStatus, int>(CrudStatus.NoEffect, 0);
                }
            }
            else
            {
                return new Tuple<CrudStatus, int>(CrudStatus.InvalidParameter, 0);
            }
        }

        public CrudStatus SaveReturnInvoice(InvoiceReturnDetails invoiceDetails)
        {
            if (invoiceDetails != null && invoiceDetails.Products != null)
            {
                _myshopDb = new MyshopDb();
                var _oldInvoice = _myshopDb.Sale_Tr_Invoice.Where(x => x.InvoiceId.Equals(invoiceDetails.InvoiceId) && !x.IsDeleted && x.ShopId.Equals(WebSession.ShopId)).FirstOrDefault();
                if (_oldInvoice == null)
                {
                    return CrudStatus.NotExist;
                }
                else if (_oldInvoice.IsCancelled)
                {
                    return CrudStatus.InvoiceAlreadyCancelled;
                }
                else
                {
                    _oldInvoice.IsAmountRefunded = invoiceDetails.RefundAmount > 0 ? true : false;
                    _oldInvoice.RefundPayModeId = invoiceDetails.RefundPayModeId;
                    _oldInvoice.RefundAmount = invoiceDetails.RefundAmount;
                    _oldInvoice.BalanceAmount = invoiceDetails.BalanceAmount;
                    _oldInvoice.IsSync = false;
                    _oldInvoice.ModifiedBy = WebSession.UserId;
                    _oldInvoice.ModifiedDate = DateTime.Now;
                    _myshopDb.Entry(_oldInvoice).State = EntityState.Modified;
                    int _retult = _myshopDb.SaveChanges();
                    if (_retult > 0)
                    {
                        var _oldInvoiceDetails = _myshopDb.Sale_Dtl_Invoice.Where(x => x.InvoiceId.Equals(_oldInvoice.InvoiceId) && !x.IsDeleted && x.ShopId.Equals(WebSession.ShopId)).ToList();
                        foreach (var _currentProduct in invoiceDetails.Products)
                        {
                            var _returnProduct = _oldInvoiceDetails.Where(x => x.ProductId.Equals(_currentProduct.ProductId)).FirstOrDefault();
                            _returnProduct.IsReturn = true;
                            _returnProduct.IsSync = false;
                            _returnProduct.ModifiedBy = WebSession.UserId;
                            _returnProduct.ModifiedDate = DateTime.Now;
                            _returnProduct.ReturnAmount = _currentProduct.ReturnAmount;
                            _returnProduct.ReturnDate = DateTime.Now;
                            _returnProduct.ReturnQty = _currentProduct.ReturnQty;
                            _returnProduct.ReturnRemark = _currentProduct.ReturnRemark;
                            _myshopDb.Entry(_returnProduct).State = EntityState.Modified;
                        }

                        return _myshopDb.SaveChanges() > 0 ? CrudStatus.Updated : CrudStatus.PartiallyUpdated;
                    }
                    else
                        return CrudStatus.NoEffect;
                }
            }
            else
            {
                return CrudStatus.InvalidParameter;
            }
        }

        public CrudStatus AddCustomer(string firstName, string lastName, string custMobile, int State, int City)
        {
            _myshopDb = new MyshopDb();
            Gbl_Master_Customer _newCustomer = new Gbl_Master_Customer();
            _newCustomer.CreatedBy = WebSession.UserId;
            _newCustomer.CreatedDate = DateTime.Now;
            _newCustomer.FirstName = firstName;
            _newCustomer.LastName = lastName;
            _newCustomer.State = State;
            _newCustomer.District = City;
            _newCustomer.IsDeleted = false;
            _newCustomer.IsSync = false;
            _newCustomer.Mobile = custMobile;
            _newCustomer.CustomerTypeId = 1;
            _newCustomer.ShopId = WebSession.ShopId;
            _myshopDb.Gbl_Master_Customer.Add(_newCustomer);
            return _myshopDb.SaveChanges() > 0 ? CrudStatus.Inserted : CrudStatus.NoEffect;
        }

        public List<DashboardInvoiceModel> GetSalesList(int PageNo = 1, int PageSize = 10)
        {
            _myshopDb = new MyshopDb();
            var _list = _myshopDb.Sale_Tr_Invoice.Where(x => !x.IsDeleted && x.ShopId.Equals(WebSession.ShopId)).ToList().OrderByDescending(x => x.InvoiceDate);
            List<DashboardInvoiceModel> salesList = new List<DashboardInvoiceModel>();
            foreach (var item in _list.Skip((PageNo - 1) * PageSize).Take(PageSize))
            {
                DashboardInvoiceModel _newItem = new DashboardInvoiceModel();
                _newItem.Amount = item.GrandTotal;
                _newItem.CustomerName = string.Format("{0} {1} {2}", item.Gbl_Master_Customer.FirstName, item.Gbl_Master_Customer.MiddleName, item.Gbl_Master_Customer.LastName);
                //;
                _newItem.InvoiceDate = item.InvoiceDate;
                _newItem.InvoiceNo = item.InvoiceId;
                _newItem.IsCancelled = item.IsCancelled;
                _newItem.BalanceAmount = item.BalanceAmount;
                _newItem.RefundAmount = item.RefundAmount;
                _newItem.IsRefund = item.IsAmountRefunded;
                _newItem.PaymentMode = item.Gbl_Master_PayMode.PayMode;
                _newItem.TotalInvoice = _list.Count();
                salesList.Add(_newItem);
            }
            return salesList;
        }

        public CrudStatus CancelInvoice(int InvoiceId,string InvoiceRemark)
        {
            if(InvoiceId<1)
            {
                return CrudStatus.InvalidParameter;
            }
            else
            {
                _myshopDb = new MyshopDb();
                var _oldInvoice = _myshopDb.Sale_Tr_Invoice.Where(x => !x.IsDeleted && x.InvoiceId.Equals(InvoiceId) && x.ShopId.Equals(WebSession.ShopId)).FirstOrDefault();
                if(_oldInvoice.IsCancelled)
                    return CrudStatus.InvoiceAlreadyCancelled;
                else
                {
                    _oldInvoice.IsCancelled = true;
                    _oldInvoice.CancelledDate = DateTime.Now;
                    _oldInvoice.CancelRemark = InvoiceRemark;
                    _oldInvoice.IsSync = false;
                    _oldInvoice.ModifiedBy = WebSession.UserId;
                    _oldInvoice.ModifiedDate = DateTime.Now;
                    _myshopDb.Entry(_oldInvoice).State = EntityState.Modified;
                    return _myshopDb.SaveChanges() > 0 ? CrudStatus.Updated : CrudStatus.NoEffect;
                }
            }
        }
    }
}