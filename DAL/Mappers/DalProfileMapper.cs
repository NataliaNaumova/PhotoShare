using DAL.Interface.DTO;
using ORM.Entities;

namespace DAL.Mappers
{
    public static class DalProfileMapper
    {
        public static DalProfile ToDalProfile(this Profile profile)
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

        public static Profile ToOrmProfile(this DalProfile dalProfile)
        {
            if (dalProfile == null)
                return null;
            byte[] avatar = null;
            if (dalProfile.Avatar != null)
            {
                avatar = new byte[dalProfile.Avatar.Length];
                dalProfile.Avatar.CopyTo(avatar, 0);
            }

            return new Profile()
            {
                Id = dalProfile.Id,
                FirstName = dalProfile.FirstName,
                LastName = dalProfile.LastName,
                Avatar = avatar
            };
        }
    }
}
