using System;
using System.Collections.Generic;
using Base.DTOs.MST;
using PagingExtensions;

namespace MasterData.Params.Outputs
{
    public class LandOfficePaging
    {
        public List<LandOfficeDTO> LandOffices { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
