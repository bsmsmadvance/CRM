﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.AuditLogs.FIN
{
    [Description("ข้อมูลประวัติการแก้ไข OfflinePaymentHeader")]
    [Table("OfflinePaymentHeaderLog", Schema = Schema.FINANCE)]
    public class OfflinePaymentHeaderLog : BaseEntityLog
    {
    }
}
