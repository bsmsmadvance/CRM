﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.MasterKeys
{
    public static class MinPriceWorkflowTypeKeys
    {
        // Quarterly
        public static string Quarterly = "1";
        // Adhoc <= 5%
        public static string AdhocLessThan5Percent = "2";
        // Adhoc > 5%
        public static string AdhocMoreThan5Percent = "3";
    }
}
