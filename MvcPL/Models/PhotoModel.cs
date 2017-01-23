using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class PhotoModel
    {
        public PhotoModel()
        {
            Tags = new List<TagModel>();
            Likes = new List<LikeModel>();
        }
        public int Id { get; set; }

        public DateTime CreationTime { get; set; }

        public string Description { get; set; }

        public int? ProfileId { get; set; }

        public byte[] Image { get; set; }

        public ICollection<TagModel> Tags { get; set; }
        public ICollection<LikeModel> Likes { get; set; }

        public string UserLogin { get; set; }
    }
}