using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Net;
using System;
using OnlineSubscriptionBackEnd.Model;
using System.Text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace OnlineSubscriptionBackEnd.Controllers
{
	public class Email
	{
		DataHandeler dh = new DataHandeler();
		public bool SendEmail(string SenderAddress, string SenderPassword, string Smtp_port, string smtp_server, string receiverEmail, string emailsubject, string emailbody)
		{
			bool result = false;
			try
			{
				string htele = emailbody;
				string Emailfrom = SenderAddress;
				string Emailto = receiverEmail;
				string Emailfrompassword = SenderPassword;

				var fromAddress = new MailAddress(Emailfrom);
				var toAddress = new MailAddress(Emailto);
				string fromPassword = Emailfrompassword;
				string subject = emailsubject;
				string body = htele;

				var smtp = new SmtpClient
				{
					Host = smtp_server,
					Port = Convert.ToInt16(Smtp_port),
					EnableSsl = false,
					Timeout = 5000,
					DeliveryMethod = SmtpDeliveryMethod.Network,
					UseDefaultCredentials = false,
					Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
				};
				using (var message = new MailMessage(fromAddress, toAddress)
				{
					IsBodyHtml = true,
					Subject = subject,
					Body = body
				})
				{
					smtp.Send(message);
					result = true;
				}
				return result;
			}
			catch (Exception ex)
			{
				return result;
				throw ex;
			}

		}




		public bool SendAppointmentEmail(string Fullname, string PhoneNO, string EmailAddress, string messagebody)
		{
			bool result = false;
			try
			{
				string htele = @"<html><body><h4>Hello, Greeting from " + Fullname + "</h4>" +
				@"<p>" + messagebody + "</p>" +
				@"<p style =""color :black"">Sincerely</p>" +
				@"<p style =""color :black"">" + Fullname + "</p>" +
				@"<p style =""color :black"">" + PhoneNO + "</p>" +
				@"<p style =""color :black"">" + EmailAddress + "</p>" +
				"</body></html>";

				string SenderAddress = "";
				string SenderPassword = "";
				string Smtp_port = "";
				string smtp_server = "";
				string receiverEmail = "";

				//string htele = emailbody;
				string Emailfrom = SenderAddress;
				string Emailto = receiverEmail;
				string Emailfrompassword = SenderPassword;

				var fromAddress = new MailAddress(Emailfrom);
				var toAddress = new MailAddress(Emailto);
				string fromPassword = Emailfrompassword;
				string subject = "Appointment Suscribed Email";
				string body = htele;

				var smtp = new SmtpClient
				{
					Host = smtp_server,
					Port = Convert.ToInt16(Smtp_port),
					EnableSsl = false,
					Timeout = 5000,
					DeliveryMethod = SmtpDeliveryMethod.Network,
					UseDefaultCredentials = false,
					Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
				};
				using (var message = new MailMessage(fromAddress, toAddress)
				{
					IsBodyHtml = true,
					Subject = subject,
					Body = body
				})
				{
					smtp.Send(message);
					result = true;

				}
				return result;
			}
			catch (Exception ex)
			{
				return result;
				throw ex;
			}
		}


		public bool SendDynamicEmail(SendEmailRequestDTO request)
		{
			DataTable dt1 = new DataTable();
			string _isvalid = "";
			SqlParameter[] parm = {
					new SqlParameter("@TokenNo", request.TokenNo),
					new SqlParameter("@TypeId", request.EmailType),
				};

			dt1 = dh.ReadData("usp_Adm_S_EmailTemplate", parm, CommandType.StoredProcedure);
			bool result = false;
			string receiverEmail = request.ReceiverEmail;
			int emailType = request.EmailType;
			string emailBody = string.Empty;
			string emailSubject = string.Empty;
			string EmailHeader = dt1.Rows[0]["EmailHeader"].ToString();
			bool _UseBanner = Convert.ToBoolean(dt1.Rows[0]["UseBanner"].ToString());
			bool _SendEmailBodyPDF = Convert.ToBoolean(dt1.Rows[0]["SendEmailBodyPDF"].ToString());
			string _bannerName = dt1.Rows[0]["BannerName"].ToString();

			if (dt1.Rows.Count > 0)
			{
				emailSubject = dt1.Rows[0]["EmailSubject"].ToString();
				emailBody = dt1.Rows[0]["EmailBody"].ToString();

				StringBuilder sbEmailBody = new StringBuilder(emailBody);

				if (_SendEmailBodyPDF == true)
				{
					foreach (JProperty property in request.data.Properties())
					{
						sbEmailBody.Replace("{{" + property.Name + "}}", property.Value.ToString());
					}
				}

				StringBuilder sbEmailHeader = new StringBuilder(EmailHeader);
				foreach (JProperty property in request.data.Properties())
				{
					sbEmailHeader.Replace("{{" + property.Name + "}}", property.Value.ToString());
				}

				result = SendEmailDynamic(dt1.Rows[0]["ActivityEmail"].ToString(), dt1.Rows[0]["EmailPassword"].ToString(), dt1.Rows[0]["Smtp_port"].ToString(), dt1.Rows[0]["Smtp_Server"].ToString(), receiverEmail, emailSubject, sbEmailHeader.ToString(), sbEmailBody.ToString(), _UseBanner, _SendEmailBodyPDF, _bannerName, request.AttachmentContentHTML, request.AttachmentFileName);
			}
			return result;
		}

		public bool SendEmailDynamic(string SenderAddress, string SenderPassword, string Smtp_port, string smtp_server, string receiverEmail, string emailsubject, string EmailHeader, string emailbody, bool _UseBanner, bool _SendEmailBodyPDF, string _bannerName, string AttachmentContentHTML = "", string AttachmentFileName = "")
		{
			bool result = false;
			try
			{
				//var generalSetting = _generalSetupRepository.GetGeneralSetting().FirstOrDefault();
				var smtpServer = smtp_server;
				var smtpPort = Smtp_port;
				var senderEmail = SenderAddress;
				var senderPassword = SenderPassword;

				string htele = EmailHeader;
				string Emailfrom = senderEmail;
				string Emailto = receiverEmail;
				string Emailfrompassword = senderPassword;
				var fromAddress = new MailAddress(Emailfrom);
				var toAddress = new MailAddress(Emailto);
				string fromPassword = Emailfrompassword;
				string subject = emailsubject;
				string body = htele;
				AttachmentContentHTML = emailbody;
				AttachmentFileName = "FeeStatement";


				var smtp = new SmtpClient
				{
					Host = smtpServer,
					Port = Convert.ToInt16(smtpPort),
					EnableSsl = true,
					DeliveryMethod = SmtpDeliveryMethod.Network,
					UseDefaultCredentials = true,
					Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
				};
				using (var message = new MailMessage(fromAddress, toAddress)
				{
					IsBodyHtml = true,
					Subject = subject,
					Body = body,

				})
				{
					if (_SendEmailBodyPDF == true)
					{

						if (!string.IsNullOrEmpty(AttachmentContentHTML))
						{
							StringReader sr = new StringReader(AttachmentContentHTML);
							Document pdfDoc = new Document(PageSize.A4, 5f, 5f, 10f, 0f);
							HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
							//XmlTextWriter htmlparser = new XmlTextWriter(pdfDoc);
							using (MemoryStream memoryStream = new MemoryStream())
							{
								PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
								pdfDoc.Open();
								htmlparser.Parse(sr);
								pdfDoc.Close();
								byte[] bytes = memoryStream.ToArray();
								memoryStream.Close();
								message.Attachments.Add(new Attachment(new MemoryStream(bytes), AttachmentFileName + ".pdf"));
							}

						}
					}
					if (_UseBanner == true)
					{
						var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\EmailTemplate", _bannerName);
						FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
						byte[] tmpBytes = new byte[fs.Length];
						fs.Read(tmpBytes, 0, Convert.ToInt32(fs.Length));
						MemoryStream mystream = new MemoryStream(tmpBytes);
						byte[] bytes2 = mystream.ToArray();
						message.Attachments.Add(new Attachment(new MemoryStream(bytes2), _bannerName));
					}
					smtp.Send(message);
					result = true;
				}
				return result;
			}
			catch (Exception ex)
			{
				var error = ex;
				return result;
			}

		}
	}
}
