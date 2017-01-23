using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static class BllProfileMapper
    {
        public static DalProfile ToDalProfile(this ProfileEntity profile)
        {
            if (profile == null)
                return null;
            byte[] avatar = null;
            if (profile.Avatar != null)
            {
                avatar = new byte[profile.Avatar.Length];
                profile.Avatar.CopyTo(avatar, 0);
            }
            return new DalProfile()
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Avatar = avatar
            };
        }

        public static ProfileEntity ToBllProfile(this DalProfile dalProfile)
        {
            if (dalProfile == null)
                return null;
            byte[] avatar = null;
            if (dalProfile.Avatar != null)
            {
                avatar = new byte[dalProfile.Avatar.Length];
                dalProfile.Avatar.CopyTo(avatar, 0);
            }

            return new ProfileEntity()
            {
                Id = dalProfile.Id,
                FirstName = dalProfile.FirstName,
                LastName = dalProfile.LastName,
                Avatar = avatar
            };
        }
    }
}
