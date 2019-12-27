using System;
using System.Collections.Generic;
using System.Text;
using Base.DTOs.FIN;
using PagingExtensions;
namespace Finance.Params.Outputs
{
    public class FeeCreditDebitCardPaging
    {
        public List<FeeCreditDebitCardDTO> FeeCreditDebitCards { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
