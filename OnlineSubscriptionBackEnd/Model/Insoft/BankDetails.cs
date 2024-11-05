using System;

namespace OnlineSubscriptionBackEnd.Model.Insoft
{
    public class MessageResponce
    {
        public int status { get; set; }
        public string message { get; set; }
    }
    public class FonePayDynamicQRResponce
    {
        public string hashKey { get; set; } = "";
        public string qrMessage { get; set; } = "";
        public string clientCode { get; set; } = "";
        public string status { get; set; } = "";
        public string statusCode { get; set; } = "";
        public string success { get; set; } = "";
        public string deviceId { get; set; } = "";
        public string requested_date { get; set; } = DateTime.Now.ToString();
        public string merchantCode { get; set; } = "";
        public string merchantWebSocketUrl { get; set; } = "";
        public string thirdpartyQrWebSocketUrl { get; set; } = "";
        public string nfcThirdPartyQrUrl { get; set; } = "";
    }
    public class FonePayDynamicQRRequestWithFonePay
    {
        public string amount { get; set; } = "";
        public string remarks1 { get; set; } = "";
        public string remarks2 { get; set; } = "";
        public string prn { get; set; } = "";
        public string merchantCode { get; set; } = "";
        public string dataValidation { get; set; } = "";
        public string username { get; set; } = "";
        public string password { get; set; } = "";
    }

    public class BankDetails
    {
        //public string BankName { get; set; }
        //public string AccountHolder { get; set; }
        //public string AccountNumber { get; set; }
        //public float Amount { get; set; }
        //public string TokenNo { get; set; }


        public string amount { get; set; } = "";
        public string remarks1 { get; set; } = "";
        public string remarks2 { get; set; } = "";
        public string prn { get; set; } = "";
        public string merchantCode { get; set; } = "";
        public string secretKey { get; set; } = "";
        public string username { get; set; } = "";
        public string password { get; set; } = "";
        public string Purpose { get; set; } = "";

    }
}
