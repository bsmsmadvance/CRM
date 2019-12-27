using System;
using System.Collections.Generic;
using Base.DTOs.MST;
using PagingExtensions;

namespace MasterData.Params.Outputs
{
    public class CompanyPaging
    {
        public List<CompanyDTO> Companies { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
