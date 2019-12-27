using System;
using System.Collections.Generic;
using Base.DTOs.MST;
using PagingExtensions;

namespace MasterData.Params.Outputs
{
    public class EDCPaging
    {
        public List<EDCDTO> EDCs { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
