using System;
using System.Collections.Generic;
using System.Text;
using Base.DTOs.FIN;
using PagingExtensions;

namespace Finance.Params.Outputs
{
    public class DirectCreditDebitApprovalFormPaging
    {
        public List<DirectCreditDebitApprovalFormDTO> DirectCreditDebitApprovalForms { get; set; }
        public PageOutput PageOutput { get; set; }
    }

    public class GetUnitDirectCreditDebitApprovalPaging
    {
        public List<AgreementNoTransferDTO> AgreementNoTransfer { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
