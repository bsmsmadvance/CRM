using System;
using System.Collections.Generic;
using System.Text;
using Base.DTOs.FIN;
using PagingExtensions;

namespace Finance.Params.Outputs
{
    public class FeeChequePaging
    {
        public List<FeeChequeDTO> FeeCheques { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
