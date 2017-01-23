using DAL.Interface.DTO;


namespace DAL.Interface.Repository.ModelRepositories
{
    public interface IPhotoRepository : IRepository<DalPhoto>
    {
        void RemoveLike(DalLike like);
        void AddLike(DalLike like);
    }
}
