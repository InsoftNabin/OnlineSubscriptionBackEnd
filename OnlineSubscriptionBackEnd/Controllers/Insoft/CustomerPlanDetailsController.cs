
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System;
using OnlineSubscriptionBackEnd.Model.Insoft;
using Newtonsoft.Json;
using System.Collections.Generic;

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

                string productKey = null;
                string customerKey = null;
                string productGUID = null;
                string licenseKey = null;

                //---------------------------------------- Check if the entry exists in the database-----------------------------
                SqlParameter[] parm = {
                                     new SqlParameter("@TokenNo", ai.TokenNo),
                                     new SqlParameter("@CustomerId", ai.CustomerId),
                                     new SqlParameter("@ProductId", ai.ProductId)
                                 };

                string data = dh.ReadToJson("[Insoft_getKeysforValidity]", parm, CommandType.StoredProcedure);

                if (!string.IsNullOrEmpty(data))
                {
                    //----------------------------------------- Deserialize the result--------------------------------------------
                    var dataList = JsonConvert.DeserializeObject<List<KeyResponse>>(data);
                    if (dataList != null && dataList.Count > 0)
                    {
                        var firstEntry = dataList[0];
                        customerKey = firstEntry.customerKey;
                        productKey = firstEntry.productKey;

                        // If customerKey is empty, generate a new one
                        if (string.IsNullOrWhiteSpace(customerKey))
                        {
                            GenerateKeyController gk = new GenerateKeyController();
                            Customer cs = new Customer { GUID = Guid.NewGuid().ToString() }; //--------was GUID=productKey initially------
                            var responseCustomerKey = gk.ProduceCustomerKey(cs);
                            if (responseCustomerKey is OkObjectResult okResult)
                            {
                                customerKey = okResult.Value?.ToString();
                            }
                            else
                            {
                                throw new Exception("Failed to generate CustomerKey. Invalid response type.");
                            }
                        }


                        // --------------------------------------------Create LicenseKey with existing keys-----------------------------------------
                        SubProduct sp = new SubProduct
                        {
                            ProductGUID = productKey,
                            clientGUID = customerKey,
                            ExpiryDate = ai.ExpiryDate,
                            MachineKey = ai.Machinekey
                        };

                        var requestLicenseKey = new GenerateKeyController().ProduceValidityKeyInitial(sp);

                        if (requestLicenseKey is OkObjectResult okResult2)
                        {
                            var result = okResult2.Value as dynamic;
                            if (result != null)
                            {

                                licenseKey = result.ToString();


                                if (!string.IsNullOrEmpty(licenseKey))
                                {

                                }
                                else
                                {
                                    throw new Exception("Failed to generate a valid license key.");
                                }
                            }
                            else
                            {
                                throw new Exception("Invalid response format.");
                            }
                        }
                        else
                        {
                            throw new Exception("Failed to generate LicenseKey. Invalid response type.");
                        }
                    }
                    else
                    {
                        // ------------------------------------------Generate new keys if no existing entry found-------------------------
                        GenerateKeyController gk = new GenerateKeyController();

                        //------------------------------------------ Generate ProductKey---------------------------------------
                        var result = gk.ProduceProductKey();
                        if (result is OkObjectResult okResult)
                        {
                            productKey = okResult.Value?.ToString();
                            productGUID = productKey;
                        }
                        else
                        {
                            throw new Exception("Failed to generate ProductKey. Invalid response type.");
                        }

                        //------------------------- Generate CustomerKey--------------------------------------------------------
                        Customer cs = new Customer { GUID = productGUID };
                        var responseCustomerKey = gk.ProduceCustomerKey(cs);
                        if (responseCustomerKey is OkObjectResult okResult1)
                        {
                            customerKey = okResult1.Value?.ToString();
                        }
                        else
                        {
                            throw new Exception("Failed to generate CustomerKey. Invalid response type.");
                        }

                        // ---------------------------------------Generate LicenseKey---------------------------
                        SubProduct sp = new SubProduct
                        {
                            ProductGUID = productGUID,
                            clientGUID = customerKey,
                            ExpiryDate = ai.ExpiryDate
                        };

                        var requestLicenseKey = gk.ProduceValidityKey(sp);
                        if (requestLicenseKey is OkObjectResult okResult2)
                        {
                            licenseKey = okResult2.Value?.ToString();
                        }
                        else
                        {
                            throw new Exception("Failed to generate LicenseKey. Invalid response type.");
                        }
                    }
                }


                SqlParameter[] parm1 = {
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
                        new SqlParameter("@fonepayTraceId",ai.fonepayTraceId),
                        new SqlParameter("@LicenseKey",licenseKey),
                        new SqlParameter("@MachineKey",ai.Machinekey)
                    };
                AffectedRows = AffectedRows + dh.InsertUpdate("[Insoft_IU_CustomerPlanDetails]", parm1, CommandType.StoredProcedure);
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