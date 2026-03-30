using Nucleus.Data.DAL;
using Nucleus.Data.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Data.DAL
{
    public class clsUserData
    {
        public static List<clsUser> GetAllUsers()
        {
            string CommandText = "sp_GetUsers";
            DataTable dt = DbHelper.GetDataTable(CommandType.StoredProcedure,
                CommandText);
            List<clsUser> Users = new List<clsUser>();
            foreach (DataRow dr in dt.Rows)
            {
                clsUser User = new clsUser();

                User.UserID = Convert.ToInt32(dr["UserID"]);
                User.UserName = dr["UserName"].ToString();
                User.Email = dr["Email"].ToString();

                Users.Add(User);
            }
            return Users;
        }
    }
}
