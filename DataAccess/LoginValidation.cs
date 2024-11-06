using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using DataProvider;

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

                sli.TokenNo = dt.Rows[0]["Token"].ToString();
                sli.Status = Int32.Parse(dt.Rows[0]["StatusCode"].ToString());
                sli.Message = dt.Rows[0]["Message"].ToString();
                sli.Type= dt.Rows[0]["Type"].ToString();
                sli.landingPage = dt.Rows[0]["landingPage"].ToString();
                //sli.Id = dt.Rows[0]["Id"].ToString();
                sli.UserName = dt.Rows[0]["UserName"].ToString();
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
