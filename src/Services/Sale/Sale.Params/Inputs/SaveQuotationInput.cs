using System;
using System.Collections.Generic;
using Base.DTOs.SAL;

namespace Sale.Params.Inputs
{
    public class SaveQuotationInput
    {
        public Guid? UnitID { get; set; }
        public QuotationPriceListDTO PriceList { get; set; }
        public QuotationBookingPromotionDTO BookingPromotion { get; set; }
        public QuotationTransferPromotionDTO TransferPromotion { get; set; }
        public List<QuotationPromotionExpenseDTO> Expenses { get; set; }
    }
}
