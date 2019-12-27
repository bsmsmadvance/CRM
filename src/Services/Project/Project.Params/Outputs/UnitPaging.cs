﻿using Base.DTOs.PRJ;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Params.Outputs
{
    public class UnitPaging
    {
        public List<UnitDTO> Units { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
