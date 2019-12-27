using System;
using System.Collections.Generic;
using Base.DTOs.CMS;
using PagingExtensions;

namespace Commission.Params.Outputs
{
    public class RateSettingFixSaleModelPaging
    {
        public List<RateSettingFixSaleTransferModelDTO> RateSettingFixSaleModels { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
