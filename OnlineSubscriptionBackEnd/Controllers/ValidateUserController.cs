using DataAccess;
using DataProvider;
using Microsoft.AspNetCore.Mvc;
using System;

namespace OnlineSubscriptionBackEnd.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class ValidateUserController : Controller
    {
        [HttpPost]
        public JsonResult ValidateUser([FromBody] LoginValidator lv)
        {
            try
            {
                LoginValidation lv1 = new LoginValidation();
                SelectLoginInfo li = lv1.GetUserValidation(lv);
                return Json(li);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPost]
        public JsonResult ValidateUserExternallink([FromBody] LoginValidator lv)
        {
            try
            {
                LoginValidation lv1 = new LoginValidation();
                SelectLoginInfo li = lv1.GetUserValidationWithExternalLink(lv);
                return Json(li);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public JsonResult ValidateAdmin([FromBody] LoginValidator lv)
        {
            try
            {
                LoginValidation lv1 = new LoginValidation();
                SelectLoginInfo li = lv1.GetAdminValidation(lv);
                return Json(li);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
