using System;
using System.Collections.Generic;
using Base.DTOs.CMS;
using PagingExtensions;

namespace Commission.Params.Outputs
{
    public class RateSettingFixSalePaging
    {
        public List<RateSettingFixSaleTransferDTO> RateSettingFixSales { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
