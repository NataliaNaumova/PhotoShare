using DAL.Interface.DTO;
using ORM.Entities;

namespace DAL.Mappers
{
    public static class DalLikeMapper
    {
        public static DalLike ToDalLike(this Like like)
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

        public static Like ToOrmLike(this DalLike dalLike)
        {
            if (dalLike == null)
            {
                return null;
            }

            return new Like
            {
                Id = dalLike.Id,
                ProfileId = dalLike.ProfileId,
                PhotoId = dalLike.PhotoId
            };
        }
    }
}
