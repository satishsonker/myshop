using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Myshop.Areas.StockManagement.Models
{
    public class ShopModel
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="ShopId is required")]
        [Range(1,int.MaxValue,ErrorMessage ="ShopId should be greater than 0 (Zero)")]
        public int ShopId { get; set; }
    }

    public class VendorModel : ShopModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "VendorName is required")]
        [StringLength(maximumLength: 150, MinimumLength = 3, ErrorMessage = "Invalid VendorName (3 Min and 150 max chars)")]
        public String VendorName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "VendorId should be greater or Equal than 0 (Zero)")]
        public int VendorId { get; set; }

        [StringLength(maximumLength: 200, MinimumLength = 3, ErrorMessage = "Invalid VendorMobile (3 Min and 200 max chars)")]
        public String VendorDesc { get; set; } = "";

        [Required(AllowEmptyStrings = false, ErrorMessage = "VendorMobile is required")]
        [StringLength(maximumLength: 15, MinimumLength = 10, ErrorMessage = "Invalid VendorMobile (10 Min and 15 max chars)")]
        public String VendorMobile { get; set; } = "";

        [StringLength(maximumLength: 250, MinimumLength = 3, ErrorMessage = "Invalid VendorMobile (3 Min and 250 max chars)")]
        public String VendorAddress { get; set; } = "";
    }

    public class BrandModel : ShopModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "BrandName is required")]
        [StringLength(maximumLength:50,MinimumLength =3, ErrorMessage = "Invalid BrandName (3 Min and 50 max chars)")]
        public String BrandName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "BrandId should be greater or Equal than 0 (Zero)")]
        public int BrandId { get; set; }

        public String BrandDesc { get; set; } = "";
    }
    public class CategoryModel : ShopModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Category name is required")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Invalid Category Name (3 Min and 50 max chars)")]
        public String CatName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "CatId should be greater or Equal than 0 (Zero)")]
        public int CatId { get; set; }

        public String CatDesc { get; set; } = "";
    }

    public class UnitModel : ShopModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unit name is required")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Invalid Unit Name (3 Min and 50 max chars)")]
        public String UnitName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "UnitId should be greater or Equal than 0 (Zero)")]
        public int UnitId { get; set; }

        public String UnitDesc { get; set; } = "";
    }

    public class SubCategoryModel : ShopModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Sub Category name is required")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Invalid Sub Category Name (3 Min and 50 max chars)")]
        public String SubCatName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "CatogaryId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "CatogaryId should be greater than 0 (Zero)")]
        public int CatId { get; set; }

        public String SubCatDesc { get; set; } = "";

        [Range(0, int.MaxValue, ErrorMessage = "SubCatId should be greater or Equal than 0 (Zero)")]
        public int SubCatId { get; set; }
    }

    public class ProductModel : ShopModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Product name is required")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "Product Name (3 Min and 50 max chars)")]
        public String ProductName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "ProductId should be greater than 0 (Zero)")]
        public int ProductId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Sub Catogary Id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Sub Catogary Id should be greater than 0 (Zero)")]
        public int SubCatId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "UnitId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "UnitId should be greater than 0 (Zero)")]
        public int UnitId { get; set; }

        public String Desc { get; set; } = "";

        public String Color { get; set; } = "";

        public String ProductCode { get; set; } = "";

        [Required(AllowEmptyStrings = false, ErrorMessage = "MinQuantity is required")]
        [Range(1, Double.MaxValue, ErrorMessage = "MinQuantity should be greater than 0 (Zero)")]
        public int MinQuantity { get; set; }
    }   
}