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
        public static bool UpdateUser(clsUserModel User)
        {
            string Query = @"UPDATE Users
                SET UserName = @UserName, Email = @Email
                WHERE UserID = @UserID;";
            SqlParameter[] Parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", SqlDbType.Int)
                { Value = User.UserID },
                new SqlParameter("@UserName", SqlDbType.NVarChar, 50)
                { Value = User.UserName },
                new SqlParameter("@Email", SqlDbType.NVarChar, 50)
                { Value = User.Email },
            };
            return DbHelper.ExecuteNonQuery
                (CommandType.Text, Query, Parameters) > 0;
        }
        public static List<clsUserModel> GetUsers(int? UserID)
        {
            SqlParameter[] Parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", SqlDbType.Int)
                { Value = UserID }
            };
            return clsDataManager.Query<clsUserModel>
                (CommandType.StoredProcedure, "sp_GetUsersByUserID", Parameters);
        }
    }
}
