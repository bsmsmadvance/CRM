using System;
using System.Collections.Generic;
using System.Text;
using Base.DTOs.FIN;
using PagingExtensions;

namespace Finance.Params.Outputs
{
    public class DepositPaging
    {
        public List<DepositDetailDTO> DepositDetails { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
