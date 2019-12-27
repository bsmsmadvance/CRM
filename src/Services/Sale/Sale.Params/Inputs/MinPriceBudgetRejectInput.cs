using System;
using System.Collections.Generic;
using Base.DTOs.SAL;

namespace Sale.Params.Inputs
{
    public class MinPriceBudgetRejectInput
    {
        public List<MinPriceBudgetWorkflowDTO> MinPriceBudgetWorkFlows { get; set; }
        public string RejectComment { get; set; }
    }
}
