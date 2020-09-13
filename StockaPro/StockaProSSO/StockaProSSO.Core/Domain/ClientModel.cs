//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Text;

//namespace StockaProSSO.Core.Domain
//{
//    public class ClientModel
//    {
//        public ClientModel()
//        {
//            RequireConsent = true;
//            AllowAccessToAllScopes = false;
//            AccessTokenLifetime = 3600; // 1hr
//            AbsoluteRefreshTokenLifetime = 2592000; //30 days
//        //    AccessTokenType = AccessTokenEnum.Jwt;
//            AllowAccessToAllGrantTypes = false;
//            AllowAccessTokensViaBrowser = true;
//            AllowClientCredentialsOnly = false;
//            AllowRememberConsent = true;
//            AlwaysSendClientClaims = false;
//            AuthorizationCodeLifetime = 300; // 5mins
//            IdentityTokenLifetime = 300; //5mins
//            EnableLocalLogin = true;
//            IncludeJwtId = false;
//            LogoutSessionRequired = true;
//            RequireConsent = true;
//            PrefixClientClaims = true;
//            RequireSignOutPrompt = false;
//            SlidingRefreshTokenLifetime = 1296000;// 15 days
//            UpdateAccessTokenOnRefresh = false;
//            RefreshTokenExpiration = TokenExpiration.Absolute; //Absolute
//          //  Flow = Flows.Implicit; // Implicit
//          //  RefreshTokenUsage = TokenUsage.OneTimeOnly; //Onetime only
//            Enabled = true;



//        }


//        public int Id { get; set; }
//        [Range(0, int.MaxValue)]
//        public virtual int AbsoluteRefreshTokenLifetime { get; set; }
//        [Range(0, int.MaxValue)]
//        public int AccessTokenLifetime { get; set; }
//        [Range(0, 1)]
//        public int ClientAccessTokenType { get; set; }
//        [JsonIgnore]
//        public AccessTokenType AccessTokenType
//        {
//            get
//            {
//                AccessTokenEnum ac;
//                switch (this.ClientAccessTokenType)
//                {
//                    case 0:
//                        ac = AccessTokenEnum.Jwt;
//                        break;
//                    case 1:
//                        ac = AccessTokenEnum.Reference;
//                        break;
//                    default:
//                        ac = AccessTokenEnum.Jwt;
//                        break;
//                }
//                return ac;
//            }
//            set
//            {
//                switch (value)
//                {
//                    case AccessTokenEnum.Jwt:
//                        ClientAccessTokenType = 0;
//                        break;
//                    case AccessTokenEnum.Reference:
//                        ClientAccessTokenType = 1;
//                        break;
//                    default:
//                        break;
//                }
//            }
//        }
//        public bool AllowAccessToAllGrantTypes { get; set; }
//        public bool AllowAccessToAllScopes { get; set; }
//        public bool AllowAccessTokensViaBrowser { get; set; }
//        public bool AllowClientCredentialsOnly { get; set; }
//        public bool AllowRememberConsent { get; set; }
//        public bool AlwaysSendClientClaims { get; set; }
//        [Range(0, int.MaxValue)]
//        public virtual int AuthorizationCodeLifetime { get; set; }
//        [Required]
//        public string ClientName { get; set; }
//        [Required]
//        public string ClientId { get; set; }
//        [Required]
//        public bool Enabled { get; set; }

//        [JsonIgnore]
//        public Flows Flow
//        {
//            get
//            {
//                Flows cf;
//                switch (this.ClientFlow)
//                {
//                    case 0:
//                        cf = Flows.AuthorizationCode;
//                        break;
//                    case 1:
//                        cf = Flows.Implicit;
//                        break;
//                    case 2:
//                        cf = Flows.Hybrid;
//                        break;
//                    case 3:
//                        cf = Flows.ClientCredentials;
//                        break;
//                    case 4:
//                        cf = Flows.ResourceOwner;
//                        break;
//                    case 5:
//                        cf = Flows.Custom;
//                        break;
//                    case 6:
//                        cf = Flows.AuthorizationCodeWithProofKey;
//                        break;
//                    case 7:
//                        cf = Flows.HybridWithProofKey;
//                        break;

//                    default:
//                        cf = Flows.Implicit;
//                        break;

//                }

//                return cf;
//            }
//            set
//            {
//                switch (value)
//                {
//                    case Flows.AuthorizationCode:
//                        ClientFlow = 0;
//                        break;
//                    case Flows.Implicit:
//                        ClientFlow = 1;
//                        break;
//                    case Flows.Hybrid:
//                        ClientFlow = 2;
//                        break;
//                    case Flows.ClientCredentials:
//                        ClientFlow = 3;
//                        break;
//                    case Flows.ResourceOwner:
//                        ClientFlow = 4;
//                        break;
//                    case Flows.Custom:
//                        ClientFlow = 5;
//                        break;
//                    case Flows.AuthorizationCodeWithProofKey:
//                        ClientFlow = 6;
//                        break;
//                    case Flows.HybridWithProofKey:
//                        ClientFlow = 7;
//                        break;
//                    default:
//                        break;

