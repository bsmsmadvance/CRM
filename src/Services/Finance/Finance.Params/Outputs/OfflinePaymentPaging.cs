using System;
using System.Collections.Generic;
using System.Text;
using Base.DTOs.FIN;
using PagingExtensions;


namespace Finance.Params.Outputs
{
    public class OfflinePaymentPaging
    {
        public List<OfflinePaymentDTO> OfflinePayments { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
