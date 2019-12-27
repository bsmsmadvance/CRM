using System;
using System.Collections.Generic;
using Base.DTOs.MST;
using PagingExtensions;

namespace MasterData.Params.Outputs
{
    public class BankAccountPaging
    {
        public List<BankAccountDTO> BankAccounts { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
