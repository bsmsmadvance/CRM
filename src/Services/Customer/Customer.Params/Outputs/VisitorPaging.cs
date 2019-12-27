using Base.DTOs.CTM;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Params.Outputs
{
    public class VisitorPaging
    {
        public List<VisitorListDTO> Visitors { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
