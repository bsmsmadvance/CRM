using System;
using System.Collections.Generic;
using Base.DTOs.CMS;
using PagingExtensions;

namespace Commission.Params.Outputs
{
    public class CommissionHighRiseTransferVeiwPaging
    {
        //public List<CommissionHighRiseTransferVeiwDTO> CommissionHighRiseTransferVeiws { get; set; }
        public PageOutput PageOutput { get; set; }
        public CalculatePerMonthHighRiseTransferDTO CalculatePerMonthHighRiseTransfer { get; set; }
    }
}
