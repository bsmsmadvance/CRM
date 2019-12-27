using Database.Models.FIN;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Services.Common
{
    public class PaymentLists
    {
        public List<PaymentItem> PaymentItems { get; set; }
        public List<PaymentMethodToItem> PaymentMethodToItems { get; set; }
    }
}
