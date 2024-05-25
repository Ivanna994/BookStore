using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BookStore.DbContexts
{
    public class DataContext : DbContext
    {

        private readonly IConfiguration _configuration;
        /// <summary>
        /// Data context 
        /// </summary>
        public DataContext()
        {

        }
        /// <summary>
        /// Data context 
        /// </summary>
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //if (!options.IsConfigured)
            //{
            //    options.UseSqlServer("server=SQLRazvoj1;database=bookstore;trusted_connection=true;TrustServerCertificate=True;MultipleActiveResultSets=True");
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasData(
               new Author()
               {
                   AuthorId = 1,
                   Name = "Charles Dickens"
               },
               new Author()
               {
                   AuthorId = 2,
                   Name = "Paulo Coelho"
               },
               new Author()
               {
                   AuthorId = 3,
                   Name = "J. K. Rowling"
               },
               new Author()
               {
                   AuthorId = 4,
                   Name = "Agatha Christie"
               },
               new Author()
               {
                   AuthorId = 5,
                   Name = "William Shakespeare"
               },
               new Author()
               {
                   AuthorId = 6,
                   Name = "Stephen King"
               },
               new Author()
               {
                   AuthorId = 7,
                   Name = "Jane Austen"
               },
               new Author()
               {
                   AuthorId = 8,
                   Name = "Leo Tolstoy"
               });

            modelBuilder.Entity<Book>()
             .HasData(
               new Book()
               {
                   BookId = 1,
                   AuthorId = 1,
                   Title = "A Tale of Two Cities"
               },
              new Book()
              {
                  BookId = 2,
                  AuthorId = 1,
                  Title = "Dombey and Son"
              },
               new Book()
               {
                   BookId = 3,
                   AuthorId = 1,
                   Title = "David Copperfield"
               },
              new Book()
              {
                  BookId = 4,
                  AuthorId = 1,
                  Title = "Martin Chuzzlewit"
              },
               new Book()
               {
                   BookId = 5,
                   AuthorId = 2,
                   Title = "The Alchemist"
               },
               new Book()
               {
                   BookId = 6,
                   AuthorId = 2,
                   Title = "Brida"
               },
               new Book()
               {
                   BookId = 7,
                   AuthorId = 2,
                   Title = "Aleph"
               },
               new Book()
               {
                   BookId = 8,
                   AuthorId = 2,
                   Title = "Amor"
               },
               new Book()
               {
                   BookId = 9,
                   AuthorId = 2,
                   Title = "Eleven Minutes"
               },
               new Book()
               {
                   BookId = 10,
                   AuthorId = 2,
                   Title = "Veronika Decides to Die"
               },
               new Book()
               {
                   BookId = 11,
                   AuthorId = 2,
                   Title = "The Zahir"
               },
               new Book()
               {
                   BookId = 12,
                   AuthorId = 2,
                   Title = "Like The Flowing River"
               }
               ,
               new Book()
               {
                   BookId = 13,
                   AuthorId = 3,
                   Title = "Harry Potter and the Sorcerer's Stone"
               }
               ,
               new Book()
               {
                   BookId = 14,
                   AuthorId = 3,
                   Title = "Harry Potter and the Chamber of Secrets"
               }
               ,
               new Book()
               {
                   BookId = 15,
                   AuthorId = 3,
                   Title = "Harry Potter and the Prisoner of Azkaban"
               }
               ,
               new Book()
               {
                   BookId = 16,
                   AuthorId = 3,
                   Title = "Harry Potter and the Goblet of Fire"
               }
               ,
               new Book()
               {
                   BookId = 17,
                   AuthorId = 3,
                   Title = "Harry Potter and the Order of the Phoenix"
               }
               ,
               new Book()
               {
                   BookId = 18,
                   AuthorId = 3,
                   Title = "Harry Potter and the Half-Blood Prince"
               }
               ,
               new Book()
               {
                   BookId = 19,
                   AuthorId = 3,
                   Title = "Harry Potter and the Deathly Hallows"

               },
               new Book()
               {
                   BookId = 20,
                   AuthorId = 3,
                   Title = "Harry Potter and the Cursed Child"

               },
               new Book()
               {
                   BookId = 21,
                   AuthorId = 4,
                   Title = "And Then There Were None"

               },
               new Book()
               {
                   BookId = 22,
                   AuthorId = 4,
                   Title = "The Murder of Roger Ackroyd"

               },
               new Book()
               {
                   BookId = 23,
                   AuthorId = 4,
                   Title = "The Hollow"

               },
               new Book()
               {
                   BookId = 24,
                   AuthorId = 5,
                   Title = "Hamlet"

               },
               new Book()
               {
                   BookId = 25,
                   AuthorId = 5,
                   Title = "Romeo and Juliet"

               },
               new Book()
               {
                   BookId = 26,
                   AuthorId = 6,
                   Title = "The Shining"

               },
               new Book()
               {
                   BookId = 27,
                   AuthorId = 7,
                   Title = "Pride and Prejudice"

               },
               new Book()
               {
                   BookId = 28,
                   AuthorId = 8,
                   Title = "War and Peace"

               },
               new Book()
               {
                   BookId = 29,
                   AuthorId = 8,
                   Title = "Anna Karenina"

               },
               new Book()
               {
                   BookId = 30,
                   AuthorId = 8,
                   Title = "Resurrection"

               }
               );
            base.OnModelCreating(modelBuilder);
        }

    }
}
