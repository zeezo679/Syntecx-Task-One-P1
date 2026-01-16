using System.ComponentModel.DataAnnotations;

namespace Synt_W1_P1.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
