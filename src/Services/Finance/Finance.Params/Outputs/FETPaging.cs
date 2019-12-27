using System;
using System.Collections.Generic;
using System.Text;
using Base.DTOs.FIN;
using PagingExtensions;

namespace Finance.Params.Outputs
{
    public class FETPaging
    {
        public List<FETDTO> FETs { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
