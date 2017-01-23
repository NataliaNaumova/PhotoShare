using System;
using DAL.Interface.Repository.ModelRepositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Helper;
using DAL.Mappers;
using ORM.Entities;


namespace DAL.Concrete.ModelRepositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DbContext _context;

        public PhotoRepository(DbContext context)
        {
            _context = context;
        }


        public IEnumerable<DalPhoto> GetAll()
        {
            return _context.Set<Photo>().OrderByDescending(photo => photo.Id).ToList().Select(photo => photo.ToDalPhoto());
        }

        public DalPhoto GetById(int key)
        {
            var orm = _context.Set<Photo>().FirstOrDefault(photo => photo.Id == key);
            if (!ReferenceEquals(orm, null))
                return orm.ToDalPhoto();
            return null;
        }

        public DalPhoto GetOneByPredicate(Expression<Func<DalPhoto, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalPhoto> GetAllByPredicate(Expression<Func<DalPhoto, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalPhoto, Photo>(Expression.Parameter(typeof(Photo), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Photo, bool>>(visitor.Visit(predicate.Body), visitor.NewParameterExp);
            var final = _context.Set<Photo>().Where(express).OrderByDescending(photo => photo.Id).ToList();
            return final.Select(photo => photo.ToDalPhoto());
        }

        public void Create(DalPhoto e)
        {
            var photo = e.ToOrmPhoto();
            var existingTags = new List<Tag>();
            foreach (var tag in photo.Tags)
            {
                if (_context.Set<Tag>().SingleOrDefault(t => t.Name == tag.Name) != null)
                {
                    existingTags.Add(tag);
                    _context.Set<Tag>().Single(t => t.Name == tag.Name).Photos.Add(photo);
                }
            }

            foreach (var tag in existingTags)
            {
                photo.Tags.Remove(tag);
            }

            _context.Set<Photo>().Add(photo);
            _context.SaveChanges();
        }

        public void Delete(DalPhoto e)
        {
            var photo = _context.Set<Photo>().Single(u => u.Id == e.Id);
            _context.Set<Photo>().Remove(photo);
            _context.SaveChanges();
        }

        public void Update(DalPhoto photo)
        {
            _context.Set<Photo>().AddOrUpdate(photo.ToOrmPhoto());
            _context.SaveChanges();
        }

        public void AddLike(DalLike e)
        {
            var photo = _context.Set<Photo>().First(p => p.Id == e.PhotoId);
            _context.Set<Photo>().Attach(photo);
            photo.Likes.Add(e.ToOrmLike());
            _context.SaveChanges();
        }

        public void RemoveLike(DalLike e)
        {
            var photo = _context.Set<Photo>().First(p => p.Id == e.PhotoId);
            photo.Likes.Remove(e.ToOrmLike());
            var like =
                _context.Set<Like>().First(l => l.ProfileId == e.ProfileId && l.PhotoId == e.PhotoId);
            _context.Set<Like>().Remove(like);
            _context.SaveChanges();

        }
    }
}
