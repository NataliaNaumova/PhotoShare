using System.Linq;
using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static class BllTagMapper
    {
        public static DalTag ToDalTag(this TagEntity tag)
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

        public static TagEntity ToBllTag(this DalTag dalTag)
        {
            if (dalTag == null)
                return null;
            return new TagEntity()
            {
                Id = dalTag.Id,
                Name = dalTag.Name,
                Photos = dalTag.Photos.Select(p => p.ToBllPhoto()).ToList()
            };
        }
    }
}
