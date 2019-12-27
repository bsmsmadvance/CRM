using System;
using System.Collections.Generic;
using Base.DTOs.CMS;
using PagingExtensions;

namespace Commission.Params.Outputs
{
    public class RateSettingTransferPaging
    {
        public List<RateSettingSaleTransferDTO> RateSettingTransfers { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
