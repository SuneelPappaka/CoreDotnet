using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CoreDotnet.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Email Mandatoy")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Mandatoy")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
