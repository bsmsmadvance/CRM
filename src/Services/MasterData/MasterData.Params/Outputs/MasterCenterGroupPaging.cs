using System;
using System.Collections.Generic;
using Base.DTOs.MST;
using PagingExtensions;

namespace MasterData.Params.Outputs
{
    public class MasterCenterGroupPaging
    {
        public List<MasterCenterGroupDTO> MasterCenterGroups { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
