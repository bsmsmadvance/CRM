using Base.DTOs.MST;

namespace Sale.Params.Inputs
{
    public class MinPriceBudgetReasonInput
    {
        /// <summary>
        /// เหตุผลขออนุมัติ Min Price (กรณีติด Workflow)
        /// </summary>
        public MasterCenterDropdownDTO MinPriceRequestReason { get; set; }
        /// <summary>
        /// เหตุผลขออนุมัติ Min Price อื่นๆ (กรณีติด Workflow)
        /// </summary>
        public string OtherMinPriceRequestReason { get; set; }
    }
}