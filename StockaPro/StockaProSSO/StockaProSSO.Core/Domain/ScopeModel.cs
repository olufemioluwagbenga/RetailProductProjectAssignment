using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StockaProSSO.Core.Domain
{
    public class ScopeModel
    {
        public ScopeModel()
        {

            Enabled = true;
            Type = 1; // Resource
            Emphasize = false;
            IncludeAllClaimsForUser = false;
            ShowInDiscoveryDocument = true;
            Required = false;


        }

        public int Id { get; set; }
        public string ClaimsRule { get; set; }

        public string Description { get; set; }
        [Required(AllowEmptyStrings = false)]

        public string DisplayName { get; set; }

        public bool Emphasize { get; set; }
        [Required]
        public bool Enabled { get; set; }

        public bool IncludeAllClaimsForUser { get; set; }

        [Required(AllowEmptyStrings = false)]
        //[RegularExpression(@"[a-zA-Z]+[0-9]*[\-\'\._]*[a-zA-Z]*[0-9]*\s*", ErrorMessage = "The {0} format is invalid")]
        public string Name { get; set; }

        public bool Required { get; set; }

        public bool ShowInDiscoveryDocument { get; set; }
        [Required]
        public int Type { get; set; }

        public List<ScopeClaimModel> ScopeClaims { get; set; }
        public List<ScopeSecretModel> ScopeSecrets { get; set; }



    }

    public class ScopeClaimModel
    {
        public ScopeClaimModel()
        { }
        public ScopeClaimModel(string name, bool alwaysInclude = false)
        {
            Name = name;
            AlwaysIncludeInIdToken = alwaysInclude;
        }


        public int Id { get; set; }
        public bool AlwaysIncludeInIdToken { get; set; }

        public string Description { get; set; }
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"[a-zA-Z]+[0-9]*[\-\'\._]*[a-zA-Z]*[0-9]*\s*", ErrorMessage = "The {0} format is invalid")]
        public string Name { get; set; }
    }

    public class ScopeSecretModel
    {
        public ScopeSecretModel()
        {
            Expiration = DateTimeOffset.Now.AddDays(30);
        }
        public int Id { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public DateTimeOffset? Expiration { get; set; }

        [StringLength(250)]
        public string Type { get; set; }
        [Required]
        [StringLength(250)]
        public string Value { get; set; }
    }

    public class ScopeUser
    {
        public string UserId { get; set; }
        public int ScopeKey { get; set; }
    }
}
