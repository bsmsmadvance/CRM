using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Params.Filters
{
    public class OpportunityFilter
    {
        public Guid? ProjectID { get; set; }
        public DateTime? ArriveDateFrom { get; set; }
        public DateTime? ArriveDateTo { get; set; }
        public Guid? ContactID { get; set; }
        public string ContactNo { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string SalesOpportunityKey { get; set; }
        public Guid? OwnerID { get; set; }
        public string StatusQuestionaireKey { get; set; }
        public DateTime? UpdatedDateFrom { get; set; }
        public DateTime? UpdatedDateTo { get; set; }
        public string ExcludeIDs { get; set; }
    }
}
