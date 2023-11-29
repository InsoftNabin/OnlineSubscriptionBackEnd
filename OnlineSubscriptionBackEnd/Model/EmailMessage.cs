namespace OnlineSubscriptionBackEnd.Model
{
	public class EmailMessage
	{
		public string TokenNo { get; set; }
		public string RegNo { get; set; }
		public string Name { get; set; }
		public string Subject { get; set; }
		public string ReceiverEmail { get; set; }
		public string EmailBody { get; set; }
		public string Gcode { get; set; }
	}
}
