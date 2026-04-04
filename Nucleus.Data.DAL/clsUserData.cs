using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Data.Core;
using System.Data.SqlClient;
using System.Data;

namespace Nucleus.Data.DAL
{
    public class clsUserData
    {
        public static List<clsUserModel> GetUsers()
        {
            return clsDataManager.Query<clsUserModel>
                (CommandType.StoredProcedure, "sp_GetUsers");
        }
    }
}
