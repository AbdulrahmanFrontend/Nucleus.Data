using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Data.Core;
using System.IO;
using LL;

namespace Nucleus.Data.DAL
{
    public class clsDataManager
    {
        public static List<T> Query<T>(CommandType Type, string CommandText,
            SqlParameter[] Parameters = null, 
            Action<clsQueryOptions> OptionsAction = null) where T : new()
        {
            var Options = new clsQueryOptions 
            {
                UseCache = true, Page = 1, PageSize = 0 
            };
            OptionsAction?.Invoke(Options);
            
            if(Options.Page > 0 && Options.PageSize > 0)
            {
                List<SqlParameter> ParamsList =
                    Parameters?.ToList() ?? new List<SqlParameter>();
                ParamsList.Add(new SqlParameter("@Page", SqlDbType.Int) 
                { 
                    Value = Options.Page
                });
                ParamsList.Add(new SqlParameter("@PageSize", SqlDbType.Int)
                { 
                    Value = Options.PageSize
                });
                Parameters = ParamsList.ToArray();
            }
            string ParamsKey = Parameters == null ? "NULL" : string.Join("_",
                Parameters.Select(p => p.ParameterName + "_" + p.Value));
            string Key = $"{CommandText}_{typeof(T).Name}_{ParamsKey}";

            List<T> ObjsList;
            if (Options.UseCache &&
                clsCacheManager.Cache.TryGetValue(Key, out var Json))
            {
                var Result = JsonSerializer.Deserialize<List<T>>(Json);
                return Result ?? new List<T>();
            }
            else
            {
                DataTable dt = 
                    DbHelper.GetDataTable(Type, CommandText, Parameters);
                ObjsList = new List<T>();
                foreach (DataRow dr in dt.Rows)
                {
                    T Obj = clsMapper.Map<T>(dr);
                    ObjsList.Add(Obj);
                }

                if (Options.UseCache)
                {
                    clsCacheManager.Cache[Key] = 
                        JsonSerializer.Serialize(ObjsList);
                    clsCacheManager.SaveCache();
                }
            }

            return ObjsList;
        }
    }
}
