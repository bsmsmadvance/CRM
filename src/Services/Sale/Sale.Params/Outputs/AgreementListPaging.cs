using Base.DTOs.SAL;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sale.Params.Outputs
{
    public class AgreementListPaging
    {
        public List<AgreementListDTO> Agreements { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
