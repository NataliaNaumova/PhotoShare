using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class TagEntity
    {
        public TagEntity()
        {
            Photos = new List<PhotoEntity>();
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public virtual ICollection<PhotoEntity> Photos { get; set; }

    }
}
