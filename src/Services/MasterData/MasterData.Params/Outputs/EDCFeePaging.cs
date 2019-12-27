using System;
using System.Collections.Generic;
using Base.DTOs.MST;
using PagingExtensions;

namespace MasterData.Params.Outputs
{
    public class EDCFeePaging
    {
        public List<EDCFeeDTO> EDCFees { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
