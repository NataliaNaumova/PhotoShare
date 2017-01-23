using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.DTO;
using DAL.Interface.Helper;
using DAL.Interface.Repository;
using DAL.Interface.Repository.ModelRepositories;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;

        public UserService(IUnitOfWork uow, IUserRepository repository)
        {
            this.uow = uow;
            userRepository = repository;
        }
        public UserEntity GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException();
            return userRepository.GetById(id).ToBllUser();
        }

        public IEnumerable<UserEntity> GetAllEntities()
        {
            return userRepository.GetAll().ToList().Select(user => user.ToBllUser());
        }

        public UserEntity GetOneByPredicate(Expression<Func<UserEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<UserEntity, DalUser>(Expression.Parameter(typeof(DalUser), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalUser, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return userRepository.GetOneByPredicate(exp2).ToBllUser();
        }

        public IEnumerable<UserEntity> GetAllByPredicate(Expression<Func<UserEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<UserEntity, DalUser>(Expression.Parameter(typeof(DalUser), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalUser, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return userRepository.GetAllByPredicate(exp2).Select(user => user.ToBllUser()).ToList();
        }

        public void Create(UserEntity user)
        {
            if (ReferenceEquals(user, null))
                throw new ArgumentNullException();

            userRepository.Create(user.ToDalUser());
            uow.Commit();
        }

        public void Delete(UserEntity user)
        {
            if (ReferenceEquals(user, null))
                throw new ArgumentNullException();

            userRepository.Delete(user.ToDalUser());
            uow.Commit();
        }

        public void Update(UserEntity user)
        {
            if (ReferenceEquals(user, null))
                throw new ArgumentNullException();

            userRepository.Update(user.ToDalUser());
            uow.Commit();
        }
    }
}
