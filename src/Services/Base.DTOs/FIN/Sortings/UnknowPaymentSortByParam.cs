using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.FIN
{
    public class UnknownPaymentSortByParam
    {
        public UnknownPaymentSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum UnknownPaymentSortBy
    {
        ReceiveDate,
        ReverseDate,
        Company,
        BankAccount,
        Project,
        Unit,
        UnknownPaymentStatus,
        UnknownPaymentCode,
        Amount,
        RVDocumentCode,
        Updated
    }
}
