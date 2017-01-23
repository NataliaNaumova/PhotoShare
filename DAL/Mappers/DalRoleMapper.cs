using DAL.Interface.DTO;
using ORM.Entities;

namespace DAL.Mappers
{
    public static class DalRoleMapper
    {
        public static DalRole ToDalRole(this Role role)
        {
            if (role == null)
                return null;
            return new DalRole()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static Role ToOrmRole(this DalRole dalRole)
        {
            if (dalRole == null)
                return null;
            return new Role()
            {
                Id = dalRole.Id,
                Name = dalRole.Name
            };
        }
    }
}
