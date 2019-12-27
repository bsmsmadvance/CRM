using System;
using System.Collections.Generic;
using System.Text;

namespace Promotion.Params.Filters
{
    public class PreSalePromotionRequestListFilter : BaseFilter
    {
        public DateTime? RequestDateFrom { get; set; }
        public DateTime? RequestDateTo { get; set; }
        public Guid? ProjectID { get; set; }
        public DateTime? PRCompletedDateFrom { get; set; }
        public DateTime? PRCompletedDateTo { get; set; }
        public string PromotionRequestPRStatusKey { get; set; }
        public string UnitNo { get; set; }
        public string MasterPreSalePromotion_PromotionNo { get; set; }
    }
}
