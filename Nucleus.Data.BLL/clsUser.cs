using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Data.Core;
using Nucleus.Data.DAL;

namespace Nucleus.Data.BLL
{
    public class clsUser
    {
        public static List<clsUserModel> GetUsers()
        {
            return clsUserData.GetUsers();
        }
    }
}
