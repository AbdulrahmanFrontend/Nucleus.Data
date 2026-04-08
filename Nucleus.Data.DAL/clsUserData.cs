using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus.Data.Core;
using System.Data.SqlClient;
using System.Data;
using LoggingLayer;

namespace Nucleus.Data.DAL
{
    public class clsUserData
    {
        public static List<clsUserModel> GetUsers()
        {
            return clsDataManager.Query<clsUserModel>
                (CommandType.StoredProcedure, "sp_GetUsers", null, 
                Options => 
                {
                    Options.Page = 2;
                    Options.PageSize = 2;
                    Options.UseCache = true;
                });
        }
        public static clsResult UpdateUser(clsUserModel User)
        {
            SqlParameter[] Parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", SqlDbType.Int)
                { Value = User.UserID },
                new SqlParameter("@UserName", SqlDbType.NVarChar, 50)
                { Value = User.UserName },
                new SqlParameter("@Email", SqlDbType.NVarChar, 50)
                { Value = User.Email },
            };
            clsResult Result = new clsResult();
            Result.IsSuccess = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                    "sp_UpdateUser", Parameters) > 0;
            Result.Message = Result.IsSuccess ?
                "User updated successfully." : "No changes were made.";
            return Result;
        }
        public static List<clsUserModel> GetUsers(int? UserID)
        {
            SqlParameter[] Parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", SqlDbType.Int)
                { Value = UserID }
            };
            return clsDataManager.Query<clsUserModel>
                (CommandType.StoredProcedure, "sp_GetUsersByUserID", Parameters,
                Options => Options.UseCache = false);
        }
    }
}
