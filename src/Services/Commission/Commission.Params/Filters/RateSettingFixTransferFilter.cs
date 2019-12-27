using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs;

namespace Commission.Params.Filters
{
    public class RateSettingFixTransferFilter : BaseFilter
    {
        public List<Guid> ListProjectId { get; set; }
        public DateTime? ActiveDate { get; set; }
        public Guid? ProjectID { get; set; }
        public decimal? Amount { get; set; }
        public string CreateUserName { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
        public bool? IsActive { get; set; }
    }
}
