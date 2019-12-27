using Base.DTOs.CMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commission.Params.Inputs
{
    public class RateSettingAgentInput
    {
        public List<ProjectInput> ListProject { get; set; }
        public List<RateSettingAgentDTO> ListRateSettingAgent { get; set; }
    }
}
