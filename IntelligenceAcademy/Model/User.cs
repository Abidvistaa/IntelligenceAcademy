using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntelligenceAcademy.Model
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string AccountType { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public DateTime ActionDate { get; set; }
    }
}
