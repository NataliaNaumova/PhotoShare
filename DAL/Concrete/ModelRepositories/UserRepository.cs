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
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _context;

        public UserRepository(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<DalUser> GetAll()
        {
            return _context.Set<User>().ToList().Select(user => user.ToDalUser());
        }

        public DalUser GetById(int key)
        {
            var ormuser = _context.Set<User>().FirstOrDefault(user => user.Id == key);
            if (!ReferenceEquals(ormuser, null))
                return ormuser.ToDalUser();
            return null;
        }

        public DalUser GetOneByPredicate(Expression<System.Func<DalUser, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalUser> GetAllByPredicate(Expression<System.Func<DalUser, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalUser, User>(Expression.Parameter(typeof(User), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<User, bool>>(visitor.Visit(predicate.Body), visitor.NewParameterExp);
            var final = _context.Set<User>().Where(express).ToList();
            return final.Select(user => user.ToDalUser());
        }

        public void Create(DalUser dalUser)
        {
            _context.Set<User>().Add(dalUser.ToOrmUser());
            _context.SaveChanges();
        }

        public void Delete(DalUser dalUser)
        {
            var user = _context.Set<User>().FirstOrDefault(u => u.Id == dalUser.Id);
            if (user != null)
            {
                var profile = _context.Set<Profile>().FirstOrDefault(u => u.Id == user.Id);
                if (profile != null)
                {
                    var photos = _context.Set<Photo>().Where(p => p.ProfileId == profile.Id);
                    if (photos != null)
                    {
                        foreach (var photo in photos)
                        {
                            _context.Set<Photo>().Remove(photo);
                        }
                    }
                    _context.Set<Profile>().Remove(profile);
                    _context.Set<User>().Remove(user);
                    _context.SaveChanges();
                }
            }
        }

        public void Update(DalUser entity)
        {
            _context.Set<User>().AddOrUpdate(entity.ToOrmUser());
            _context.SaveChanges();
        }


    }
}
