using System;
using DAL.Interface.Repository.ModelRepositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Helper;
using DAL.Mappers;
using ORM.Entities;


namespace DAL.Concrete.ModelRepositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly DbContext _context;

        public LikeRepository(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<DalLike> GetAll()
        {
            return _context.Set<Like>().ToList().Select(like => like.ToDalLike());
        }

        public DalLike GetById(int key)
        {
            var orm = _context.Set<Like>().FirstOrDefault(like => like.Id == key);
            if (!ReferenceEquals(orm, null))
                return orm.ToDalLike();
            return null;
        }

        public DalLike GetOneByPredicate(Expression<Func<DalLike, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalLike> GetAllByPredicate(Expression<Func<DalLike, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalLike, Like>(Expression.Parameter(typeof(Like), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Like, bool>>(visitor.Visit(predicate.Body), visitor.NewParameterExp);
            var final = _context.Set<Like>().Where(express).ToList();
            return final.Select(like => like.ToDalLike());
        }

        public void Create(DalLike e)
        {
            var photo = _context.Set<Photo>().First(p => p.Id == e.PhotoId);
            _context.Set<Photo>().Attach(photo);
            photo.Likes.Add(e.ToOrmLike());
            _context.SaveChanges();
        }

        public void Delete(DalLike e)
        {
            var photo = _context.Set<Photo>().First(p => p.Id == e.PhotoId);
            photo.Likes.Remove(e.ToOrmLike());
            var like =
                _context.Set<Like>().First(l => l.ProfileId == e.ProfileId && l.PhotoId == e.PhotoId);
            _context.Set<Like>().Remove(like);
            _context.SaveChanges();

        }

        public void Update(DalLike entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
