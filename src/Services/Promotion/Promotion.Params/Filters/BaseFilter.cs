﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Promotion.Params.Filters
{
    public class BaseFilter
    {
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedFrom { get; set; }
        public DateTime? UpdatedTo { get; set; }
    }
}
