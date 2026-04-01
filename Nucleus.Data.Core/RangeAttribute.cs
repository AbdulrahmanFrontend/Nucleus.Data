using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Data.Core
{
    public class RangeAttribute : Attribute
    {
        public int Min { get; }
        public int Max { get; }
        public string ErrorMessage { get; }
        public RangeAttribute(int min, int max, string errorMessage = null)
        {
            this.Min = min;
            this.Max = max;
            this.ErrorMessage = errorMessage ?? 
                $"Value must be between {this.Min} and {this.Max}.";
        }
    }
}
