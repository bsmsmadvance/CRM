using Base.DTOs.USR;
using Identity.Params.Filters;
using Identity.Params.Outputs;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services
{
    public interface IUserService
    {
        Task<UserPaging> GetUserListAsync(UserFilter filter, PageParam pageParam, UserListSortByParam sortByParam);
        Task<List<UserListDTO>> GetUserDropdownListAsync(string name);
    }
}
