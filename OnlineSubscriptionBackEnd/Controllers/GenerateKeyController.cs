using Microsoft.AspNetCore.Mvc;
using OnlineSubscriptionBackEnd.Model.Insoft;
using System.Data.SqlClient;
using System.Data;
using System;
using OnlineSubscriptionBackEnd.Model;
using OnlineSubscriptionBackEnd.Controllers.Insoft;
using com.sun.org.apache.xerces.@internal.util;
using DataAccess;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace OnlineSubscriptionBackEnd.Controllers
{
    public class GenerateKeyController : Controller
    {
        DataHandeler dh = new DataHandeler();
        SubscriptionController ss=new SubscriptionController();
        public ActionResult ProduceProductKey()
        {
            try
            {
                string ProductKey = LicenseGenerator.GenerateProductKey();
                return Ok(ProductKey);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while generating the product key.");
            }
        }

        [HttpPost]
        public ActionResult ProduceCustomerKey([FromBody] Customer p)
        {
            try
            {

                string CustomerKey = LicenseGenerator.GenerateClientKey(p.GUID.ToString());
                p.CustomerKey = CustomerKey;
                return Ok(CustomerKey );

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost]
        public ActionResult ProduceValidityKeyInitial([FromBody] SubProduct subProduct)
        {
            try
            {
                // Ensure input validation
                if (string.IsNullOrEmpty(subProduct.clientGUID) ||
                    string.IsNullOrEmpty(subProduct.ProductGUID))
                {
                    return BadRequest("Invalid inputs for generating the validity key.");
                }

                // Generate the ValidityKey
                string validityKey = LicenseGenerator.GenerateValidityKey(
                subProduct.ProductGUID,
                subProduct.clientGUID,
                DateTime.Parse(subProduct.ExpiryDate),
                subProduct.MachineKey
            );

                return Ok(validityKey);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error generating validity key: {ex.Message}");
            }
        }


        [HttpPost]
        public ActionResult ProduceValidityKey([FromBody] SubProduct subProduct)
        {
            try
            {
                // Validate required inputs
                if (string.IsNullOrEmpty(subProduct.clientGUID) || string.IsNullOrEmpty(subProduct.ProductGUID))
                {
                    return BadRequest("Invalid inputs for generating the validity key.");
                }

                // Prepare SQL parameter for database call
                SqlParameter[] parm = {
            new SqlParameter("@SubscriptionGUID", subProduct.clientGUID)
        };

                // Fetch data from stored procedure
                string DataInfo = dh.ReadToJson("[usp_S_getExpiryDateFromSubsGUID]", parm, CommandType.StoredProcedure);
                string validityKey;
                DateTime expirationDate;

                if (!string.IsNullOrEmpty(DataInfo))
                {
                    // Deserialize the returned data (it should include ExpiryDate or StatusCode)
                    var result = JsonConvert.DeserializeObject<List<dynamic>>(DataInfo);

                    // Check for StatusCode value and handle accordingly
                    if (result != null && result.Count > 0)
                    {
                        if (result[0].StatusCode == 404)
                        {
                            // No active subscription, handle as fallback
                            if (!DateTime.TryParse(subProduct.ExpiryDate, out expirationDate))
                            {
                                return BadRequest("Invalid expiration date format in subProduct.ExpiryDate.");
                            }
                        }
                        else
                        {
                            // ExpiryDate found in database, use it
                            expirationDate = result[0].ExpirationDate;
                        }

                        // Generate validity key
                        validityKey = LicenseGenerator.GenerateValidityKey(
                            subProduct.ProductGUID,
                            subProduct.clientGUID,
                            expirationDate,
                            subProduct.MachineKey
                        );
                    }
                    else
                    {
                        return BadRequest("No data returned from the stored procedure.");
                    }
                }
                else
                {
                    return BadRequest("Error retrieving data from the database.");
                }

                // Decode validity key
                var (productKey, clientKey, uniqueMachineKey, decodedExpirationDate) = LicenseGenerator.DecodeValidityKey(validityKey);

                // Calculate remaining days
                int remainingDays = (decodedExpirationDate - DateTime.Now).Days;
                string formattedExpirationDate = decodedExpirationDate.ToString("yyyy/MM/dd");
                string statusMessage = remainingDays > 0 ? "Valid Subscription" : "Expired Subscription";
                int statusCode = remainingDays > 0 ? 1 : 0;

                // Return success response
                return Ok(new
                {
                    Status = statusCode,
                    Message = statusMessage,
                    ExpirationDate = formattedExpirationDate,
                    RemainingDays = remainingDays,
                    ValidityKey = validityKey,
                    UniqueMachineKey = uniqueMachineKey
                });
            }
            catch (Exception ex)
            {
                // Return error response
                return BadRequest($"Error generating validity key: {ex.Message}");
            }
        }






        [HttpPost]
        public IActionResult DecryptValidityKey([FromBody] DecryptKey request)
        {
            try
            {
                if (string.IsNullOrEmpty(request?.validityKey))
                {
                    return BadRequest("Invalid inputs provided.");
                }

                var (productKey, clientKey, UniqueMachineKey, expirationDate) = LicenseGenerator.DecodeValidityKey(request.validityKey);

                UniqueMachineKey = UniqueMachineKey ?? "";

                var remainingDays = (expirationDate - DateTime.Now).Days;

                var formattedExpirationDate = expirationDate.ToString("yyyy/MM/dd");

                string statusMessage = remainingDays > 0 ? "Valid Subscription" : "Expired Subscription";
                int statusCode = remainingDays > 0 ? 1 : 0;

                return Ok(new
                {
                    Status = statusCode,
                    ProductKey = productKey,
                    ClientKey = clientKey,
                    UniqueMachineKey = UniqueMachineKey, 
                    Message = statusMessage,
                    ExpirationDate = formattedExpirationDate,
                    RemainingDays = remainingDays
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error.", Error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CheckValidity([FromBody] DecryptKey dk)
        {
            try
            {
                if (string.IsNullOrEmpty(dk.validityKey))
                {
                    return BadRequest(new { Message = "Invalid inputs provided." });
                }
                bool isValid = LicenseValidator.IsSoftwareValid(dk.validityKey);
                if (isValid)
                {
                    return Ok(new { Message = "License is valid." });
                }
                else
                {
                    return BadRequest(new { Message = "License is invalid or expired." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error.", Error = ex.Message });
            }
        }

    }
}
