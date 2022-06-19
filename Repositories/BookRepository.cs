using BookAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _context;

        public BookRepository(BookContext context)
        {
            _context = context;
        }
        public async Task<Book> Create(Book book)
        {
            _context.books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task Delete(int id)
        {
            var BookToDelete = await _context.books.FindAsync(id);
            _context.books.Remove(BookToDelete);
           await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> Get()
        {
            return await _context.books.ToListAsync();
        }

        public async Task<Book> Get(int id)
        {
            return await _context.books.FindAsync(id); ;
        }

        public async Task Update(Book book)
        {
           _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
