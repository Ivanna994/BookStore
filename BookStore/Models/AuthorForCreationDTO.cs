using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class AuthorForCreationDTO
    {
        //[Required(ErrorMessage = "You should provide a Name value.")]
        //[MinLength(3)]
        //[MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
