using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMWebApp.Helper
{
    public class GlobalConstants
    {
        public const bool DEBUG_MODE = true;

        #region App & Developer infomation
        public static string APP_NAME = "1";
        public const string APP_VERSION = "1.0";
        public static string BUSINESS_ID = "1";
        public static string BUSINESS_NAME = "HRM";
        public const string BUSINESS_LICENSE = "LICENSED";
        public const string DEVELOPER_NAME = "Datatech";
        public const string DEVELOPER_WEBSITE = "http://datatech.vn/";
        public const string DEVELOPER_EMAIL = "contact@datatech.vn";
        #endregion

        public const int PAGE_SIZE = 50;
        public const int PAGE_SIZE_SMALL = 5;

        #region SESSION KEYS
        public const string SESSION_KEY_SESSION_NAME = "session";
        public const string SESSION_KEY_SESSION_ID = "id";
        public const string SESSION_KEY_CURRENT_USER = "session_key_current_user";
        //public const string SESSION_KEY_AUTHENTICATION = "session_key_authentication";
        public const string SESSION_KEY_IS_LOGIN_FROM_CLIENT_UI = "session_key_is_login_from_client_ui";
        //store for confirm create profile
        public const string SESSION_KEY_TEMPORARY_AUTHENTICATION = "session_key_authentication_temp";
        public const string SESSION_KEY_TEMPORARY_SNSPROFILE = "session_key_sns_profile_temp";
        public const string SESSION_KEY_CLIENT_REQUEST_MODEL = "session_key_client_request_model";
        public const string SESSION_KEY_USER = "session_key_user";
        public const string SESSION_KEY_CLIENT = "session_key_client";
        public const string SESSION_KEY_SYSTEMUSER = "session_key_systemuser";
        public const string SESSION_KEY_SYSTEM_SETTING = "session_key_systemc_configuration";
        public const string SESSION_SNSAUTHENTICATION_PROCESS_STATE = "session_snsauthentication_process_state";
        public const string SESSION_BROWSER_ID = "SESSION_BROWSER_ID";
        public const string SESSION_BROWSER_ID_EMAIL_SENDING_REQUEST_STATUS = "SESSION_BROWSER_ID_EMAIL_SENDING_REQUEST_STATUS";
        public const string SESSION_BROWSER_ID_EMAIL_SENDING_REQUESTCOUNT = "SESSION_BROWSER_ID_EMAIL_SENDING_REQUESTCOUNT";
        public const string SESSION_BROWSER_ID_EMAIL_SENDING_REQUEST_BLOCK_UNTIL = "SESSION_BROWSER_ID_EMAIL_SENDING_REQUEST_BLOCK_UNTIL";
        public const string COOKIE_BROWSER_ID = "COOKIE_BROWSER_ID";
        public const string COOKIE_BROWSER_ID_EMAIL_SENDING_REQUESTCOUNT = "COOKIE_BROWSER_ID_EMAIL_SENDING_REQUESTCOUNT";
        public const string COOKIE_BROWSER_ID_EMAIL_SENDING_REQUEST_BLOCK_UNTIL = "COOKIE_BROWSER_ID_EMAIL_SENDING_REQUEST_BLOCK_UNTIL";
        public static string SESSION_LATEST_USER_POST_AT = "SESSION_LATEST_USER_POST_AT";
        public static string SESSION_KEY_RECENT_COMMENTS = "SESSION_KEY_RECENT_COMMENTS";

        /// <summary>
        /// Save client's information at client panel of Timeline. 
        /// Data is MTimeline_ClientSummary
        /// </summary>
        public static string VIEWDATA_CLIENT_SUMMARY = "VIEWDATA_CLIENT_SUMMARY";
        #endregion

        #region ERROR CODE
        public const String CODE_UNDEFINED_ERROR = "CODE_UNDEFINED_ERROR";
        public const String CODE_ACCOUNT_MANAGER_USER_NAME_IS_USED = "CODE_ACCOUNT_MANAGER_USER_NAME_IS_USED";
        public const String CODE_ACCOUNT_MANAGER_YOU_ARE_LOGINED = "CODE_ACCOUNT_MANAGER_YOU_ARE_LOGINED";
        public const String CODE_ACCOUNT_MANAGER_CANNOT_CREATE_NEW_ACCOUNT = "CODE_ACCOUNT_MANAGER_CANNOT_CREATE_NEW_ACCOUNT";
        public const String CODE_ACCOUNT_MANAGER_USER_NAME_IS_NOT_EXIST = "CODE_ACCOUNT_MANAGER_USER_NAME_IS_NOT_EXIST";
        public const String CODE_ACCOUNT_MANAGER_ACCOUNT_IS_NOT_EXIST = "CODE_ACCOUNT_MANAGER_ACCOUNT_IS_NOT_EXIST";
        public const String CODE_ACCOUNT_MANAGER_CANNOT_UPDATE_ACCOUNT_INFORMATION = "CODE_ACCOUNT_MANAGER_CANNOT_UPDATE_ACCOUNT_INFORMATION";
        public const String CODE_EMAIL_IS_IN_USED = "CODE_EMAIL_IS_IN_USED";
        public const string CODE_MESSAGE_INVALID_INFORMATION = "CODE_MESSAGE_INVALID_INFORMATION";
        public const string CODE_NOT_FOUND_PROFILE = "CODE_NOT_FOUND_PROFILE";
        public const string CODE_NOT_FOUND_PAYMENT_PACKAGE = "CODE_NOT_FOUND_PAYMENT_PACKAGE";
        public const string CODE_CANNOT_UPDATE_PROFILE = "CODE_CANNOT_UPDATE_PROFILE";
        public const string CODE_CANNOT_CREATE_NEW_CLIENT = "CODE_CANNOT_CREATE_NEW_CLIENT";
        public const string CODE_PERMISSION_IS_NOT_GRANTED = "CODE_PERMISSION_IS_NOT_GRANTED";
        public const string CODE_AN_ERROR_OCCURS = "CODE_AN_ERROR_OCCURS";
        public const string CODE_CANNOT_RESET_PASSWORD = "CODE_CANNOT_RESET_PASSWORD";
        public const string CODE_INVALID_REQUEST_MODEL = "CODE_INVALID_REQUEST_MODEL";
        public const string CODE_INVALID_IMAGE_TYPE = "CODE_INVALID_IMAGE_TYPE";
        public const string CODE_IMAGE_TYPE_NOT_SUPPORT = "CODE_IMAGE_TYPE_NOT_SUPPORT";
        public const string CODE_INVALID_EMAIL_ADDRESS = "CODE_INVALID_EMAIL_ADDRESS";
        public const string CODE_THE_CORRESPONDING_RECORD = "CODE_THE_CORRESPONDING_RECORD";
        public const string CODE_RESET_PASSWORD_SENDING_REQUEST_IS_BLOCKED = "CODE_RESET_PASSWORD_SENDING_REQUEST_IS_BLOCKED";
        public const string CODE_RESET_PASSWORD_KEY_IS_EXPIRED = "CODE_RESET_PASSWORD_KEY_IS_EXPIRED";
        public const string CODE_EMAIL_VERIFICATION_SENDING_REQUEST_IS_BLOCKED = "CODE_EMAIL_VERIFICATION_SENDING_REQUEST_IS_BLOCKED";
        public const string CODE_NOT_FOUND_CORRESPONDING_RECORD = "CODE_NOT_FOUND_CORRESPONDING_RECORD";
        public const string CODE_OK = "CODE_OK";
        public const string CODE_FAIL = "CODE_FAIL";
        public const string CODE_A_VERIFICATION_CODE_IS_SENT_TO_YOUR_EMAIL = "CODE_A_VERIFICATION_CODE_IS_SENT_TO_YOUR_EMAIL";
        public const string CODE_INVALID_EMAIL_VERIFICATION_INFORMATION = "CODE_INVALID_EMAIL_VERIFICATION_INFORMATION";
        public const string CODE_INVALID_PAYMENT_INFORMATION = "CODE_INVALID_PAYMENT_INFORMATION";
        public const string CODE_ONLINE_PAYMENT_REPLY = "CODE_ONLINE_PAYMENT_REPLY";
        public const string CODE_INVALID_LICENCE_KIND = "CODE_INVALID_LICENCE_KIND";
        public const string CODE_INVALID_MONEY_KIND = "CODE_INVALID_MONEY_KIND";
        public const string CODE_INVALID_AUTHENTICATION = "CODE_INVALID_AUTHENTICATION";
        public const string CODE_PAYMENT_TRANSACTION_IS_NOT_VERIFIED = "CODE_PAYMENT_TRANSACTION_IS_NOT_VERIFIED";
        public const string CODE_PAYMENT_TRANSACTION_IS_NOT_COMPLETED_YET = "CODE_PAYMENT_TRANSACTION_IS_NOT_COMPLETED_YET";
        public const string CODE_CANNOT_COMPLETE_THE_TRANSACTION = "CODE_CANNOT_COMPLETE_THE_TRANSACTION";
        public const string CODE_INVALID_IMAGE_PATH = "CODE_INVALID_IMAGE_PATH";
        public const string CODE_INVALID_CURRENCY = "CODE_INVALID_CURRENCY";
        public const string CODE_DUPLICATE_RECORD = "CODE_DUPLICATE_RECORD";
        public const string CODE_THE_ORIGINAL_COMMENTS_IS_REMOVED = "CODE_THE_ORIGINAL_COMMENTS_IS_REMOVED";
        public const string CODE_TIMELINE_ID_IS_USED = "CODE_TIMELINE_ID_IS_USED";
        public static string CODE_SIMILAR_CONTENT = "CODE_SIMILAR_CONTENT";
        public static string CODE_POST_COMMENT_SO_FAST = "CODE_POST_COMMENT_SO_FAST";
        public const string CODE_TIMELINEID_IS_USED = "CODE_TIMELINEID_IS_USED";
        public const string CODE_BLOCKED = "CODE_BLOCKED";
        public const string CODE_UNAUTHENTICATED = "CODE_UNAUTHENTICATED";
        #endregion

        #region MESSAGES
        public const String MESSAGE_YOU_DONT_HAVE_RIGHT_ON_THIS_ACTION = "You dont have right on this action";
        #endregion


        //--------------------

        #region Default Values
        public static int DEFAULT_SALT_LENGTH = 5;
        public static int DEFAULT_PAGE_SIZE = 20;
        public static int COOKIE_LIFE = 15; //15 days
        /// <summary></summary>
        public enum SortTypes { NoSorting, Ascending, Descending, MostVoted };
        #endregion

        #region Constants
        public const string COOKIE_KEY_HOME_ACTION = "COOKIE_KEY_HOME_ACTION";
        public const string COOKIE_KEY_LOGIN = "COOKIE_KEY_LOGIN";
        public const string COOKIE_KEY_LOGIN_USERNAME = "COOKIE_KEY_LOGIN_USERNAME";
        public const string COOKIE_KEY_LOGIN_PASSWORD = "COOKIE_KEY_LOGIN_PASSWORD";
        public const string COOKIE_KEY_LOGIN_REMMEMBER = "COOKIE_KEY_LOGIN_REMMEMBER";
        public const string COOKIE_KEY_LOGIN_ROLE = "COOKIE_KEY_LOGIN_ROLE";
        public const string COOKIE_KEY_LOGIN_ACTIONS = "COOKIE_KEY_LOGIN_ACTIONS";
        public const string SESSION_KEY_ADMINISTRATION = "SESSION_KEY_ADMINISTRATION"; //For Administration (Highest role)
        public const string SESSION_KEY_MEMBER = "SESSION_KEY_MEMBER"; //For member (register from frontEnd)
        public const string FILE_UPLOAD_PATH = "/Content/Uploads/";
        public const string FILE_UPLOAD_ALLOW_EXTENSION = ".xls,.xlsx";
        #endregion
    }
}