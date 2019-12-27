using Base.DTOs.PRM;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Promotion.Params.Outputs
{
    public class MasterPreSalePromotionPaging
    {
        public List<MasterPreSalePromotionDTO> MasterPreSalePromotionDTOs { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
