using System;
namespace MasterData.Params.Filters
{
    public class EDCBankFilter : BaseFilter
    {
        public Guid? BankID { get; set; }
        public string BankName { get; set; }
    }
}
