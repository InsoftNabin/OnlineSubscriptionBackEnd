namespace OnlineSubscriptionBackEnd.Model
{
    public class ResponceModel
    {
        public object data { get; set; }
        public int status { get; set; }
        public string message { get; set; }
        public int nextPage { get; set; }
    }
}
