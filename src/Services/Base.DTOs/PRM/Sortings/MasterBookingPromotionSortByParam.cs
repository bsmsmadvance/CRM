using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRM
{
    public class MasterBookingPromotionSortByParam
    {
        public MasterBookingPromotionSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum MasterBookingPromotionSortBy
    {
        PromotionNo,
        Name,
        Project,
        StartDate,
        EndDate,
        CashDiscount,
        FGFDiscount,
        TransferDiscount,
        PromotionStatus,
        IsUsed,
        Updated,
        UpdatedBy
    }
}
