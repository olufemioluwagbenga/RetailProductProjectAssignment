using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StockaProSSO.Core.Domain
{
    public class AccountModels
    {

    }

    /// <summary>
    /// UserModel class
    /// </summary>
    public class UserModel
    {
        public UserModel()
        {
            ConfirmEmail = true;
        }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "First Name", Description = "Your given name")]
        [RegularExpression(@"[a-zA-Z]+[\-\'\.]*[a-zA-Z]*\s*", ErrorMessage = "The {0} format is invalid")]
        [StringLength(64, ErrorMessage = "First Name must be at least 2 characters long", MinimumLength = 2)]
        public string FirstName { get; set; }

        //[RegularExpression(@"[a-zA-Z]+[\-\'\.]*[a-zA-Z]*\s*", ErrorMessage = "The {0} format is invalid")]
        //public string TestName { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Last Name", Description = "Your family name")]
        [RegularExpression(@"[a-zA-Z]+[\-\'\.]*[a-zA-Z]*\s*", ErrorMessage = "The {0} format is invalid")]
        [StringLength(64, ErrorMessage = "Last Name must be at least 2 characters long", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(100, ErrorMessage = "Last Name must be at least 2 characters long", MinimumLength = 2)]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        //[Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "The Password must be at least 6 characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "The {0} format is invalid")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 9)]
        public string PhoneNumber { get; set; }
        public bool ConfirmEmail { get; set; }
        public IList<ClaimModel> Claims { get; set; }

    }

    public class ClaimModel
    {
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"[a-zA-Z]+[\-\'\._]*[a-zA-Z]*\s*", ErrorMessage = "The {0} format is invalid")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {1} characters long.", MinimumLength = 2)]
        public string Type { get; set; }

        [Required(AllowEmptyStrings = false)]
        //[RegularExpression(@"[a-zA-Z]+[\-\'\._]*[a-zA-Z]*\s*", ErrorMessage = "The {0} format is invalid")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {1} characters long.", MinimumLength = 2)]
        public string Value { get; set; }
    }

    public class UserUpdateModel
    {
        [Required]
        [Display(Name = "First Name", Description = "Your given name")]
        [RegularExpression(@"[a-zA-Z]+[\-\'\.]*[a-zA-Z]*\s*", ErrorMessage = "The {0} format is invalid")]
        [StringLength(100, ErrorMessage = "Firstname accepts a minimum of 2 characters and a maximum of 100", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name", Description = "Your family name")]
        [RegularExpression(@"[a-zA-Z]+[\-\'\.]*[a-zA-Z]*\s*", ErrorMessage = "The {0} format is invalid")]
        [StringLength(100, ErrorMessage = "Lastname accepts a minimum of 2 characters and a maximum of 100", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Username accepts a minimum of 2 characters and a maximum of 100", MinimumLength = 2)]
        public string UserName { get; set; }

        //[Required]
        //[EmailAddress(ErrorMessage = "Please enter a valid email address")]
        //public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "The {0} format is invalid")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 9)]
        public string PhoneNumber { get; set; }

    }

    public class UserClaim
    {
        [Required(AllowEmptyStrings = false)]
        public string UserId { get; set; }
        [Required]
        public IList<ClaimModel> Claims { get; set; }
    }

    public class AccountConfirmation
    {
        [Required(AllowEmptyStrings = false)]
        public string UserId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Token { get; set; }
    }

    public class ResetPassword
    {
        [Required(AllowEmptyStrings = false)]
        public string UserId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Token { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "The Password must be at least 6 characters long.", MinimumLength = 6)]
        public string NewPassword { get; set; }
    }

    public class ChangePassword
    {
        [Required(AllowEmptyStrings = false)]
        public string UserId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "The Password must be at least 6 characters long.", MinimumLength = 6)]
        public string CurrentPassword { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "The Password must be at least 6 characters long.", MinimumLength = 6)]
        public string NewPassword { get; set; }
    }

    //when the user is changing his/her password from the identity server itself
    public class ChangePasswordModel
    {
        [Required]
        //[StringLength(50, ErrorMessage = "The Password must be at least 6 characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]

        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "The Password must be at least 6 characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Error: Passwords do not match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }

    public class Result<T>
    {
        public Result()
        {

        }
        public Result(T item)
        {
            Response = item;
        }
        public bool Exists { get; set; }
        public T Response { get; set; }
        public string CustomValue { get; set; }
        public long TotalRecords { get; set; }
    }

    public class TwoFactorResult
    {
        public IdentityResult Result { get; set; }
        public string Type { get; set; }
    }
    public class RegisterUserResult
    {
        public string UserId { get; set; }
        public string EmailConfirmationToken { get; set; }
        public string PasswordResetToken { get; set; }
    }

    public class ForgotPassowrdResult
    {
        public string UserId { get; set; }
        public string PasswordToken { get; set; }
    }

    public class GoogleAuthModel
    {
        public string Code { get; set; }
        public string SecretKey { get; set; }
        public string BarcodeUrl { get; set; }
    }
}
