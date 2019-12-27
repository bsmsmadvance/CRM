using System;
using System.Collections.Generic;
using Base.DTOs.CMS;
using PagingExtensions;

namespace Commission.Params.Outputs
{
    public class ChangeLCTransferPaging
    {
        public List<ChangeLCTransferDTO> ChangeLCTransfers { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
