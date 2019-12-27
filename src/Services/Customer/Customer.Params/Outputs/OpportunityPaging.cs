using Base.DTOs.CTM;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Params.Outputs
{
    public class OpportunityPaging
    {
        public List<OpportunityListDTO> Opportunities { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
