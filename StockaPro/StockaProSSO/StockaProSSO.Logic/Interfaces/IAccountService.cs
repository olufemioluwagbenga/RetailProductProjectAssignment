using StockaProSSO.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockaProSSO.Logic.Interfaces
{
    public interface IAccountService
    {
        Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId);
        Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId);
    }
}
