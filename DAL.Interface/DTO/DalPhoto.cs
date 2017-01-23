using System;
using System.Collections.Generic;

namespace DAL.Interface.DTO
{
    public class DalPhoto : IEntity
    {
        public DalPhoto()
        {
            Tags = new HashSet<DalTag>();
            Likes = new HashSet<DalLike>();
        }
        public int Id { get; set; }

        public DateTime CreationTime { get; set; }

        public string Description { get; set; }

        public int? ProfileId { get; set; }
        
        public byte[] Image { get; set; }

        public virtual ICollection<DalTag> Tags { get; set; }
        public virtual ICollection<DalLike> Likes { get; set; }
    }
}
