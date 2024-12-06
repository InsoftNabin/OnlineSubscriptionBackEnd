using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using OnlineSubscriptionBackEnd.Model.Insoft;

using Newtonsoft.Json.Linq;
using OnlineSubscriptionBackEnd.Model;


namespace OnlineSubscriptionBackEnd.Controllers.Insoft
{
    [Route("[controller]/[action]")]
    public class CustomerwiseModulesSelectionController : Controller
    {
        DataHandeler dh = new DataHandeler();

        //[HttpPost]
        //public ActionResult InsertUpdate([FromBody] CustomerwiseModules ai)
        //{
        //    try
        //    {
        //        ValidityKey vk = new ValidityKey();


        //        GenerateKeyController gk = new GenerateKeyController();

        //        var result = gk.ProduceValidityKey(SubProduct sp);

        //        string Key = null;
        //        if (result is OkObjectResult okResult)
        //        {
        //            Key = okResult.Value?.ToString();

        //        }



        //        int totalAffectedRows = 0;
        //        foreach (var subProduct in ai.subProducts)
        //        {
        //            SqlParameter[] parameters = {
        //                new SqlParameter("@Id", subProduct.Id),
        //                new SqlParameter("@TokenNo", ai.TokenNo),
        //                new SqlParameter("@CustomerId", ai.CustomerId),
        //                new SqlParameter("@ProductId", subProduct.ProductId),
        //                new SqlParameter("@AgentId", ai.AgentId),
        //                new SqlParameter("@SubProductId", subProduct.SubProductId),
        //                new SqlParameter("@JoinDate", subProduct.JoinDate),
        //                new SqlParameter("@LastRenewDate", subProduct.LastRenewDate),
        //                new SqlParameter("@ExpiryDate", subProduct.ExpiryDate),
        //                new SqlParameter("@Initial", ai.Initial),
        //                new SqlParameter("@MonthlyCharge", subProduct.MonthlyCharge),
        //                new SqlParameter("@SerialNumber", ai.SerialNumber),
        //                new SqlParameter("@SiteURL", ai.SiteURL),
        //                new SqlParameter("@Remarks", subProduct.Remarks),
        //                new SqlParameter("@Plan", subProduct.Plan),
        //                new SqlParameter("@TotalPrice",ai.TotalPrice)

        //         };
        //            totalAffectedRows += dh.InsertUpdate("[Insoft_IU_InsertUpdateCustomerwisemoduledetails]", parameters, CommandType.StoredProcedure);
        //        }
        //        return Json(totalAffectedRows);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPost]
        public ActionResult InsertUpdate([FromBody] CustomerwiseModules ai)
        {
            try
            {
                // Total affected rows counter
                int totalAffectedRows = 0;

                // Iterate through SubProducts
                foreach (var subProduct in ai.subProducts)
                {
                    GenerateKeyController gk = new GenerateKeyController();


                    var result = gk.ProduceProductKey();
                    string Key = null;
                    string GUID="";
                    string UniqCustomerKey;

                    if (result is OkObjectResult okResult)
                    {
                        Key = okResult.Value?.ToString();
                        GUID = Key;
                    }

                    Customer cs = new Customer();
                    cs.GUID = GUID;


                    var responseCustomerKey = gk.ProduceCustomerKey(cs);

                    string CtKey = null;
                    if (responseCustomerKey is OkObjectResult okResult1)
                    {
                        CtKey = okResult1.Value?.ToString();
                        UniqCustomerKey = CtKey;
                    }
                    else
                    {
                        throw new Exception("Failed to generate ProductKey. Invalid response type.");
                    }




                    // Insert or update in the database
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
                new SqlParameter("@Remarks", subProduct.Remarks),
                new SqlParameter("@Plan", subProduct.Plan),
                new SqlParameter("@TotalPrice", ai.TotalPrice),
                new SqlParameter("@ValidityKey", UniqCustomerKey),
                
            };

                    totalAffectedRows += dh.InsertUpdate("[Insoft_IU_InsertUpdateCustomerwisemoduledetails]", parameters, CommandType.StoredProcedure);
                }

                return Json(totalAffectedRows);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


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