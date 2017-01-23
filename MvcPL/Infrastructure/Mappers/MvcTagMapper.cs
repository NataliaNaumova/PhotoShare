using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcTagMapper
    {
        public static TagModel ToMvcTag(this TagEntity tag)
        {
            if (tag == null)
                return null;
            return new TagModel()
            {
                Id = tag.Id,
                Name = tag.Name,
                Photos = tag.Photos.Select(p => p.ToMvcPhoto()).ToList()
            };
        }

        public static TagEntity ToBllTag(this TagModel tagModel)
        {
            if (tagModel == null)
                return null;
            return new TagEntity()
            {
                Id = tagModel.Id,
                Name = tagModel.Name,
                Photos = tagModel.Photos.Select(p => p.ToBllPhoto()).ToList()
            };
        }
    }
}