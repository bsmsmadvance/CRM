using System;
using System.Collections.Generic;
using Base.DTOs.SAL;

namespace Sale.Params.Inputs
{
    public class CreateChangePromotionWorkflowInput
    {
        public UnitInfoBookingPromotionDTO BookingPromotion { get; set; }
        public List<BookingPromotionExpenseDTO> Expenses { get; set; }
    }
}
