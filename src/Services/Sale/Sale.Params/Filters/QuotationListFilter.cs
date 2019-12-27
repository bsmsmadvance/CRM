using System;
namespace Sale.Params.Filters
{
    public class QuotationListFilter
    {
        public string QuotationNo { get; set; }
        public Guid? ProjectID { get; set; }
        public string UnitNo { get; set; }
        public string HouseNoFrom { get; set; }
        public string HouseNoTo { get; set; }
        public DateTime? IssueDateFrom { get; set; }
        public DateTime? IssueDateTo { get; set; }
        public string QuotationStatusKey { get; set; }
        public Guid? CreatedByUserID { get; set; }
    }
}
