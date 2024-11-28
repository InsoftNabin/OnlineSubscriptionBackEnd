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

                string CustomerKey = LicenseGenerator.GenerateClientKey(p.Id.ToString());
                p.CustomerKey = CustomerKey;
                return Ok(new { CustomerKey = p.CustomerKey });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult ProduceValidityKey([FromBody] ValidityKey vk)
        {
            try
            {
                string validityKey = LicenseGenerator.GenerateValidityKey(vk.ProductKey,vk.ClientKey,vk.ExpirationDate);
                return Ok(new { validityKey });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




    }
}
