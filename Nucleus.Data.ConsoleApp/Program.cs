using Nucleus.Data.DAL;
using System.Data;
using System.Linq;

namespace Nucleus.Data.DemoApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Users = clsUserData.GetAllUsers();
            Users.ForEach(u =>
            {
                Console.WriteLine($"ID: {u.UserID}, Name: {u.UserName}, Email: {u.Email}");
            });
        }
    }
}
