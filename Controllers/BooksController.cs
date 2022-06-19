using BookAPI.Models;
using BookAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookRepository.Get();
        }
        [HttpGet]
        public async Task<ActionResult<Book>> GetBooks(int id)
        {
            return await _bookRepository.Get(id);
        }
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook([FromBody ]Book book)
        {
            var newBook = await _bookRepository.Create(book);
            return CreatedAtAction(nameof(GetBooks), new { id = newBook.id }, newBook);
        }
        [HttpDelete ("(id)")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var deleteBook = await _bookRepository.Get(id);
            if(deleteBook != null)
            {
                await _bookRepository.Delete(deleteBook.id);
                return NoContent();
            }
            else { return NotFound();}
            
        }
        [HttpPut]
        public async Task<ActionResult> PutBook(int id, [FromBody]Book book)
        {
            if(id == book.id)
            {
                await _bookRepository.Update(book);
                return NoContent();
            }
            else { return BadRequest(); }
            
        }
    }
}
