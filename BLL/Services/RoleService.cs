using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.DTO;
using BLL.Interface.Helper;
using DAL.Interface.Repository;
using DAL.Interface.Repository.ModelRepositories;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;
        private readonly IUnitOfWork uow;

        public RoleService(IUnitOfWork uow, IRoleRepository repository)
        {
            this.uow = uow;
            roleRepository = repository;
        }

        public RoleEntity GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException();
            return roleRepository.GetById(id).ToBllRole();
        }

        public IEnumerable<RoleEntity> GetAllEntities()
        {
            return roleRepository.GetAll().ToList().Select(role => role.ToBllRole());
        }

        public RoleEntity GetOneByPredicate(System.Linq.Expressions.Expression<System.Func<RoleEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<RoleEntity, DalRole>(Expression.Parameter(typeof(DalRole), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalRole, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return roleRepository.GetOneByPredicate(exp2).ToBllRole();
        }

        public IEnumerable<RoleEntity> GetAllByPredicate(System.Linq.Expressions.Expression<System.Func<RoleEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<RoleEntity, DalRole>(Expression.Parameter(typeof(DalRole), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalRole, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return roleRepository.GetAllByPredicate(exp2).Select(user => user.ToBllRole()).ToList();
        }

        public void Create(RoleEntity role)
        {
            if (ReferenceEquals(role, null))
                throw new ArgumentNullException();

            roleRepository.Create(role.ToDalRole());
            uow.Commit();
        }

        public void Delete(RoleEntity item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();

            roleRepository.Delete(item.ToDalRole());
            uow.Commit();
        }

        public void Update(RoleEntity item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();

            roleRepository.Update(item.ToDalRole());
            uow.Commit();
        }
    }
}
