using Necleus.Data.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Data.DAL
{
    public class clsUserData
    {
        public static DataTable GetAllUsers()
        {
            string Query = "EXEC sp_GetUsers;";
            return DbHelper.GetDataTable(Query);
        }
    }
}
