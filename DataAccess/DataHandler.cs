﻿using DataProvider;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;


namespace DataAccess
{
    public class DataHandeler
    {
        //public string _connectionString = "Server=DESKTOP-Q33RBL2\\INSOFTDATA22; Database=OnlineSubscription; user Id=sa; Password=L@xmir1n1";
        
     //   public string _connectionString = "Server=202.51.74.37,1435; Database=OnlineSubscription; user Id=sa; Password=L@xmir1n1";  //nabin dai

      public string _connectionString = "Server=202.166.211.166,3637; Database=OnlineSubscription; user Id=sa; Password=L@xmir1n1";   //ankit poudel server

        public string ByToken(string TokenNo)
        {
            SqlConnection conn = new SqlConnection(this._connectionString);
            try
            {
                string Schoolcode = TokenNo.Split("_")[1].ToString();

                SqlParameter[] parm = {
                    new SqlParameter("@Initial",Schoolcode)
                };

                string sql = "[Usp_S_SchoolDetails_ByInitialAndSchoolCode]";

                SqlCommand cmd = new SqlCommand();
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    if (parm != null)
                    {
                        cmd.Parameters.AddRange(parm);
                    }
                    conn.Open();
                    IDataReader reader = cmd.ExecuteReader();
                    DataTable ds = new DataTable();
                    ds.Load(reader);

                    string Connectionstring = "";
                    if (ds.Rows.Count > 0)
                    {
                        Connectionstring = ds.Rows[0]["DataBaseLink"].ToString();
                    }

                    return Connectionstring;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //---------------for Insert operation -----------------
        public int Insert(string sql, SqlParameter[] param, CommandType cmdType)
        {
            SqlConnection con = new SqlConnection(this._connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                {
                    cmd.Connection = con;
                    cmd.CommandText = sql;
                    cmd.CommandType = cmdType;
                    cmd.CommandTimeout = 0;
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    return i;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }


        //----------------------InsertUpdate -----------------------------------
        public int InsertUpdate(string sql, SqlParameter[] param, CommandType cmdType)
        {
            SqlConnection con = new SqlConnection(this._connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                {
                    cmd.Connection = con;
                    cmd.CommandText = sql;
                    cmd.CommandType = cmdType;
                    cmd.CommandTimeout = 0;
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    return i;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }



        //---------------------------Query Executer---------------------------
        public int ExecuteNonQuery(string sql, SqlParameter[] param, CommandType cmdType)
        {
            SqlConnection con = new SqlConnection(this._connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                {
                    cmd.Connection = con;
                    cmd.CommandText = sql;
                    cmd.CommandType = cmdType;
                    cmd.CommandTimeout = 0;
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    return i;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        //-------------------for update operation---------------------
        public int Update(string sql, SqlParameter[] param, CommandType cmdType)
        {
            SqlConnection con = new SqlConnection(this._connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                {
                    cmd.Connection = con;
                    cmd.CommandText = sql;
                    cmd.CommandType = cmdType;
                    cmd.CommandTimeout = 0;
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    return i;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        //---------------------for Delete Operation ----------------------------
        public int Delete(string sql, SqlParameter[] param, CommandType cmdType)
        {
            SqlConnection con = new SqlConnection(this._connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                {
                    cmd.Connection = con;
                    cmd.CommandText = sql;
                    cmd.CommandType = cmdType;
                    cmd.CommandTimeout = 0;
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    return i;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        //------------------Read All Data--------------------------
        public DataTable ReadAllData(string sql, CommandType cmdType)
        {
            SqlConnection conn = new SqlConnection(this._connectionString);
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = sql;
                comm.CommandType = cmdType;
                comm.CommandTimeout = 0;
                conn.Open();
                SqlDataAdapter ada = new SqlDataAdapter(comm);
                DataTable ds = new DataTable();
                ada.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //-----------------------Read data----------------------------
        public DataTable ReadData(string sql, SqlParameter[] parm, CommandType cmdType)
        {
            SqlConnection conn = new SqlConnection(this._connectionString);
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = sql;
                comm.CommandType = cmdType;
                comm.CommandTimeout = 0;
                if (parm != null)
                {
                    comm.Parameters.AddRange(parm);
                }
                conn.Open();
                SqlDataAdapter ada = new SqlDataAdapter(comm);
                DataTable ds = new DataTable();
                ada.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        //---------------------------readdata all data in Json----------------
        public string ReadAllToJson(string sql, CommandType cmdType)
        {
            SqlConnection con = new SqlConnection(this._connectionString);
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = cmdType;
                    cmd.CommandText = sql;
                    cmd.CommandTimeout = 0;
                    con.Open();

                    IDataReader reader = cmd.ExecuteReader();
                    StringBuilder sb = new StringBuilder();
                    StringWriter sw = new StringWriter(sb);

                    using (JsonWriter jsonWriter = new JsonTextWriter(sw))
                    {
                        jsonWriter.WriteStartArray();

                        while (reader.Read())
                        {
                            jsonWriter.WriteStartObject();

                            int fields = reader.FieldCount;

                            for (int i = 0; i < fields; i++)
                            {
                                jsonWriter.WritePropertyName(reader.GetName(i));
                                if (reader[i] == System.DBNull.Value)
                                {
                                    jsonWriter.WriteValue("");
                                }
                                else
                                {
                                    jsonWriter.WriteValue(reader[i]);
                                }
                            }
                            jsonWriter.WriteEndObject();
                        }
                        jsonWriter.WriteEndArray();
                        return sw.ToString();
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }



        public string ReadToJsonSMS(string sql, SqlParameter[] parm, CommandType cmdType, string connString)
        {
            SqlConnection con = new SqlConnection(connString);
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = cmdType;
                    cmd.CommandText = sql;
                    cmd.CommandTimeout = 0;
                    if (parm != null)
                    {
                        cmd.Parameters.AddRange(parm);
                    }
                    // await con.OpenAsync();
                    con.Open();
                    IDataReader reader = cmd.ExecuteReader();
                    StringBuilder sb = new StringBuilder();
                    StringWriter sw = new StringWriter(sb);

                    using (JsonWriter jsonWriter = new JsonTextWriter(sw))
                    {
                        jsonWriter.WriteStartArray();

                        while (reader.Read())
                        {
                            jsonWriter.WriteStartObject();

                            int fields = reader.FieldCount;

                            for (int i = 0; i < fields; i++)
                            {
                                jsonWriter.WritePropertyName(reader.GetName(i));
                                if (reader[i] == System.DBNull.Value)
                                {
                                    jsonWriter.WriteValue("");
                                }
                                else
                                {
                                    jsonWriter.WriteValue(reader[i]);
                                }
                            }
                            jsonWriter.WriteEndObject();
                        }
                        jsonWriter.WriteEndArray();
                        cmd.Parameters.Clear();
                        return sw.ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }




        //---------------------------readdata selected data in Json----------------
        //  public async Task<string> ReadToJson(string sql, SqlParameter[] parm, CommandType cmdType)
        public string ReadToJson(string sql, SqlParameter[] parm, CommandType cmdType)
        {
            SqlConnection con = new SqlConnection(this._connectionString);
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = cmdType;
                    cmd.CommandText = sql;
                    cmd.CommandTimeout = 0;
                    if (parm != null)
                    {
                        cmd.Parameters.AddRange(parm);
                    }
                    // await con.OpenAsync();
                    con.Open();
                    IDataReader reader = cmd.ExecuteReader();
                    StringBuilder sb = new StringBuilder();
                    StringWriter sw = new StringWriter(sb);

                    using (JsonWriter jsonWriter = new JsonTextWriter(sw))
                    {
                        jsonWriter.WriteStartArray();

                        while (reader.Read())
                        {
                            jsonWriter.WriteStartObject();

                            int fields = reader.FieldCount;

                            for (int i = 0; i < fields; i++)
                            {
                                jsonWriter.WritePropertyName(reader.GetName(i));
                                if (reader[i] == System.DBNull.Value)
                                {
                                    jsonWriter.WriteValue("");
                                }
                                else
                                {
                                    jsonWriter.WriteValue(reader[i]);
                                }
                            }

                            jsonWriter.WriteEndObject();
                        }
                        jsonWriter.WriteEndArray();
                        return sw.ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        //-----------------------execute-Scaller Function----------------------
        public string ExecuteScaller(string sql, SqlParameter[] parm, CommandType cmdType)
        {
            SqlConnection conn = new SqlConnection(this._connectionString);
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = sql;
                comm.CommandType = cmdType;
                comm.CommandTimeout = 0;
                if (parm != null)
                {
                    comm.Parameters.AddRange(parm);
                }
                conn.Open();
                string result = (string)comm.ExecuteScalar();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }



        public string DataTableToJSON(DataTable Dt, string tagname)
        {
            string[] StrDc = new string[Dt.Columns.Count];
            string HeadStr = string.Empty;


            for (int i = 0; i < Dt.Columns.Count; i++)
            {
                StrDc[i] = Dt.Columns[i].Caption;
                HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";
            }
            HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);
            StringBuilder Sb = new StringBuilder();
            Sb.Append("{\"" + tagname + "\" : [");
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                string TempStr = HeadStr;
                Sb.Append("{");
                for (int j = 0; j < Dt.Columns.Count; j++)
                {
                    switch (Dt.Columns[j].DataType.ToString())
                    {
                        case "System.DateTime":
                            DateTime cv = (DateTime)Dt.Rows[i][j];
                            TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾", cv.Year + "," + (cv.Month) + "," + cv.Day + "," + cv.Hour + "," + cv.Minute + "," + cv.Second + "," + cv.Millisecond);
                            break;
                        case "System.Boolean":
                            TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾", Dt.Rows[i][j].ToString().ToLower());
                            break;
                        default:
                            string str = Dt.Rows[i][j].ToString();
                            TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾", str);
                            break;
                    }
                }
                Sb.Append(TempStr + "},");
            }
            Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));
            Sb.Append("]");
            return Sb.ToString();
        }
    }
}
