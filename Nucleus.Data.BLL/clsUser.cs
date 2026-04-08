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
        public static clsResult UpdateUser(clsUserModel User)
        {
            return clsUserData.UpdateUser(User);
        }
        public static List<clsUserModel> GetUsers(int? UserID)
        {
            return clsUserData.GetUsers(UserID);
        }
    }
}
