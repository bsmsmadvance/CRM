using System;
using System.Collections.Generic;
using System.Text;
using Base.DTOs.FIN;
using PagingExtensions;

namespace Finance.Params.Outputs
{
    public class DirectCreditDebitExportDetailPaging
    {
        public List<DirectCreditDebitExportHeaderDTO> DirectCreditDebitExportHeader { get; set; }
       // public List<DirectCreditDebitExportDetailDTO> DirectCreditDebitExportDetails { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
