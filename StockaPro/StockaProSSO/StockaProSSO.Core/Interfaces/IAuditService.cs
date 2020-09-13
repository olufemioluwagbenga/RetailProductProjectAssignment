using StockaProSSO.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockaProSSO.Core.Interfaces
{
    public interface IAuditService
    {
        Task<long> Save(Audit entity);
        Audit Get(long Id);
        Result<IEnumerable<Audit>> Audits(DateTime? startDate, DateTime? endDate, int skip, int pageSize, string action, int? sectionId);
        Task NewUser(string userName, CurrentUser currentUser, string ip);
        Task ConfirmAccount(string userName, string ip);
        Task AddClaims(string userName, CurrentUser currentUser, string ip, IEnumerable<ClaimModel> claims);
        Task RemoveClaims(string userName, CurrentUser currentUser, string ip, IEnumerable<ClaimModel> claims);
        Task ResetPassword(string userId, string createdBy, string ip);
        Task ForgotPassword(string userId, string createdBy, string ip);
        Task CreateScope(string scopeName, CurrentUser currentUser, string ip);
        Task UpdateScope(string scopeName, CurrentUser currentUser, string ip);
        Task CreateClient(string clientName, CurrentUser currentUser, string ip);
        Task UpdateClient(string clientName, CurrentUser currentUser, string ip);
        Task UnlockAccount(string userName, CurrentUser currentUser, string ip);
        Task LockAccount(string userName, CurrentUser currentUser, string ip);
        Task Enable2fa(string userName, CurrentUser currentUser, string ip);
        Task Disable2fa(string userName, CurrentUser currentUser, string ip);
        Task ChangePassword(string userName, CurrentUser currentUser, string ip);
        Task UpdateUser(string userName, CurrentUser currentUser, string ip);
        Task AddClearance(CurrentUser currentUser, string ip, string clientName, string affectedUser);
        Task ToggleClearance(CurrentUser currentUser, string ip, string clientName, string affectedUser, bool toggle);
        Task<Result<IEnumerable<Audit>>> TopTen();
        string FormatAudit(Audit model);
        Task EnableGoogleAuth(User currentUser, string ip);
    }
}
