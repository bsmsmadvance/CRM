using System;
using System.Collections.Generic;
using Base.DTOs.MST;
using PagingExtensions;

namespace MasterData.Params.Outputs
{
    public class BGPaging
    {
        public List<BGDTO> BGs { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
