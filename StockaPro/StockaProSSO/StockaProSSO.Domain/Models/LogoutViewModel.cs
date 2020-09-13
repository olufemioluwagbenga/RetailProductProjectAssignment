using System;
using System.Collections.Generic;
using System.Text;

namespace StockaProSSO.Domain.Models
{
    public class LogoutViewModel : LogoutInputModel
    {
        public bool ShowLogoutPrompt { get; set; }
    }
}
