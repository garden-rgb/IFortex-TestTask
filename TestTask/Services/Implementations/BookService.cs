using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        public BookService(ApplicationDbContext context) 
        {
            _context = context;
        }

        /// <summary>
        /// Get Books that contain "Red" in the title and were published after the release of Sabaton's "Carolus Rex" album
        /// </summary>
        public Task<List<Book>> GetBooks()
        {
            var carolusRexReleaseDate = new DateTime(2012, 5, 22);

            return _context.Books.AsNoTracking()
                .Where(book => book.Title.Contains("Red")
                               && book.PublishDate > carolusRexReleaseDate)
                .ToListAsync();
        }

        /// <summary>
        /// Get the Book with the highest cost of the published edition
        /// </summary>
        public Task<Book> GetBook()
        {
            return _context.Books.AsNoTracking()
                .OrderByDescending(book => book.Price * book.QuantityPublished)
                .FirstOrDefaultAsync();
        }
    }
}
