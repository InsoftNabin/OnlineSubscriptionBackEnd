namespace OnlineSubscriptionBackEnd.Model
{
    public class ApiRequest
    {
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
        public string? UniqueMachineCode { get; set; }
        public string? validityKey { get; set; }

    }
}
