using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class PhotoEntity
    {
        public PhotoEntity()
        {
            Tags = new HashSet<TagEntity>();
            Likes = new HashSet<LikeEntity>();
        }
        public int Id { get; set; }

        public DateTime CreationTime { get; set; }

        public string Description { get; set; }

        public int? ProfileId { get; set; }

        public byte[] Image { get; set; }

        public virtual ICollection<TagEntity> Tags { get; set; }
        public virtual ICollection<LikeEntity> Likes { get; set; }
    }
}
