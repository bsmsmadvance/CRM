using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using models = Database.Models;
using Identity.Services.JsonModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Database.Models;
using Identity.Params.Outputs;
using Microsoft.EntityFrameworkCore;

namespace Identity.Services
{
    public class UserSyncService : IUserSyncService
    {
        private readonly DatabaseContext DB;
        private readonly IConfiguration Configuration;

        public UserSyncService(IConfiguration configuration, DatabaseContext db)
        {
            this.Configuration = configuration;
            this.DB = db;
        }

        public async Task<UserSyncResponse> SyncUserDataAsync()
        {
            Console.WriteLine("Start Sync User Data from AP Authorize API.");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("api_key", Configuration["APAuthorizeAPI:Key"]);
            client.DefaultRequestHeaders.Add("api_token", Configuration["APAuthorizeAPI:Token"]);
            var requestUrl = $"{Configuration["APAuthorizeAPI:Host"]}/api/Authorize/AllEmpByApp?AppCode=crm";
            var response = await client.GetAsync(requestUrl);
            var data = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<List<EmployeeJsonModel>>(data);

            var addingUsers = new List<models.USR.User>();
            var updatingUsers = new List<models.USR.User>();
            var deletingUsers = new List<models.USR.User>();
            var allDBUsers = DB.Users.Where(o => !o.IsClient).ToList();

            foreach (var item in results)
            {
                var existingUser = allDBUsers.FirstOrDefault(o => o.EmployeeNo == item.EmployeeID);
                if (existingUser == null)
                {
                    existingUser = new models.USR.User();
                    item.ToUserModel(ref existingUser);
                    addingUsers.Add(existingUser);
                }
                else
                {
                    item.ToUserModel(ref existingUser);
                    updatingUsers.Add(existingUser);
                }
            }

            foreach (var item in allDBUsers)
            {
                var existingUser = results.FirstOrDefault(o => o.EmployeeID == item.EmployeeNo);
                if (existingUser == null)
                {
                    item.IsDeleted = true;
                    deletingUsers.Add(item);
                }
            }

            //create
            await DB.AddRangeAsync(addingUsers);
            //update
            DB.UpdateRange(updatingUsers);
            //delete
            DB.UpdateRange(deletingUsers);

            await DB.SaveChangesAsync();

            Console.WriteLine($"Create Users: {addingUsers.Count}");
            Console.WriteLine($"Update Users: {updatingUsers.Count}");
            Console.WriteLine($"Delete Users: {deletingUsers.Count}");

            Console.WriteLine("Finished.");
            return new UserSyncResponse()
            {
                Created = addingUsers.Count,
                Updated = updatingUsers.Count,
                Deleted = deletingUsers.Count
            };
        }

        /// <summary>
        /// ดึงข้อมูล Role ทั้งหมดจาก AP Authorize API (GetRoleInProject)
        /// </summary>
        /// <returns></returns>
        public async Task<RoleSyncResponse> SyncRoleDataAsync()
        {
            //TODO: [Palm] SyncRoleDataAsync
            throw new NotImplementedException();
        }

