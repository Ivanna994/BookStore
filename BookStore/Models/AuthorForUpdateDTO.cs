using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class AuthorForUpdateDTO
    {
        public int AuthorId { get; set; }

        //[Required(ErrorMessage = "You should provide a Name value for author.")]
        //[MinLength(3)]
        //[MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
