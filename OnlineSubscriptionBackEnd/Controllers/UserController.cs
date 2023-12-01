using Microsoft.AspNetCore.Mvc;
using OnlineSubscriptionBackEnd.Model;
using System.Data.SqlClient;
using System.Data;
using DataAccess;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        [HttpPost]
        public JsonResult AddOrganization([FromBody] OrgDetails em)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@TokenNo", em.TokenNo),
                    new SqlParameter("@CompanyName", em.CompanyName),
                    new SqlParameter("@Module", em.Module),
                    new SqlParameter("@DisplayName", em.DisplayName),
                    new SqlParameter("@Initial", em.Initial),
                    new SqlParameter("@PanVatNo", em.PanVatNo),
                    new SqlParameter("@Address", em.Address),
                    new SqlParameter("@PhoneNo", em.PhoneNo),
                    new SqlParameter("@ContactMobile", em.ContactMobile),
                    new SqlParameter("@OrganizationMail", em.OrganizationMail),
                    new SqlParameter("@OrganizationMotto", em.OrganizationMotto),
                    new SqlParameter("@Website", em.Website),
                    new SqlParameter("@ImageName", em.ImageName),
                    new SqlParameter("@ImageData", em.ImageData),
                    new SqlParameter("@Token", em.Token)
                };
                int Block = dh.InsertUpdate("Usp_I_OrganizationDetails", parm, CommandType.StoredProcedure);
                return Json(Block);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult GetAllOrganizations([FromBody] OrgDetails em)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@TokenNo", em.TokenNo)
                };
                int Block = dh.InsertUpdate("Usp_s_OrganizationDetails", parm, CommandType.StoredProcedure);
                return Json(Block);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public JsonResult GetOrganizationsById([FromBody] OrgDetails em)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@TokenNo", em.TokenNo),
                    new SqlParameter("@Token", em.Token)
                };
                int Block = dh.InsertUpdate("Usp_s_OrganizationDetailsById", parm, CommandType.StoredProcedure);
                return Json(Block);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult TrashOrganizationsById([FromBody] OrgDetails em)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@TokenNo", em.TokenNo),
                    new SqlParameter("@Token", em.Token)
                };
                int Block = dh.InsertUpdate("Usp_D_OrganaizationDetails", parm, CommandType.StoredProcedure);
                return Json(Block);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public JsonResult GetModules([FromBody] OrgDetails em)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@TokenNo", em.TokenNo)
                };
                int Block = dh.InsertUpdate("Usp_S_Modules", parm, CommandType.StoredProcedure);
                return Json(Block);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
