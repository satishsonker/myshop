using DataLayer;
using Myshop.App_Start;
using Myshop.GlobalResource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Myshop.Areas.StockManagement.Models
{
    public class StockEntryDetails
    {
        MyshopDb myshop;
        public Enums.CrudStatus SetStock(StockEntryModel model,List<Stk_Dtl_Entry> productList, Enums.CrudType crudType)
        {
            try
            {
                myshop = new MyshopDb();
                Stk_Tr_Entry newStock = new Stk_Tr_Entry();
                int result = 0;
                if (crudType == Enums.CrudType.Insert)
                {                    
                    newStock.AdditionalAmt = model.AdditionalAmt;
                    newStock.ChequePageId = model.ChequePageId;
                    newStock.DebitAccount = model.DebitAccountId;
                    newStock.PaidAmt = model.PaidAmt;
                    newStock.PayModeId = model.PayModeId;
                    newStock.ReceiptDate = model.VendorReceiptDate;
                    newStock.RemainingAmt = model.RemainingAmt;
                    newStock.TotalAmt = model.TotalAmt;
                    newStock.ShopReceiptEntryNo = model.ShopReceiptEntryNo;
                    newStock.VendorId = model.VendorId;
                    newStock.VendorReceiptNo = model.VendorReceiptNo;
                    newStock.CreatedBy = WebSession.UserId;
                    newStock.CreatedDate = DateTime.Now;
                    newStock.IsDeleted = false;
                    newStock.IsSync = false;
                    newStock.ModifiedBy = WebSession.UserId;
                    newStock.ShopId = WebSession.ShopId;
                    newStock.ModificationDate = DateTime.Now;
                    myshop.Entry(newStock).State = EntityState.Added;
                    result = myshop.SaveChanges();
                    if(result>0)
                    {
                        foreach (Stk_Dtl_Entry  item in productList)
                        {
                            item.StockMstId = newStock.Id;
                            item.ModificationDate = DateTime.Now;
                            item.CreatedDate = DateTime.Now;
                            item.CreatedBy = WebSession.UserId;
                            item.ModifiedBy = WebSession.UserId;
                            item.IsDeleted = false;
                            item.IsSync = false;
                            item.ShopId = WebSession.ShopId;
                            myshop.Entry(item).State = EntityState.Added;
                        }
                        result = myshop.SaveChanges();
                    }
                }
                else
                {
                    var oldStock = myshop.Stk_Tr_Entry.Where(x => x.Id.Equals(model.StockId) && x.IsDeleted == false).FirstOrDefault();
                    if (oldStock != null)
                    {
                        if (crudType == Enums.CrudType.Update)
                        {                            
                            oldStock.ChequePageId = model.ChequePageId;
                            oldStock.DebitAccount = model.DebitAccountId;
                            oldStock.PaidAmt = model.PaidAmt;
                            oldStock.AdditionalAmt = model.AdditionalAmt;
                            oldStock.RemainingAmt = model.RemainingAmt;
                            oldStock.TotalAmt = model.TotalAmt;
                            oldStock.PayModeId = model.PayModeId;
                            oldStock.ReceiptDate = model.VendorReceiptDate;
                            oldStock.ShopReceiptEntryNo = model.ShopReceiptEntryNo;
                            oldStock.VendorId = model.VendorId;
                            oldStock.VendorReceiptNo = model.VendorReceiptNo;
                            oldStock.IsSync = false;
                            oldStock.ModifiedBy = WebSession.UserId;
                            oldStock.ModificationDate = DateTime.Now;
                            myshop.Entry(oldStock).State = EntityState.Modified;
                            var oldTrStock = myshop.Stk_Dtl_Entry.Where(x => x.StockMstId.Equals(model.StockId) && x.IsDeleted == false).ToList();
                            if(oldTrStock!=null)
                            {
                                foreach (Stk_Dtl_Entry item in productList)
                                {
                                    var oldItem = oldTrStock.Where(xy => xy.StockTrId.Equals(item.StockTrId)).FirstOrDefault();
                                    if (oldItem != null)
                                    {
                                        oldItem.ModificationDate = DateTime.Now;
                                        oldItem.ModifiedBy = WebSession.UserId;
                                        oldItem.IsDeleted = false;
                                        oldItem.IsSync = false;
                                        oldItem.SellPrice = item.SellPrice;
                                        oldItem.PurchasePrice = item.PurchasePrice;
                                        oldItem.Qty = item.Qty;
                                        oldItem.Description = item.Description;
                                        oldItem.Color = item.Color;
                                        myshop.Entry(oldItem).State = EntityState.Modified;
                                    }
                                    else
                                    {
                                        item.StockMstId = newStock.Id;
                                        item.ModificationDate = DateTime.Now;
                                        item.CreatedDate = DateTime.Now;
                                        item.CreatedBy = WebSession.UserId;
                                        item.ModifiedBy = WebSession.UserId;
                                        item.IsDeleted = false;
                                        item.IsSync = false;
                                        item.ShopId = WebSession.ShopId;
                                        myshop.Entry(item).State = EntityState.Added;
                                    }
                                }
                            }

                        }
                        else if (crudType == Enums.CrudType.Delete)
                        {
                            oldStock.IsDeleted = true;
                            oldStock.IsSync = false;
                            oldStock.ModifiedBy = WebSession.UserId;
                            oldStock.ModificationDate = DateTime.Now;
                            myshop.Entry(oldStock).State = EntityState.Modified;
                        }
                        result = myshop.SaveChanges();
                    }
                }                
                if(result>0 && model.ChequePageId>0)
                {
                    var ChequePage = myshop.Gbl_Master_BankChequeDetails.Where(x => x.ChequePageId.Equals(model.ChequePageId) && x.IsDeleted == false).First();
                    if(ChequePage!=null)
                    {
                        ChequePage.IsSync = false;
                        ChequePage.IsUsed = true;
                        ChequePage.Desciption = Resource.ChequeIssue;
                        ChequePage.ModificationDate = DateTime.Now;
                        ChequePage.ModifiedBy = WebSession.UserId;
                        myshop.Entry(ChequePage).State = EntityState.Modified;
                        myshop.SaveChanges();
                    }
                }
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
        public IEnumerable<object> GetStockJson()
        {
            try
            {
                myshop = new MyshopDb();
                var catList = (from stk in myshop.Stk_Tr_Entry.Where(x => x.IsDeleted == false && x.ShopId.Equals(WebSession.ShopId))
                               from ven in myshop.Gbl_Master_Vendor.Where(y => y.IsDeleted == false && y.VendorId.Equals(stk.VendorId))//.DefaultIfEmpty()
                               from pay in myshop.Gbl_Master_PayMode.Where(z => z.IsDeleted == false && z.PayModeId.Equals(stk.PayModeId))//.DefaultIfEmpty()
                               orderby stk.Id descending
                               select new
                               {
                                   stk.VendorReceiptNo,
                                   VendorReceiptDate=stk.ReceiptDate,
                                   stk.PaidAmt,
                                   stk.TotalAmt,
                                   stk.RemainingAmt,
                                   stk.AdditionalAmt,
                                   stk.ShopReceiptEntryNo,
                                   ven.VendorName,
                                   stk.VendorId,
                                   stk.PayModeId,
                                   StockId=stk.Id,
                                   stk.ChequePageId,
                                   stk.DebitAccount,
                                   pay.PayMode
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
        public IEnumerable<object> GetStockDetailsJson(int stockId)
        {
            try
            {
                myshop = new MyshopDb();
                var catList = (from stk in myshop.Stk_Dtl_Entry.Where(x => x.IsDeleted == false && x.ShopId.Equals(WebSession.ShopId) && x.StockMstId.Equals(stockId))
                               orderby stk.ProductId descending
                               select new
                               {
                                   stk.ProductId,
                                   stk.Gbl_Master_Product.ProductName,
                                   stk.BrandId,
                                   stk.Gbl_Master_Brand.BrandName,
                                   stk.SellPrice,
                                   stk.PurchasePrice,
                                   stk.Qty,
                                   stk.Color,
                                   stk.Description,
                                   stk.Gbl_Master_Product.SubCatId,
                                   stk.Gbl_Master_Product.Gbl_Master_SubCategory.SubCatName,
                                   stk.Gbl_Master_Product.Gbl_Master_SubCategory.Gbl_Master_Category.CatName,
                                   stk.Gbl_Master_Product.Gbl_Master_SubCategory.CatId,
                                   stk.StockTrId
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
        public IEnumerable<object> GetUniqueStockProducts()
        {
            try
            {
                myshop = new MyshopDb();
                var catList = (from cat in myshop.Stk_Dtl_Entry.Where(x => x.IsDeleted == false && x.ShopId.Equals(WebSession.ShopId))
                               from pro in myshop.Gbl_Master_Product.Where(y => y.ProductId.Equals(cat.ProductId) && y.IsDeleted == false)
                               orderby pro.ProductName
                               select new
                               {
                                   cat.ProductId,
                                   pro.ProductName
                               }).Distinct().ToList();
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


    }

    public class StockEntryModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "VendorId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "VendorId should be greater than 0 (Zero)")]
        public int VendorId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "StockId should be greater than -1 (Zero)")]
        public int StockId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "PayModeId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "PayModeId should be greater than 0 (Zero)")]
        public int PayModeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "VendorReceiptNo is required")]
        [StringLength(maximumLength: 10, ErrorMessage = "Max 10 chars is allowed in VendorReceiptNo", MinimumLength = 0)]
        public string VendorReceiptNo { get; set; } = "";

        [Required(AllowEmptyStrings = false, ErrorMessage = "ShopReceiptEntryNo is required")]
        [StringLength(maximumLength: 20, ErrorMessage = "Max 20 chars is allowed in ShopReceiptEntryNo", MinimumLength = 0)]
        public string ShopReceiptEntryNo { get; set; } = "";

        public DateTime VendorReceiptDate { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "DebitAccountId should be greater than -1")]
        public int DebitAccountId { get; set; }

        [Display(Name ="Cheque Number")]
        [Range(0, int.MaxValue, ErrorMessage = "ChequePageId should be greater than -1")]
        public int ChequePageId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Total Amount is required")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid Total Amount")]
        public decimal TotalAmt { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Additional Amount is required")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid Additional Amount")]
        public decimal AdditionalAmt { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Paid Amount is required")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid Paid Amount")]
        public decimal PaidAmt { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Remaining Amount is required")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid Remaining Amount")]
        public decimal RemainingAmt { get; set; }
    }
}