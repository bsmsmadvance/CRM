using System;
using System.Collections.Generic;
using Base.DTOs.CMS;
using PagingExtensions;

namespace Commission.Params.Outputs
{
    public class CommissionSettingPaging
    {
        public List<CommissionSettingDTO> CommissionSettings { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
