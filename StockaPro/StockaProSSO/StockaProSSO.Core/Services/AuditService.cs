using Microsoft.EntityFrameworkCore;
using StockaProSSO.Core.Domain;
using StockaProSSO.Core.Interfaces;
using StockaProSSO.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockaProSSO.Core.Services
{
    public class AuditService : Repository<Audit, long>, IAuditService
    {
        //  private EmailService _mailService = new EmailService();
        public AuditService(StockaProContext context) : base(context)
        {

        }

        public async Task<long> Save(Audit entity)
        {
            try
            {
                Insert(entity);
                return await SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Audit Get(long Id)
        {
            try
            {
                return Find(Id);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public Result<IEnumerable<Audit>> Audits(DateTime? startDate, DateTime? endDate, int skip, int pageSize, string action, int? sectionId)
        {
            try
            {
                var result = new Result<IEnumerable<Audit>>(new List<Audit>());
                if (startDate == null || endDate == null)
                {
                    startDate = DateTime.Now;
                    endDate = DateTime.Now;

                }
                endDate = endDate.Value.AddHours(23).AddMinutes(59).AddSeconds(59);
                var query = GetAll();
                if (action != null)
                {
                    query = query.Where(x => x.Action == action);
                }

                if (sectionId != null)
                {
                    query = query.Where(x => x.SectionId == sectionId);
                }

                query = query.Where(x => x.TimeStamp >= startDate && x.TimeStamp <= endDate);
                result.TotalRecords = query.LongCount();
                query = query.OrderByDescending(x => x.TimeStamp)
                             .Skip(skip)
                             .Take(pageSize);

                result.Response = query.ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task NewUser(string userName, CurrentUser currentUser, string ip)
        {
            try
            {
                var audit = new Audit
                {
                    Action = AppConstants.UserRegistration,
                    Description = string.Format("{0} created user with username {1}", currentUser.UserName, userName),
                    Ip = ip,
                    TimeStamp = DateTime.Now,
                    CreatedBy = currentUser.FullName,
                    SectionId = Convert.ToInt32(AuditSections.Account)

                };
                await Save(audit);

            }
            catch (Exception ex)
            {
            }
        }

        public async Task ConfirmAccount(string userName, string ip)
        {
            try
            {
                var audit = new Audit
                {
                    Action = AppConstants.AccountConfimation,
                    Description = string.Format("user {0} confirmed email account", userName),
                    Ip = ip,
                    TimeStamp = DateTime.Now,
                    CreatedBy = userName,

                    SectionId = Convert.ToInt32(AuditSections.Account)

                };
                await Save(audit);
            }
            catch (Exception ex)
            {
            }
        }

        public async Task AddClaims(string userName, CurrentUser currentUser, string ip, IEnumerable<ClaimModel> claims)
        {
            try
            {
                var claimsList = claims.Select(x => x.Value).ToList();
                var claimsString = string.Join(",", claimsList);
                var audit = new Audit
                {
                    Action = AppConstants.AddClaim,
                    Description = string.Format("user {0} added claim(s) {1} to user with username {2}",
                                                  currentUser.UserName, claimsString, userName),
                    Ip = ip,
                    TimeStamp = DateTime.Now,
                    CreatedBy = currentUser.FullName,
                    SectionId = Convert.ToInt32(AuditSections.Account)

                };
                await Save(audit);
                //  
            }
            catch (Exception ex)
            {
            }
        }

        public async Task RemoveClaims(string userName, CurrentUser currentUser, string ip, IEnumerable<ClaimModel> claims)
        {
            try
            {
                var claimsList = claims.Select(x => x.Value).ToList();
                var claimsString = string.Join(",", claimsList);
                var audit = new Audit
                {
                    Action = AppConstants.RemoveClaim,
                    Description = string.Format("user {0} removed claim(s) {1} of user with username {2}", currentUser.UserName, claimsString, userName),
                    Ip = ip,
                    TimeStamp = DateTime.Now,
                    CreatedBy = currentUser.FullName,
                    SectionId = Convert.ToInt32(AuditSections.Account)

                };
                await Save(audit);
                //
            }
            catch (Exception ex)
            {

            }
        }


        public async Task ResetPassword(string userId, string createdBy, string ip)
        {
            try
            {

                var audit = new Audit
                {
                    Action = AppConstants.ResetPassword,
                    Description = string.Format("user with id {0} performed a password reset", userId),
                    Ip = ip,
                    TimeStamp = DateTime.Now,
                    CreatedBy = createdBy,
                    SectionId = Convert.ToInt32(AuditSections.Account)

                };
                await Save(audit);
                //  
            }
            catch (Exception ex)
            {

            }
        }


        public async Task ForgotPassword(string userId, string createdBy, string ip)
        {
            try
            {

                var audit = new Audit
                {
                    Action = AppConstants.ForgotPassword,
                    Description = string.Format("user with id {0} made a forgot password request ", userId),
                    Ip = ip,
                    TimeStamp = DateTime.Now,
                    CreatedBy = createdBy,
                    SectionId = Convert.ToInt32(AuditSections.Account)

                };
                await Save(audit);
                // 
            }
            catch (Exception ex)
            {
            }
        }

        public async Task CreateScope(string scopeName, CurrentUser currentUser, string ip)
        {
            try
            {

                var audit = new Audit
                {
                    Action = AppConstants.CreateScope,
                    Description = string.Format("user {0}  created scope with name {1} ", currentUser.UserName, scopeName),
                    Ip = ip,
                    TimeStamp = DateTime.Now,
                    CreatedBy = currentUser.FullName,
                    SectionId = Convert.ToInt32(AuditSections.Scope)

                };
                await Save(audit);

            }
            catch (Exception ex)
            {

            }
        }

        public async Task UpdateScope(string scopeName, CurrentUser currentUser, string ip)
        {
            try
            {

                var audit = new Audit
                {
                    Action = AppConstants.UpdateScope,
                    Description = string.Format("user {0} updated scope with name {1} ", currentUser.UserName, scopeName),
                    Ip = ip,
                    TimeStamp = DateTime.Now,
                    CreatedBy = currentUser.FullName,
                    SectionId = Convert.ToInt32(AuditSections.Scope)

                };
                await Save(audit);

            }
            catch (Exception ex)
            {

            }
        }

        public async Task CreateClient(string clientName, CurrentUser currentUser, string ip)
        {
            try
            {

                var audit = new Audit
                {
                    Action = AppConstants.CreateClient,
                    Description = string.Format("user {0} created client with name {1}  ", currentUser.UserName, clientName),
                    Ip = ip,
                    TimeStamp = DateTime.Now,
                    CreatedBy = currentUser.FullName,
                    SectionId = Convert.ToInt32(AuditSections.Client)

                };



                await Save(audit);


            }
            catch (Exception ex)
            {

            }
        }

        public async Task UpdateClient(string clientName, CurrentUser currentUser, string ip)
        {
            try
            {

                var audit = new Audit
                {
                    Action = AppConstants.UpdateClient,
                    Description = string.Format("user {0} updated client with name {1} ", currentUser.UserName, clientName),
                    Ip = ip,
                    TimeStamp = DateTime.Now,
                    CreatedBy = currentUser.FullName,
                    SectionId = Convert.ToInt32(AuditSections.Client)

                };
                await Save(audit);

            }
            catch (Exception ex)
            {

            }
        }

        public async Task LockAccount(string userName, CurrentUser currentUser, string ip)
        {
            try
            {

                var audit = new Audit
                {
                    Action = AppConstants.LockAccount,
                    Description = string.Format("user {0} locked user account with username {1} ", currentUser.UserName, userName),
                    Ip = ip,
                    TimeStamp = DateTime.Now,
                    CreatedBy = currentUser.FullName,
                    SectionId = Convert.ToInt32(AuditSections.Account)

                };
                await Save(audit);

            }
            catch (Exception ex)
            {

            }
        }

        public async Task UnlockAccount(string userName, CurrentUser currentUser, string ip)
        {
            try
            {

                var audit = new Audit
                {
                    Action = AppConstants.UnlockAccount,
                    Description = string.Format("user {0} unlocked user account with username {1} ", currentUser.UserName, userName),
                    Ip = ip,
                    TimeStamp = DateTime.Now,
                    CreatedBy = currentUser.FullName,
                    SectionId = Convert.ToInt32(AuditSections.Account)

                };
                await Save(audit);

            }
            catch (Exception ex)
            {

            }
        }

        public async Task Enable2fa(string userName, CurrentUser currentUser, string ip)
        {
            try
            {

                var audit = new Audit
                {
                    Action = AppConstants.EnableTwoFactor,
                    Description = string.Format("user {0} enabled two factor authentication for user with username {1} ", currentUser.UserName, userName),
                    Ip = ip,
                    TimeStamp = DateTime.Now,
                    CreatedBy = currentUser.FullName,
                    SectionId = Convert.ToInt32(AuditSections.Account)

                };
                await Save(audit);

            }
            catch (Exception ex)
            {

            }
        }

        public async Task Disable2fa(string userName, CurrentUser currentUser, string ip)
        {
            try
            {

                var audit = new Audit
                {
                    Action = AppConstants.DisableTwoFactor,
                    Description = string.Format("user {0} disabled two factor authentication on account with username {1} ", currentUser.UserName, userName),
                    Ip = ip,
                    TimeStamp = DateTime.Now,
                    CreatedBy = currentUser.FullName,
                    SectionId = Convert.ToInt32(AuditSections.Account)

                };
                await Save(audit);

            }
            catch (Exception ex)
            {

            }
        }

        public async Task ChangePassword(string userName, CurrentUser currentUser, string ip)
        {

            try
            {

                var audit = new Audit
                {
                    Action = AppConstants.ChangePassword,
                    Description = string.Format("user {0} changed password of account with username {1} ", currentUser.FullName, userName),
                    Ip = ip,
                    TimeStamp = DateTime.Now,
                    CreatedBy = currentUser.FullName,
                    SectionId = Convert.ToInt32(AuditSections.Account)

                };
                await Save(audit);

            }
            catch (Exception ex)
            {

            }
        }

        public async Task UpdateUser(string userName, CurrentUser currentUser, string ip)
        {
            try
            {

                var audit = new Audit
                {
                    Action = AppConstants.UpdateUser,
                    Description = string.Format("user {0} updated user account with username {1} ", currentUser.UserName, userName),
                    Ip = ip,
                    TimeStamp = DateTime.Now,
                    CreatedBy = currentUser.FullName,
                    SectionId = Convert.ToInt32(AuditSections.Account)

                };
                await Save(audit);

            }
            catch (Exception ex)
            {

            }
        }

        public async Task Login(string userName, string createdBy, string ip, string clientId)
        {
            try
            {

                var audit = new Audit
                {
                    Action = AppConstants.Login,
                    Description = string.Format("user {0} signed in to {1} ", userName, clientId != null ? clientId : string.Empty),
                    Ip = ip,
                    TimeStamp = DateTime.Now,
                    CreatedBy = createdBy,
                    SectionId = Convert.ToInt32(AuditSections.Account)

                };
                await Save(audit);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task Logout(string userName, string createdBy, string ip, string clientId)
        {
            try
            {

                var audit = new Audit
                {
                    Action = AppConstants.Logout,
                    Description = string.Format("user {0} signed out from {1}", userName, clientId != null ? clientId : string.Empty),
                    Ip = ip,
                    TimeStamp = DateTime.Now,
                    CreatedBy = createdBy,
                    SectionId = Convert.ToInt32(AuditSections.Account)

                };
                await Save(audit);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task AddClearance(CurrentUser currentUser, string ip, string clientNames, string affectedUser)
        {
            try
            {

                var audit = new Audit
                {
                    Action = AppConstants.AddClearance,
                    Description = string.Format("user {0} added clearance for user {1}. clearance applied to client(s) {2}", currentUser.UserName, affectedUser, clientNames != null ? clientNames : string.Empty),
                    Ip = ip,
                    TimeStamp = DateTime.Now,
                    CreatedBy = currentUser.FullName,
                    SectionId = Convert.ToInt32(AuditSections.Account)

                };
                await Save(audit);

            }
            catch (Exception ex)
            {

            }
        }

        public async Task ToggleClearance(CurrentUser currentUser, string ip, string clientName, string affectedUser, bool toggle)
        {
            try
            {
                string action = String.Empty;
                string state = string.Empty;
                if (toggle)
                {
                    action = AppConstants.EnableClearance;
                    state = "enabled";
                }
                else
                {
                    action = AppConstants.DisableClearance;
                    state = "disabled";
                }
                var audit = new Audit
                {
                    Action = action,
                    Description = string.Format("user {0} {1} clearance for user {2}. action applied to client {3}", currentUser.UserName, state, affectedUser, clientName != null ? clientName : string.Empty),
                    Ip = ip,
                    TimeStamp = DateTime.Now,
                    CreatedBy = currentUser.FullName,
                    SectionId = Convert.ToInt32(AuditSections.Account)

                };
                await Save(audit);

            }
            catch (Exception ex)
            {

            }
        }

        public async Task<Result<IEnumerable<Audit>>> TopTen()
        {
            try
            {
                var result = new Result<IEnumerable<Audit>>(new List<Audit>());
                var query = GetAll();

                result.TotalRecords = query.LongCount();
                query = query.OrderByDescending(x => x.TimeStamp)
                             .Take(10);

                result.Response = await query.ToListAsync();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string FormatAudit(Audit model)
        {
            string message = "<p><strong>Action:</strong> " + model.Action + "</p><p><strong>Description:</strong> " + model.Description +
                                   "</p><p><strong> Ip: </strong> " + model.Ip + " </p><p><strong>Created By:</strong> " + model.CreatedBy + "</p><p><strong>Time: </strong> " + model.TimeStamp.ToLongDateString() + "</p>" +
                                   "</p><p><strong>Environment: </strong>"; // + AppConstants.PublicOrigin + "</p>";

            return message;

        }

        public async Task EnableGoogleAuth(User user, string ip)
        {
            try
            {
                var audit = new Audit
                {
                    Action = AppConstants.EnableGoogleAuth,
                    Description = string.Format("user {0} enabled google authentication", user.UserName),
                    Ip = ip,
                    TimeStamp = DateTime.Now,
                    CreatedBy = user.FirstName + ", " + user.LastName,
                    SectionId = Convert.ToInt32(AuditSections.Account)

                };
                await Save(audit);
            }
            catch (Exception ex)
            {


            }

        }
    }
}
