using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Data.Core
{
    public class clsValidator
    {
        public static List<string> ValidateProperty<T>(PropertyInfo Prop, T obj)
        {
            List<string> Errors = new List<string>();
            var Value = Prop.GetValue(obj);
            var RequiredAttr = Prop.GetCustomAttribute<RequiredAttribute>();
            if (RequiredAttr != null)
            {
                if(Value == null || (Value is string Str && string.IsNullOrWhiteSpace(Str)))
                {
                    Errors.Add(RequiredAttr.ErrorMessage);
                }
            }
            var RangeAttr = Prop.GetCustomAttribute<RangeAttribute>();
            if (RangeAttr != null)
            {
                if (Value != null)
                {
                    if (Value is int IntValue && (IntValue < RangeAttr.Min
                        || IntValue > RangeAttr.Max))
                    {
                        Errors.Add(RangeAttr.ErrorMessage);
                    }
                }
            }
            var StrValue = Value as string;
            var MaxLengthAttr = Prop.GetCustomAttribute<MaxLengthAttribute>();
            if (MaxLengthAttr != null)
            {
                if(StrValue != null)
                {
                    if (StrValue.Length > MaxLengthAttr.Length)
                    {
                        Errors.Add(MaxLengthAttr.ErrorMessage);
                    }
                }
            }
            return Errors;
        }
        public static Dictionary<string, List<string>> ValidateObject<T>(T obj)
        {
            var Type = typeof(T);
            var Errors = new Dictionary<string, List<string>>();
            var PropErrors = new List<string>();
            foreach (var prop in Type.GetProperties())
            {
                PropErrors = ValidateProperty<T>(prop, obj);
                if (PropErrors.Count > 0)
                {
                    Errors[prop.Name] = PropErrors;
                }
            }
            return Errors;
        }
        public static bool IsValid<T>(T obj)
        {
            return ValidateObject<T>(obj).Count == 0;
        }
    }
}
