﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class CommissionLowRiseVeiwSortByParam
    {
        public CommissionLowRiseVeiwSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum CommissionLowRiseVeiwSortBy
    {
        UnitNo
    }
}
