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
    public class ProductsController : Controller
    {
        DataHandeler dh = new DataHandeler();

        [HttpPost]
        public JsonResult InsertUpdate([FromBody] Products ai)
        {
            try
            {
                GenerateKeyController gk = new GenerateKeyController();
                var result = gk.ProduceProductKey();

                string productKey = null;
                if (result is OkObjectResult okResult)
                {
                    productKey = okResult.Value?.ToString(); 
                }
                else
                {
                    throw new Exception("Failed to generate ProductKey. Invalid response type.");
                }

                int AffectedRows = 0;

                   SqlParameter[] parm = {
                        new SqlParameter("@Id", ai.Id),
                        new SqlParameter("@Name", ai.Name),
                        new SqlParameter("@Description", ai.Description),
                        new SqlParameter("@Version", ai.Version),
                        new SqlParameter("@siteURL",ai.siteURL),
                        new SqlParameter("@ProductKey", productKey) 
                    };

                AffectedRows += dh.InsertUpdate("[Insoft_IU_Product]", parm, CommandType.StoredProcedure);
                return Json(AffectedRows);
            }
            catch (Exception ex)
            {
                // Return a proper error response
                return Json(new { success = false, message = ex.Message });
            }
        }




        [HttpPost]
        public ActionResult getProducts([FromBody]  string  TokenNo)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@TokenNo",TokenNo)
                };
                string data = dh.ReadToJson("[Insoft_S_GetAllProducts]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public ActionResult getProduct([FromBody] Products p)
        {
            try
            {
                SqlParameter[] parm = {
                 
                    new SqlParameter("@Id",p.Id)
                };
                string data = dh.ReadToJson("[Insoft_S_GetProductById]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPost]
        public ActionResult GetAllProductsByCustomer([FromBody] CustomerwiseModules p)
        {
            try
            {
                SqlParameter[] parm = {

                    new SqlParameter("@TokenNo",p.TokenNo),
                    new SqlParameter("@CustomerCode",p.CustomerId)
                };
                string data = dh.ReadToJson("[Insoft_S_GetAllProductsByCustomer]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPost]
        public ActionResult DeleteProduct([FromBody] Products p)
        {
            try
            {
                SqlParameter[] parm = {
                 
                    new SqlParameter("@Id",p.Id)
                };
                string data = dh.ReadToJson("[Insoft_D_Product]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}