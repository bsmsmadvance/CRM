using System;
namespace Auth
{
    public class JsonWebToken
    {
        public string token { get; set; }
        public long expires { get; set; }
        public long expires_in { get; set; }
        public string refresh_token { get; set; }
        public Guid user_id { get; set; }
        public string display_name { get; set; }
    }

    public class ClientJsonWebToken
    {
        public string token { get; set; }
        public long expires { get; set; }
        public long expires_in { get; set; }
        public string display_name { get; set; }
    }
}
