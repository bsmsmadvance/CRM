using System;
using System.Collections.Generic;
using Base.DTOs.CMS;
using PagingExtensions;

namespace Commission.Params.Outputs
{
    public class ChangeLCSalePaging
    {
        public List<ChangeLCSaleDTO> ChangeLCSales { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
