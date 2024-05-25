using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class BookForCreationDTO
    {
        public int AuthorId { get; set; }
        //[JsonIgnore]
        //public virtual Author? Author { get; set; }

        //[Required(ErrorMessage = "You should provide a Title value.")]
        //[MinLength(3)]
        //[MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        public string? SubTitle { get; set; }
    }
}
