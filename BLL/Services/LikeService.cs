using System;
using BLL.Interface.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.DTO;
using BLL.Interface.Helper;
using DAL.Interface.Repository;
using DAL.Interface.Repository.ModelRepositories;

namespace BLL.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository likeRepository;
        private readonly IUnitOfWork uow;

        public LikeService(IUnitOfWork uow, ILikeRepository repository)
        {
            this.uow = uow;
            likeRepository = repository;
        }
        public LikeEntity GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException();
            var like = likeRepository.GetById(id);
            if (ReferenceEquals(like, null))
                return null;
            return like.ToBllLike();
        }

        public IEnumerable<LikeEntity> GetAllEntities()
        {
            return likeRepository.GetAll().ToList().Select(l => l.ToBllLike());
        }

        public LikeEntity GetOneByPredicate(Expression<Func<LikeEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<LikeEntity, DalLike>(Expression.Parameter(typeof(DalLike), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalLike, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return likeRepository.GetOneByPredicate(exp2).ToBllLike();
        }

        public IEnumerable<LikeEntity> GetAllByPredicate(Expression<Func<LikeEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<LikeEntity, DalLike>(Expression.Parameter(typeof(DalLike), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalLike, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return likeRepository.GetAllByPredicate(exp2).Select(like => like.ToBllLike()).ToList();
        }

        public void Create(LikeEntity item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();

            likeRepository.Create(item.ToDalLike());
            uow.Commit();
        }

        public void Delete(LikeEntity item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();

            likeRepository.Delete(item.ToDalLike());
            uow.Commit();
        }

        public void Update(LikeEntity item)
        {
            throw new System.NotImplementedException();
        }
    }
}
