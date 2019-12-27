using System;
using System.Collections.Generic;
using Base.DTOs.CMS;
using PagingExtensions;

namespace Commission.Params.Outputs
{
    public class RateSettingFixTransferModelPaging
    {
        public List<RateSettingFixSaleTransferModelDTO> RateSettingFixTransferModels { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
