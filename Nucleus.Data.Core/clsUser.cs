using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Nucleus.Data.Core
{
    public class clsUser
    {
        [RequiredAttribute]
        [ColumnAttribute("User_ID")]
        public int UserID { get; set; }
        [RequiredAttribute]
        [MaxLengthAttribute(50)]
        public string UserName { get; set; }
        [MaxLengthAttribute(50)]
        public string Email { get; set; }
    }
}
