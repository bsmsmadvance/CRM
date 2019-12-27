using System;
using System.Collections.Generic;
using System.Text;
using Base.DTOs.FIN;
using PagingExtensions;

namespace Finance.Params.Outputs
{
    public class EditReceiptPaging
    {
        public List<EditReceiptDTO> EditReceipts { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
