using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcPhotoMapper
    {
        public static PhotoModel ToMvcPhoto(this PhotoEntity photo)
        {
            if (photo == null)
                return null;
            byte[] image = null;
            if (photo.Image != null)
            {
                image = new byte[photo.Image.Length];
                photo.Image.CopyTo(image, 0);
            }

            return new PhotoModel()
            {
                Id = photo.Id,
                CreationTime = photo.CreationTime,
                Description = photo.Description,
                ProfileId = photo.ProfileId,
                Image = image,
                Likes = photo.Likes.Select(l => l.ToMvcLike()).ToList(),
                Tags = photo.Tags.Select(t => t.ToMvcTag()).ToList()
            };
        }

        public static PhotoEntity ToBllPhoto(this PhotoModel photoModel)
        {
            if (photoModel == null)
                return null;
            byte[] image = null;
            if (photoModel.Image != null)
            {
                image = new byte[photoModel.Image.Length];
                photoModel.Image.CopyTo(image, 0);
            }

            return new PhotoEntity()
            {
                Id = photoModel.Id,
                CreationTime = photoModel.CreationTime,
                Description = photoModel.Description,
                ProfileId = photoModel.ProfileId,
                Image = image,
                Likes = photoModel.Likes.Select(l => l.ToBllLike()).ToList(),
                Tags = photoModel.Tags.Select(t => t.ToBllTag()).ToList()
            };
        }
    }
}