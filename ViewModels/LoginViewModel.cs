using System.ComponentModel.DataAnnotations;

namespace CoreDotnet.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email Mandatoy")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Mandatoy")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Remember me??")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }

    }
}
