using Microsoft.EntityFrameworkCore;
using StockaProSSO.Core.Domain;
using StockaProSSO.Core.Domain.Entities;
using StockaProSSO.Core.Interfaces;
using StockaProSSO.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StockaProSSO.Core.Services
{
 
    public class AuthorizeService : Repository<AdminAuthorize, int>, IAuthorizeService
    {
        public AuthorizeService(StockaProContext context) : base(context)
        {
        }
        public AdminAuthorize Get(int id)
        {
            try
            {
                return Find(id);
            }
            catch (Exception ex)
            {
                throw;
            }
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AdminAuthorize>> GetByClient(int clientId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AdminAuthorize>> GetByUser(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<int>> Add(AuthorizeModel model)
        {
            var result = new Result<int>();
            try
            {

                foreach (var cKey in model.ClientKeys)
                {
                    var entity = new AdminAuthorize {
                        UserId = model.Userid,
                     CreatedDate = model.CreatedDate,
                     UpdatedAt = model.UpdatedAt,
                     Enabled = model.Enabled
                    };
                    entity.ClientKey = cKey;
                    Insert(entity);
                }


                result.Response = await SaveChangesAsync();
                return result;
            }

            catch (DbUpdateException dbUpEx)
            {
                result.CustomValue = dbUpEx.InnerException != null ? dbUpEx.InnerException.InnerException.Message : string.Empty;
                result.Response = 0;
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Result<int>> Toggle(ToggleAuthorizeModel model)
        {
            var result = new Result<int>();


            try
            {
                var clearance = Find(model.Id);
                if (clearance == null)
                {
                    result.Exists = false;
                    result.CustomValue = "invalid clearance";
                    return result;
                }

                result.Exists = true;
                clearance.UpdatedAt = DateTime.Now;
                clearance.Enabled = model.Enabled;


                Update(clearance);
                result.Response = await SaveChangesAsync();
                return result;
            }

            catch (DbUpdateException dbUpEx)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal async Task<List<AdminAuthorize>> UserClearance(string userId)
        {
            var result = new Result<List<AdminAuthorize>>();
            try
            {
                var query = GetAll();
                query = query.Where(x => x.UserId == userId);
                result.Response = await query.ToListAsync();
                result.TotalRecords = result.Response.Count;
                return result.Response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool HasClearance(List<Claim> newClaims, string userId, int clientKey)
        {
            try
            {
                //check is claims to be added include one of the  vgg admin roles
                var adminRoles = AppEnums.ApplicationRolesList();
                int vggAdminRoleCount = 0;
                foreach (var item in newClaims)
                {
                    if (adminRoles.Contains(item.Value, StringComparer.OrdinalIgnoreCase))
                        vggAdminRoleCount++;

                }
                //vgg admin roles are not a part of the claims to  be added so clearance check is not necessary
                if (vggAdminRoleCount == 0)
                {
                    return true;
                }

                return CheckClearance(userId, clientKey);
               
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool CheckClearance(string userId, int clientKey)
        {
            try
            {
                var clearance = GetAll().Where(x => x.UserId == userId && x.ClientKey == clientKey).FirstOrDefault();
                if (clearance == null)
                {
                    return false;
                }
                else if (!clearance.Enabled)
                {
                    return false;
                }
                else
                {
                    return true;  //user has clearance
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}