using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

namespace ORM.Entities
{
    public class Photo
    {
        public Photo()
        {
            Tags = new HashSet<Tag>();
            Likes = new HashSet<Like>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Created")]
        public DateTime CreationTime { get; set; }

        public string Description { get; set; }

        public int? ProfileId { get; set; }

        [ForeignKey("ProfileId")]
        public virtual Profile Profile { get; set; }

        [Required]
        public byte[] Image { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
