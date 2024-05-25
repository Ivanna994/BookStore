using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using AutoMapper;
using BookStore.Entities;
using System.ComponentModel.DataAnnotations;
using ValidationException = BookStore.Exceptions.ValidationException;
using Microsoft.AspNetCore.Hosting.Server;

namespace BookStoreAPI.Controllers
{

    /// <summary>
    /// Manipulation with book
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookController : Controller
    {
        private readonly IBookStoreRepository _bookStoreRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Manipulation with book
        /// </summary>
        public BookController(IBookStoreRepository bookStoreRepository, IMapper mapper)
        {
            _bookStoreRepository = bookStoreRepository ?? throw new ArgumentNullException(nameof(bookStoreRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Returns all books 
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAllBooksAsync()
        {
            return Ok(await _bookStoreRepository.GetBooksAsync());
        }

        /// <summary>
        /// Returns all books by criteria
        /// </summary>
        /// <param name="title">Book title with an equal operator</param>
        /// <param name="authorName">Author name with an equal operator</param>
        /// <param name="searchQuery">Search by book title and author name with contains method</param>
        /// <param name="pageNumber">Current page</param>
        /// <param name="pageSize">Page size</param>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksAsync(
            string? title, string? authorName, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {

            var (bookEntities, paginationMetadata) = await _bookStoreRepository
                .GetBooksAsync(title, authorName, searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<Book>>(bookEntities));

        }

        /// <summary>
        /// Returns all books by id
        /// </summary>
        /// <param name="id">Book title with an equal operator</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookStoreRepository.GetBookAsync(id);
            if (book == null)
            {
                return BadRequest("Book is not found.");
            }
            return book;
        }

        /// <summary>
        /// Create new book
        /// </summary>
        /// <param name="request">Book with Title and Subtitle optionally</param>
        [HttpPost]
        public async Task<ActionResult> CreateBook(BookForCreationDTO request)
        {
            try
            {
                await _bookStoreRepository.CreateBookAsync(request);
                return Ok();
            }

            catch (ValidationException v)
            {
                return BadRequest(new { error = new { message = v.Message } });
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }

        }

        /// <summary>
        /// Update the book
        /// </summary>
        /// <param name="request">Book with Title and Subtitle optionally</param>
        [HttpPut]
        public async Task<ActionResult> UpdateBook(BookForUpdateDTO request)
        {
            try
            {
                if (!await _bookStoreRepository.BookExistsAsync(request.BookId))
                {
                    return NotFound("Book is not found.");
                }

                var dbBook = await _bookStoreRepository.GetBookAsync(request.BookId, false);
                if (dbBook == null)
                {
                    return NotFound();
                }

                _mapper.Map(request, dbBook);
                await _bookStoreRepository.SaveChangesAsync();
                return NoContent();

            }
            catch (ValidationException v)
            {
                return BadRequest(new { error = new { message = v.Message } });
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete the book
        /// </summary>
        /// <param name="bookId">Book ID</param>
        [HttpDelete]
        public async Task<ActionResult> DeleteBook(int bookId)
        {
            if (!await _bookStoreRepository.BookExistsAsync(bookId))
            {
                return NotFound();
            }
            _bookStoreRepository.DeleteBookAsync(bookId);
            await _bookStoreRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
