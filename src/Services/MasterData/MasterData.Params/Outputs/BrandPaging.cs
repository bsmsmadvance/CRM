﻿using System;
using System.Collections.Generic;
using Base.DTOs.MST;
using PagingExtensions;

namespace MasterData.Params.Outputs
{
    public class BrandPaging
    {
        public List<BrandDTO> Brands { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
