using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockaProSSO.Core.Domain
{
    public class Role : IdentityRole
    {
        public Role() : base() { }

        public Role(string name) : base(name) { }
    }
}
