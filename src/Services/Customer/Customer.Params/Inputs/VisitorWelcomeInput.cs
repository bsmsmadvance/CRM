using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Params.Inputs
{
    public class VisitorWelcomeInput
    {
        /// <summary>
        /// สถานะต้อนรับลูกค้า (true = ต้อนรับ/false = ยกเลิกต้อนรับ)
        /// </summary>
        public bool IsWelcome { get; set; }
    }
}
