using System.ComponentModel.DataAnnotations;

namespace IntelligenceAcademy.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

    }
}
