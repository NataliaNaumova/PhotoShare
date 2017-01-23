using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static class BllLikeMapper
    {
        public static DalLike ToDalLike(this LikeEntity like)
        {
            if (like == null)
            {
                return null;
            }

            return new DalLike
            {
                Id = like.Id,
                ProfileId = like.ProfileId,
                PhotoId = like.PhotoId
            };
        }

        public static LikeEntity ToBllLike(this DalLike dalLike)
        {
            if (dalLike == null)
            {
                return null;
            }

            return new LikeEntity
            {
                Id = dalLike.Id,
                ProfileId = dalLike.ProfileId,
                PhotoId = dalLike.PhotoId
            };
        }
    }
}
