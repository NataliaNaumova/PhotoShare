using System;
using BLL.Interface.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.DTO;
using DAL.Interface.Helper;
using DAL.Interface.Repository;
using DAL.Interface.Repository.ModelRepositories;

namespace BLL.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository photoRepository;
        private readonly IUnitOfWork uow;

        public PhotoService(IUnitOfWork uow, IPhotoRepository repository)
        {
            this.uow = uow;
            photoRepository = repository;
        }

        public PhotoEntity GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException();
            var photo = photoRepository.GetById(id);
            if (ReferenceEquals(photo, null))
                return null;
            return photo.ToBllPhoto();
        }

        public IEnumerable<PhotoEntity> GetAllEntities()
        {
            return photoRepository.GetAll().ToList().Select(p => p.ToBllPhoto());
        }

        public PhotoEntity GetOneByPredicate(Expression<Func<PhotoEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<PhotoEntity, DalPhoto>(Expression.Parameter(typeof(DalPhoto), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalPhoto, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return photoRepository.GetOneByPredicate(exp2).ToBllPhoto();
        }

        public IEnumerable<PhotoEntity> GetAllByPredicate(Expression<Func<PhotoEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<PhotoEntity, DalPhoto>(Expression.Parameter(typeof(DalPhoto), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalPhoto, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return photoRepository.GetAllByPredicate(exp2).Select(photo => photo.ToBllPhoto()).ToList();
        }

        public void Create(PhotoEntity item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();

            photoRepository.Create(item.ToDalPhoto());
            uow.Commit();
        }

        public void Delete(PhotoEntity item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();

            photoRepository.Delete(item.ToDalPhoto());
            uow.Commit();
        }

        public void Update(PhotoEntity item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();

            photoRepository.Update(item.ToDalPhoto());
            uow.Commit();
        }

        public void RemoveLike(LikeEntity likeEntity)
        {
            if (ReferenceEquals(likeEntity, null))
                throw new ArgumentNullException();

            photoRepository.RemoveLike(likeEntity.ToDalLike());
            uow.Commit();
        }

        public void AddLike(LikeEntity likeEntity)
        {
            if (ReferenceEquals(likeEntity, null))
                throw new ArgumentNullException();

            photoRepository.AddLike(likeEntity.ToDalLike());
            uow.Commit();
        }
    }
}
