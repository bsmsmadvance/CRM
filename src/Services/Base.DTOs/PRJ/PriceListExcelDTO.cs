﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class PriceListExcelDTO
    {
        public int Success { get; set; }
        public int Error { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}