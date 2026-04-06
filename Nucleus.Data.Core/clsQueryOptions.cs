using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Data.Core
{
    public class clsQueryOptions
    {
        public bool UseCache { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; }
    }
}
