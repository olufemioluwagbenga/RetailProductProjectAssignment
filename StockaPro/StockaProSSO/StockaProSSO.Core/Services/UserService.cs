using StockaProSSO.Core.Domain;
using StockaProSSO.Core.Interfaces;
using StockaProSSO.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockaProSSO.Core.Services
{
    public class ClientService : Repository<UserClient, int>, IClientService
    {
        private AuthorizeService _authorizeService = new AuthorizeService(new StockaProContext());
        public ClientService(StockaProContext context) : base(context)
        {
        }
     //   private ClientConfigurationDbContext clientContext = new ClientConfigurationDbContext(AppSettings.DbConnection);

        public async Task<Result<int>> AddUserClient(string userId, int clientId)
        {
            try
            {
                var result = new Result<int>();
                var client = clientContext.Clients.FirstOrDefault(x => x.Id == clientId);
                if (client == null)
                {
                    result.Response = 0;
                    result.CustomValue = AppConstants.InvalidScope;
                    return result;
                }
                var userClient = new UserClient { UserId = userId, ClientId = clientId, TimeStamp = DateTime.Now };
                Insert(userClient);
                result.Response = await SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex);
                throw;
            }
        }



        public async Task<Result<int>> Add(ClientModel model)
        {
            try
            {
                var result = new Result<int>();
                var entity = model.ToModel();

                entity.ClientId = entity.ClientId.ToLower();
                var exists = clientContext.Clients.Where(x => x.ClientId == entity.ClientId).FirstOrDefault();
                if (exists != null)
                {
                    result.Exists = true;
                    return result;
                }

                var clientSecrets = entity.ClientSecrets;
                if (clientSecrets.Any())
                {
                    foreach (var item in clientSecrets)
                    {
                        item.Value = item.Value.Sha256();
                    }
                }

                entity.ClientSecrets = clientSecrets;
                clientContext.Clients.Add(entity);
                await clientContext.SaveChangesAsync();
                result.Response = entity.Id;
                result.CustomValue = entity.ClientName;

                return result;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex);
                throw;
            }

        }

        public Result<int> Update(ClientModel model, int id)
        {
            try
            {
                var result = new Result<int>();
                var existingClient = clientContext.Clients
                                    .Where(x => x.Id == id)
                                    .Include(x => x.AllowedScopes)
                                    .Include(x => x.ClientSecrets)
                                     .Include(x => x.RedirectUris)
                                     .Include(x => x.PostLogoutRedirectUris)
                                     .Include(x => x.AllowedCorsOrigins)
                                     .Include(x => x.IdentityProviderRestrictions)
                                     .Include(x => x.Claims)
                                     .Include(x => x.AllowedCustomGrantTypes)
                                     .SingleOrDefault();


                if (existingClient == null)
                {
                    result.Exists = false;
                    return result;
                }

                var duplicateClientId = clientContext.Clients
                                            .Where(x => x.ClientId == model.ClientId)
                                            .FirstOrDefault();

                if (duplicateClientId != null && duplicateClientId.Id != id)
                {
                    throw new Exception("pre-existing clientid");
                }

                clientContext.Entry(existingClient).CurrentValues.SetValues(model); // copy all values from object

                #region Update related properties


                var exRedirectUris = existingClient.RedirectUris.ToList();
                //delete redirect uri's that have been removed
                foreach (var item in exRedirectUris)
                {
                    if (!model.RedirectUris.Any(x => x.Id == item.Id))
                    {
                        existingClient.RedirectUris.Remove(item);
                    }
                }
                if (model.RedirectUris != null && model.RedirectUris.Any())
                {
                    //update and add new uris
                    foreach (var item in model.RedirectUris)
                    {
                        var exRedirectUri = existingClient.RedirectUris.FirstOrDefault(x => x.Id == item.Id);
                        if (exRedirectUri != null)
                        {
                            clientContext.Entry(exRedirectUri).CurrentValues.SetValues(item);
                        }
                        else
                        {
                            var redirectUri = new ClientRedirectUri
                            {
                                Uri = item.Uri,

                            };
                            existingClient.RedirectUris.Add(redirectUri);
                        }
                    }
                }


                //delete post redirect uri's that have been removed

                var exPostRedirectUris = existingClient.PostLogoutRedirectUris.ToList();
                foreach (var item in exPostRedirectUris)
                {
                    if (!model.PostLogoutRedirectUris.Any(x => x.Id == item.Id))
                    {
                        existingClient.PostLogoutRedirectUris.Remove(item);
                    }
                }

                if (model.PostLogoutRedirectUris != null && model.PostLogoutRedirectUris.Any())
                {
                    foreach (var item in model.PostLogoutRedirectUris)
                    {
                        var exPostRedirectUri = existingClient.PostLogoutRedirectUris.FirstOrDefault(x => x.Id == item.Id);
                        if (exPostRedirectUri != null)
                        {
                            clientContext.Entry(exPostRedirectUri).CurrentValues.SetValues(item);
                        }
                        else
                        {
                            var postRedirectUri = new ClientPostLogoutRedirectUri
                            {
                                Uri = item.Uri,

                            };
                            existingClient.PostLogoutRedirectUris.Add(postRedirectUri);
                        }
                    }
                }
                //update and add new post redirect uris




                var exIdProvRestrictions = existingClient.IdentityProviderRestrictions.ToList();
                foreach (var item in exIdProvRestrictions)
                {
                    if (!model.IdentityProviderRestrictions.Any(x => x.Id == item.Id))
                    {
                        existingClient.IdentityProviderRestrictions.Remove(item);
                    }
                }

                if (model.IdentityProviderRestrictions != null && model.IdentityProviderRestrictions.Any())
                {
                    foreach (var item in model.IdentityProviderRestrictions)
                    {
                        var exIdProvRestriction = existingClient.IdentityProviderRestrictions.FirstOrDefault(x => x.Id == item.Id);
                        if (exIdProvRestriction != null)
                        {
                            clientContext.Entry(exIdProvRestriction).CurrentValues.SetValues(item);
                        }
                        else
                        {
                            var IdentityProvRestriction = new ClientIdPRestriction
                            {
                                Provider = item.Provider,

                            };
                            existingClient.IdentityProviderRestrictions.Add(exIdProvRestriction);
                        }
                    }
                }
                //update and add new post redirect uris



                //delete post redirect uri's that have been removed


                //delete allowed scopes that have been removed

                var exScopes = existingClient.AllowedScopes.ToList();
                foreach (var item in exScopes)
                {
                    if (!model.AllowedScopes.Any(x => x.Id == item.Id))
                    {
                        existingClient.AllowedScopes.Remove(item);
                    }
                }

                if (model.AllowedScopes != null && model.AllowedScopes.Any())
                {
                    foreach (var item in model.AllowedScopes)
                    {
                        var exScope = existingClient.AllowedScopes.FirstOrDefault(x => x.Id == item.Id);
                        if (exScope != null)
                        {
                            clientContext.Entry(exScope).CurrentValues.SetValues(item);
                        }
                        else
                        {
                            var newScope = new ClientScope
                            {
                                Scope = item.Scope,

                            };
                            existingClient.AllowedScopes.Add(newScope);
                        }
                    }
                }





                //delete client claims that have been removed
                var exClaims = existingClient.Claims.ToList();
                foreach (var item in exClaims)
                {
                    if (!model.Claims.Any(x => x.Id == item.Id))
                    {
                        existingClient.Claims.Remove(item);
                    }
                }

                if (model.Claims != null && model.Claims.Any())
                {
                    //update and add new post redirect uris
                    foreach (var item in model.Claims)
                    {
                        var exClaim = existingClient.Claims.FirstOrDefault(x => x.Id == item.Id);
                        if (exClaim != null)
                        {
                            clientContext.Entry(exClaim).CurrentValues.SetValues(item);
                        }
                        else
                        {
                            var newClaim = new ClientClaim
                            {
                                Type = item.Type,
                                Value = item.Value

                            };
                            existingClient.Claims.Add(newClaim);
                        }
                    }
                }

                //delete client claims that have been removed

                var exAllowedCors = existingClient.AllowedCorsOrigins.ToList();
                foreach (var item in exAllowedCors)
                {
                    if (!model.AllowedCorsOrigins.Any(x => x.Id == item.Id))
                    {
                        existingClient.AllowedCorsOrigins.Remove(item);
                    }
                }

                if (model.AllowedCorsOrigins != null && model.AllowedCorsOrigins.Any())
                {
                    //update and add new post redirect uris
                    foreach (var item in model.AllowedCorsOrigins)
                    {
                        var exAllowedCor = existingClient.AllowedCorsOrigins.FirstOrDefault(x => x.Id == item.Id);
                        if (exAllowedCor != null)
                        {
                            clientContext.Entry(exAllowedCor).CurrentValues.SetValues(item);
                        }
                        else
                        {
                            var newClientCor = new ClientCorsOrigin
                            {
                                Origin = item.Origin
                            };
                            existingClient.AllowedCorsOrigins.Add(newClientCor);
                        }
                    }
                }







                //delete custom grant types that have been removed

                var exCustomGrants = existingClient.AllowedCustomGrantTypes.ToList();
                foreach (var item in exCustomGrants)
                {
                    if (!model.AllowedCustomGrantTypes.Any(x => x.Id == item.Id))
                    {
                        existingClient.AllowedCustomGrantTypes.Remove(item);
                    }
                }

                if (model.AllowedCustomGrantTypes != null && model.AllowedCustomGrantTypes.Any())
                {
                    //update and add new post redirect uris
                    foreach (var item in model.AllowedCustomGrantTypes)
                    {
                        var exCustomGrant = existingClient.AllowedCustomGrantTypes.FirstOrDefault(x => x.Id == item.Id);
                        if (exCustomGrant != null)
                        {
                            clientContext.Entry(exCustomGrant).CurrentValues.SetValues(item);
                        }
                        else
                        {
                            var newCustomGrant = new ClientCustomGrantType
                            {
                                GrantType = item.GrantType
                            };
                            existingClient.AllowedCustomGrantTypes.Add(newCustomGrant);
                        }
                    }
                }







                //delete custom grant types that have been removed

                var exSecrets = existingClient.ClientSecrets.ToList();
                foreach (var item in exSecrets)
                {
                    if (!model.ClientSecrets.Any(x => x.Id == item.Id))
                    {
                        existingClient.ClientSecrets.Remove(item);
                    }
                }

                if (model.ClientSecrets != null && model.ClientSecrets.Any())
                {
                    //update and add new post redirect uris
                    foreach (var item in model.ClientSecrets)
                    {
                        var exSecret = existingClient.ClientSecrets.FirstOrDefault(x => x.Id == item.Id);
                        if (exSecret != null)
                        {
                            exSecret.Value = exSecret.Value;
                            clientContext.Entry(exSecret).CurrentValues.SetValues(item);
                        }
                        else
                        {
                            var newSecret = new ClientSecret
                            {
                                Type = item.Type,
                                Value = item.Value.Sha256(),
                                Expiration = item.Expiration,
                                Description = item.Description,

                            };
                            existingClient.ClientSecrets.Add(newSecret);
                        }
                    }
                }


                #endregion


                clientContext.SaveChanges();
                result.Exists = true;
                result.Response = existingClient.Id;
                result.CustomValue = existingClient.ClientName;
                return result;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex);
                throw;
            }
        }

        public async Task<Result<ClientModel>> GetClient(int id)
        {
            var result = new Result<ClientModel>(new ClientModel());
            try
            {
                var entity = await clientContext.Clients
                                   .Where(x => x.Id == id)
                                   .Include(x => x.AllowedScopes)
                                   .Include(x => x.ClientSecrets)
                                    .Include(x => x.RedirectUris)
                                    .Include(x => x.PostLogoutRedirectUris)
                                    .Include(x => x.AllowedCorsOrigins)
                                    .Include(x => x.IdentityProviderRestrictions)
                                    .Include(x => x.Claims)
                                    .Include(x => x.AllowedCustomGrantTypes)
                                    .FirstOrDefaultAsync();

                if (entity == null)
                {
                    result.Exists = false;
                    return result;
                }
                var client = entity.ToClientModel();
                result.Response = client;
                result.Exists = true;
                return result; ;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex);
                throw;
            }


        }

        public async Task<ClientE> GetClient(string clientId = null, int? clientKey = null)
        {
            var result = new Result<ClientModel>(new ClientModel());
            try
            {
                var query = clientContext.Clients.AsQueryable();
                if (clientKey != null)
                {
                    query = query.Where(x => x.Id == clientKey);
                }
                else
                {
                    query = query.Where(x => x.ClientId == clientId);
                }

                var entity = query.FirstOrDefault();
                return entity;

                //var client = entity.ToClientModel();              
                //return client;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex);
                throw;
            }
        }


        public async Task<Result<IEnumerable<ClientDto>>> LookUpClients()
        {
            try
            {
                var result = new Result<IEnumerable<ClientDto>>(new List<ClientDto>());
                var query = clientContext.Clients.AsQueryable();
                result.Response = await query.Where(x => x.Enabled == true)
                                              .Select(x => new ClientDto { Id = x.Id, ClientName = x.ClientName, ClientId = x.ClientId })
                                              .OrderBy(x => x.ClientName).ToListAsync();
                result.TotalRecords = result.Response.Count();
                result.Exists = result.TotalRecords > 0 ? true : false;
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Result<IEnumerable<ClientModel>>> GetClients(string userId = null)
        {
            var result = new Result<IEnumerable<ClientModel>>(new List<ClientModel>());
            try
            {
                var query = clientContext.Clients.AsQueryable();

                if (userId != null)
                {
                    var clientIds = GetUserClients(userId);
                    query = query.Where(x => clientIds.Contains(x.Id));
                }

                var clientEntities = await query.Include(x => x.AllowedScopes)
                                   .Include(x => x.ClientSecrets)
                                    .Include(x => x.RedirectUris)
                                    .Include(x => x.PostLogoutRedirectUris)
                                    .Include(x => x.AllowedCorsOrigins)
                                    .Include(x => x.IdentityProviderRestrictions)
                                    .Include(x => x.Claims)
                                    .Include(x => x.AllowedCustomGrantTypes)
                                    .ToListAsync();


                var clients = clientEntities.ToClientModels();
                result.Response = clients.OrderBy(x => x.ClientName);

                result.TotalRecords = clientEntities.Count;
                result.Exists = result.TotalRecords > 0 ? true : false;
                return result;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex);
                throw;
            }

        }

        public bool IsValidClientId(string clientId)
        {
            try
            {
                clientId = clientId.Trim();
                var exists = clientContext.Clients.FirstOrDefault(x => x.ClientId == clientId);
                var isValid = (exists == null) ? false : true;
                return isValid;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex);
                throw;
            }
        }

        private List<int> GetUserClients(string userId)
        {
            try
            {
                var query = GetAll();
                query = query.Where(x => x.UserId == userId);
                return query.Select(x => x.ClientId).ToList();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(ex);
                throw;
            }
        }

        public async Task<List<AdminClearanceModel>> GetAdminClearance(string userId)
        {
            try
            {
                var clearanceLst = await _clearanceService.UserClearance(userId);
                if (clearanceLst == null)
                    return new List<AdminClearanceModel>();
                var clientIds = clearanceLst.Select(x => x.ClientKey).ToList();
                var query = clientContext.Clients.Where(x => clientIds.Contains(x.Id));
                var res = await query.Select(x => new AdminClearanceModel { ClientKey = x.Id, ClientName = x.ClientName, ClientId = x.ClientId, }).ToListAsync();
                foreach (var item in clearanceLst)
                {
                    var clearance = res.First(x => x.ClientKey == item.ClientKey);
                    clearance.ClearanceId = item.Id;
                    clearance.IsEnabled = item.Enabled;
                    clearance.CreatedDate = item.CreatedDate;
                    clearance.LastUpdate = item.UpdatedAt;
                }

                return res;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}