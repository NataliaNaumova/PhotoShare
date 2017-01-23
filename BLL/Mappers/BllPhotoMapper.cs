using System.Linq;
using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Mappers
{
    public static class BllPhotoMapper
    {
        public static DalPhoto ToDalPhoto(this PhotoEntity photo)
        {
            if (photo == null)
                return null;
            byte[] image = null;
            if (photo.Image != null)
            {
                image = new byte[photo.Image.Length];
                photo.Image.CopyTo(image, 0);
            }

            return new DalPhoto()
            {
                Id = photo.Id,
                CreationTime = photo.CreationTime,
                Description = photo.Description,
                ProfileId = photo.ProfileId,
                Image = image,
                Likes = photo.Likes.Select(l => l.ToDalLike()).ToList(),
                Tags = photo.Tags.Select(t => new DalTag()
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToList()
            };
        }

        public static PhotoEntity ToBllPhoto(this DalPhoto dalPhoto)
        {
            if (dalPhoto == null)
                return null;
            byte[] image = null;
            if (dalPhoto.Image != null)
            {
                image = new byte[dalPhoto.Image.Length];
                dalPhoto.Image.CopyTo(image, 0);
            }

            return new PhotoEntity()
            {
                Id = dalPhoto.Id,
                CreationTime = dalPhoto.CreationTime,
                Description = dalPhoto.Description,
                ProfileId = dalPhoto.ProfileId,
                Image = image,
                Likes = dalPhoto.Likes.Select(l => l.ToBllLike()).ToList(),
                Tags = dalPhoto.Tags.Select(t => new TagEntity()
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToList()
            };
        }
    }
}
