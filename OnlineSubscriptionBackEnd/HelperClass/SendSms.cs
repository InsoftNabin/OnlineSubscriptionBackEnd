using DataAccess;
using Newtonsoft.Json;
using OnlineSubscriptionBackEnd.Model;
using OnlineSubscriptionBackEnd.Model.Insoft;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSubscriptionBackEnd.HelperClass
{
    public class SendSms
    {
      
        public static HttpClient Initial(string ApiToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            DataHandeler dh = new DataHandeler();
            string baseurl = "https://sms.insoftsms.com/smsapi/";
            client.BaseAddress = new Uri(baseurl);
            return client;
        }

        public static async Task<string> SendWebSMSPost(SMSSend ss) //string MobileNo, string SMS, string ApiToken, string SenderId)
        {
            try
            {
                HttpClient client = Initial(ss.token);

                //string ApiString = "/smsapi/SendSmsPost?senderid=" + ss.senderid + "&numberto=" + ss.numberto + "&message=" + ss.message + "&token=" + ss.token + "";
                //int a = ApiString.Count();

                //HttpResponseMessage res = await client.GetAsync(ApiString);
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(ss), UTF8Encoding.UTF8, "application/json");
                HttpResponseMessage res = await client.PostAsync("SendSmsPost", httpContent);
                //res.EnsureSuccessStatusCode();
                string responcebody = await res.Content.ReadAsStringAsync();
                ResponceMessage rm = JsonConvert.DeserializeObject<ResponceMessage>(responcebody);

                if (rm.response_code == 200)
                {
                    string result = "Success";
                    return result;
                }
                else
                {
                    return "Not Success";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
