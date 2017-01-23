using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcLikeMapper
    {
        public static LikeModel ToMvcLike(this LikeEntity like)
        {
            if (like == null)
            {
                return null;
            }

            return new LikeModel
            {
                Id = like.Id,
                ProfileId = like.ProfileId,
                PhotoId = like.PhotoId
            };
        }

        public static LikeEntity ToBllLike(this LikeModel likeModel)
        {
            if (likeModel == null)
            {
                return null;
            }

            return new LikeEntity
            {
                Id = likeModel.Id,
                ProfileId = likeModel.ProfileId,
                PhotoId = likeModel.PhotoId
            };
        }
    }
}