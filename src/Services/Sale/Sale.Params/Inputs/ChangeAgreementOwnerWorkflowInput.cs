using System;
using System.Collections.Generic;
using System.Text;
using Base.DTOs.SAL;

namespace Sale.Params.Inputs
{
    public class ChangeAgreementOwnerWorkflowInput
    {
        public ChangeAgreementOwnerWorkflowDTO ChangeAgreementOwnerWorkflow { get; set; }
        public List<AgreementOwnerDTO> ListAgreementOwner { get; set; }
    }


    public class AddContactToAgreementOwnerInput
    {
        public Guid AgreementId { get; set; }
        public List<Guid> ListContractId { get; set; }
    }
}
