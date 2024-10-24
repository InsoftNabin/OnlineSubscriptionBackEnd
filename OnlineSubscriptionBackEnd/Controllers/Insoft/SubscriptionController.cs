﻿using DataAccess;
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
    public class SubscriptionController : Controller
    {
        DataHandeler dh = new DataHandeler();

        [HttpPost]
        public JsonResult InsertUpdateSubscription([FromBody] Subscription ai)
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
                        new SqlParameter("@NoOfMonths",ai.NoOfMonths)
                    };
                AffectedRows = AffectedRows + dh.InsertUpdate("[Insoft_IU_SubType]", parm, CommandType.StoredProcedure);
                return Json(AffectedRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPost]
        public ActionResult getSubscription([FromBody] string TokenNo)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@TokenNo",TokenNo)
                };
                string data = dh.ReadToJson("[Insoft_S_GetAllSubTypes]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public ActionResult getSubscriptionById([FromBody] Subscription p)
        {
            try
            {
                SqlParameter[] parm = {

                    new SqlParameter("@Id",p.Id)
                };
                string data = dh.ReadToJson("[Insoft_S_GetSubTypeById]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public ActionResult DeleteSubscription([FromBody] Subscription p)
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