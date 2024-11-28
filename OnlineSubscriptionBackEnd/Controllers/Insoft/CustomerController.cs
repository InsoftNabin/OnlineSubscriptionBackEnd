using DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System;
using OnlineSubscriptionBackEnd.Model.Insoft;
using OnlineSubscriptionBackEnd.Model;

namespace OnlineSubscriptionBackEnd.Controllers.Insoft
{
    [Route("[controller]/[action]")]
    public class CustomerController : Controller
    {
        DataHandeler dh = new DataHandeler();

        [HttpPost]
        public JsonResult InsertUpdateCustomer([FromBody] Customer ai)
        {
            try
            {
                int AffectedRows = 0;
                //string conn = "";
                //conn = dh.ByToken(ai.TokenNo);
                SqlParameter[] parm = {
                        //new SqlParameter("@TokenNo",ai.TokenNo),
                        new SqlParameter("@Id",ai.Id),
                        new SqlParameter("@customercode",ai.CustomerCode),
                        new SqlParameter("@customername",ai.CustomerName),
                        new SqlParameter("@address",ai.Address),
                        new SqlParameter("@panvat",ai.panvatno),
                        new SqlParameter("@contact",ai.Contact),
                        new SqlParameter("@databaselink",ai.DataBaseLink),
                        new SqlParameter("@contactperson1",ai.ContactPerson1),
                        new SqlParameter("@contactperson2",ai.ContactPerson2),
                        new SqlParameter("@contactperson1mobno",ai.ContactPerson1_MobileNo),
                        new SqlParameter("@contactperson2mobno",ai.ContactPerson2_MobileNo),
                        new SqlParameter("@email",ai.EmailAddress),
                        new SqlParameter("@website",ai.Website),
                        new SqlParameter("@GUID",ai.GUID),
                        new SqlParameter("@CompanyCode",ai.CompanyCode),
                        new SqlParameter("@BySMSApiToken",ai.BySMSApiToken),
                        //new SqlParameter("@AddedBy",ai.AddedBy)
                    };
                AffectedRows = AffectedRows + dh.InsertUpdate("Insoft_IU_Customer", parm, CommandType.StoredProcedure);
                return Json(AffectedRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPost]
        public ActionResult getAgentIdwithToken([FromBody] string tokenNo)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@Token",tokenNo)
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
        public ActionResult getAllCustomergetAgentIdwithTokenwiseModules([FromBody] string TokenNo)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@TokenNo",TokenNo)
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
        public ActionResult getSubsbyCusandProductIdWithUnpaidType([FromBody] CustomerwiseModules p)
        {
            try
            {
                SqlParameter[] parm = {
                    //new SqlParameter("@Id",p.Id),
                    new SqlParameter("@ProductId",p.ProductId),
                    new SqlParameter("@CustomerId",p.CustomerId),
                };
                string data = dh.ReadToJson("[Insoft_S_CustomerandProductSubscriptionTypeWithUnpaidType]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult getSubsbyCusandProductIdWithUnpaidTypeforAgent([FromBody] CustomerwiseModules p)
        {
            try
            {
                SqlParameter[] parm = {
                    //new SqlParameter("@Id",p.Id),
                    new SqlParameter("@ProductId",p.ProductId),
                    new SqlParameter("@CustomerId",p.CustomerId),
                };
                string data = dh.ReadToJson("[Agent_S_CustomerandProductSubscriptionTypeWithUnpaidType]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPost]
        public ActionResult getCountries([FromBody] string TokenNo)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@TokenNo",TokenNo)
                };
                string data = dh.ReadToJson("[Insoft_S_GetAllCountries]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult getCustomers([FromBody] string TokenNo)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@TokenNo",TokenNo)
                };
                string data = dh.ReadToJson("Insoft_S_GetAllCustomer", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public ActionResult getInvoiceForPrint([FromBody] InvoiceData id)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@Ukid",id.unqId) 
                };
                string data = dh.ReadToJson("[Customer_S_LogForPrint]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult getCustomerById([FromBody] Customer p)
        {
            try
            {
                SqlParameter[] parm = {

                    new SqlParameter("@Id",p.Id)
                };
                string data = dh.ReadToJson("Insoft_S_GetCustomerById", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult getCustomerByProductId([FromBody] Customer p)
        {
            try
            {
                SqlParameter[] parm = {

                    new SqlParameter("@ProductId",p.ProductId)
                };
                string data = dh.ReadToJson("[Insoft_S_GetCustomerByProductId]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult getCustSubsc([FromBody] Customer p)
        {
            try
            {
                SqlParameter[] parm = {

                    new SqlParameter("@Id",p.Id)
                };
                string data = dh.ReadToJson("[Insoft_S_GetCustomerSubs]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult getCustomerByAgentId([FromBody] Customer p)
        {
            try
            {
                SqlParameter[] parm = {

                    new SqlParameter("@Id",p.Id)
                };
                string data = dh.ReadToJson("Insoft_S_GetCustomerByAgentId", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult getCustomerByAgentandProductId([FromBody] Customer p)
        {
            try
            {
                SqlParameter[] parm = {

                    new SqlParameter("@Id",p.Id),
                    new SqlParameter("@ProductId",p.ProductId)
                };
                string data = dh.ReadToJson("[Agent_S_GetCustomerByProductId]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        [HttpPost]
        public ActionResult getSubsbyCusandProductId([FromBody] CustomerwiseModules p)
        {
            try
            {
                SqlParameter[] parm = {
                    //new SqlParameter("@Id",p.Id),
                    new SqlParameter("@ProductId",p.ProductId),
                    new SqlParameter("@CustomerId",p.CustomerId),

                };
                string data = dh.ReadToJson("[Insoft_S_CustomerandProductSubscriptionType]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult DeleteCustomer([FromBody] Customer p)
        {
            try
            {
                SqlParameter[] parm = {

                    new SqlParameter("@Id",p.Id)
                };
                string data = dh.ReadToJson("Insoft_D_Customer", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}