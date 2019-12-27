using System;
using System.Collections.Generic;
using Base.DTOs.MST;
using PagingExtensions;

namespace MasterData.Params.Outputs
{
    public class SubBGPaging
    {
        public List<SubBGDTO> SubBGs { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
