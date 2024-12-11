using System;

namespace OnlineSubscriptionBackEnd.Model
{
    public class ValidityKey
    {
       
        public string ProductKey { get; set; }
        public string ClientKey { get; set; }
        public string UniqueMachineKey { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ExpiryDate { get; set; }
        public int RemainingDays { get; set; }
        public string  Message { get; set; }
        public int Status { get; set; }
    }
    public class DecryptKey
    {
        public string validityKey { get; set; }
    }
}
