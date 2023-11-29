using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineSubscriptionBackEnd.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static com.sun.net.httpserver.Authenticator;

namespace OnlineSubscriptionBackEnd.Controllers
{
	[Route("[controller]/[action]")]
	public class EmailMessagingController : Controller
	{
		DataHandeler dh = new DataHandeler();
		[HttpPost]
		[Obsolete]
		public JsonResult SendReceiptInEmail([FromBody] EmailMessage em)
		{
			try
			{
				String Status = "";

				SqlParameter[] parm = {
					new SqlParameter("@Email", em.ReceiverEmail),
					new SqlParameter("@Code", em.Gcode),
				};
				DataTable dt = dh.ReadData("usp_Org_Adm_EmailInfoCredintial", parm, CommandType.StoredProcedure);
				if (dt.Rows[0]["Status"].ToString()== "AlreadyExists")
				{
					Status = "{\"Status\":\"AlreadyExists\"}";

                }
				else
				{
					if (dt.Rows.Count > 0)
					{

						Email email = new Email();

						//bool status=email.SendEmail(dt.Rows[0]["ActivityEmail"].ToString(), dt.Rows[0]["EmailPassword"].ToString(), dt.Rows[0]["Smtp_port"].ToString(), dt.Rows[0]["Smtp_Server"].ToString(), em[i].ReceiverEmail,em[i].Subject,em[i].EmailBody);
						bool status = email.SendEmailDynamic(dt.Rows[0]["ActivityEmail"].ToString(), dt.Rows[0]["EmailPassword"].ToString(), dt.Rows[0]["Smtp_port"].ToString(), dt.Rows[0]["Smtp_Server"].ToString(), em.ReceiverEmail, em.Subject.ToString(), em.EmailBody, "", false, false, "", "", "");
						if (status == true)
						{
							Status =  "{\"Status\":\"Success\"}";
                        }
						else
						{
							Status = "{\"Status\":\"Error\"}";

                        }
					}
					else
					{
						Status = "{\"Status\":\"Error\"}"; ;
					}
				}
				
				return Json(Status);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

        [HttpPost]
        public JsonResult GetCode([FromBody] EmailMessage em)
		{
			try
			{
				SqlParameter[] parm = {
					new SqlParameter("@email", em.ReceiverEmail),
					new SqlParameter("@Code", em.Gcode),
				};
				string Block = dh.ReadToJson("usp_S_GetcodefromEmail", parm, CommandType.StoredProcedure);
				
				return Json(Block);

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
