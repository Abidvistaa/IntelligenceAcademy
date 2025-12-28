using System.ComponentModel.DataAnnotations;

namespace IntelligenceAcademy.ViewModel
{
    public class GoogleSignInViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
