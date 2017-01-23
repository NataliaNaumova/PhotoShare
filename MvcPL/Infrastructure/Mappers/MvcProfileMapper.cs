using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcProfileMapper
    {
        public static ProfileModel ToMvcProfile(this ProfileEntity profile)
        {
            if (profile == null)
                return null;
            byte[] avatar = null;
            if (profile.Avatar != null)
            {
                avatar = new byte[profile.Avatar.Length];
                profile.Avatar.CopyTo(avatar, 0);
            }
            return new ProfileModel()
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Avatar = avatar
            };
        }

        public static ProfileEntity ToBllProfile(this ProfileModel profileModel)
        {
            if (profileModel == null)
                return null;
            byte[] avatar = null;
            if (profileModel.Avatar != null)
            {
                avatar = new byte[profileModel.Avatar.Length];
                profileModel.Avatar.CopyTo(avatar, 0);
            }

            return new ProfileEntity()
            {
                Id = profileModel.Id,
                FirstName = profileModel.FirstName,
                LastName = profileModel.LastName,
                Avatar = avatar
            };
        }
    }
}