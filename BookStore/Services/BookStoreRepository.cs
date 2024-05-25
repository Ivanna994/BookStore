using AutoMapper;
using BookStore.DbContexts;
using BookStore.Entities;
using BookStore.Models;
using BookStore.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using ValidationException = BookStore.Exceptions.ValidationException;

namespace BookStore.Services
{

    public class BookStoreRepository : IBookStoreRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private BindingList<string> errors = new BindingList<string>();

        public BookStoreRepository(DataContext dataContext, IMapper mapper)
        {
            _context = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            _mapper = mapper;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _context.Book.OrderBy(b => b.Title).ToListAsync();
        }

        public async Task<Book?> GetBookAsync(int bookId, bool includeAuthor = true)
        {
            if (includeAuthor)
                return await _context.Book.Include(b => b.Author)
                        .Where(b => b.BookId == bookId).FirstOrDefaultAsync();

            return await _context.Book.FirstOrDefaultAsync(b => b.BookId == bookId);
        }

        public async Task<(IEnumerable<Book>, PaginationMetadata)> GetBooksAsync(
            string? title, string? authorName, string? searchQuery, int pageNumber, int pageSize)
        {
            var collection = _context.Book as IQueryable<Book>;

            if (!string.IsNullOrWhiteSpace(title))
            {
                title = title.Trim();
                collection = collection.Where(b => b.Title == title);
            }

            await collection.Include(a => a.Author).FirstOrDefaultAsync();

            if (!string.IsNullOrWhiteSpace(authorName))
            {
                searchQuery = authorName.Trim();
                collection = collection.Where(b => b.Author != null && b.Author.Name == authorName);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(b => b.Title.Contains(searchQuery)
                    || (b.Author != null && b.Author.Name.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(b => b.BookId)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task<IEnumerable<Book?>> GetBooksForAuthorAsync(int authorId, bool includeAuthor = true)
        {
            if (includeAuthor)
                return await _context.Book.Include(b => b.Author).Where(b => b.AuthorId == authorId).ToListAsync();

            return await _context.Book.Where(b => b.AuthorId == authorId).ToListAsync(); ;
        }

        public async Task<Book?> GetBookForAuthorAsync(int authorId, int bookId, bool includeAuthor = true)
        {

            if (includeAuthor)
                return await _context.Book.Include(b => b.Author)
                    .Where(b => b.BookId == bookId && b.AuthorId == authorId)
                    .FirstOrDefaultAsync();

            return await _context.Book
                      .Where(b => b.BookId == bookId && b.AuthorId == authorId)
                      .FirstOrDefaultAsync();
        }

        public async Task<bool> BookExistsAsync(int bookId)
        {
            return await _context.Book.AnyAsync(b => b.BookId == bookId);
        }

        public async Task CreateBookAsync(BookForCreationDTO request)
        {
            CreateBookValidatior validationRules = new CreateBookValidatior();
            var result = validationRules.Validate(request);

            if (!result.IsValid)
            {
                var validationErrors = new Dictionary<string, string[]>(result.ToDictionary());
                throw new ValidationException(validationErrors);
            }

            var book = _mapper.Map<Entities.Book>(request);
            book.AuthorId = request.AuthorId;

            await _context.Book.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task<Book?> UpdateBookAsync(BookForUpdateDTO request)
        {

            UpdateBookValidator validationRules = new UpdateBookValidator();
            var result = validationRules.Validate(request);

            if (!result.IsValid)
            {
                var validationErrors = new Dictionary<string, string[]>(result.ToDictionary());
                throw new ValidationException(validationErrors);
            }

            var dbBook = await _context.Book.FindAsync(request.BookId);
            if (dbBook != null)
            {
                throw new ValidationException($"BookId {request.BookId} is not exists");
            }
            var book = _mapper.Map<Entities.Book>(dbBook);

            _mapper.Map(dbBook, request);
            await _context.SaveChangesAsync();
            return await _context.Book.FindAsync(dbBook.BookId);

        }

        public void DeleteBookAsync(int bookId)
        {
            var dbBook = _context.Book.Find(bookId);
            if (dbBook == null)
            {
                throw new ValidationException($"BookId {bookId} is not exists");
            }
            _context.Book.Remove(dbBook);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        #region Author

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await _context.Author.OrderBy(a => a.Name).ToListAsync();
        }

        public async Task<Author?> GetAuthorAsync(int authorId)
        {
            return await _context.Author.FirstOrDefaultAsync(a => a.AuthorId == authorId);
        }

        public async Task<bool> AuthorExistsAsync(int authorId)
        {
            return await _context.Author.AnyAsync(a => a.AuthorId == authorId);
        }

        public async Task CreateAuthorAsync(AuthorForCreationDTO request)
        {

            CreateAuthorValidatior validationRules = new CreateAuthorValidatior();
            var result = validationRules.Validate(request);

            if (!result.IsValid)
            {
                var validationErrors = new Dictionary<string, string[]>(result.ToDictionary());
                throw new ValidationException(validationErrors);
            }

            var Author = _mapper.Map<Entities.Author>(request);

            await _context.Author.AddAsync(Author);
            await _context.SaveChangesAsync();
        }

        public async Task<Author?> UpdateAuthorAsync(AuthorForUpdateDTO request)
        {
            UpdateAuthorValidatior validationRules = new UpdateAuthorValidatior();
            var result = validationRules.Validate(request);

            if (!result.IsValid)
            {
                var validationErrors = new Dictionary<string, string[]>(result.ToDictionary());
                throw new ValidationException(validationErrors);
            }

            var dbAuthor = await _context.Author.FindAsync(request.AuthorId);
            var author = _mapper.Map<Entities.Author>(dbAuthor);

            _mapper.Map(dbAuthor, request);

            await _context.SaveChangesAsync();
            return await _context.Author.FindAsync(dbAuthor!.AuthorId);
        }

        public void DeleteAuthorAsync(int authorId)
        {
            var dbAuthor = _context.Author.Find(authorId);
            if (dbAuthor == null)
            {
                return;
            }
            _context.Author.Remove(dbAuthor);
        }

        #endregion



    }
}
