using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ORM.Entities
{
    public class Profile
    {
        public Profile()
        {
            Photos = new HashSet<Photo>();
            Likes = new HashSet<Like>();
        }

        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }

        [MinLength(2), MaxLength(40)]
        public string FirstName { get; set; }

        [MinLength(2), MaxLength(40)]
        public string LastName { get; set; }

        public byte[] Avatar { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
