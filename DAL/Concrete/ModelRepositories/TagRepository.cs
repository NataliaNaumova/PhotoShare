using System;
using DAL.Interface.Repository.ModelRepositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using DAL.Interface.DTO;
using ORM.Entities;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.Helper;
using DAL.Mappers;

namespace DAL.Concrete.ModelRepositories
{
    public class TagRepository : ITagRepository
    {
        private readonly DbContext _context;

        public TagRepository(DbContext context)
        {
            _context = context;
        }
        public IEnumerable<DalTag> GetAll()
        {
            return _context.Set<Tag>().ToList().Select(tag => tag.ToDalTag());
        }

        public DalTag GetById(int key)
        {
            var ormtag = _context.Set<Tag>().FirstOrDefault(tag => tag.Id == key);
            if (!ReferenceEquals(ormtag, null))
                return ormtag.ToDalTag();
            return null;
        }

        public DalTag GetOneByPredicate(Expression<Func<DalTag, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalTag> GetAllByPredicate(Expression<Func<DalTag, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalTag, Tag>(Expression.Parameter(typeof(Tag), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Tag, bool>>(visitor.Visit(predicate.Body), visitor.NewParameterExp);
            var final = _context.Set<Tag>().Where(express).ToList();
            return final.Select(tag => tag.ToDalTag());
        }

        public void Create(DalTag e)
        {
            var tag = e.ToOrmTag();
            _context.Set<Tag>().Add(tag);
            _context.SaveChanges();
        }
        
        public void Delete(DalTag e)
        {
            var tag = _context.Set<Tag>().Single(u => u.Id == e.Id);
            _context.Set<Tag>().Remove(tag);
            _context.SaveChanges();
        }

        public void Update(DalTag entity)
        {
            _context.Set<Tag>().AddOrUpdate(entity.ToOrmTag());
            _context.SaveChanges();
        }
    }
}
