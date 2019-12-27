using System;
using System.Collections.Generic;
using Base.DTOs.CMS;
using PagingExtensions;

namespace Commission.Params.Outputs
{
    public class DeductMoneyPaging
    {
        public List<IncreaseDeductMoneyDTO> DeductMoneys { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
