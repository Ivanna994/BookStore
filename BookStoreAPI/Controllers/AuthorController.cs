using BookStore.DbContexts;
using BookStore.Entities;
using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using ValidationException = BookStore.Exceptions.ValidationException;

namespace BookStoreAPI.Controllers
{

    /// <summary>
    /// Manipulation with author
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthorController : Controller
    {
        private readonly IBookStoreRepository _bookStoreRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Manipulation with author
        /// </summary>
        public AuthorController(IBookStoreRepository bookStoreRepository, IMapper mapper)
        {
            _bookStoreRepository = bookStoreRepository ?? throw new ArgumentNullException(nameof(bookStoreRepository));
            _mapper = mapper;
        }

        /// <summary>
        /// Get all authors
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<Author>>> GetAuthors()
        {
            return Ok(await _bookStoreRepository.GetAuthorsAsync());
        }

        /// <summary>
        /// Get an author by id
        /// </summary>
        /// <param name="authorId">Author id</param>
        [HttpGet("{authorId}")]
        public async Task<ActionResult<Author>> GetAuthor(int authorId)
        {
            var author = await _bookStoreRepository.GetAuthorAsync(authorId);
            if (author == null)
            {
                return BadRequest("Author is not found.");
            }
            return author;
        }

        /// <summary>
        /// Create an author
        /// </summary>
        /// <param name="request">Author name</param>
        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor(AuthorForCreationDTO request)
        {
            try
            {
                await _bookStoreRepository.CreateAuthorAsync(request);
                return Ok();
            }
            catch (ValidationException v)
            {
                return BadRequest(new
                {
                    error = new
                    {
                        message = v.Message
                    }
                });
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Update an author
        /// </summary>
        /// <param name="request">Author id and name</param>
        [HttpPut]
        public async Task<ActionResult> UpdateAuthor(AuthorForUpdateDTO request)
        {
            try
            {

                if (!await _bookStoreRepository.AuthorExistsAsync(request.AuthorId))
                {
                    return NotFound("Author is not found.");
                }

                var dbAuthor = await _bookStoreRepository.GetAuthorAsync(request.AuthorId);
                if (dbAuthor == null)
                {
                    return NotFound();
                }

                _mapper.Map(request, dbAuthor);
                await _bookStoreRepository.SaveChangesAsync();
                return Ok();
            }
            catch (ValidationException v)
            {
                return BadRequest(new
                {
                    error = new
                    {
                        message = v.Message
                    }
                });
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete an author and all his books
        /// </summary>
        /// <param name="authorId">Author id and name</param>
        [HttpDelete]
        public async Task<ActionResult> DeleteAuthor(int authorId)
        {
            if (!await _bookStoreRepository.AuthorExistsAsync(authorId))
            {
                return NotFound();
            }
            _bookStoreRepository.DeleteAuthorAsync(authorId);
            await _bookStoreRepository.SaveChangesAsync();

            return NoContent();
        }

    }
}
