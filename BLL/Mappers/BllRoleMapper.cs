using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static class BllRoleMapper
    {
        public static DalRole ToDalRole(this RoleEntity role)
        {
            if (role == null)
                return null;
            return new DalRole()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static RoleEntity ToBllRole(this DalRole dalRole)
        {
            if (dalRole == null)
                return null;
            return new RoleEntity()
            {
                Id = dalRole.Id,
                Name = dalRole.Name
            };
        }
    }
}
