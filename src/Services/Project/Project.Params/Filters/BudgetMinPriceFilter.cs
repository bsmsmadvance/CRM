using System;
namespace Project.Params.Filters
{
    public class BudgetMinPriceFilter
    {
        public Guid? ProjectID { get; set; }
        public int? Year { get; set; }
        public int? Quarter { get; set; }
        public string UnitNo { get; set; }
        public decimal? AmonutTo { get; set; }
        public decimal? AmonutFrom { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedTo { get; set; }
        public DateTime? UpdatedFrom { get; set; }
        public Guid? UnitStatus { get; set; }
    }
}
