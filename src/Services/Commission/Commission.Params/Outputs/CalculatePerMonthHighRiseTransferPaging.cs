using System;
using System.Collections.Generic;
using Base.DTOs.CMS;
using PagingExtensions;

namespace Commission.Params.Outputs
{
    public class CalculatePerMonthHighRiseTransferPaging
    {
        public List<CalculatePerMonthHighRiseTransferDTO> CalculatePerMonthHighRiseTransfers { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
