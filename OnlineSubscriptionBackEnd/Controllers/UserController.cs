using Microsoft.AspNetCore.Mvc;
using OnlineSubscriptionBackEnd.Model;
using System.Data.SqlClient;
using System.Data;
using DataAccess;
using System;

namespace OnlineSubscriptionBackEnd.Controllers
{
    public class UserController : Controller
    {
        DataHandeler dh = new DataHandeler();
        [HttpPost]
        public JsonResult Register([FromBody] UserInfo em)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@Email", em.Email),
                    new SqlParameter("@Name", em.Name),
                    new SqlParameter("@Address", em.Address),
                    new SqlParameter("@Contact", em.Contact),
                    new SqlParameter("@Password", em.Password),
                };
                int Block = dh.InsertUpdate("usp_U_UserInfo", parm, CommandType.StoredProcedure);

                return Json(Block);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
