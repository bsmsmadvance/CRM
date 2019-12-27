using System;
using System.Collections.Generic;
using Base.DTOs.PRJ;
using Base.DTOs.PRM;

namespace Promotion.Params.Inputs
{
    public class PreSaleRequestSavePRParam
    {
        public List<UnitDropdownSellPriceDTO> Units { get; set; }
        public List<PreSalePromotionRequestItemDTO> Items { get; set; }
    }
}
