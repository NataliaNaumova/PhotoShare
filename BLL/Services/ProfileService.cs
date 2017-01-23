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
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork uow;
        private readonly IProfileRepository profileRepository;

        public ProfileService(IUnitOfWork uow, IProfileRepository repository)
        {
            this.uow = uow;
            this.profileRepository = repository;
        }

        public ProfileEntity GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException();
            var profile = profileRepository.GetById(id);
            if (ReferenceEquals(profile, null))
                return null;
            return profile.ToBllProfile();
        }

        public IEnumerable<ProfileEntity> GetAllEntities()
        {
            return profileRepository.GetAll().ToList().Select(profile => profile.ToBllProfile());
        }

        public ProfileEntity GetOneByPredicate(System.Linq.Expressions.Expression<System.Func<ProfileEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<ProfileEntity, DalProfile>(Expression.Parameter(typeof(DalProfile), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalProfile, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return profileRepository.GetOneByPredicate(exp2).ToBllProfile();
        }

        public IEnumerable<ProfileEntity> GetAllByPredicate(System.Linq.Expressions.Expression<System.Func<ProfileEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<ProfileEntity, DalProfile>(Expression.Parameter(typeof(DalProfile), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalProfile, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return profileRepository.GetAllByPredicate(exp2).Select(user => user.ToBllProfile()).ToList();
        }

        public void Create(ProfileEntity profile)
        {
            if (ReferenceEquals(profile, null))
                throw new ArgumentNullException();

            profileRepository.Create(profile.ToDalProfile());
            uow.Commit();
        }

        public void Delete(ProfileEntity profile)
        {
            if (ReferenceEquals(profile, null))
                throw new ArgumentNullException();

            profileRepository.Delete(profile.ToDalProfile());
            uow.Commit();
        }

        public void Update(ProfileEntity item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();

            profileRepository.Update(item.ToDalProfile());
            uow.Commit();
        }
    }
}
