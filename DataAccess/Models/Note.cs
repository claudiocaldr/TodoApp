using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Title { get; set; }
        [Required]
        [MinLength(5)]
        public string Description { get; set; }
    }
}
