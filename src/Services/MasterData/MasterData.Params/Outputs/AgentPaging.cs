﻿using Base.DTOs.MST;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterData.Params.Outputs
{
    public class AgentPaging
    {
        public List<AgentDTO> Agents { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
