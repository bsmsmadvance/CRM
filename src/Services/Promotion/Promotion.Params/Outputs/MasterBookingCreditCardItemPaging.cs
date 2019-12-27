using Base.DTOs.PRM;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Promotion.Params.Outputs
{
    public class MasterBookingCreditCardItemPaging
    {
        public List<MasterBookingCreditCardItemDTO> MasterBookingCreditCardItemDTOs { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
