////using Microsoft.AspNetCore.Mvc;

//namespace OnlineSubscriptionBackEnd.Controllers.Insoft
//{
//    public class CustomerwiseModulesSelectionController : Controller
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
using OnlineSubscriptionBackEnd.Model.Insoft;

using Newtonsoft.Json.Linq;


namespace OnlineSubscriptionBackEnd.Controllers.Insoft
{
    [Route("[controller]/[action]")]
    public class CustomerwiseModulesSelectionController : Controller
    {
        DataHandeler dh = new DataHandeler();

        [HttpPost]
        public ActionResult InsertUpdate([FromBody] CustomerwiseModules ai)
        {
            try
            {
                int AffectedRows = 0;
                //string conn = "";
                //conn = dh.ByToken(ai.TokenNo);

                SqlParameter[] parameters = {
                new SqlParameter("@Id", ai.Id),
                new SqlParameter("@TokenNo", ai.TokenNo),
                new SqlParameter("@CustomerId", ai.CustomerId),
                new SqlParameter("@ProductId", ai.ProductId),
                new SqlParameter("@AgentId", ai.AgentId),
                new SqlParameter("@SubProductId", ai.SubProductId),
                new SqlParameter("@JoinDate", ai.JoinDate),
                new SqlParameter("@LastRenewDate", ai.LastRenewDate),
                new SqlParameter("@ExpiryDate", ai.ExpiryDate),
                new SqlParameter("@Initial", ai.Initial),
                new SqlParameter("@MonthlyCharge", ai.MonthlyCharge),
                new SqlParameter("@SerialNumber", ai.SerialNumber),
                new SqlParameter("@SiteURL", ai.SiteURL),
                new SqlParameter("@Remarks", ai.Remarks)
                };
                AffectedRows = AffectedRows + dh.InsertUpdate("[Insoft_S_InsertUpdateCustomerwisemoduledetails]", parameters, CommandType.StoredProcedure);
                return Json(AffectedRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //[HttpPost]
        //public JsonResult InsertUpdate([FromBody] CustomerwiseModules ai )
        //{
        //    try
        //    {
        //        int totalAffectedRows = 0;

            
        //            SqlParameter[] parameters = {
        //        new SqlParameter("@Id", ai.Id),
        //        new SqlParameter("@TokenNo", ai.TokenNo),
        //        new SqlParameter("@CustomerId", ai.CustomerId),
        //        new SqlParameter("@ProductId", ai.ProductId),
        //        new SqlParameter("@AgentId", ai.AgentId),
        //        new SqlParameter("@SubProductId", ai.SubProductId),
        //        new SqlParameter("@JoinDate", ai.JoinDate),
        //        new SqlParameter("@LastRenewDate", ai.LastRenewDate),
        //        new SqlParameter("@ExpiryDate", ai.ExpiryDate),
        //        new SqlParameter("@Initial", ai.initial),
        //        new SqlParameter("@MonthlyCharge", ai.MonthlyCharge),
        //        new SqlParameter("@SerialNumber", ai.SerialNumber),
        //        new SqlParameter("@SiteURL", ai.siteURL),
        //        new SqlParameter("@Remarks", ai.Remarks)
       

        //            totalAffectedRows += dh.InsertUpdate("[Insoft_S_InsertUpdateCustomerwisemoduledetails]", parameters, CommandType.StoredProcedure);
        //        }

        //        return Json(totalAffectedRows);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}



        [HttpPost]
        public ActionResult getAllCustomerwiseModules([FromBody] string TokenNo)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@TokenNo",TokenNo)
                };
                string data = dh.ReadToJson("[Insoft_S_GetAllCustomerwiseModules]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public ActionResult getCustomerwiseModulesById([FromBody] CustomerwiseModules p)
        {
            try
            {
                SqlParameter[] parm = {

                    new SqlParameter("@Id",p.Id)
                };
                string data = dh.ReadToJson("[Insoft_S_GetCustomerwiseModulesById]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public ActionResult DeleteCustomerwiseModule([FromBody] CustomerwiseModules p)
        {
            try
            {
                SqlParameter[] parm = {

                    new SqlParameter("@Id",p.Id)
                };
                string data = dh.ReadToJson("[Insoft_D_SubType]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}