using System;
using System.Collections.Generic;
using Base.DTOs.CMS;
using PagingExtensions;

namespace Commission.Params.Outputs
{
    public class CommissionHighRiseSaleVeiwPaging
    {
        //public List<CommissionHighRiseSaleVeiwDTO> CommissionHighRiseSaleVeiws { get; set; }
        public PageOutput PageOutput { get; set; }
        public CalculatePerMonthHighRiseSaleDTO CalculatePerMonthHighRiseSale { get; set; }
    }
}
