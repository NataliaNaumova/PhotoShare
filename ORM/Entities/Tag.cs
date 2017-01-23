using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Entities
{
    public class Tag
    {
        public Tag()
        {
            Photos = new HashSet<Photo>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(1), MaxLength(80)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}
