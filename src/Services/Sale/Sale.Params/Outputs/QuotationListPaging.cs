using System;
using System.Collections.Generic;
using Base.DTOs.SAL;
using PagingExtensions;

namespace Sale.Params.Outputs
{
    public class QuotationListPaging
    {
        public List<QuotationListDTO> Quotations { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
