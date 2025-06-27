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

        public ActionResult InsertUpdate([FromBody] CustomerwiseModules ai)
        {
            try
            {
                int totalAffectedRows = 0;
                foreach (var subProduct in ai.subProducts)
                {
                    string productKey = null;
                    string customerKey = null;
                    string productGUID = null;
                    string licenseKey = null;

                    //---------------------------------------- Check if the entry exists in the database-----------------------------
                    SqlParameter[] parm = {
                                     new SqlParameter("@TokenNo", ai.TokenNo),
                                     new SqlParameter("@CustomerId", ai.CustomerId),
                                     new SqlParameter("@ProductId", subProduct.ProductId)
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
                                ExpiryDate = subProduct.ExpiryDate,
                                MachineKey = ai.MachineKey
                            };

                            var requestLicenseKey = new GenerateKeyController().ProduceValidityKey(sp);

                            if (requestLicenseKey is OkObjectResult okResult2)
                            {
                                var result = okResult2.Value as dynamic;
                                if (result != null)
                                {

                                    licenseKey = result.ValidityKey?.ToString();


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
                                ExpiryDate = subProduct.ExpiryDate
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

                    //--------------------------- Insert or update in the database----------------------------
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
                 new SqlParameter("@Sukey", ai.Sukey),
                new SqlParameter("@Remarks", subProduct.Remarks),
                new SqlParameter("@Plan", subProduct.Plan),
                new SqlParameter("@TotalPrice", ai.TotalPrice),
                new SqlParameter("@ValidityKey", customerKey),
                new SqlParameter("@LicenseKey", licenseKey),
                new SqlParameter("@MachineKey",ai.MachineKey)
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