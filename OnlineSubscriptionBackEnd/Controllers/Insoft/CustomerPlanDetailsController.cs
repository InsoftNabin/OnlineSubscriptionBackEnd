//using DataAccess;
//using Microsoft.AspNetCore.Mvc;
//using OnlineSubscriptionBackEnd.Model.Insoft;
//using System.Data.SqlClient;
//using System.Data;

//namespace OnlineSubscriptionBackEnd.Controllers.Insoft
//{
//    public class CustomerPlanDetailsController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}
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
                        new SqlParameter("@SubscriptionType",ai.SubscriptionType)
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
        public ActionResult getCustomerPlanDetails([FromBody] CustomerPlan p)
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

    }
}