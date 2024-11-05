using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using DataAccess;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineSubscriptionBackEnd.Model.Insoft;



namespace OnlineSubscriptionBackEnd.Controllers.Insoft
{
    [Route("[controller]/[action]")]
    public class QRCodeForPaymentController : Controller
    {
        DataHandeler dh = new DataHandeler();


        [HttpPost]
        public JsonResult GetFonePayDetails()
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@TokenNo", "aa"),
                };

                string data = dh.ReadToJson("usp_s_GetFonePayDetails", parm, CommandType.StoredProcedure);
                JArray jArray = (JArray)JsonConvert.DeserializeObject(data);
                return Json(jArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult GetFonePayDetails1([FromBody] CustomerPlan p)
        {
            try
            {
                SqlParameter[] parm = {
                     new SqlParameter("@TokenNo", "aa")
                };
                string data = dh.ReadToJson("[usp_s_GetFonePayDetails]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

