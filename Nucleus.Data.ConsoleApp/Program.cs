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
            var Users = clsUser.GetUsers();
            Users.ForEach(u =>
            {
                Console.WriteLine($"ID: {u.UserID}, Name: {u.UserName}, Email: {u.Email}");
            });
        }
    }
}
