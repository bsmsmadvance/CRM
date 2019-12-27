using System;
using System.Collections.Generic;
using Base.DTOs.MST;
using PagingExtensions;

namespace MasterData.Params.Outputs
{
    public class ProvincePaging
    {
        public List<ProvinceDTO> Provinces { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
