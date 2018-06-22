using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myshop.App_Start
{
    public static class Custom
    {
        public static class Message
        {
            public static string ErrorLogEmailSubject = "Error Notification - "+WebSession.ShopName;
        }
    }
}