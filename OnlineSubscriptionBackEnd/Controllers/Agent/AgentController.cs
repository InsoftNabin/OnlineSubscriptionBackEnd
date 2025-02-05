//using DataAccess;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using OnlineSubscriptionBackEnd.Model.Insoft;
//using System.Data.SqlClient;
//using System.Data;

//namespace OnlineSubscriptionBackEnd.Controllers.Agent
//{
//    public class AgentController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}



using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using OnlineSubscriptionBackEnd.Model.Agent;

using Newtonsoft.Json.Linq;
using OnlineSubscriptionBackEnd.Model.Insoft;


namespace OnlineSubscriptionBackEnd.Controllers.Insoft
{
    [Route("[controller]/[action]")]
    public class AgentController : Controller
    {
        DataHandeler dh = new DataHandeler();

        [HttpPost]
        public JsonResult InsertUpdate([FromBody] Agent ai)
        {
            try
            {
                int AffectedRows = 0;
                //string conn = "";
                //conn = dh.ByToken(ai.TokenNo);
                SqlParameter[] parm = {
                        //new SqlParameter("@TokenNo",ai.TokenNo),
                        new SqlParameter("@Id",ai.Id),
                        new SqlParameter("@Name",ai.Name),
                        new SqlParameter("@ContactPerson",ai.ContactPerson),
                        new SqlParameter("@Address",ai.Address),
                        new SqlParameter("@Email",ai.Email),
                        new SqlParameter("@Website",ai.Website),
                        new SqlParameter("@Bank",ai.Bank),
                        new SqlParameter("@AccountNo",ai.AccountNo),
                        new SqlParameter("@ContactNo",ai.ContactNo),
                        new SqlParameter("@PanNo",ai.PanNo),
                        new SqlParameter("@SMSToken",ai.SMSToken),
                        new SqlParameter("@SMSSenderId",ai.SMSSenderId),
                        new SqlParameter("@Type",ai.Type),
                        new SqlParameter("@UserId",ai.UserId),
                        new SqlParameter("@Password",ai.@Password),
                        new SqlParameter("@Country",ai.Country)
                        
                        
                    };
                AffectedRows = AffectedRows + dh.InsertUpdate("[Insoft_IU_Agent]", parm, CommandType.StoredProcedure);
                return Json(AffectedRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public JsonResult StoreOTPBeforeLogin([FromBody] Agent ai)
        {
            try
            {
                int AffectedRows = 0;
                //string conn = "";
                //conn = dh.ByToken(ai.TokenNo);
                SqlParameter[] parm = {
                        new SqlParameter("@TokenNo",ai.TokenNo),
                        new SqlParameter("@SessionToken",ai.SessionToken),
                        new SqlParameter("@OTP",ai.OTP)
                    };
                AffectedRows = AffectedRows + dh.InsertUpdate("[usp_LoginAgent_StoreOTPBeforeLogin]", parm, CommandType.StoredProcedure);
                return Json(AffectedRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult CheckOTP([FromBody] Agent ai)
        {
            try
            {
                int AffectedRows = 0;
                //string conn = "";
                //conn = dh.ByToken(ai.TokenNo);
                SqlParameter[] parm = {
                        new SqlParameter("@TokenNo",ai.TokenNo),
                        new SqlParameter("@SessionToken",ai.SessionToken),
                        new SqlParameter("@OTP",ai.OTP)
                    };
                string data = dh.ReadToJson("[usp_LoginAgent_CheckOtpBeforeLogin]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public ActionResult getAgentByToken([FromBody] Agent p)
        {
            try
            {
                SqlParameter[] parm = {

                    new SqlParameter("@Id",p.Id)
                };
                string data = dh.ReadToJson("[Agent_getIdFromToken]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPost]
        public ActionResult getAgents([FromBody] string TokenNo)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@TokenNo",TokenNo)
                };
                string data = dh.ReadToJson("[Insoft_S_GetAllAgents]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult getAgent([FromBody] Agent p)
        {
            try
            {
                SqlParameter[] parm = {

                    new SqlParameter("@Id",p.Id)
                };
                string data = dh.ReadToJson("[Insoft_S_GetAgentById]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public ActionResult DeleteAgent([FromBody] Agent p)
        {
            try
            {
                SqlParameter[] parm = {

                    new SqlParameter("@Id",p.Id)
                };
                string data = dh.ReadToJson("[Insoft_D_Agent]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
