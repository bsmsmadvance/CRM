using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Params.Filters
{
    public class AddresssFilter : BaseFilter
    {
        public string categoryType { get; set; }
        public string addressNameTH { get; set; }
        public string addressNameEN { get; set; }
        public string titleDeedNo { get; set; }
        public string postalCode { get; set; }
        public Guid? provinceID { get; set; }
        public Guid? districtID { get; set; }
        public Guid? subDistrictID { get; set; }
        public string villageNo { get; set; }
        public string laneTH { get; set; }
        public string laneEN { get; set; }
        public string roadTH { get; set; }
        public string roadEN { get; set; }
    }
}
