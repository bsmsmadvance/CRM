using System;
using System.Collections.Generic;
using Base.DTOs.CMS;
using PagingExtensions;

namespace Commission.Params.Outputs
{
    public class CalculatePerMonthHighRiseSalePaging
    {
        public List<CalculatePerMonthHighRiseSaleDTO> CalculatePerMonthHighRiseSales { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
