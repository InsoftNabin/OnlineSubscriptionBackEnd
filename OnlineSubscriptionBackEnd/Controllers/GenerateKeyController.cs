using Microsoft.AspNetCore.Mvc;
using OnlineSubscriptionBackEnd.Model.Insoft;
using System.Data.SqlClient;
using System.Data;
using System;
using OnlineSubscriptionBackEnd.Model;

namespace OnlineSubscriptionBackEnd.Controllers
{
    public class GenerateKeyController : Controller
    {
        
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

        //[HttpPost]
        //public ActionResult ProduceValidityKey([FromBody] ValidityKey vk)
        //{
        //    try
        //    {

        //        if (string.IsNullOrEmpty(vk.ProductKey) || string.IsNullOrEmpty(vk.ClientKey) ||  vk.ExpirationDate == default)
        //        {
        //            return BadRequest("Invalid inputs provided.");
        //        }

        //        string validityKey = LicenseGenerator.GenerateValidityKey(vk.ProductKey, vk.ClientKey, vk.ExpirationDate, vk.UniqueMachineKey);
        //        return Ok(new { validityKey });
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}




        [HttpPost]
        public ActionResult ProduceValidityKey([FromBody] SubProduct subProduct)
        {
            try
            {
                // Ensure input validation
                if (string.IsNullOrEmpty(subProduct.clientGUID) ||
                    string.IsNullOrEmpty(subProduct.ProductGUID) )
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
        public IActionResult DecryptValidityKey([FromBody] DecryptKey request)
        {
            try
            {
                if (string.IsNullOrEmpty(request?.validityKey))
                {
                    return BadRequest("Invalid inputs provided.");
                }

                var (productKey, clientKey, UniqueMachineKey, expirationDate) = LicenseGenerator.DecodeValidityKey(request.validityKey);

                return Ok(new
                {
                    ProductKey = productKey,
                    ClientKey = clientKey,
                    UniqueMachineKey = UniqueMachineKey,
                    ExpirationDate = expirationDate
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
