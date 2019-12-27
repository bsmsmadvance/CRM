using Base.DTOs.CMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commission.Params.Inputs
{
    public class RateSettingSaleInput
    {
        public List<ProjectInput> ListProject { get; set; }
        public List<RateSettingSaleTransferDTO> ListRateSettingSale { get; set; }
    }
}
