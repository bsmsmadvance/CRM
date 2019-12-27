using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Services.JsonModels
{
    public class TokenDetail
    {
        public string token { get; set; }
    }

    public class Token
    {
        public int sc { get; set; }
        public TokenDetail d { get; set; }
        public string m { get; set; }
    }
}
