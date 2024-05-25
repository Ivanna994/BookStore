using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Entities
{

    public class Book
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        [ForeignKey("AuthorId")]
        public Author? Author { get; set; }
        public int AuthorId { get; set; }
 
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Title { get; set; } = null!;

        [MinLength(3)]
        [MaxLength(100)]
        public string? SubTitle { get; set; }

    }
}
