using DAL.Interface.DTO;
using ORM.Entities;

namespace DAL.Mappers
{
    public static class DalUserMapper
    {
        public static DalUser ToDalUser(this User user)
        {
            if (user == null)
            {
                return null;
            }

            return new DalUser
            {
                Id = user.Id,             
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
                RoleId = user.RoleId
            };
        }

        public static User ToOrmUser(this DalUser dalUser)
        {
            if (dalUser == null)
            {
                return null;
            }

            return new User
            {
                Id = dalUser.Id,
                Login = dalUser.Login,
                Password = dalUser.Password,
                Email = dalUser.Email,
                RoleId = dalUser.RoleId
            };
        }
    }
}
