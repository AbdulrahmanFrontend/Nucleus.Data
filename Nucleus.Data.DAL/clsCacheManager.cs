using LL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace Nucleus.Data.DAL
{
    public class clsCacheManager
    {
        public static Dictionary<string, string> Cache =
            new Dictionary<string, string>();
        private static readonly string _FilePath = "QueryCache.json";
        private static void _LoadCache()
        {
            try
            {
                if (File.Exists(_FilePath))
                {
                    var Json = File.ReadAllText(_FilePath);
                    Cache = JsonSerializer.Deserialize<Dictionary<string, string>>(Json)
                        ?? new Dictionary<string, string>();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Failed to load cache;", ex);
            }
        }
        static clsCacheManager()
        {
            _LoadCache();
        }
        public static void SaveCache()
        {
            try
            {
                var Json = JsonSerializer.Serialize(Cache);
                File.WriteAllText(_FilePath, Json);
            }
            catch (Exception ex)
            {
                Logger.LogError("Failed to save cache;", ex);
            }
        }
    }
}
