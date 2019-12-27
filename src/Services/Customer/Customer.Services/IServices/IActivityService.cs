using Base.DTOs.CTM;
using Customer.Params.Filters;
using Customer.Params.Outputs;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Services.ContactServices
{
    public interface IActivityService
    {
        Task<ActivityPaging> GetActivityListAsync(ActivityFilter filter, PageParam pageParam, ActivityListSortByParam sortByParam);
        Task<UpdateOverdueResponse> UpdateActivityTaskOverdueAsync();
    }
}
