﻿namespace Base.DTOs.FIN
{
    public class ReceiptInfoSortByParam
    {
        public ReceiptInfoSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum ReceiptInfoSortBy
    {
        ReceiptTempNo,
        ReceiveDate,
        ProjectNo,
        UnitNo,
        BankAccount,
        PaymentType,
        ReceiptDescription,
        Amount,
        DepositNo,
        RVNumber,
        ReceiptStatus
    }
}
