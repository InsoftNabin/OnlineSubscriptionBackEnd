namespace OnlineSubscriptionBackEnd.Model
{
    public class ResponceMessage
    {
        public int response_code { get; set; }
        public string response { get; set; }
        public string message_count { get; set; }
        public string balance_deducted { get; set; }
        public string message_type { get; set; }
    }

    public class ResponceMessage1
    {
        public string response { get; set; }
    }
}