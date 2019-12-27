using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRM
{
    public class MasterBookingCreditCardItemSortByParam
    {
        public MasterBookingCreditCardItemSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum MasterBookingCreditCardItemSortBy
    {
        Bank,
        NameTH,
        NameEN,
        Fee,
        UnitTH,
        UnitEN,
        PromotionItemStatus,
        Quantity,
        Updated,
        UpdatedBy
    }
}
