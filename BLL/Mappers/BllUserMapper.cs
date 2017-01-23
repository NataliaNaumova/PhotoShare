using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static class BllUserMapper
    {
        public static DalUser ToDalUser(this UserEntity user)
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

        public static UserEntity ToBllUser(this DalUser dalUser)
        {
            if (dalUser == null)
            {
                return null;
            }

            return new UserEntity
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
