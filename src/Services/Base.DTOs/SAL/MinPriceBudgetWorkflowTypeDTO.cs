using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL
{
    public class MinPriceBudgetWorkflowTypeDTO
    {
        /// <summary>
        /// Type ของ MinPrice flow (eg. Adhog > 5%)
        /// </summary>
        public MST.MasterCenterDropdownDTO MinPriceWorkflowType { get; set; }
        /// <summary>
        /// สถานะ MinPrice Workflow (true = เข้าเงื่อนไข Workflow)
        /// </summary>
        public bool? IsMinPriceWorkflow { get; set; }
        /// <summary>
        /// สถานะ BudgetPromotion Workflow (true = เข้าเงื่อนไข Workflow)
        /// </summary>
        public bool? IsBudgetPromotionWorkflow { get; set; }
    }
}
