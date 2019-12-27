using System;
using System.Collections.Generic;
using System.Text;
using Base.DTOs.FIN;
using PagingExtensions;

namespace Finance.Params.Outputs
{
    public class ReceiptInfoPaging
    {
        public List<ReceiptInfoDTO> ReceiptInfos { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
