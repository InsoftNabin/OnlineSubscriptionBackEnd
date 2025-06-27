using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using OnlineSubscriptionBackEnd.Model.Insoft;

using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using OnlineSubscriptionBackEnd.Model;
using System.Text;


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
                        new SqlParameter("@NoOfMonths",ai.NoOfMonths),
                        new SqlParameter("@IsPaidBased",ai.IsPaidBased),
                        new SqlParameter("@IsTrial",ai.IsTrial)
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
        public JsonResult SubsbyCustProdandType([FromBody] List<CustomerwiseModules> ai)
        {
            try
            {
                int affectedRows = 0;

                foreach (var item in ai)
                {
                    SqlParameter[] parm = {
                new SqlParameter("@CustomerId", item.CustomerId),
                new SqlParameter("@ProductId", item.ProductId),
                new SqlParameter("@SubscriptionType", item.AgentId),
                new SqlParameter("@Active", item.Active)
                     };

                    affectedRows += dh.InsertUpdate("[Insoft_IU_CustomerandProductSubscriptionType]", parm, CommandType.StoredProcedure);
                }

                return Json(affectedRows);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult SubsbyCustProdandTypeInitially([FromBody] CustomerwiseModules item)
        {
            try
            {
                int affectedRows = 0;


                SqlParameter[] parm = {
            new SqlParameter("@CustomerId", item.CustomerId),
            new SqlParameter("@ProductId", item.ProductId),
            new SqlParameter("@SubscriptionType", item.AgentId),
            new SqlParameter("@Active", item.Active)
        };


                affectedRows = dh.InsertUpdate("[Insoft_IU_CustomerandProductSubscriptionTypeInitial]", parm, CommandType.StoredProcedure);
                return Json(new { success = true, affectedRows });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
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
        public ActionResult getVouchersForVerification([FromBody] AllActivites aa)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@TokenNo",aa.TokenNo),
                    new SqlParameter("@Product",aa.Product),
                    new SqlParameter("@PaymentMode",aa.PaymentMode)
                };
                string data = dh.ReadToJson("[Insoft_S_GetAllVouchersForVerification]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPost]
        public ActionResult VerifyorRejectSubscription([FromBody] CustomerPlan p)
        {
            try
            {
                SqlParameter[] parm = {

                    new SqlParameter("@ukid",p.ProductId),
                    new SqlParameter ("@IsVerifiedPayment",p.CustomerId),
                    new SqlParameter("@Remarks",p.Remarks),
                    new SqlParameter("@ActualExpiryDate",p.ActualExpiryDate)

                };
                string data = dh.ReadToJson("[Insoft_IU_SubVerification]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public ActionResult GetSubscriptionLogByCustandprodId([FromBody] CustomerPlan p)
        {
            try
            {
                SqlParameter[] parm = {

                    new SqlParameter("@ProductId",p.ProductId),
                    new SqlParameter ("@CustomerId",p.CustomerId)

                };
                string data = dh.ReadToJson("[Insoft_S_GetSubscriptionLog]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult GetSerialKeyLog([FromBody] CustomerPlan p)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@ProductId",p.ProductId),
                    new SqlParameter ("@CustomerId",p.CustomerId)
                };
                string data = dh.ReadToJson("[Insoft_s_serialkeys]", parm, CommandType.StoredProcedure);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Getcustomerbyskey([FromBody] SerialKey p)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@subkeytype",p.SKey),
                };
                string data = dh.ReadToJson("[Insoft_s_customerbyserialkey]", parm, CommandType.StoredProcedure);
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





        //[HttpPost]
        //public IActionResult ValidateSubscription([FromBody] ApiRequest ar /*string TokenNo,int Program,int Semester,int Sec,string SubjectCode, int TermId*/)
        //{
        //    try
        //    {
        //        if (ar.validityKey!=null)
        //        {
        //            GenerateKeyController gk = new GenerateKeyController();


        //        }
        //        else
        //        {
        //            StringBuilder Sb = new StringBuilder();
        //            var jsonstring = "";
        //            DataTable dt = new DataTable();

        //            SqlParameter[] parm =
        //            {
        //            new SqlParameter("@CustomerId",ar.CustomerId),
        //            new SqlParameter("@ProductId",ar.ProductId)
        //        };

        //            string data = dh.ReadToJson("[Insoft_S_ValidateSubscription]", parm, CommandType.StoredProcedure);

        //            List<SubscriptionResponse> student = JsonConvert.DeserializeObject<List<SubscriptionResponse>>(data);
        //            if (student.Count > 0)
        //            {
        //                if (student[0].Status == "200")
        //                {
        //                    ResponceModel rm = new ResponceModel
        //                    {
        //                        //data = student
        //                        Status = student[0].Status,
        //                        Message = student[0].Message,
        //                        ExpireDate = student[0].ExpireDate,
        //                        RemainingDays = student[0].RemainingDays,
        //                        LandingPage = student[0].LandingPage
        //                    };
        //                    return StatusCode(StatusCodes.Status200OK, rm);
        //                }
        //                else
        //                {
        //                    ResponceModel rm = new ResponceModel
        //                    {
        //                        Status = student[0].Status,
        //                        Message = student[0].Message,
        //                        ExpireDate = student[0].ExpireDate,
        //                        RemainingDays = student[0].RemainingDays,
        //                        LandingPage = student[0].LandingPage
        //                    };

        //                    return StatusCode(StatusCodes.Status200OK, rm);
        //                }
        //            }
        //            else
        //            {
        //                Sb.Append("{\"status\":404,\"message\":\"Data not found\"}");
        //                jsonstring = Sb.ToString();
        //                JObject myObj = (JObject)JsonConvert.DeserializeObject(jsonstring);
        //                //return Json(myObj);
        //                return StatusCode(StatusCodes.Status404NotFound, myObj);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ResponceModel rm = new ResponceModel();
        //        rm.Status = "417";
        //        rm.Message = ex.ToString();
        //        return StatusCode(StatusCodes.Status417ExpectationFailed, rm);
        //    }
        //}



        [HttpPost]
        public IActionResult ValidateSubscriptionwiithCustomerandProduct([FromBody] ApiRequest ar)
        {
            try
            {
                SqlParameter[] parm;

                if (!string.IsNullOrEmpty(ar.UniqueMachineCode))
                {
                    parm = new SqlParameter[]
                    {
                new SqlParameter("@CustomerGUIDId", ar.CustomerId),
                new SqlParameter("@ProductGUIDId", ar.ProductId),
                new SqlParameter("@UniqueMachineKey", ar.UniqueMachineCode)
                    };
                }
                else
                {
                    parm = new SqlParameter[]
                    {
                new SqlParameter("@CustomerGUIDId", ar.CustomerId),
                new SqlParameter("@ProductGUIDId", ar.ProductId)
                    };
                }

                return FetchSubscriptionDetailsCandP(parm);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status417ExpectationFailed, new ResponceModel
                {
                    Status = "417",
                    Message = ex.Message
                });
            }
        }


        [HttpPost]
        public IActionResult ValidateSubscription([FromBody] ApiRequest ar)
        {
            try
            {
                if (!string.IsNullOrEmpty(ar.subscriptionGUID))
                {
                    //GenerateKeyController gk = new GenerateKeyController();
                    //var actionResult = gk.DecryptValidityKey(new DecryptKey { validityKey = ar.validityKey });

                    //if (actionResult is OkObjectResult okResult && okResult.Value != null)
                    //{
                    //    var decodedData = okResult.Value;
                    //    var vk = JsonConvert.DeserializeObject<ValidityKey>(JsonConvert.SerializeObject(decodedData));

                    //SqlParameter[] parm =
                    //{
                    //     new SqlParameter("@CustomerId",vk.ProductKey ),
                    //     new SqlParameter("@ProductId", vk.ClientKey),
                    //     new SqlParameter("@UniqueMachineKey", ar.UniqueMachineCode)
                    //};

                    SqlParameter[] parm =
                    {
                            new SqlParameter("@SubscriptionGUID", ar.subscriptionGUID)
                        };


                    return FetchSubscriptionDetails(parm);
                }
                else
                {
                    return BadRequest(new { Status = "400", Message = "Failed to decode validity key." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status417ExpectationFailed, new ResponceModel
                {
                    Status = "417",
                    Message = ex.Message
                });
            }
        }

        private IActionResult FetchSubscriptionDetails(SqlParameter[] parm)
        {
            try
            {
                string data = dh.ReadToJson("[Insoft_S_ValidateSubscription]", parm, CommandType.StoredProcedure);

                List<SubscriptionResponse> student = JsonConvert.DeserializeObject<List<SubscriptionResponse>>(data);
                if (student.Count > 0)
                {
                    var subscription = student[0];
                    ResponceModel rm = new ResponceModel
                    {
                        Status = subscription.Status,
                        Message = subscription.Message,
                        ExpireDate = subscription.ExpireDate,
                        RemainingDays = subscription.RemainingDays,
                        LandingPage = subscription.LandingPage
                    };

                    return StatusCode(StatusCodes.Status200OK, rm);
                }
                else
                {
                    return NotFound(new { Status = "404", Message = "Data not found." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Failed to fetch subscription details.", Error = ex.Message });
            }
        }


        private IActionResult FetchSubscriptionDetailsCandP(SqlParameter[] parm)
        {
            try
            {
                string data = dh.ReadToJson("[Insoft_S_ValidateSubscriptionwithCustomer&Product]", parm, CommandType.StoredProcedure);

                List<SubscriptionResponse> student = JsonConvert.DeserializeObject<List<SubscriptionResponse>>(data);
                if (student.Count > 0)
                {
                    var subscription = student[0];
                    ResponceModel rm = new ResponceModel
                    {
                        Status = subscription.Status,
                        Message = subscription.Message,
                        ExpireDate = subscription.ExpireDate,
                        RemainingDays = subscription.RemainingDays,
                        LandingPage = subscription.LandingPage
                    };

                    return StatusCode(StatusCodes.Status200OK, rm);
                }
                else
                {
                    return NotFound(new { Status = "404", Message = "Data not found." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Failed to fetch subscription details.", Error = ex.Message });
            }
        }




    }
}