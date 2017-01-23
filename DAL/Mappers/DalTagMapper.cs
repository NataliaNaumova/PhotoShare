using System.Linq;
using DAL.Interface.DTO;
using ORM.Entities;

namespace DAL.Mappers
{
    public static class DalTagMapper
    {
        public static DalTag ToDalTag(this Tag tag)
        {
            if (tag == null)
                return null;
            return new DalTag()
            {
                Id = tag.Id,
                Name = tag.Name,
                Photos = tag.Photos.Select(p => p.ToDalPhoto()).ToList()
            };
        }

        public static Tag ToOrmTag(this DalTag dalTag)
        {
            if (dalTag == null)
                return null;
            return new Tag()
            {
                Id = dalTag.Id,
                Name = dalTag.Name,
                Photos = dalTag.Photos.Select(p => p.ToOrmPhoto()).ToList()
            };
        }
    }
}
