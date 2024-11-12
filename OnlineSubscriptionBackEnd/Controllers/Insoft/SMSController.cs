using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DataAccess;
using OnlineSubscriptionBackEnd.Model.Insoft;
using OnlineSubscriptionBackEnd.Model;
using OnlineSubscriptionBackEnd.HelperClass;


namespace OnlineSubscriptionBackEnd.Controllers.Insoft
{
    [Route("[controller]/[action]")]
    public class SMSController : Controller
    {
        DataHandeler dh = new DataHandeler();


        //[HttpPost]
        //public async Task<JsonResult> SendSMS1([FromBody] SMSResponse sMSResponse)
        //{
                
        // SqlParameter[] parm1 = {
        //              new SqlParameter("@TokenNo",sMSResponse.TokenNo)
        // };

        //            string SmsTokenAndSenderId = dh.ReadToJson("[Insoft_Adm_S_SmsApiTokenAndSenderIdByToken]", parm1, CommandType.StoredProcedure);
        //            JArray jArray2 = (JArray)JsonConvert.DeserializeObject(SmsTokenAndSenderId);


        //            string SmsToken = jArray2[0]["SmsApiToken"].ToString();
        //            string SmsSenderId = jArray2[0]["SmsApiSenderId"].ToString();

        //    if (jArray2 != null && jArray2.Count > 0)
        //    {
        //        return Ok(jArray2);  // Return 200 OK
        //    }

        //    //for (var i = 0; i < sMSResponse.Count; i++)
        //    //{
        //    //    SMSSend ss = new SMSSend();
        //    //    ss.numberto = sMSResponse[i].MobileNo;
        //    //    ss.senderid = SmsSenderId;
        //    //    ss.token = SmsToken;
        //    //    ss.message = sMSResponse[i].SMS_Message;

        //    //    //string PhoneNo = sMSResponse[i].MobileNo;
        //    //    //string Smsstring = sMSResponse[i].SMS_Message;

        //    //    //string res = await SendSms.SendWebSMS(PhoneNo, Smsstring, SmsToken, SmsSenderId);

        //    //    string res = await SendSms.SendWebSMSPost(ss);

        //    //    if (res == "Success")
        //    //    {

        //    //    }
        //    //    else
        //    //    {

        //    //    }

        //    //}
        //}

       




        [HttpPost]
        public async Task<JsonResult> SendSMS([FromBody] SMSResponse sMSResponses)
        {
            string ErrorMessage = "No SMS messages to send.";

            if (sMSResponses == null )
            {
                return Json(JsonConvert.DeserializeObject(ErrorMessage));
            }
           

            SqlParameter[] parm1 = {
                                     new SqlParameter("@TokenNo", sMSResponses.TokenNo)
                                    };

            string SmsTokenAndSenderId = dh.ReadToJson("[Insoft_Adm_S_SmsApiTokenAndSenderIdByToken]", parm1, CommandType.StoredProcedure);
            JArray jArray2 = (JArray)JsonConvert.DeserializeObject(SmsTokenAndSenderId);

            if (jArray2 == null || jArray2.Count == 0)
            {
                return Json("No token and senderId found for the provided token.");
            }

            string SmsToken = jArray2[0]["SMSToken"]?.ToString();
            string SmsSenderId = jArray2[0]["SMSSenderId"]?.ToString();
            //string ContactNumber = jArray2[0]["UserContact"]?.ToString();

            if (string.IsNullOrEmpty(SmsToken) || string.IsNullOrEmpty(SmsSenderId))
            {
                return Json("Invalid SmsApiToken or SmsApiSenderId.");
            }


            
                SMSSend ss = new SMSSend()
                {
                    numberto = sMSResponses.MobileNo,
                    senderid = SmsSenderId,
                    token = SmsToken,
                    message = sMSResponses.SMS_Message
                };

                string res = await SendSms.SendWebSMSPost(ss);

                if (res != "Success")
                {

                    return Json(500, "Error sending SMS to " + ss.numberto);
                }
         

            return Json("200");
        }



    }
}