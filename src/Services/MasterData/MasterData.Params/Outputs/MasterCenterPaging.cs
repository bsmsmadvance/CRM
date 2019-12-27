using System;
using System.Collections.Generic;
using Base.DTOs.MST;
using PagingExtensions;

namespace MasterData.Params.Outputs
{
    public class MasterCenterPaging
    {
        public List<MasterCenterDTO> MasterCenters { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
