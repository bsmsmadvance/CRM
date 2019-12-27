using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Params.Filters
{
    public class FETFilterViewUnit
    {
        public Guid? ProjectID { get; set; }
        public Guid? UnitID { get; set; }
        public Guid? ContactID { get; set; }
        public string ContactName { get; set; }
        public string OwnerNameFET { get; set; }
        public int? countUnit { get; set; }

        public int? countAmountFrom { get; set; }
        public int? countAmountTo { get; set; }

    }
}
