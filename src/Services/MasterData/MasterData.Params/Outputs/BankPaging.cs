using System;
using System.Collections.Generic;
using Base.DTOs.MST;
using PagingExtensions;

namespace MasterData.Params.Outputs
{
    public class BankPaging
    {
        public List<BankDTO> Banks { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
