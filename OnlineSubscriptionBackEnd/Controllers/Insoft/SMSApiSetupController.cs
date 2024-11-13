using DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using OnlineSubscriptionBackEnd.Model.Insoft;
using System;

namespace OnlineSubscriptionBackEnd.Controllers.Insoft
{
    public class SMSApiSetupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        DataHandeler dh = new DataHandeler();

        [HttpPost]
        public JsonResult InsertUpdate([FromBody] SMSSetup ai)
        {
            try
            {
                int AffectedRows = 0;
               
                SqlParameter[] parm = {
                    new SqlParameter   ("@TokenNo",ai.TokenNo),
                     new SqlParameter("@SenderId",ai.senderId),
                     new SqlParameter("@SMSApiToken",ai.SMSApiToken)
                    };
                AffectedRows = AffectedRows + dh.InsertUpdate("[Insoft_IU_SMSApiSetup]", parm, CommandType.StoredProcedure);
                return Json(AffectedRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
