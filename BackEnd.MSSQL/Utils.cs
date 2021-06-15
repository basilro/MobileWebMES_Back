using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BackEnd.MSSQL
{
    class Utils
    {
        /// <summary>
        /// SQL QUERY를 실행시켜서 Json 배열로 반환한다
        /// </summary>
        public static JArray getQuerys(string reName, params object[] args)
        {
            return Querys(string.Format(Localizer.getString(reName), args));
        }

        /// <summary>
        /// SQL QUERY를 실행시켜서 Json 배열로 반환한다
        /// </summary>
        public static JArray getQuerys(string reName, string args)
        {
            return Querys(string.Format(Localizer.getString(reName), args));
        }

        public static JObject newgetQuerys(string reName, params object[] args)
        {
            return newQuerys(string.Format(Localizer.getString(reName), args));
        }
        /// <summary>
        /// SQL QUERY를 실행시켜서 Json 배열로 반환한다
        /// </summary>
        private static JObject newQuerys(string cmdText)
        {
            JArray array = new JArray();
            JObject json = new JObject();
            SqlConnection sqlcon = null;
            SqlCommand command = null;
            try
            {
                sqlcon = new SqlConnection(Loader._constring);
                sqlcon.Open();
                command = new SqlCommand(cmdText, sqlcon);

                SqlDataReader dataReader = command.ExecuteReader();
                using (DataSet dsResult = new DataSet())
                {
                    while (!dataReader.IsClosed)
                        dsResult.Tables.Add().Load(dataReader);
                    
                    string convert = JsonConvert.SerializeObject(dsResult);
                    json = JObject.Parse(convert);
                    Log.DEBUG(JsonConvert.SerializeObject(dsResult));
                }

                return json;
            }
            catch (InvalidOperationException ex)
            {
                Log.ERROR("[TestService.GetMenu] {0}", ex);
                Loader.SqlCon.Close();
                return json;
            }
            catch (InvalidCastException ex)
            {
                Log.ERROR("[TestService.GetMenu] {0}", ex);
                Loader.SqlCon.Close();
                return json;
            }
            catch (SqlException ex)
            {
                Log.ERROR("[TestService.GetMenu] {0}", ex);
                return json;
            }
            finally
            {

                if (sqlcon != null) sqlcon.Close();
                if (command != null) command.Dispose();
            }
        }

        /// <summary>
        /// SQL QUERY를 실행시켜서 Json 배열로 반환한다
        /// </summary>
        private static JArray Querys(string cmdText)
        {
            JArray array = new JArray();
            SqlConnection sqlcon = null;
            SqlCommand command = null;
            try
            {
                sqlcon = new SqlConnection(Loader._constring);
                sqlcon.Open();
                command = new SqlCommand(cmdText, sqlcon);
                //Loader.SqlCon.Open();
                //SqlCommand command = new SqlCommand(cmdText, Loader.SqlCon);
                //using (SqlDataReader data = command.ExecuteReader())//System.Data.CommandBehavior.CloseConnection))
                //{
                SqlDataReader data = command.ExecuteReader();
                List<string> cols = new List<string>();
                for (int i = 0; i < data.FieldCount; i++)
                {
                    cols.Add(data.GetName(i));
                }
                while (data.Read())
                {
                    JObject json = new JObject();
                    foreach (string col in cols)
                    {
                        json[col] = data[col].ToString();
                    }
                    array.Add(json);
                }
                
                
                //Loader.SqlCon.Close();

                return array;
            }
            catch (InvalidOperationException ex)
            {
                Log.ERROR("[TestService.GetMenu] {0}", ex);
                Loader.SqlCon.Close();
                return array;
            }
            catch(InvalidCastException ex)
            {
                Log.ERROR("[TestService.GetMenu] {0}", ex);
                Loader.SqlCon.Close();
                return array;
            }
            catch (SqlException ex)
            {
                Log.ERROR("[TestService.GetMenu] {0}", ex);
                return array;
            }
            finally
            {

                if (command != null) command.Dispose();
                if (sqlcon != null) sqlcon.Close();
            }
        }

        /// <summary>
        /// SQL QUERY를 실행시켜서 Json을 반환한다
        /// </summary>
        public static JObject getQuery(string reName, params object[] args)
        {
            return Query(string.Format(Localizer.getString(reName), args));
        }

        /// <summary>
        /// SQL QUERY를 실행시켜서 Json을 반환한다
        /// </summary>
        public static JObject getQuery(string reName, string args)
        {
            return Query(string.Format(Localizer.getString(reName), args));
        }

        /// <summary>
        /// SQL QUERY를 실행시켜서 Json을 반환한다
        /// </summary>
        private static JObject Query(string cmdText)
        {
            JObject json = new JObject();
            try
            {
                //Loader.SqlCon.Open();
                //SqlCommand command = new SqlCommand(cmdText, Loader.SqlCon);
                SqlConnection sqlcon = new SqlConnection(Loader._constring);
                sqlcon.Open();
                SqlCommand command = new SqlCommand(cmdText, sqlcon);
                using (SqlDataReader data = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    List<string> cols = new List<string>();
                    for (int i = 0; i < data.FieldCount; i++)
                    {
                        cols.Add(data.GetName(i));
                    }

                    while (data.Read())
                    {
                        foreach (string col in cols)
                        {
                            json[col] = data[col].ToString();
                        }
                    }
                }
                command.Dispose();
                sqlcon.Close();
                //Loader.SqlCon.Close();

                return json;
            }
            catch (SqlException ex)
            {
                Log.ERROR("[TestService.GetMenu] {0}", ex);
                return json;
            }
        }
    }
}
