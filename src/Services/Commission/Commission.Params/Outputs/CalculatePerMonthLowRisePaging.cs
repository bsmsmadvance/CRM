using System;
using System.Collections.Generic;
using Base.DTOs.CMS;
using PagingExtensions;

namespace Commission.Params.Outputs
{
    public class CalculatePerMonthLowRisePaging
    {
        public List<CalculatePerMonthLowRiseDTO> CalculatePerMonthLowRises { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
