using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORM.Entities
{
    public class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50), MinLength(3)]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}
