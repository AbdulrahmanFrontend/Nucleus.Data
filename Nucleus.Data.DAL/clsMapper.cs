using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Data.DAL
{
    public class clsMapper
    {
        public static T Map<T>(DataRow row) where T : new()
        {
            T obj = new T();
            foreach (var prop in typeof(T).GetProperties())
            {
                if (row.Table.Columns.Contains(prop.Name))
                {
                    var Value = row[prop.Name];
                    var propType = Nullable.GetUnderlyingType(prop.PropertyType)
                        ?? prop.PropertyType;
                    var SafeValue = Convert.ChangeType(Value, propType);
                    prop.SetValue(obj, SafeValue);
                }
            }
            return obj;
        }
    }
}
