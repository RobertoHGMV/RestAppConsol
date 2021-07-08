using System;

namespace RestAppConsol.Models
{
    public class DataToken
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        public CustomerToken User { get; set; }

        public DataCookie Cookie { get; set; }

        public class CustomerToken
        {
            public string CardCode { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }
    }
}
