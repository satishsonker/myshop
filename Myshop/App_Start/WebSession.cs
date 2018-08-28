using System;
using System.Collections.Generic;
using System.Web;

namespace Myshop.App_Start
{
    public static class WebSession
    {
        public static string UserMobile
        {
            get { return HttpContext.Current.Session["mobile"].ToString(); }
            set { HttpContext.Current.Session["mobile"] = value; }
        }

        public static List<ShopCollection> ShopList
        {
            get
            {
                if (HttpContext.Current.Session["ShopList"] != null)
                {
                    return HttpContext.Current.Session["ShopList"] as List<ShopCollection>;
                }
                else
                {
                    List<ShopCollection> _col = new List<ShopCollection>();
                    _col.Add(new ShopCollection { ShopId = 0, ShopName = "Select" });
                    return _col;
                }
            }
            set { HttpContext.Current.Session["ShopList"] = value; }
        }

        public static int UserId
        {
            get
            {
                if (HttpContext.Current.Session["userid"] != null)
                {
                    return Convert.ToInt32(HttpContext.Current.Session["userid"].ToString());
                }
                else
                    return 0;
            }
            set { HttpContext.Current.Session["userid"] = value; }
        }

        public static int ShopId
        {
            get
            {
                if (HttpContext.Current.Session["shopid"] != null)
                {
                    return Convert.ToInt32(HttpContext.Current.Session["shopid"].ToString());
                }
                else
                    return 0;
            }
            set { HttpContext.Current.Session["shopid"] = value; }
        }

        public static string ShopName
        {
            get
            {
                if (HttpContext.Current.Session["shopname"] != null)
                {
                    return HttpContext.Current.Session["shopname"].ToString();
                }
                else
                    return "No Shop";
            }
            set { HttpContext.Current.Session["shopname"] = value; }
        }

        public static string Username
        {
            get
            {
                if (HttpContext.Current.Session["username"] == null)
                {
                    return string.Empty;
                }
                else
                {
                    return HttpContext.Current.Session["username"].ToString();
                }
            }
            set { HttpContext.Current.Session["username"] = value; }
        }

        public static string Name
        {

            get
            {
                if (HttpContext.Current.Session["name"] == null)
                {
                    return string.Empty;
                }
                else
                {
                    return HttpContext.Current.Session["name"].ToString();
                }
            }
            set { HttpContext.Current.Session["name"] = value; }
        }

        public static string UserType
        {
            get
            {
                if (HttpContext.Current.Session["usertype"] == null)
                {
                    return string.Empty;
                }
                else
                {
                    return HttpContext.Current.Session["usertype"].ToString();
                }
            }
            set { HttpContext.Current.Session["usertype"] = value; }
        }

        public static bool? UserIsActive
        {
            get { return Convert.ToBoolean(HttpContext.Current.Session["userisactive"].ToString()); }
            set { HttpContext.Current.Session["userisactive"] = value; }
        }

        public static bool? UserIsBlocked
        {
            get { return Convert.ToBoolean(HttpContext.Current.Session["userisblocked"].ToString()); }
            set { HttpContext.Current.Session["userisblocked"] = value; }
        }

        public static DateTime DowntimeStart { get; set; }
        public static DateTime DowntimeEnd { get; set; }
        public static string DowntimeMessage { get; set; }

        public static bool? UserIsDeleted
        {
            get { return Convert.ToBoolean(HttpContext.Current.Session["userisdeleted"].ToString()); }
            set { HttpContext.Current.Session["userisdeleted"] = value; }
        }

        public static bool? UserCanRead
        {
            get {
                if (HttpContext.Current.Session["usercanread"] != null)
                {
                    return Convert.ToBoolean(HttpContext.Current.Session["usercanread"].ToString());
                }
                else
                return false;
            }
            set { HttpContext.Current.Session["usercanread"] = value; }
        }

        public static bool? UserCanWrite
        {
            get {
                if (HttpContext.Current.Session["usercanwrite"] != null)
                {
                    return Convert.ToBoolean(HttpContext.Current.Session["usercanwrite"].ToString());
                }
                else
                    return false;
                }
            set { HttpContext.Current.Session["usercanwrite"] = value; }
        }

        public static bool? UserCanDelete
        {
            get {
                if (HttpContext.Current.Session["usercandelete"] != null)
                {
                    return Convert.ToBoolean(HttpContext.Current.Session["usercandelete"].ToString());
                }
                else
                    return false;
            }
            set { HttpContext.Current.Session["usercandelete"] = value; }
        }

        public static bool? UserCanUpdate
        {
            get
            {
                if (HttpContext.Current.Session["usercanupdate"] != null)
                {
                    return Convert.ToBoolean(HttpContext.Current.Session["usercanupdate"].ToString());
                }
                else
                    return false;
            }
            set { HttpContext.Current.Session["usercanupdate"] = value; }
        }

        public static List<CustomPermission> Permission
        {
            get
            {
                if (HttpContext.Current.Session["userpermission"] != null)
                {
                    return HttpContext.Current.Session["userpermission"] as List<CustomPermission>;
                }
                else
                {
                    return new List<CustomPermission>();
                }
            }
            set { HttpContext.Current.Session["userpermission"] = value; }
        }

        public static int NotificationCount
        {
            get
            {
                if (HttpContext.Current.Session["NotificationCount"] != null)
                {
                    return Convert.ToInt32(HttpContext.Current.Session["NotificationCount"]);
                }
                else
                {
                    return 0;
                }
            }
            set { HttpContext.Current.Session["NotificationCount"] = value; }
        }
        public static string UserPhoto
        {
            get
            {
                if (HttpContext.Current.Session["UserPhoto"] != null)
                {
                    return HttpContext.Current.Session["UserPhoto"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            set { HttpContext.Current.Session["UserPhoto"] = value; }
        }
    }

    public  class ShopCollection
    {
        public  int ShopId { get; set; }
        public  string ShopName{ get; set; }
    }

    public class CustomPermission
    {
        public bool Delete { get; set; }
        public bool IsBlockAccess { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Write { get; set; }
        public int UserId { get; set; }
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string Url { get; set; }
        public int ParentId { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
    }
}