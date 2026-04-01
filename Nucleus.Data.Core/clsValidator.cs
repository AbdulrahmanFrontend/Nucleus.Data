using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Data.Core
{
    public class clsValidator
    {
        public static List<string> Validate<T>(T obj)
        {
            List<string> Errors = new List<string>();
            var Type = typeof(T);
            foreach (var prop in Type.GetProperties())
            {
                var Value = prop.GetValue(obj);
                var RequiredAttr = prop.GetCustomAttribute<RequiredAttribute>();
                if(RequiredAttr != null)
                {
                    if(Value == null || 
                        (Value is string Str && string.IsNullOrEmpty(Str)))
                    {
                        Errors.Add(RequiredAttr.ErrorMessage + ", ");
                    }
                }
                var RangeAttr = prop.GetCustomAttribute<RangeAttribute>();
                if (RangeAttr != null)
                {
                    if (Value != null)
                    {
                        if(Value is int IntValue && (IntValue < RangeAttr.Min 
                            || IntValue > RangeAttr.Max))
                        {
                            Errors.Add(RangeAttr.ErrorMessage + ", ");
                        }
                    }
                }
                var MaxLengthAttr = prop.GetCustomAttribute<MaxLengthAttribute>();
                if(MaxLengthAttr != null)
                {
                    var value = prop.GetValue(obj) as string;
                    if(value != null && (value.Length > MaxLengthAttr.Length))
                    {
                        Errors.Add(MaxLengthAttr.ErrorMessage);
                    }
                }
            }
            return Errors;
        }
    }
}
