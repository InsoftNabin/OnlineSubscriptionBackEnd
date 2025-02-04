using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using DataProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace DataAccess
{
    public class LoginValidation
    {
        DataHandeler dh = new DataHandeler();
        public SelectLoginInfo GetUserValidation(LoginValidator lv)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@Email",lv.UserName),
                    new SqlParameter("@Password",lv.Password),
                };

                DataTable dt = dh.ReadData("[usp_LoginUser_Validation]", parm, CommandType.StoredProcedure);

                SelectLoginInfo sli = new SelectLoginInfo();
                sli.Role = dt.Rows[0]["Type"].ToString();
                sli.TokenNo = dt.Rows[0]["Token"].ToString();
                sli.Status = Int32.Parse(dt.Rows[0]["StatusCode"].ToString());
                sli.Message = dt.Rows[0]["Message"].ToString();
                sli.Type= dt.Rows[0]["Type"].ToString();
                sli.landingPage = dt.Rows[0]["landingPage"].ToString();
                sli.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                //sli.Id = dt.Rows[0]["Id"].ToString();
                sli.UserName = dt.Rows[0]["UserName"].ToString();
                sli.PhoneNo = dt.Rows[0]["ContactNo"].ToString();
                sli.Email = dt.Rows[0]["Email"].ToString();
               // sli.Secret = dt.Rows[0]["Secret"].ToString();

                return sli;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }










        public SelectLoginInfo GetUserValidationWithExternalLink(LoginValidator lv)
        {
            try
            {
           

                

                SqlParameter[] parm = {
                    new SqlParameter("@CustomerSubscriptionGuid",lv.CustomerSubscriptionGuid)
                };




                DataTable dt = dh.ReadData("[usp_LoginUser_Validation_ExternalLink]", parm, CommandType.StoredProcedure);

                SelectLoginInfo sli = new SelectLoginInfo();

                sli.TokenNo = dt.Rows[0]["Token"].ToString();
                sli.Status = Int32.Parse(dt.Rows[0]["StatusCode"].ToString());
                sli.Message = dt.Rows[0]["Message"].ToString();
                sli.Type = dt.Rows[0]["Type"].ToString();
                sli.landingPage = dt.Rows[0]["landingPage"].ToString();
                //sli.Id = dt.Rows[0]["Id"].ToString();
                sli.UserName = dt.Rows[0]["UserName"].ToString();
                sli.Customer = Convert.ToInt32(dt.Rows[0]["Customer"]);
                sli.Product = Convert.ToInt32(dt.Rows[0]["Product"]);


                return sli;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public SelectLoginInfo GetAdminValidation(LoginValidator lv)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@Email",lv.UserName),
                    new SqlParameter("@Password",lv.Password),
                };

                DataTable dt = dh.ReadData("[Usp_verifyAdminLogin]", parm, CommandType.StoredProcedure);

                SelectLoginInfo sli = new SelectLoginInfo();

                sli.TokenNo = dt.Rows[0]["Token"].ToString();
                sli.Status = Int32.Parse(dt.Rows[0]["StatusCode"].ToString());
                sli.Message = dt.Rows[0]["Message"].ToString();
                return sli;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
