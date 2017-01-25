using System;
using BLL.Interface.Services;
using BLL.Interface.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BLL.Mappers;
using DAL.Interface.DTO;
using BLL.Interface.Helper;
using DAL.Interface.Repository;
using DAL.Interface.Repository.ModelRepositories;


namespace BLL.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository tagRepository;
        private readonly IUnitOfWork uow;

        public TagService(IUnitOfWork uow, ITagRepository repository)
        {
            this.uow = uow;
            tagRepository = repository;
        }
        public TagEntity GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException();
            var tag = tagRepository.GetById(id);
            if (ReferenceEquals(tag, null))
                return null;
            return tag.ToBllTag();
        }

        public IEnumerable<TagEntity> GetAllEntities()
        {
            return tagRepository.GetAll().ToList().Select(tag => tag.ToBllTag());
        }

        public TagEntity GetOneByPredicate(Expression<Func<TagEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<TagEntity, DalTag>(Expression.Parameter(typeof(DalTag), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalTag, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return tagRepository.GetOneByPredicate(exp2).ToBllTag();
        }

        public IEnumerable<TagEntity> GetAllByPredicate(Expression<Func<TagEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<TagEntity, DalTag>(Expression.Parameter(typeof(DalTag), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalTag, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return tagRepository.GetAllByPredicate(exp2).Select(user => user.ToBllTag()).ToList();
        }

        public void Create(TagEntity item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();
            tagRepository.Create(item.ToDalTag());
            uow.Commit();
        }

        public void Delete(TagEntity item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();
            tagRepository.Delete(item.ToDalTag());
            uow.Commit();
        }

        public void Update(TagEntity item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();
            tagRepository.Update(item.ToDalTag());
            uow.Commit();
        }
    }
}
