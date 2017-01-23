using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcPL.Models.ViewModels
{
    public class LoginViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Enter your login")]
        [Required(ErrorMessage = "The field can not be empty.")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9_]+$", ErrorMessage = "Invalid login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "The field can not be empty.")]
        [DataType(DataType.Password)]
        [Display(Name = "Enter your password")]
        public string Password { get; set; }
    }
}