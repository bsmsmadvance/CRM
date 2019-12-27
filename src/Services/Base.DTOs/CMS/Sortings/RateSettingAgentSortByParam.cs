using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class RateSettingAgentSortByParam
    {
        public RateSettingAgentSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum RateSettingAgentSortBy
    {
        ProjectID,
        ActiveDate,
        AgentID,
        Amount,
        IsActive
    }
}
