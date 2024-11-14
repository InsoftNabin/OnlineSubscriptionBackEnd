
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System;
using OnlineSubscriptionBackEnd.Model.Insoft;

namespace OnlineSubscriptionBackEnd.Controllers.Insoft
{
    [Route("[controller]/[action]")]
    public class CustomerPlanDetailsController : Controller
    {
        DataHandeler dh = new DataHandeler();

        [HttpPost]
        public JsonResult InsertUpdate([FromBody] CustomerPlan ai)
        {
            try
            {
                int AffectedRows = 0;
                SqlParameter[] parm = {
                        new SqlParameter("@CustomerId",ai.CustomerId),
                        new SqlParameter("@ProductId",ai.ProductId),
                        new SqlParameter("@ExpiryDate",ai.ExpiryDate),
                        new SqlParameter("@TransactionId",ai.TransactionId),
                        new SqlParameter("@Remarks",ai.Remarks),
                        new SqlParameter("@PaymentMethod",ai.PaymentMethod),
                        new SqlParameter("@ReferenceId",ai.ReferenceId),
                        new SqlParameter("@PaymentPartner",ai.PaymentPartner),
                        new SqlParameter("@PaidAmount",ai.PaidAmount),
                        new SqlParameter("@GeneratedSerialNo",ai.GeneratedSerialNo),
                        new SqlParameter("@SubscriptionType",ai.SubscriptionType),
                        new SqlParameter("@VoucherImage",ai.VoucherImage),
                        new SqlParameter("@IsVerifiedPayment",ai.IsVerifiedPayment),
                        new SqlParameter("@fonepayTraceId",ai.fonepayTraceId)
                    };
                AffectedRows = AffectedRows + dh.InsertUpdate("[Insoft_IU_CustomerPlanDetails]", parm, CommandType.StoredProcedure);
                return Json(AffectedRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
        [HttpPost]
        public ActionResult getCurrentPlan([FromBody] CustomerPlan p)
        {
            try
            {
                SqlParameter[] parm = {
                    
                    new SqlParameter("@CustomerId",p.CustomerId),
                    new SqlParameter("@ProductId",p.ProductId)

                };
                string data = dh.ReadToJson("[Insoft_S_CustomerPlanDetails]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public ActionResult getCustomerSelectedPlansOnly([FromBody] CustomerPlan p)
        {
            try
            {
                SqlParameter[] parm = {

                    new SqlParameter("@CustomerId",p.CustomerId),
                    new SqlParameter("@ProductId",p.ProductId)

                };
                string data = dh.ReadToJson("Insoft_S_CustomerselectedPlans", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}