using StockaProSSO.Core.Domain;
using StockaProSSO.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StockaProSSO.Core.Interfaces
{
    public interface IAuthorizeService
    {
        Task<Result<int>> Add(AuthorizeModel model);
        AdminAuthorize Get(int id);
        Task<IEnumerable<AdminAuthorize>> GetByUser(string userId);
        Task<IEnumerable<AdminAuthorize>> GetByClient(int clientId);
        Task<Result<int>> Toggle(ToggleAuthorizeModel model);
        bool HasClearance(List<Claim> newClaims, string userId, int clientKey);
        bool CheckClearance(string userId, int clientKey);
    }
}
