using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMReport.Reports.PF
{
    public class PFParams
    {
        //Parameters
        public string param {get;set;}
        public string token { get; set; }
        public string AgreementNo { get; set; }
        public string FloorPlan { get; set; }
        public string RoomPlan { get; set; }
        public string QuotationNo { get; set; }
        public string ProjectNo { get; set; }
        public string ProductID { get; set; }
        public string ProductID1 { get; set; }
        public string HistoryID { get; set; }
        public string ReceivedID { get; set; }
        public string UserID { get; set; }
        public string ContactID { get; set; }
        public string ContactID2 { get; set; }
        public string UnitNumber { get; set; }
        public string UnitNumber1 { get; set; }
        public string ContractNumber { get; set; }
        public string BookingNumber { get; set; }
        public string OperateType { get; set; }
        public string ContractReference { get; set; }
        public string RCReference { get; set; }

        public string TransferNumber { get; set; }
        public string NitiBankName { get; set; }
        public string NitiBankType { get; set; }
        public string NitiBankNo { get; set; }
        public string CustomerBankName { get; set; }
        public string CustomerBankType { get; set; }
        public string CustomerBankNo { get; set; }
        public string CustomerBankName2 { get; set; }
        public string CustomerBankType2 { get; set; }
        public string CustomerBankNo2 { get; set; }

        public string Layer1 { get; set; }
        public string Layer2 { get; set; }
        public string Layer3 { get; set; }
        public string Layer4 { get; set; }
        public string Layer5 { get; set; }
        public string Layer6 { get; set; }
        public string Layer7 { get; set; }
        public string Layer8 { get; set; }
        public string Layer9 { get; set; }
        public string Layer10 { get; set; }
        public string Layer11 { get; set; }
        public string AssignName { get; set; }
        public string HomeType { get; set; }
        public string HomeOffice { get; set; }
        public string Hurdle { get; set; }
        public string AddressType { get; set; }
        public string AddressTypeChange { get; set; }
        public string Other { get; set; }
        public string UserName { get; set; }

        public string ReceivePromotionID { get; set; }
        public string PromotionType { get; set; }
        public string DocumentID { get; set; }
        public string DocumentType { get; set; }

        //DateTime
        public string DateStart { get; set; }
        public string LVDate { get; set; }
        public string LVDateCust { get; set; }

        //Convert
        public DateTime? DS { get; set; }

        //Common
        public string dbConnection { get; set; }
        public string dbUsername { get; set; }
        public string dbPassword { get; set; }
        public string reportName { get; set; }
        public string fullReportPath { get; set; }
        public string downloadAs { get; set; }
    }
}