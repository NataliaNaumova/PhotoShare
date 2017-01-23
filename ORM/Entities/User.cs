using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(40)]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [MinLength(5), MaxLength(70)]
        public string Email { get; set; }

        [Required]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        public virtual Profile Profile { get; set; }

    }
}
