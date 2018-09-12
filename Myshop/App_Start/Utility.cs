using DataLayer;
using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.Collections.Generic;
using System.Drawing;

namespace Myshop.App_Start
{

    public static class Utility
    {
        public static string AppBaseDirectory
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory.ToString();
            }
        }

        #region Constant Variable
        /// <summary>
        /// SMS Gateway Account ID
        /// </summary>
        private const string AccountSid = "ACfcd25c47b291d9d7746459856b3c23a9";

        /// <summary>
        /// SMS Gateway Authuthentication Token
        /// </summary>
        private const string AuthToken = "2be3c7dd82b85fdd795668280d91488f";

        /// <summary>
        /// SMS Gateway Sender Mobile No/Shortcode
        /// </summary>
        private const string SenderMobileNo = "+14159171379";

        /// <summary>
        /// SMS Gateway Message unique Id Length
        /// </summary>
        private const int MessageIdLength = 34;

        /// <summary>
        /// Same OTP will be sent within this time frame
        /// </summary>
        private const int SameOTPMinutes = 15;

        private const string CapitalAlphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string SmallAlphabets = CapitalAlphabets.ToLower();
        private static readonly string Symbols = "!@#$%^&*()_+-=";
        private static readonly string Digits = "0123456789";
        #endregion

        /// <summary>
        /// Get Hash value to ant string
        /// </summary>
        /// <param name="inputStr">input string</param>
        /// <returns></returns>
        public static string getHash(string inputStr)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputStr))
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }

        /// <summary>
        /// Send OTP in form for SMS to any particular mobile no.
        /// </summary>
        /// <param name="MobileNo">receiver mobile No.</param>
        /// <param name="inputMessageBody">Specific message want to send to receiver</param>
        /// <returns>Message Id which send from SMS Gateway</returns>
        public static string CreateOTP(string MobileNo, string inputMessageBody = "")
        {
            MobileNo = MobileNo.Contains("+") ? "91" + MobileNo.Trim() : "+91" + MobileNo.Trim();

            string MessageBody = string.Format("OTP for {0} web application is ", GetAppSettingsValue("Shopname"));
            Random otp = new Random();
            string newOTP = otp.Next(1000, 9999).ToString();
            return _SendSMS((string.IsNullOrEmpty(inputMessageBody) ? MessageBody + newOTP : inputMessageBody), MobileNo);
        }
        public static string CreateOTP(string MobileNo, out string _outOTP, string inputMessageBody = "")
        {
            MobileNo = MobileNo.Contains("+") ? "91" + MobileNo.Trim() : "+91" + MobileNo.Trim();
            TwilioClient.Init(AccountSid, AuthToken);
            string MessageBody = string.Format("OTP for {0} web application is ", GetAppSettingsValue("Shopname"));
            Random otp = new Random();
            _outOTP = otp.Next(1000, 9999).ToString();
            return _SendSMS((string.IsNullOrEmpty(inputMessageBody) ? MessageBody + _outOTP : inputMessageBody), MobileNo);
        }

        public static Enums.OtpStatus VerifyOTP(string Otp, string messageId)
        {
            try
            {
                TwilioClient.Init(AccountSid, AuthToken);
                var message = MessageResource.Fetch(messageId.Trim());
                bool isValidOTP = false;
                DateTime? otpSentTime = message.DateSent;
                double timeDifference = DateTime.Now.Subtract(Convert.ToDateTime(otpSentTime.ToString())).TotalMinutes;
                if (timeDifference <= 30)
                {
                    isValidOTP = message.Body.Contains(Otp.Trim());
                    return isValidOTP ? Enums.OtpStatus.Valid : Enums.OtpStatus.Invalid;
                }
                else
                {
                    return Enums.OtpStatus.Expire;
                }
            }
            catch (Exception ex)
            {
                return Enums.OtpStatus.Exception;
            }
        }

        internal static string ChangePasswordEmailBody(string userName)
        {
            string body = string.Empty;
            //using streamreader for reading my htmltemplate   

            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/EmailTemplate/ChangePassword.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{username}", userName); //replacing the required things 
            body = body.Replace("{shopname}", GetAppSettingsValue("shopname")); //replacing the required things 
            return body;
        }

        internal static string EmailErrorLogBody(ErrorLog log)
        {
            string body = string.Empty;
            //using streamreader for reading my htmltemplate   

            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/EmailTemplate/ErrorLog.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{username}", WebSession.Username); //replacing the required things 
            body = body.Replace("{shopname}", WebSession.ShopName); //replacing the required things 
            body = body.Replace("{errorid}", log.Id.ToString()); //replacing the required things
            body = body.Replace("{userid}", WebSession.UserId.ToString()); //replacing the required things
            body = body.Replace("{path}", log.Area + "/" + log.Controller + "/" + log.Action); //replacing the required things
            body = body.Replace("{message}", log.Message); //replacing the required things
            body = body.Replace("{outer}", log.OuterException); //replacing the required things
            body = body.Replace("{inner}", log.InnerException); //replacing the required things
            return body;
        }

        internal static string EmailResetBody(string _userName, string _userid, string _uniqueId, string _os, string _browser, DateTime _expireTime, string _otp = "")
        {
            string body = string.Empty;
            //using streamreader for reading my htmltemplate   

            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/EmailTemplate/PasswordReset.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{username}", _userName); //replacing the required things 
            body = body.Replace("{shopname}", GetAppSettingsValue("Shopname"));
            body = body.Replace("{datetime}", _expireTime.ToShortDateString() + " " + _expireTime.ToShortTimeString());
            body = body.Replace("{os}", _os);
            body = body.Replace("{otp}", _otp);
            body = body.Replace("{browser}", _browser);
            body = body.Replace("{supportEmail}", GetAppSettingsValue("SupportEmail"));
            body = body.Replace("{ProductSupportTeam}", GetAppSettingsValue("ProductSupportTeamName"));
            body = body.Replace("{link}", GetAppSettingsValue("AppBaseAddress") + "login/SetPassword?txn=" + _uniqueId + "&txnid=" + _userid); //replacing the required things 
            return body;

        }

        internal static void EmailSendHtmlFormatted(string toEmail, string subject, string body)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(GetAppSettingsValue("GmailSenderEmail"));

                mailMessage.Subject = subject;

                mailMessage.Body = body;

                mailMessage.IsBodyHtml = true;

                mailMessage.To.Add(new MailAddress(toEmail.Trim()));

                SendEmail(mailMessage);

            }

        }
        internal static void EmailSendHtmlFormatted(List<string> toEmail, string subject, string body)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(GetAppSettingsValue("GmailSenderEmail"));

                mailMessage.Subject = subject;

                mailMessage.Body = body;

                mailMessage.IsBodyHtml = true;
                foreach (string mailId in toEmail)
                {
                    mailMessage.To.Add(new MailAddress(mailId.Trim()));
                }

                SendEmail(mailMessage);

            }

        }

        internal static string EmailNotificationBody(string userName, string message)
        {
            string body = string.Empty;
            //using streamreader for reading my htmltemplate   

            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/EmailTemplate/Notification.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{username}", userName); //replacing the required things 
            body = body.Replace("{message}", message); //replacing the required things 
            body = body.Replace("{shopname}", WebSession.ShopName); //replacing the required things 
            return body;
        }

        internal static string EmailUserCreationBody(string userName, string email, string mobile, string password, string resetlink, string expiretime)
        {
            string body = string.Empty;
            string shopname = string.IsNullOrEmpty(WebSession.ShopName) ? GetAppSettingsValue("Shopname") : WebSession.ShopName;
            //using streamreader for reading my htmltemplate   

            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/EmailTemplate/UserCreation.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{username}", userName); //replacing the required things 
            body = body.Replace("{email}", email); //replacing the required things 
            body = body.Replace("{mobile}", mobile); //replacing the required things
            body = body.Replace("{password}", password); //replacing the required things
            body = body.Replace("{resetlink}", resetlink); //replacing the required things
            body = body.Replace("{expiretime}", expiretime); //replacing the required things
            body = body.Replace("{shopname}", shopname); //replacing the required things 
            return body;
        }

        internal static string EmailUserAdminPasswordResetBody(string userName, string email, string password)
        {
            string body = string.Empty;
            string shopname = string.IsNullOrEmpty(WebSession.ShopName) ? GetAppSettingsValue("Shopname") : WebSession.ShopName;
            //using streamreader for reading my htmltemplate   

            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/EmailTemplate/PasswordResetbyAdmin.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{username}", userName); //replacing the required things 
            body = body.Replace("{email}", email); //replacing the required things 
            body = body.Replace("{password}", password); //replacing the required things
            body = body.Replace("{shopname}", shopname); //replacing the required things 
            return body;
        }

        private static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetAppSettingsValue(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key].ToString().ToLower();
            }
            catch (Exception)
            {
                return key + " not found in webconfig AppSetting";
            }

        }
        internal static string GetAppSettingsValue(string key, string _returnDefault = "")
        {
            try
            {
                return ConfigurationManager.AppSettings[key].ToString().ToLower();
            }
            catch (Exception)
            {

                return _returnDefault == string.Empty ? key + " not found in webconfig AppSetting" : _returnDefault;
            }

        }

        internal static Enums.CrudStatus CrudStatus(int result, Enums.CrudType type)
        {
            return result > 0 ? (type == Enums.CrudType.Insert ? Enums.CrudStatus.Inserted : (type == Enums.CrudType.Update ? Enums.CrudStatus.Updated : Enums.CrudStatus.Deleted)) : Enums.CrudStatus.NoEffect;
        }

        internal static bool IsUserExist(string username)
        {
            try
            {
                if (!string.IsNullOrEmpty(username))
                {
                    MyshopDb myshop = new MyshopDb();
                    var user = myshop.Gbl_Master_User.FirstOrDefault(x => x.Username.ToLower().Equals(username.ToLower()));
                    return user == null ? false : true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static string DateControlFormat(DateTime date)
        {
            if (date.Year < 1900)
                date = DateTime.Now;
            return date.Date.ToString("yyyy-MM-dd");
        }
        public static byte[] GetImageThumbnails(byte[] ImageByte, int Size = 20)
        {
            return GetThumbnail(ImageByte, Size);
        }

        private static byte[] GetThumbnail(byte[] ByteArray, int size)
        {
            using (MemoryStream ms = new MemoryStream())
            using (Image thumbnail = Image.FromStream(new MemoryStream(ByteArray)).GetThumbnailImage(size, size, null, new IntPtr()))
            {
                thumbnail.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
        private static void SendEmail(MailMessage mailMessage)
        {
            SmtpClient smtp = new SmtpClient();

            smtp.Host = GetAppSettingsValue("GmailHost");

            smtp.EnableSsl = Convert.ToBoolean(GetAppSettingsValue("GmailEnableSsl"));

            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();

            NetworkCred.UserName = GetAppSettingsValue("GmailSenderEmail"); //reading from web.config  

            NetworkCred.Password = GetAppSettingsValue("GmailSenderEmailPassword"); //reading from web.config  

            smtp.UseDefaultCredentials = true;

            smtp.Credentials = NetworkCred;

            smtp.Port = int.Parse(GetAppSettingsValue("GmailPort")); //reading from web.config  

            smtp.Send(mailMessage);
        }

        internal static string _SendSMS(string _body, string _to, string _from = "")
        {
            TwilioClient.Init(AccountSid, AuthToken);
            string _senderMobile = string.IsNullOrEmpty(_from) ? SenderMobileNo : _from;
            var message = MessageResource.Create(
                    to: new PhoneNumber(_to),
                    from: new PhoneNumber(_senderMobile),
                    body: _body);
            return message.Sid;
        }

        internal static string GetDefaultPassword(int _passwordLength)
        {
            string _password = string.Empty;
            int _nextChar = -1;
            Random _ran = new Random();
            for (int i = 0; i < _passwordLength-1; i++)
            {               
                _nextChar = _ran.Next(1, 4);
                if (_nextChar == 1)
                    _password += CapitalAlphabets.ToCharArray()[_ran.Next(0, 25)].ToString();
                else if (_nextChar == 2)
                    _password += SmallAlphabets.ToCharArray()[_ran.Next(0, 25)].ToString();
                else if (_nextChar == 3)
                    _password += Symbols.ToCharArray()[_ran.Next(0, 13)].ToString();
                else if (_nextChar == 4)
                    _password += Digits.ToCharArray()[_ran.Next(0, 9)].ToString();
            }
            return _password;
        }
    }
}