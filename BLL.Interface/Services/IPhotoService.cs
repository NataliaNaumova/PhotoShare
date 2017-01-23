using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IPhotoService : IService<PhotoEntity>
    {
        void RemoveLike(LikeEntity likeEntity);
        void AddLike(LikeEntity likeEntity);
    }
}
