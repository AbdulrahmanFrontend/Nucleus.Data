using Nucleus.Data.BLL;
using Nucleus.Data.Core;
using Nucleus.Data.DAL;
using System.Data;
using System.Linq;

namespace Nucleus.Data.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Users = clsUser.GetUsers().Where(u => u.UserID > 1).ToList();
            Users.ForEach(u =>
            {
                Console.WriteLine($"ID: {u.UserID}, Name: {u.UserName}, Email: {u.Email}");
            });
            clsUserModel userToUpdate = new clsUserModel { UserID = 2, UserName = null, Email = "Ali@gmail.com" };
            Users = clsUser.GetUsers(userToUpdate.UserID);
            Users.ForEach(u =>
            {
                Console.WriteLine($"ID: {u.UserID}, Name: {u.UserName}, Email: {u.Email}");
            });
            Users = clsUser.GetUsers();
            Users.ForEach(u =>
            {
                Console.WriteLine($"ID: {u.UserID}, Name: {u.UserName}, Email: {u.Email}");
            });
            Users = clsUser.GetUsers(1);
            Users.ForEach(u =>
            {
                Console.WriteLine($"ID: {u.UserID}, Name: {u.UserName}, Email: {u.Email}");
            });
            Users = clsUser.GetUsers();
            Users.ForEach(u =>
            {
                Console.WriteLine($"ID: {u.UserID}, Name: {u.UserName}, Email: {u.Email}");
            });
        }
    }
}
