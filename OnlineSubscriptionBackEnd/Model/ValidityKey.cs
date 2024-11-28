using System;

namespace OnlineSubscriptionBackEnd.Model
{
    public class ValidityKey
    {
       
        public string ProductKey { get; set; }
        public string ClientKey { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
