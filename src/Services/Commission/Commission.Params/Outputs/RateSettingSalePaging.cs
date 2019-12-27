using System;
using System.Collections.Generic;
using Base.DTOs.CMS;
using PagingExtensions;

namespace Commission.Params.Outputs
{
    public class RateSettingSalePaging
    {
        public List<RateSettingSaleTransferDTO> RateSettingSales { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
