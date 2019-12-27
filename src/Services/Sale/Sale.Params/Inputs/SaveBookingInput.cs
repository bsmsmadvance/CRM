using Base.DTOs.SAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sale.Params.Inputs
{
    public class SaveBookingInput
    {
        public BookingDTO Booking { get; set; }
        public BookingPriceListDTO PriceList { get; set; }
        public BookingPromotionDTO BookingPromotion { get; set; }
        public List<BookingPromotionExpenseDTO> Expenses { get; set; }
    }
}
