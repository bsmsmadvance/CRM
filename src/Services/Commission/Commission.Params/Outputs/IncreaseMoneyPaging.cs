using System;
using System.Collections.Generic;
using Base.DTOs.CMS;
using PagingExtensions;

namespace Commission.Params.Outputs
{
    public class IncreaseMoneyPaging
    {
        public List<IncreaseDeductMoneyDTO> IncreaseMoneys { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
