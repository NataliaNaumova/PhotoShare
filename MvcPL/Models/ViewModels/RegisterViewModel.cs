using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcPL.Models.ViewModels
{
    public class RegisterViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Enter your e-mail")]
        [Required(ErrorMessage = "The field can not be empty.")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Wrong e-mail.")]
        [Remote("CheckEmailUniqueness", "Account", HttpMethod = "GET", ErrorMessage = "This email already exists.")]
        public string Email { get; set; }

        [Display(Name = "Enter your login")]
        [Required(ErrorMessage = "The field can not be empty.")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9_]+$", ErrorMessage = "Invalid login")]
        [StringLength(40, ErrorMessage = "The name must contain at least {2} characters.", MinimumLength = 3)]
        [Remote("CheckLoginUniqueness", "Account", HttpMethod = "GET", ErrorMessage = "This login already exists.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "The field can not be empty.")]
        [StringLength(50, ErrorMessage = "The password must contain at least {2} characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Enter your password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The field can not be empty.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm the password")]
        [Compare("Password", ErrorMessage = "Passwords must match.")]
        public string ConfirmPassword { get; set; }
    }
}