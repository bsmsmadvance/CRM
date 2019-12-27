﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.SAL.Sortings
{
    public class AgreementListSortByParam
    {
        public AgreementListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum AgreementListSortBy
    {
        UnitNo,
        FullName,
        BookingNo,
        AgreementNo,
        AgreementStatus,
        ChangeAgreementOwnerType
    }
}
