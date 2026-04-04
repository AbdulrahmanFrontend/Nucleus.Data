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

namespace Nucleus.Data.DAL
{
    public class clsDataManager
    {
        public static Dictionary<string, string> Cache = 
            new Dictionary<string, string>();
        private static readonly string _FilePath = "QueryCache.json";
        private static void _LoadCache()
        {
            if (!File.Exists(_FilePath))
            {
                Cache = new Dictionary<string, string>();
            }
            else
            {
                var Json = File.ReadAllText(_FilePath);
                Cache = JsonSerializer.Deserialize<Dictionary<string, string>>
                    (Json) ?? new Dictionary<string, string>();
            }
        }
        static clsDataManager()
        {
            _LoadCache();
        }
        private static void _SaveCache()
        {
            var Json = JsonSerializer.Serialize(Cache);
            File.WriteAllText(_FilePath, Json);
        }
        public static List<T> Query<T>(CommandType Type, string CommandText,
            SqlParameter[] Parameters = null) where T : new()
        {
            string ParamsKey = Parameters == null ?
                "NULL" : string.Join("_", Parameters.Select(p => p.Value));
            string Key = CommandText + "_" + typeof(T).Name + "_" + ParamsKey;
            _LoadCache();
            if (Cache.ContainsKey(Key))
            {
                return JsonSerializer.Deserialize<List<T>>(Cache[Key]) ?? 
                    new List<T>();
            }
            DataTable dt = DbHelper.GetDataTable(Type, CommandText, Parameters);
            List<T> ObjsList = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T Obj = clsMapper.Map<T>(dr);
                ObjsList.Add(Obj);
            }
            Cache[Key] = JsonSerializer.Serialize(ObjsList);
            _SaveCache();
            return ObjsList;
        }
    }
}
