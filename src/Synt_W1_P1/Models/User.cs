using System.ComponentModel.DataAnnotations;

namespace Synt_W1_P1.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Username must at least be 3 character sequences")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string? Address { get; set; }
    }
}
