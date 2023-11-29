using Newtonsoft.Json.Linq;

namespace OnlineSubscriptionBackEnd.Model
{
	public class SendEmailRequestDTO
	{
		public string TokenNo { get; set; }
		public string ReceiverEmail { get; set; }
		public int EmailType { get; set; }
		public JObject data { get; set; }
		public string EmailHeader { get; set; }
		public string AttachmentContentHTML { get; set; }
		public string AttachmentFileName { get; set; }
	}
}
