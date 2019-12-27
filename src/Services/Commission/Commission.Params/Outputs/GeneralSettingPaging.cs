using System;
using System.Collections.Generic;
using Base.DTOs.CMS;
using PagingExtensions;

namespace Commission.Params.Outputs
{
    public class GeneralSettingPaging
    {
        public List<GeneralSettingDTO> GeneralSettings { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
