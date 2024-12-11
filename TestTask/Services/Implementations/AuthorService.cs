using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context) 
        { 
            _context = context;
        }

        /// <summary>
        /// Get authors who wrote an even number of books published after 2015
        /// </summary>
        public Task<List<Author>> GetAuthors()
        {
            return _context.Authors.AsNoTracking()
                .Where(author => author.Books.Count(book => book.PublishDate.Year > 2015) % 2 == 0 &&
                                 author.Books.Any(book => book.PublishDate.Year > 2015))
                .ToListAsync();
        }

        /// <summary>
        /// Get the author who wrote the book with the longest title
        /// </summary>
        public Task<Author> GetAuthor()
        {
            return _context.Books.AsNoTracking()
                .OrderByDescending(book => book.Title.Length)
                .ThenBy(book => book.AuthorId)
                .Select(book => book.Author)
                .FirstOrDefaultAsync();
        }
    }
}
