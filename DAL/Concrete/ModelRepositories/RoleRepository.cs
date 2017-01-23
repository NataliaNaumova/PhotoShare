using DAL.Interface.Repository.ModelRepositories;
using System.Collections.Generic;
using DAL.Interface.DTO;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.Helper;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using ORM.Entities;
using DAL.Mappers;

namespace DAL.Concrete.ModelRepositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DbContext _context;

        public RoleRepository(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<DalRole> GetAll()
        {
            return _context.Set<Role>().ToList().Select(role => role.ToDalRole());
        }

        public DalRole GetById(int key)
        {
            var orm = _context.Set<Role>().FirstOrDefault(role => role.Id == key);
            if (!ReferenceEquals(orm, null))
            {
                return orm.ToDalRole();
            }
            return null;
        }

        public DalRole GetOneByPredicate(Expression<Func<DalRole, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalRole> GetAllByPredicate(Expression<Func<DalRole, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalRole, Role>(Expression.Parameter(typeof(Role), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Role, bool>>(visitor.Visit(predicate.Body), visitor.NewParameterExp);
            var final = _context.Set<Role>().Where(express).ToList();
            return final.Select(role => role.ToDalRole());
        }

        public void Create(DalRole e)
        {
            var role = e.ToOrmRole();
            _context.Set<Role>().Add(role);
            _context.SaveChanges();
        }

        public void Delete(DalRole e)
        {
            var role = _context.Set<Role>().Single(u => u.Id == e.Id);
            _context.Set<Role>().Remove(role);
            _context.SaveChanges();
        }

        public void Update(DalRole entity)
        {
            _context.Set<Role>().AddOrUpdate(entity.ToOrmRole());
            _context.SaveChanges();
        }
    }
}
