using BookStore.Entities;
using BookStore.Models;

namespace BookStore.Services
{
    /// <summary>
    /// Manipulate with books and authors
    /// </summary>
    public interface IBookStoreRepository
    {
        /// <summary>
        /// Returns all books
        /// </summary>
        Task<IEnumerable<Book>> GetBooksAsync();

        /// <summary>
        /// Returns book by id
        /// </summary>
        Task<Book?> GetBookAsync(int bookId, bool includeAuthor = true);

        /// <summary>
        /// Returns book by id
        /// </summary>
        Task<(IEnumerable<Book>, PaginationMetadata)> GetBooksAsync(string? title, string? authorName, string? searchQuery, int pageNumber, int pageSize);

        /// <summary>
        /// Returns books by authorId
        /// </summary>
        Task<IEnumerable<Book>> GetBooksForAuthorAsync(int authorId, bool includeAuthor = true);

        /// <summary>
        /// Returns book by authorId and bookId
        /// </summary>
        Task<Book?> GetBookForAuthorAsync(int authorId, int bookId, bool includeAuthor = true);

        /// <summary>
        /// Checks if book exists
        /// </summary>
        Task<bool> BookExistsAsync(int bookId);

        /// <summary>
        /// Create new book 
        /// </summary>
        Task CreateBookAsync(BookForCreationDTO bookForCreate);

        /// <summary>
        /// Update the book 
        /// </summary>
        Task<Book?> UpdateBookAsync(BookForUpdateDTO request);

        /// <summary>
        /// Delete the book 
        /// </summary>
        void DeleteBookAsync(int bookId);

        /// <summary>
        /// Returns all authors 
        /// </summary>
        Task<IEnumerable<Author>> GetAuthorsAsync();

        /// <summary>
        /// Returns author by authorId
        /// </summary>
        Task<Author?> GetAuthorAsync(int authorId);

        /// <summary>
        /// Checks if author exists
        /// </summary>
        Task<bool> AuthorExistsAsync(int authorId);

        /// <summary>
        /// Create new author
        /// </summary>
        Task CreateAuthorAsync(AuthorForCreationDTO authorForCreate);

        /// <summary>
        /// Update an author
        /// </summary>
        Task<Author?> UpdateAuthorAsync(AuthorForUpdateDTO request);

        /// <summary>
        /// Delete an author
        /// </summary>
        void DeleteAuthorAsync(int authorId);

        /// <summary>
        /// Saves all changes made in the context
        /// </summary>
        Task<bool> SaveChangesAsync();


    }
}
