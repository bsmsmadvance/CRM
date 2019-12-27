using System;
using System.Collections.Generic;
using Base.DTOs.MST;
using PagingExtensions;

namespace MasterData.Params.Outputs
{
    public class SubDistrictPaging
    {
        public List<SubDistrictDTO> SubDistricts { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
