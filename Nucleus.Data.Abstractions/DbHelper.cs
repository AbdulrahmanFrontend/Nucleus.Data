using LL;
using Nucleus.Data.DAL;
using Nucleus.Data.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necleus.Data.DAL
{
    public class DbHelper
    {
        private static SqlCommand _PrepareCommand(SqlConnection con, string query,
            SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(query, con);
            if (parameters != null)
            {
                foreach(var p in parameters)
                {
                    cmd.Parameters.Add(
                        new SqlParameter(p.ParameterName, p.Value ?? DBNull.Value)
                        );
                }
            }
            return cmd;
        }
        public static DataTable GetDataTable(string query, 
            SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = clsDataAccessSettings.GetConnection())
            {
                using (SqlCommand cmd = _PrepareCommand(con, query, parameters))
                {
                    try
                    {
                        con.Open();
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if(reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("GetDataTable Failed;", ex);
                        throw;
                    }
                }
            }
            return dt;
        }
        public static DataRow GetFirstRow(string query, 
            SqlParameter[] parameters = null)
        {
            DataTable dt = GetDataTable(query, parameters);
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }
        public static object GetScalar(string query,
            SqlParameter[] parameters = null)
        {
            using (SqlConnection con = clsDataAccessSettings.GetConnection())
            {
                using (SqlCommand cmd = _PrepareCommand(con, query, parameters))
                {
                    try
                    {
                        con.Open();
                        return cmd.ExecuteScalar();
                    }
                    catch(Exception ex)
                    {
                        Logger.LogError("GetScalar Failed;", ex);
                        throw;
                    }
                }
            }
        }
        public static int ExecuteNonQuery(string query, 
            SqlParameter[] parameters = null)
        {
            using (SqlConnection con = clsDataAccessSettings.GetConnection())
            {
                using (SqlCommand cmd = _PrepareCommand(con, query, parameters))
                {
                    try
                    {
                        con.Open();
                        return cmd.ExecuteNonQuery();
                    }
                    catch(Exception ex)
                    {
                        Logger.LogError("ExecuteNonQuery Failed;", ex);
                        throw;
                    }
                }
            }
        }
    }
}
