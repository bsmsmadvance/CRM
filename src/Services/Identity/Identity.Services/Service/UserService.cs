using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.DTOs.USR;
using Database.Models;
using Identity.Params.Filters;
using Identity.Params.Outputs;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;

namespace Identity.Services
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext DB;

        public UserService(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task<UserPaging> GetUserListAsync(UserFilter filter, PageParam pageParam, UserListSortByParam sortByParam)
        {
            IQueryable<UserQueryResult> query = DB.Users
                .Include(o => o.UserRoles)
                .Include(o => o.UserAuthorizeProjects)
                .Select(o => new UserQueryResult
                {
                    User = o
                });

            #region Filter
            if (!string.IsNullOrEmpty(filter.FirstName))
                query = query.Where(q => q.User.FirstName.Contains(filter.FirstName));

            if (!string.IsNullOrEmpty(filter.LastName))
                query = query.Where(q => q.User.LastName.Contains(filter.LastName));

            if (!string.IsNullOrEmpty(filter.EmployeeNo))
                query = query.Where(q => q.User.EmployeeNo.Contains(filter.EmployeeNo));
            if (!string.IsNullOrEmpty(filter.DisplayName))
                query = query.Where(q => q.User.DisplayName.Contains(filter.DisplayName));
            if (!string.IsNullOrEmpty(filter.RoleCodes))
            {
                var roleCodes = filter.RoleCodes.Split(',');
                query = query.Where(o => o.User.UserRoles.Where(m => roleCodes.Contains(m.Role.Code)).Any());
            }
            if (!string.IsNullOrEmpty(filter.AuthorizeProjectIDs))
            {
                var projectIDs = filter.AuthorizeProjectIDs.Split(',').Select(o => Guid.Parse(o)).ToList();
                query = query.Where(o => o.User.UserAuthorizeProjects.Where(m => projectIDs.Contains(m.ProjectID.Value)).Any());
            }
            #endregion

            UserListDTO.SortBy(sortByParam, ref query);
            var pageOutput = PagingHelper.Paging<UserQueryResult>(pageParam, ref query);

            var queryResults = await query.ToListAsync();

            var result = queryResults.Select(o => UserListDTO.CreateFromQueryResult(o)).ToList();

            return new UserPaging()
            {
                PageOutput = pageOutput,
                Users = result
            };
        }

        public async Task<List<UserListDTO>> GetUserDropdownListAsync(string name)
        {
            var query = DB.Users.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.FirstName.Contains(name) || o.LastName.Contains(name));
            }

            var results = await query.Take(100).Select(o => UserListDTO.CreateFromModel(o)).ToListAsync();
            return results;
        }
    }
}