        /// <summary>
        /// ดึงข้อมูล Role ของ User แต่ละคนจาก AP Authorize API (GetUserProfile)
        /// </summary>
        /// <returns></returns>
        public async Task SyncRoleOfUserDataAsync()
        {
            var allUser = await DB.Users.ToListAsync();
            List<int> roleRefIdList = new List<int>();

            foreach (var user in allUser)
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("api_key", Configuration["APAuthorizeAPI:Key"]);
                client.DefaultRequestHeaders.Add("api_token", Configuration["APAuthorizeAPI:Token"]);

                var requestUrl = $"{Configuration["APAuthorizeAPI:Host"]}/api/Authorize/GetUserProfile/" + user.EmployeeNo +"/crm";
                var response = await client.GetAsync(requestUrl);
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    UserRoleJsonTmp unescapedJsonString = JsonConvert.DeserializeObject<UserRoleJsonTmp>(data);
                    UserProjectJsonTmp unescapedJsonProjectString = JsonConvert.DeserializeObject<UserProjectJsonTmp>(data);

                    UserRoleJsonModel userRoleJsonModel = new UserRoleJsonModel();
                    UserProjectJsonModel userProjectJsonModel = new UserProjectJsonModel();

                    if (!string.IsNullOrEmpty(unescapedJsonProjectString.UserProject))
                    {
                        userProjectJsonModel.UserProject = JsonConvert.DeserializeObject<UserProject[]>(unescapedJsonProjectString.UserProject);
                        List<Guid> userProjectList = new List<Guid>();

                        if (userProjectJsonModel.UserProject.Count() > 0)
                        {
                            foreach (var item in userProjectJsonModel.UserProject)
                            {
                                var project = await DB.Projects.Where(o => o.ProjectNo == item.ProjectCode).FirstOrDefaultAsync();
                                if (project != null)
                                {
                                    userProjectList.Add(project.ID);
                                    var isExistUserProject = await DB.UserAuthorizeProjects.Where(o => o.UserID == user.ID && o.ProjectID == project.ID).AnyAsync();
                                    if (!isExistUserProject)
                                    {
                                        models.USR.UserAuthorizeProject userAuthorizeProject = new models.USR.UserAuthorizeProject()
                                        {
                                            UserID = user.ID,
                                            ProjectID = project.ID
                                        };

                                        await DB.UserAuthorizeProjects.AddAsync(userAuthorizeProject);
                                    }
                                }
                            }

                            var removeUserProjectList = await DB.UserAuthorizeProjects.Where(o => !userProjectList.Contains(o.ProjectID.Value) && o.UserID == user.ID).ToListAsync();
                            foreach (var item in removeUserProjectList)
                            {
                                item.IsDeleted = true;
                                DB.Entry(item).State = EntityState.Modified;
                            }
                            await DB.SaveChangesAsync();
                        }
                    }

                    if (!string.IsNullOrEmpty(unescapedJsonString.SysUserRoles))
                    {
                        userRoleJsonModel.SysUserRoles = JsonConvert.DeserializeObject<SysUserRoles>(unescapedJsonString.SysUserRoles);
                        List<Guid> userRoleRefIdList = new List<Guid>();

                        foreach (var item in userRoleJsonModel.SysUserRoles.Roles)
                        {
                            var roleRefId = item.RoleId;
                            var roleModel = await DB.Roles.Where(o => o.RefID == roleRefId).FirstOrDefaultAsync();
                            if (roleModel == null)
                            {
                                models.USR.Role role = new models.USR.Role()
                                {
                                    RefID = roleRefId,
                                    Name = item.RoleName,
                                    Code = item.RoleCode
                                };

                                models.USR.UserRole userRole = new models.USR.UserRole()
                                {
                                    UserID = user.ID,
                                    RoleID = role.ID
                                };

                                await DB.Roles.AddAsync(role);
                                await DB.UserRoles.AddAsync(userRole);

                                var isExistUserRoleList = userRoleRefIdList.IndexOf(role.ID) != -1;
                                if (!isExistUserRoleList)
                                {
                                    userRoleRefIdList.Add(role.ID);
                                }
                            }
                            else
                            {
                                var isExistUserRole = await DB.UserRoles.Where(o => o.UserID == user.ID && o.RoleID == roleModel.ID).AnyAsync();
                                if (!isExistUserRole)
                                {
                                    models.USR.UserRole userRole = new models.USR.UserRole()
                                    {
                                        UserID = user.ID,
                                        RoleID = roleModel.ID
                                    };

                                    await DB.UserRoles.AddAsync(userRole);
                                }

                                var isExistUserRoleList = userRoleRefIdList.IndexOf(roleModel.ID) != -1;
                                if (!isExistUserRoleList)
                                {
                                    userRoleRefIdList.Add(roleModel.ID);
                                }
                            }

                            var isExistList = roleRefIdList.IndexOf(roleRefId) != -1;
                            if (!isExistList)
                            {
                                roleRefIdList.Add(roleRefId);
                            }
                        }

                        var removeUserRoleList = await DB.UserRoles.Where(o => !userRoleRefIdList.Contains(o.RoleID.Value) && o.UserID == user.ID).ToListAsync();
                        foreach (var item in removeUserRoleList)
                        {
                            item.IsDeleted = true;
                            DB.Entry(item).State = EntityState.Modified;
                        }
                        await DB.SaveChangesAsync();
                    }
                }
            }

            if(roleRefIdList.Count > 0)
            {
                var removeRoleList = await DB.Roles.Where(o => !roleRefIdList.Contains(o.RefID.Value)).ToListAsync();
                foreach (var item in removeRoleList)
                {
                    item.IsDeleted = true;
                    DB.Entry(item).State = EntityState.Modified;
                }
            }
            await DB.SaveChangesAsync();

        }
    }
}
