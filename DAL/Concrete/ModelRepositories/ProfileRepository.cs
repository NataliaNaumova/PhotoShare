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
    public class ProfileRepository : IProfileRepository
    {
        private readonly DbContext _context;

        public ProfileRepository(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<DalProfile> GetAll()
        {
            return _context.Set<Profile>().ToList().Select(profile => profile.ToDalProfile());
        }

        public DalProfile GetById(int key)
        {
            var orm = _context.Set<Profile>().FirstOrDefault(profile => profile.Id == key);
            if (!ReferenceEquals(orm, null))
                return orm.ToDalProfile();
            return null;
        }

        public DalProfile GetOneByPredicate(Expression<Func<DalProfile, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalProfile> GetAllByPredicate(Expression<Func<DalProfile, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalProfile, Profile>(Expression.Parameter(typeof(Profile), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Profile, bool>>(visitor.Visit(predicate.Body), visitor.NewParameterExp);
            var final = _context.Set<Profile>().Where(express).ToList();
            return final.Select(profile => profile.ToDalProfile());
        }

        public void Create(DalProfile dalProfile)
        {
            _context.Set<Profile>().Add(dalProfile.ToOrmProfile());
            _context.SaveChanges();
        }

        public void Delete(DalProfile e)
        {
            var profile = _context.Set<Profile>().FirstOrDefault(u => u.Id == e.Id);
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
                _context.SaveChanges();
            }
        }

        public void Update(DalProfile entity)
        {
            _context.Set<Profile>().AddOrUpdate(entity.ToOrmProfile());
            _context.SaveChanges();
            
        }
    }
}
