﻿using System;
using System.Collections.Generic;
using Base.DTOs.CMS;
using PagingExtensions;

namespace Commission.Params.Outputs
{
    public class RateSettingAgentPaging
    {
        public List<RateSettingAgentDTO> RateSettingAgents { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
