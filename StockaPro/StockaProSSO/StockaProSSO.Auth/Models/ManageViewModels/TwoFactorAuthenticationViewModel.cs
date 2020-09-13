using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockaProSSO.Auth.Models.ManageViewModels
{
      public class TwoFactorAuthenticationViewModel
    {
        public bool HasAuthenticator { get; set; }

        public int RecoveryCodesLeft { get; set; }

        public bool Is2faEnabled { get; set; }
    }
}
