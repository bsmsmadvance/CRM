using System;
namespace MasterData.Params.Filters
{
    public class EDCFilter : BaseFilter
    {
        public string Code { get; set; }
        public string CardMachineTypeKey { get; set; }
        public Guid? BankAccountID { get; set; }
        public Guid? CompanyID { get; set; }
        public Guid? ProjectID { get; set; }
        public string ProjectStatusKey { get; set; }
        public string ReceiveBy { get; set; }
        public DateTime? ReceiveDateFrom { get; set; }
        public DateTime? ReceiveDateTo { get; set; }
        public string CardMachineStatusKey { get; set; }
    }
}
