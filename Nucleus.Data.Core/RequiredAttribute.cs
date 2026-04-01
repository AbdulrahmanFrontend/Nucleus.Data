using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Data.Core
{
    public class RequiredAttribute : Attribute
    {
        public string ErrorMessage { get; }
        public RequiredAttribute(string errorMessage = null)
        {
            this.ErrorMessage = errorMessage ?? "Value is required.";
        }
    }
}
