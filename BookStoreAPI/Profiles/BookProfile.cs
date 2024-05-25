
namespace BookStoreAPI.Profiles
{
    /// <summary>
    /// Mapping 
    /// </summary>
    public class BookProfile : Profile
    {

        /// <summary>
        /// Mapping 
        /// </summary>
        public BookProfile()
        {
    
            CreateMap<Book, BookStore.Models.BookForCreationDTO>().ReverseMap();
            CreateMap<BookStore.Models.BookForUpdateDTO, Book>(); 

        }
    }
}
