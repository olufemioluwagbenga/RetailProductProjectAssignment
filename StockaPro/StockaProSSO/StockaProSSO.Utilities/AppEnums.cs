using System;
using System.Collections.Generic;
using System.Linq;

namespace StockaProSSO.Utilities
{
    public enum ClaimTypeEnum
    {
        permission,
        role,
        application,
        given_name,
        family_name,
        phone_number,
        email,
        preferred_username,
        sub
    }

    public enum ApplicationRoles
    {
        vgg_superadmin,
        vgg_admin,
        vgg_user,
        clientadmin,
        clientuser
    }



    public enum AuditSections
    {
        Account = 1,
        Scope = 2,
        Client = 3

    }

    public enum AuditActions
    {
        Login,
        Logout,
        AddClaim,
        RemoveClaim,
        ChangePassword,
        ResetPassword,
        AddClearance,
        EnableClearance,
        DisableClearance,
        CreateScope,
        UpdateScope,
        CreateClient,
        UpdateClient,
        EnableTwoFactor,
        DisableTwoFactor,
        UserRegistration,
        AccountConfirmation,
        ForgotPassword,
        LockAccount,
        UnlockAccount
    }


    public static class AppEnums
    {
        public static List<string> GetClaimTypes()
        {
            return Enum.GetNames(typeof(ClaimTypeEnum)).ToList();
        }

        public static List<string> ApplicationRolesList()
        {
            return Enum.GetNames(typeof(ApplicationRoles)).ToList();
        }


    }
}

