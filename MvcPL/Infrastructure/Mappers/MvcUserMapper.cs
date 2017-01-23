using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcUserMapper
    {
        public static UserModel ToMvcUser(this UserEntity userEntity)
        {
            if (userEntity == null)
            {
                return null;
            }

            return new UserModel
            {
                Id = userEntity.Id,
                Login = userEntity.Login,
                Password = userEntity.Password,
                Email = userEntity.Email,
                RoleId = userEntity.RoleId
            };
        }

        public static UserEntity ToBllUser(this UserModel userModel)
        {
            if (userModel == null)
            {
                return null;
            }

            return new UserEntity
            {
                Id = userModel.Id,
                Login = userModel.Login,
                Password = userModel.Password,
                Email = userModel.Email,
                RoleId = userModel.RoleId
            };
        }
    }
}