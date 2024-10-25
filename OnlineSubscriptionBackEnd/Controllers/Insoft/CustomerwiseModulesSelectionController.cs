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
                int totalAffectedRows = 0;
                foreach (var subProduct in ai.subProducts)
                {
                    SqlParameter[] parameters = {
                        new SqlParameter("@Id", subProduct.Id),
                        new SqlParameter("@TokenNo", ai.TokenNo),
                        new SqlParameter("@CustomerId", ai.CustomerId),
                        new SqlParameter("@ProductId", subProduct.ProductId),
                        new SqlParameter("@AgentId", ai.AgentId),
                        new SqlParameter("@SubProductId", subProduct.SubProductId),
                        new SqlParameter("@JoinDate", subProduct.JoinDate),
                        new SqlParameter("@LastRenewDate", subProduct.LastRenewDate),
                        new SqlParameter("@ExpiryDate", subProduct.ExpiryDate),
                        new SqlParameter("@Initial", ai.Initial),
                        new SqlParameter("@MonthlyCharge", subProduct.MonthlyCharge),
                        new SqlParameter("@SerialNumber", ai.SerialNumber),
                        new SqlParameter("@SiteURL", ai.SiteURL),
                        new SqlParameter("@Remarks", subProduct.Remarks)
                 };
                    totalAffectedRows += dh.InsertUpdate("[Insoft_S_InsertUpdateCustomerwisemoduledetails]", parameters, CommandType.StoredProcedure);
                }
                return Json(totalAffectedRows);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                     new SqlParameter("@TokenNo", p.TokenNo),
                    new SqlParameter("@CustomerId",p.CustomerId),
                    new SqlParameter("@ProductId",p.ProductId)

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