using System;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using DependencyResolver;
using Ninject;

namespace ConsolePL
{
    public class Program
    {
        private static readonly IKernel _resolver;
        private static IUserService _userService;
        private static IProfileService _profileService;
        private static IRoleService _roleService;

        static Program()
        {
            _resolver = new StandardKernel();
            _resolver.ConfigurateResolverConsole();

        }
        static void Main(string[] args)
        {
            _userService = _resolver.Get<IUserService>();
            _profileService = _resolver.Get<IProfileService>();
            _roleService = _resolver.Get<IRoleService>();

            Process();
        }

        static void Process()
        {
            while (true)
            {
                Console.WriteLine("1 - Login");
                Console.WriteLine("2 - Register");
                switch (Console.ReadLine())
                {
                    case "1":
                        {
                            Login();
                            break;
                        }
                    case "2":
                        {
                            Register();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong input\n");
                            break;
                        }
                }
            }
        }
      
        static void Login()
        {
            bool guest = true;
            do
            {
                Console.WriteLine("Enter login");
                var login = Console.ReadLine();
                Console.WriteLine("Enter password");
                var password = Console.ReadLine();

                var user = _userService.GetOneByPredicate(u => u.Login == login);

                if ((user != null) && (VerifyHashedPassword(user.Password, password)))
                {
                    Console.WriteLine("\nYou are autorized.\n");
                    guest = false;
                }
                else
                {
                    Console.WriteLine("\nWrong login or password\n");
                }
            } while (guest);

            ProscessAutorized();
        }

        static void Register()
        {
            string login;
            string email;
            string password;
            var notvalid = true;
            do
            {
                Console.WriteLine("Enter login");
                login = Console.ReadLine();
                Console.WriteLine("Enter email");
                email = Console.ReadLine();
                var notconfirm = true;

                do
                {
                    Console.WriteLine("Enter password");
                    password = Console.ReadLine();
                    Console.WriteLine("Confirm password");
                    var confirmPassword = Console.ReadLine();
                    if (password == confirmPassword)
                    {
                        notconfirm = false;
                    }
                    else
                    {
                        Console.WriteLine("Passwords are diffetent");
                    }
                } while (notconfirm);

                notvalid = false;

                var membershipUser = _userService.GetOneByPredicate(u => u.Login == login);

                if (membershipUser != null)
                {
                    Console.WriteLine("User with this login is exist");
                    notvalid = true;
                }

                var sameEmailUser = _userService.GetOneByPredicate(u => u.Email == email);

                if (sameEmailUser != null)
                {
                    Console.WriteLine("User with this email is exist");
                    notvalid = true;
                }
            } while (notvalid);

            _userService.Create(new UserEntity()
            {
                Email = email,
                Login = login,
                Password = HashPassword(password),
                RoleId = _roleService.GetOneByPredicate(role => role.Name == "User").Id
            });;

            var user = _userService.GetOneByPredicate(u => u.Login == login);
            _profileService.Create(new ProfileEntity()
            {
                Id = user.Id
            });

        }

        static void ProscessAutorized()
        {
            while (true)
            {
                Console.WriteLine("1 - Logout");
                switch (Console.ReadLine())
                {
                    case "1":
                        {
                            return;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong input\n");
                            break;
                        }
                }
            }
        }

        public static string HashPassword(string password)
        {
            if (password == null)
                throw new ArgumentNullException("password");
            byte[] salt;
            byte[] bytes;
            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, 16, 1000))
            {
                salt = rfc2898DeriveBytes.Salt;
                bytes = rfc2898DeriveBytes.GetBytes(32);
            }
            byte[] inArray = new byte[49];
            Buffer.BlockCopy((Array)salt, 0, (Array)inArray, 1, 16);
            Buffer.BlockCopy((Array)bytes, 0, (Array)inArray, 17, 32);
            return Convert.ToBase64String(inArray);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (hashedPassword == null)
                throw new ArgumentNullException("hashedPassword");
            if (password == null)
                throw new ArgumentNullException("password");
            byte[] numArray = Convert.FromBase64String(hashedPassword);
            if (numArray.Length != 49 || (int)numArray[0] != 0)
                return false;
            byte[] salt = new byte[16];
            Buffer.BlockCopy((Array)numArray, 1, (Array)salt, 0, 16);
            byte[] a = new byte[32];
            Buffer.BlockCopy((Array)numArray, 17, (Array)a, 0, 32);
            byte[] bytes;
            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, 1000))
                bytes = rfc2898DeriveBytes.GetBytes(32);
            return ByteArraysEqual(a, bytes);
        }

        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (object.ReferenceEquals((object)a, (object)b))
                return true;
            if (a == null || b == null || a.Length != b.Length)
                return false;
            bool flag = true;
            for (int index = 0; index < a.Length; ++index)
                flag &= (int)a[index] == (int)b[index];
            return flag;
        }
    }
}
