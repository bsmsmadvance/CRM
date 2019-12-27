using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Params.Filters
{
    public class FETFilterViewProject
    {
        /// <summary>
        /// โครงการ
        /// </summary>
        public Guid? ProjectID { get; set; }

        public int? countFET { get; set; }
        public int? countUnit { get; set; }

        public int? countAmountFrom { get; set; }
        public int? countAmountTo { get; set; }

    }
}
