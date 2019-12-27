using Base.DTOs.SAL;
using PagingExtensions;
using Sale.Params.Filters;
using Sale.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Services
{
    public interface IUnitDocumentService
    {
        Task<UnitDocumentPaging> GetUnitDocumentDropdownListAsync(UnitDocumentFilter filter, PageParam pageParam, UnitDocumentSortByParam sortByParam);
    }
}
