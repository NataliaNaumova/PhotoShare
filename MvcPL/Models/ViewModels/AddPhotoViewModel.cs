using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models.ViewModels
{
    public class AddPhotoViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreationTime { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public byte[] Image { get; set; }

        [Display(Name = "Print tags separated by spaces")]
        [RegularExpression(@"[\w\s]+", ErrorMessage = "Tags should consist only of characters")]
        public string Tags { get; set; }
    }
}