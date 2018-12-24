using DataLayer;
using Myshop.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Myshop.Models
{
    public class LayoutDetails
    {
        MyshopDb MyshopDb = null;
        public IEnumerable<object> GetPushNotification()
        {
            MyshopDb = new MyshopDb();
            return MyshopDb.Gbl_Master_Notification.Where(x => !x.IsDeleted &&
                                                                    (x.IsForAll || x.UserId.Equals(WebSession.UserId)) &&
                                                                    x.ShopId.Equals(WebSession.ShopId) &&
                                                                    x.MessageExpireDate >= DateTime.Now &&
                                                                    !x.IsRead && x.IsPushed).Select(x=>new {x.Message,x.MessageExpireDate,x.NotificationId,x.PushedDate}).ToList();
        }
    }
}