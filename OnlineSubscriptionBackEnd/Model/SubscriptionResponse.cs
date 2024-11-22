namespace OnlineSubscriptionBackEnd.Model
{
    public class SubscriptionResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string ExpireDate { get; set; }
        public string RemainingDays { get; set; }
        public string LandingPage { get; set; }

    }
}
