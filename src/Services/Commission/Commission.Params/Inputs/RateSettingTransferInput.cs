using Base.DTOs.CMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commission.Params.Inputs
{
    public class RateSettingTransferInput
    {
        public List<ProjectInput> ListProject { get; set; }
        public List<RateSettingSaleTransferDTO> ListRateSettingTransfer { get; set; }
    }
}
