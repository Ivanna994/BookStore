using BookStore.Models;

namespace BookStoreAPI.Profiles
{
    /// <summary>
    /// Mapping 
    /// </summary>
    public class AuthorProfile : Profile
    {
        /// <summary>
        /// Mapping 
        /// </summary>
        public AuthorProfile()
        {
            CreateMap<AuthorForCreationDTO, Author>().ReverseMap();
            CreateMap<AuthorForUpdateDTO, Author>().ReverseMap();
        }
    }
}
