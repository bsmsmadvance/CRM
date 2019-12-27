using System;
using System.Collections.Generic;
using Base.DTOs.CMS;
using PagingExtensions;

namespace Commission.Params.Outputs
{
    public class CommissionLowRiseVeiwPaging
    {
        //public List<CommissionLowRiseVeiwDTO> CommissionLowRiseVeiws { get; set; }
        public PageOutput PageOutput { get; set; }
        public CalculatePerMonthLowRiseDTO CalculatePerMonthLowRise { get; set; }
    }
}
