using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commission.Params.Filters
{
    public class CommissionSettingFilter : BaseFilter
    {
        public Guid? BGID { get; set; }
        public Guid? ProjectID { get; set; }

    }
}
