using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myshop.App_Start
{
    public class Enums
    {
        public enum OtpStatus
        {
            Valid = 1,
            Invalid,
            Expire,
            Exception,
            InvalidUser
        }

        public enum LoginStatus
        {
            Authenticate = 1,
            InvalidCredential,
            Inactive,
            UserBlocked,
            LoginBlocked,
            UserDeleted,
            InvalidUser,
            Exception,
            Failed,
            NotExist,
            AttemptExceeded
        }

        public enum ResetLinkStatus
        {
            send = 1,
            invalidMobile,
            InactiveUser,
            BlockedUser,
            UserDeleted,
            invalidUser,
            exception,
            LinkExpire,
            InvalidLink
        }

        public enum CrudStatus
        {
            Inserted=1,
            Updated,
            Deleted,
            AlreadyExist,
            AlreadyInUse,
            AlreadyExistForSameShop,
            NotExist,
            NotInserted,
            NotUpdated,
            NotDeleted,
            NoEffect,
            Exception,
            FileUploaded,
            FileNotUploaded
        }

        public enum CrudType
        {
            Insert = 1,
            Update,
            Delete
        }

        public enum AlertType
        {
            info=1,
            danger,
            success,
            warning
        }
        public enum FileValidateStatus
        {
            InvalidFormat,
            SizeExceeded,
            InvalidHeight,
            InvalidWidth,
            SizeTooLow,
            NoFiles,
            ValidFile,
            MaxFilesLimitExceeded
        }

        public enum FileType
        {
            Image=1,
            Pdf,
            MS_Word,
            MS_Excel,
            MS_PPT,
            TextFile,
        }
        public enum FileContentType
        {
            Image_bmp = 1,
            Image_jpeg,
            application_pdf,
            text_csv,
            application_msword,
            application_vnd_ms_excel,
            application_vnd_ms_powerpoint,
            Text_Plain,
        }

        public enum validateDataOf
        {
            PanCard=1,
            AadharCard,
            Email,
            Mobile
        }
    }
}