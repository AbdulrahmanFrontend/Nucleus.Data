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
            SqlParameter[] Parameters = null) where T : new()
        {
            string ParamsKey = Parameters == null ? "NULL" : string.Join("_",
                Parameters.Select(p => p.ParameterName + "_" + p.Value));
            string Key = CommandText + "_" + typeof(T).Name + "_" + ParamsKey;
            if (clsCacheManager.Cache.TryGetValue(Key, out var Json))
            {
                var Result = JsonSerializer.Deserialize<List<T>>(Json);
                return Result ?? new List<T>();
            }
            DataTable dt = DbHelper.GetDataTable(Type, CommandText, Parameters);
            List<T> ObjsList = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T Obj = clsMapper.Map<T>(dr);
                ObjsList.Add(Obj);
            }
            clsCacheManager.Cache[Key] = JsonSerializer.Serialize(ObjsList);
            clsCacheManager.SaveCache();
            return ObjsList;
        }
    }
}
