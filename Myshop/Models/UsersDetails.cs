using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using Myshop.App_Start;
using System.Data.Entity;

namespace Myshop.Models
{
    public class UsersDetails
    {
        MyshopDb myshop;

        public List<UserNotificationModel> GetUserNotificationList()
        {
            try
            {
                myshop = new MyshopDb();
                var notifyList =(from x in myshop.Gbl_Master_Notification.Where(
                    x => x.IsDeleted == false &&
                    (x.UserId.Equals(WebSession.UserId) || x.IsForAll==true) &&
                    DbFunctions.TruncateTime(x.MessageExpireDate) >= DbFunctions.TruncateTime(DateTime.Now) &&
                    x.IsRead==false &&
                    x.IsPushed==true &&
                    DbFunctions.TruncateTime(x.PushedDate) <= DbFunctions.TruncateTime(DateTime.Now) &&
                    x.ShopId.Equals(WebSession.ShopId) &&
                    (x.Gbl_Master_NotificationType.NotificationType.ToLower().IndexOf("push") > -1 ||
                    x.Gbl_Master_NotificationType.NotificationType.ToLower().IndexOf("web") > -1) &&
                    x.Gbl_Master_NotificationType.IsDeleted==false &&
                    x.Gbl_Master_NotificationType.ShopId.Equals(WebSession.ShopId)
                    )
                    select new UserNotificationModel
                    {
                        Message=x.Message,
                        NotificationId=x.NotificationId
                    }).OrderBy(y=>y.NotificationId).ToList();
                return notifyList;
            }
            catch (Exception ex)
            {
                return new List<UserNotificationModel>();
            }
        }

        public Enums.CrudStatus DeleteUserNotificationList(int notificationId)
        {
            try
            {
                myshop = new MyshopDb();
              var notify=  myshop.Gbl_Master_Notification.Where(x => x.NotificationId.Equals(notificationId) && x.ShopId.Equals(WebSession.ShopId) && x.UserId.Equals(WebSession.UserId)).FirstOrDefault();
                if(notify!=null)
                {
                    notify.IsDeleted = true;
                    notify.ModificationDate = DateTime.Now;
                    notify.ModifiedBy = WebSession.UserId;
                    notify.IsSync = false;
                    myshop.Entry(notify).State = EntityState.Modified;
                    int result = myshop.SaveChanges();
                    return result > 0 ? Enums.CrudStatus.Deleted : Enums.CrudStatus.NotDeleted;
                }
                else
                return Enums.CrudStatus.NotExist;
            }
            catch (Exception ex)
            {
                return Enums.CrudStatus.Exception;
            }
        }
    }
}