//                }
//            }
//        }

//        [Required]
//        [Range(0, 7)]
//        public int ClientFlow { get; set; }
//        [Range(0, int.MaxValue)]
//        public int IdentityTokenLifetime { get; set; }
//        public bool EnableLocalLogin { get; set; }
//        public bool IncludeJwtId { get; set; }
//        public string LogoUri { get; set; }
//        public bool LogoutSessionRequired { get; set; }
//        public string LogoutUri { get; set; }
//        public bool RequireConsent { get; set; }
//        public string ClientUri { get; set; }
//        public bool PrefixClientClaims { get; set; }
//        public bool RequireSignOutPrompt { get; set; }
//        [Range(0, int.MaxValue)]
//        public int SlidingRefreshTokenLifetime { get; set; }
//        public bool UpdateAccessTokenOnRefresh { get; set; }

//        [Range(0, 1)]
//        public int ClientTokenExpiration { get; set; }
//        [JsonIgnore]
//        public TokenExpiration RefreshTokenExpiration
//        {
//            get
//            {
//                TokenExpiration te;
//                switch (this.ClientTokenExpiration)
//                {
//                    case 0:
//                        te = TokenExpiration.Sliding;
//                        break;
//                    case 1:
//                        te = TokenExpiration.Absolute;
//                        break;
//                    default:
//                        te = TokenExpiration.Absolute;
//                        break;
//                }
//                return te;
//            }
//            set
//            {
//                switch (value)
//                {
//                    case TokenExpiration.Sliding:
//                        ClientTokenExpiration = 0;
//                        break;
//                    case TokenExpiration.Absolute:
//                        ClientTokenExpiration = 1;
//                        break;
//                    default:
//                        break;

//                }
//            }
//        }


//        [Range(0, 1)]
//        public int ClientTokenUsage { get; set; }

//        [JsonIgnore]
//        public TokenUsage RefreshTokenUsage
//        {
//            get
//            {
//                TokenUsage tu;
//                switch (this.ClientTokenUsage)
//                {
//                    case 0:
//                        tu = TokenUsage.ReUse;
//                        break;
//                    case 1:
//                        tu = TokenUsage.OneTimeOnly;
//                        break;
//                    default:
//                        tu = TokenUsage.OneTimeOnly;
//                        break;
//                }
//                return tu;
//            }
//            set
//            {
//                switch (value)
//                {
//                    case TokenUsage.ReUse:
//                        ClientTokenUsage = 0;
//                        break;
//                    case TokenUsage.OneTimeOnly:
//                        ClientTokenUsage = 1;
//                        break;
//                    default:
//                        break;

//                }
//            }
//        }
//        public List<RedirectUri> RedirectUris { get; set; }
//        public List<PostLogoutRedirectUri> PostLogoutRedirectUris { get; set; }
//        public List<IdPRestriction> IdentityProviderRestrictions { get; set; }
//        public List<SecretModel> ClientSecrets { get; set; }
//        public List<ClientClaimModel> Claims { get; set; }
//        public List<ClientScopeModel> AllowedScopes { get; set; }
//        public List<CustomGrantType> AllowedCustomGrantTypes { get; set; }
//        public List<CorsOrigin> AllowedCorsOrigins { get; set; }
//    }


//    public class RedirectUri
//    {
//        public int Id { get; set; }
//        [Required]
//        public string Uri { get; set; }
//    }

//    public class PostLogoutRedirectUri
//    {
//        public int Id { get; set; }
//        [Required]
//        public string Uri { get; set; }
//    }

//    public class IdPRestriction
//    {
//        public int Id { get; set; }
//        [Required]
//        public string Provider { get; set; }
//    }

//    public class SecretModel
//    {
//        public SecretModel()
//        {
//            Type = "SharedSecret";

//        }
//        public int Id { get; set; }
//        [StringLength(2000)]
//        public string Description { get; set; }
//        public DateTimeOffset? Expiration { get; set; }
//        [StringLength(250)]
//        public string Type { get; set; }
//        [Required]
//        [StringLength(250)]
//        public string Value { get; set; }
//    }

//    public class ClientClaimModel
//    {
//        public virtual int Id { get; set; }
//        [Required]
//        [StringLength(250)]
//        public string Type { get; set; }
//        [Required]
//        [StringLength(250)]
//        public string Value { get; set; }
//    }

//    public class ClientScopeModel
//    {
//        public virtual int Id { get; set; }
//        [Required]
//        [StringLength(200)]
//        public string Scope { get; set; }
//    }

//    public class CustomGrantType
//    {
//        public virtual int Id { get; set; }
//        [Required]
//        [StringLength(250)]
//        public string GrantType { get; set; }

//    }

//    public class CorsOrigin
//    {
//        public int Id { get; set; }
//        [Required]
//        [StringLength(150)]
//        public string Origin { get; set; }
//    }

//    public class ClientUser
//    {
//        public string UserId { get; set; }
//        public int ClientKey { get; set; }
//    }

//    public class ClientDto
//    {
//        public int Id { get; set; }
//        public string ClientName { get; set; }
//        public string ClientId { get; set; }

//    }

//}
