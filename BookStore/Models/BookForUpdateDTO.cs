using BookStore.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookStore.Models
{
    public class BookForUpdateDTO
    {

        public int BookId { get; set; }

        public int AuthorId { get; set; }
        [JsonIgnore]
        public virtual Author? Author { get; set; }

        //[Required(ErrorMessage = "You should provide a Title value.")]
        //[MinLength(3)]
        //[MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        public string? SubTitle { get; set; }
    }
}
