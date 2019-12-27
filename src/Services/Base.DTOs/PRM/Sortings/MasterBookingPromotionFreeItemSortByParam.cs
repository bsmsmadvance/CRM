using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRM
{
    public class MasterBookingPromotionFreeItemSortByParam
    {
        public MasterBookingPromotionFreeItemSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum MasterBookingPromotionFreeItemSortBy
    {
        NameTH,
        NameEN,
        Quantity,
        UnitTH,
        UnitEN,
        ReceiveDays,
        WhenPromotionReceive,
        IsShowInContract,
        Updated,
        UpdatedBy
    }
}
