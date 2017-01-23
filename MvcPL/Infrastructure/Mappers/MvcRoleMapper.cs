using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcRoleMapper
    {
        public static RoleModel ToDalRole(this RoleEntity role)
        {
            if (role == null)
                return null;
            return new RoleModel()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static RoleEntity ToBllRole(this RoleModel roleModel)
        {
            if (roleModel == null)
                return null;
            return new RoleEntity()
            {
                Id = roleModel.Id,
                Name = roleModel.Name
            };
        }
    }
}