using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models.ViewModels
{
    public class ProfileEditViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Enter your name")]
        [StringLength(40, ErrorMessage = "The name must contain at lest {2} characters", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Display(Name = "Enter your surname")]
        [StringLength(40, ErrorMessage = "The surname must contain at least {2} characters", MinimumLength = 2)]
        public string LastName { get; set; }

        [Display(Name = "Upload new avatar")]
        public byte[] Avatar { get; set; }
    }
}