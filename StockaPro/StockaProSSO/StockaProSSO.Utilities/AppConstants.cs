using System;
using System.Collections.Generic;
using System.Text;

namespace StockaProSSO.Utilities

{
    public static class AppConstants
    {
        public const string IssuerUri = "https://sso.stockapro.com";

        public const string MvcClientUri = "https://localhost:44391/";
        public const string MvcClientLogOutUri = "https://localhost:44391/Home/SignIn";
        public const string ConnectionStringName = "IdentityServer";
        public const string AccountConfimation = "User account has not been confirmed";
        public const string InvalidUser = "User account does not exist";
        public const string NotCleared = "User account requires clearance to become active for the application";
        public const string UniquKeyViolation = "Violation of UNIQUE KEY constraint";
        public const string InvalidToken = "Invalid token.";
        public const string InvalidClient = "Invalid client.";
        public const string InvalidScope = "Invalid scope.";
        public const string InvalidPassword = "Incorrect password.";
        public const string AccountLocked = "User account is locked.";
        public const string AccountLockWarning = "Warning : Your account will be locked after the fifth (5) attempt.";
        public const string DefaultPasswordNotification = "User has not created passoword for account.";
        public const string ClientId = "client-id";
        public const string NotProvisioned = "Account has not been provisioned for client.";
        public const string InvalidClaimType = "Claims contain one or more invalid types";
        public const string InvalidRoleClaim = "Invalid role claim: User must belong to only one of the predefined roles (sys_superadmin, sys_admin, sys_user, clientadmin, clientuser) at registration. In addition to other client specific roles.";
        public const string InvalidUserDomain = "Invalid user domain. users in role sys_superadmin, sys_admin or sys_user must be registered with the predefined system domains";
        public const string LogPath = "logpath";
        public const string ClientSecret = "clientsecret";
        public const string Client = "client";
        public const string ClientUser = "clientUser";
        public const string ClientPass = "clientPass";
        public const string ClientUserScopes = "clientUserScopes";
        public const string ValidDomains = "valid-domains";
        public const string InExistingAdminRole = "Cannot add sys_superadmin or sys_admin role claim to user for client {0} as user already belong to one of the roles.";
        public const string InExistingRole = "Cannot add general role claim to user for client {0} as user already belong to a general role.";
        public const string InvalidClearance = "client clearance has not been added for user";
        public const string ContainsPassword = "Password";
        public const string ClearanceNotSet = "user has not been cleared to be sys_admin or sys_superadmin for client {0} : {1}";
        public const string ClearanceNotEnabled = "user clearance to be sys_admin or sys_superadmin has been disabled {0}";
        public const string Login = "Login";
        public const string Logout = "Logout";
        public const string AddClaim = "Add Claims";
        public const string RemoveClaim = "Remove Claims";
        public const string ChangePassword = "Change Password";
        public const string ResetPassword = "Reset Password";
        public const string AddClearance = "Add Clearance";
        public const string EnableClearance = "Enable Clearance";
        public const string DisableClearance = "Disable Clearance";
        public const string CreateScope = "Create Scope";
        public const string UpdateScope = "Update Scope";
        public const string CreateClient = "Create Client";
        public const string UpdateClient = "Update Client";
        public const string EnableTwoFactor = "Enable twofactor";
        public const string DisableTwoFactor = "Disable twofactor";
        public const string UserRegistration = "User Registration";
        public const string AccountConfirmation = "Account Confirmation";
        public const string ForgotPassword = "Forgot Password";
        public const string LockAccount = "Lock Account";
        public const string UnlockAccount = "Unlock Account";
        public const string UpdateUser = "Update User";
        public const string EnableGoogleAuth = "Enable Google Authentication";




        //public static string Baseurl
        //{
        //    get
        //    {
        //        return PublicOrigin;
        //    }
        //}
        //public static string MVCClient
        //{
        //    get
        //    {
        //        return AppSettings.Get("mvc.baseurl", "http://web.ebips.local");
        //    }
        //}


        //public static string IdSrv
        //{
        //    get
        //    {
        //        return Baseurl + "/identity";
        //    }
        //}

        //public static string IdSrvApi
        //{
        //    get
        //    {
        //        return AppSettings.Get("api-url", "http://localhost:30439/");
        //    }
        //}
        //public static string SsoCore
        //{
        //    get
        //    {
        //        return IdSrv + "/core";
        //    }
        //}

        //public static string TokenEndpoint
        //{
        //    get
        //    {
        //        return IdSrv + "/connect/token";
        //    }
        //}

        //public static string AuthorizeEndpoint
        //{
        //    get
        //    {
        //        return IdSrv + "/connect/authorize";
        //    }
        //}

        //public static string AccessTokenValidation
        //{
        //    get
        //    {
        //        return IdSrv + "/connect/accesstokenvalidation";
        //    }
        //}

        //public static string UserInfo
        //{
        //    get
        //    {
        //        return IdSrv + "/connect/userinfo";
        //    }
        //}
        //public static string IdmUrl
        //{
        //    get
        //    {
        //        return IdSrv + "/admin";
        //    }
        //}
        //public static string Dbconnectionname
        //{
        //    get
        //    {
        //        return AppSettings.Get("sso.db", "ebipsid");
        //    }
        //}
        //public static string SsoCookie
        //{
        //    get
        //    {
        //        return AppSettings.Get("sso.cookie", "SsoCookies");
        //    }
        //}
        //public static string SsoGroup
        //{
        //    get
        //    {
        //        return AppSettings.Get("SSOGroup", "EBIPS");
        //    }
        //}
        //public static string SsoAuthority
        //{
        //    get
        //    {
        //        return IdSrv;
        //    }
        //}

        //public static string PublicOrigin
        //{
        //    get
        //    {
        //        return AppSettings.Get("public-origin", "https://localhost");
        //    }
        //}

        //public static int SessionTimeout
        //{
        //    get
        //    {
        //        return Convert.ToInt32(AppSettings.Get("sessiontimeout", "20"));
        //    }
        //}
        //public static string SsoHash
        //{
        //    get
        //    {
        //        return AppSettings.Get("sso.hash", "rn35szXKJTgb8De8");

        //    }
        //}
    }
}
