using System;
using System.Collections.Generic;
using System.Text;

namespace Sale.Params.Filters
{
    public class AgreementListFilter
    {
        public Guid? ProjectID { get; set; }
        public string UnitNo { get; set; }
        public string FullName { get; set; }
        public string BookingNo { get; set; }
        public string AgreementNo { get; set; }
        public string AgreementStatusKey { get; set; }
        public string AgreementStatusKeys { get; set; }
        public string ChangeAgreementOwnerTypeKey { get; set; }
        public string ChangeAgreementOwnerTypeKeys { get; set; }
    }
}
