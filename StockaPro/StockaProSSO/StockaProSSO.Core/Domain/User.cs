using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using StockaProSSO.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace StockaProSSO.Core.Domain
{
    public class User : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public bool IsGoogleAuthenticatorEnabled { get; set; }
        public string GoogleAuthenticatorSecretKey { get; set; }

        [JsonIgnore]
        public override string PasswordHash { get; set; }
        [JsonIgnore]
        public override string SecurityStamp { get; set; }

        // [JsonIgnore]
        // public override ICollection<IdentityUserClaim> Claims{ get;}
    }

    public class UserDto
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdate { get; set; }

        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public bool IsGoogleAuthenticatorEnabled { get; set; }
        public List<ClaimModel> Claims { get; set; }
    }

    public class CurrentUser
    {
        public CurrentUser()
        { }
        public CurrentUser(ClaimsIdentity identity)
        {
            Firstname = identity.FindFirst(ClaimTypeEnum.given_name.ToString()).Value;
            LastName = identity.FindFirst(ClaimTypeEnum.family_name.ToString()).Value;
            Phone = identity.FindFirst(ClaimTypeEnum.phone_number.ToString()).Value;
            Email = identity.FindFirst(ClaimTypeEnum.email.ToString()).Value;
            UserId = identity.FindFirst(ClaimTypeEnum.sub.ToString()).Value;
            UserName = identity.FindFirst(ClaimTypeEnum.preferred_username.ToString()).Value;
            Roles = identity.Claims.Where(x => x.Type.Contains(ClaimTypeEnum.role.ToString())).ToList();
        }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FullName
        {
            get
            {
                return string.Format("{0}, {1}", Firstname, LastName);
            }
        }
        public List<Claim> Roles { get; set; }
    }
}
