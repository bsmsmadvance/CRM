using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterData.Params.Filters
{
    public class SubDistrictFilter : BaseFilter
    {
        public Guid? DistrictID { get; set; }
        public string LandOffice { get; set; }
        public string NameTH { get; set; }
        public string NameEN { get; set; }
        public string PostalCode { get; set; }
    }
}
