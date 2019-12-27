using System;
using System.Collections.Generic;
using System.Text;

namespace Sale.Params.Filters
{
    public class BookingListFilter
    {
        public Guid? ProjectID { get; set; }
        public string BookingNo { get; set; }
        public string UnitNo { get; set; }
        public string FullName { get; set; }
        public DateTime? BookingDateFrom { get; set; }
        public DateTime? BookingDateTo { get; set; }
        public DateTime? ApproveDateFrom { get; set; }
        public DateTime? ApproveDateTo { get; set; }
        public DateTime? ContractDateFrom { get; set; }
        public DateTime? ContractDateTo { get; set; }
        public string BookingStatusKey { get; set; }
        public string BookingStatusKeys { get; set; }
        public string CreateBookingFromKeys { get; set; }
        public Guid? ConfirmByID { get; set; }
        public DateTime? ConfirmDateFrom { get; set; }
        public DateTime? ConfirmDateTo { get; set; }
        public bool? IsCancelled { get; set; }
        public string ReceiptTempNo { get; set; }
        public decimal? SellingPriceFrom { get; set; }
        public decimal? SellingPriceTo { get; set; }
    }
}
