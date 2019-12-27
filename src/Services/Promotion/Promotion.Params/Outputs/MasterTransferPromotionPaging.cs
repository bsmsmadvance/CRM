using System;
using System.Collections.Generic;
using Base.DTOs.PRM;
using PagingExtensions;

namespace Promotion.Params.Outputs
{
    public class MasterTransferPromotionPaging
    {
        public List<MasterTransferPromotionDTO> MasterTransferPromotionDTOs { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
