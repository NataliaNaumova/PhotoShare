using System.Data.Entity;
using System.Web.Helpers;
using ORM.Entities;

namespace ORM
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<EntityModel> //Different modes (4 types)
    {
        protected override void Seed(EntityModel context)
        {
            #region Roles initializing
            var role = new Role
            {
                Name = "User"
            };
            var adminRole = new Role
            {
                Name = "Admin"
            };
            context.Roles.Add(role);
            context.Roles.Add(adminRole);
            context.SaveChanges();
            #endregion

            #region Users initializing
            var user1 = new User
            {
                Email = "natallianavumava@gmail.com",
                Password = Crypto.HashPassword("123456"),
                Role = adminRole,
                Login = "Natalia"
            };
            var user2 = new User
            {
                Email = "alenka.borikov@gmail.com",
                Password = Crypto.HashPassword("123456"),
                Role = role,
                Login = "Alyona"
            };
            var user3 = new User
            {
                Email = "artya12@gmail.com",
                Password = Crypto.HashPassword("123456"),
                Role = role,
                Login = "Artyom"
            };

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.Users.Add(user3);
            context.SaveChanges();
            #endregion

            #region Profiles initializing
            var profile1 = new Profile
            {
                FirstName = "Natalia",
                LastName = "Naumova",
                User = user1
            };
            var profile2 = new Profile
            {
                FirstName = "Alyona",
                LastName = "Borikova",
                User = user2
            };
            var profile3 = new Profile
            {
                FirstName = "Artyom",
                LastName = "Kazakov",
                User = user3
            };

            context.Profiles.Add(profile1);
            context.Profiles.Add(profile2);
            context.Profiles.Add(profile3);
            context.SaveChanges();
            #endregion
        }
    }
}
