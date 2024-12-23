using Microsoft.AspNetCore.Mvc;
using OnlineSubscriptionBackEnd.Model.Insoft;
using System.Data.SqlClient;
using System.Data;
using DataAccess;
using System;

namespace OnlineSubscriptionBackEnd.Controllers.Insoft
{
    public class SubscriptionReportController : Controller
    {
        DataHandeler dh = new DataHandeler();

        [HttpPost]
        public ActionResult getSubscriptionReportAdmin([FromBody] SubscriptionReport sr)
        {
            try
            {
                SqlParameter[] parm = {

                    new SqlParameter("@TokenNo",sr.TokenNo),
                    new SqlParameter("@RemainingDays",sr.RemainingDays)

                };
                string data = dh.ReadToJson("[Insoft_ReportCustomerSubs]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult getCustomerStatement([FromBody] Customer sr)
        {
            try
            {
                SqlParameter[] parm = {

                    new SqlParameter("@TokenNo",sr.TokenNo),
                    new SqlParameter("@Customercode",sr.Id)

                };
                string data = dh.ReadToJson("[Insoft_CustomerStatement]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult getRecentTransactions([FromBody] string TokenNo)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@TokenNo",TokenNo)
                };
                string data = dh.ReadToJson("[Insoft_S_getRecentTransaction]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult getCustomersandKeysbyProductTypes([FromBody] SubscriptionReport p)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@TokenNo",p.TokenNo),
                    new SqlParameter("@ProductId",p.ProductId)
                };
                string data = dh.ReadToJson("[Insoft_S_GetCustomerandKeysByProduct]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
