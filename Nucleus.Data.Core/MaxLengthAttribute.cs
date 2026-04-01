using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Data.Core
{
    public class MaxLengthAttribute : Attribute
    {
        public int Length { get; }
        public string ErrorMessage { get; }
        public MaxLengthAttribute(int length, string errorMessage = null)
        {
            this.Length = length;
            this.ErrorMessage = errorMessage ?? $"Max Length is {this.Length}.";
        }
    }
}
