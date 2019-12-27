using Base.DTOs.PRM;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Promotion.Params.Outputs
{
    public class MasterBookingPromotionItemPaging
    {
        public List<MasterBookingPromotionItemDTO> MasterBookingPromotionItemDTOs  { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
