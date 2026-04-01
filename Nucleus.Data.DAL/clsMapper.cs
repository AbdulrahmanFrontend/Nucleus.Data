using Nucleus.Data.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
                var Attr = prop.GetCustomAttribute<ColumnAttribute>();
                string ColumnName = Attr != null ? Attr.ColumnName : prop.Name;

                if (row.Table.Columns.Contains(ColumnName))
                {
                    var Value = row[ColumnName];
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
