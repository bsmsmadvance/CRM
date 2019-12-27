using System;
using System.Collections.Generic;
using Base.DTOs.MST;
using PagingExtensions;

namespace MasterData.Params.Outputs
{
    public class DistrictPaging
    {
        public List<DistrictDTO> Districts { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
