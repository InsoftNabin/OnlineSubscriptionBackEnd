using Microsoft.AspNetCore.Mvc;
using OnlineSubscriptionBackEnd.Model;
using System.Data.SqlClient;
using System.Data;
using DataAccess;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DataProvider;
using System.Collections.Generic;

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
                    new SqlParameter("@ImageType", em.ImageType),
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
                string Block = dh.ReadToJson("Usp_s_OrganizationDetails", parm, CommandType.StoredProcedure);

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
                string Block = dh.ReadToJson("Usp_s_OrganizationDetailsById", parm, CommandType.StoredProcedure);

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
                string Block = dh.ReadToJson("Usp_S_Modules", parm, CommandType.StoredProcedure);

                return Json(Block);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult GetUserDetails([FromBody] String TokenNo)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@TokenNo", TokenNo)
                };
                string Block = dh.ReadToJson("Usp_S_userDetail", parm, CommandType.StoredProcedure);
                return Json(Block);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult GetAllRegisteredName([FromBody] String TokenNo)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@TokenNo", TokenNo)
                };
                string Block = dh.ReadToJson("Usp_GetAllRegisteredName", parm, CommandType.StoredProcedure);
                return Json(Block);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public JsonResult GetAllRegisteredOrgByCustomerId([FromBody] OrgDetails OD)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@TokenNo", OD.TokenNo),
                    new SqlParameter("@CustomerId", OD.CustomerId),
                };
                string Block = dh.ReadToJson("Usp_GetAllRegisteredOrgByCustomerId", parm, CommandType.StoredProcedure);
                return Json(Block);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult UpdateOrgdetailsByAdmin([FromBody] UpdateOrg OD)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@TokenNo", OD.TokenNo),
                    new SqlParameter("@Id", OD.Token),
                    new SqlParameter("@UserName", OD.SiteUserName),
                    new SqlParameter("@Password", OD.SitePassword),
                    new SqlParameter("@SiteUrl", OD.SiteURL),
                    new SqlParameter("@Remarks", OD.Remarks),
                };
                int Block = dh.Update("Usp_U_OrgdetailsByAdmin", parm, CommandType.StoredProcedure);
                return Json(Block);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult TrashOrgdetailsByAdmin([FromBody] UpdateOrg OD)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@TokenNo", OD.TokenNo),
                    new SqlParameter("@Id", OD.Token)
                };
                int Block = dh.Update("Usp_D_Orgdetails", parm, CommandType.StoredProcedure);
                return Json(Block);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public class HostedFTPInfo
        {
            [JsonProperty("hostedFTP")]
            public string HostedFTP { get; set; }
        }
        [HttpPost]
        public JsonResult FtpDetails()
        {
            try
            {
                SqlParameter[] parm = {
                     new SqlParameter("@TokenNo", "aa"),
                };
                string DataInfo = dh.ReadToJson("[usp_S_GetSchoolAndFtpInfo]", parm, CommandType.StoredProcedure);
                List<HostedFTPInfo> hostedFTPList = JsonConvert.DeserializeObject<List<HostedFTPInfo>>(DataInfo);

                // Access the value
                string hostedFTP = hostedFTPList[0].HostedFTP;

                return Json(hostedFTP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Obsolete]
        public JsonResult SendEmail([FromBody] EmailMessage em)
        {
            try
            {
                String Status = "";

                SqlParameter[] parm = {
                    new SqlParameter("@TokenNo", em.TokenNo)
                };
                DataTable dt = dh.ReadData("usp_Org_Adm_EmailInfo", parm, CommandType.StoredProcedure);
               
                        Email email = new Email();

                        //bool status=email.SendEmail(dt.Rows[0]["ActivityEmail"].ToString(), dt.Rows[0]["EmailPassword"].ToString(), dt.Rows[0]["Smtp_port"].ToString(), dt.Rows[0]["Smtp_Server"].ToString(), em[i].ReceiverEmail,em[i].Subject,em[i].EmailBody);
                        bool status = email.SendEmailDynamic(dt.Rows[0]["ActivityEmail"].ToString(), dt.Rows[0]["EmailPassword"].ToString(), dt.Rows[0]["Smtp_port"].ToString(), dt.Rows[0]["Smtp_Server"].ToString(), em.ReceiverEmail, em.Subject.ToString(), em.EmailBody, "", false, false, "", "", "");
                        if (status == true)
                        {
                            Status = "{\"Status\":\"Success\"}";
                        }
                        else
                        {
                            Status = "{\"Status\":\"Error\"}";

                        }
              

                return Json(Status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
