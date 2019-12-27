using Base.DTOs.PRM;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Promotion.Params.Outputs
{
    public class PreSalePromotionRequestListPaging
    {
        public List<PreSalePromotionRequestListDTO> PreSalePromotionRequestLists { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
