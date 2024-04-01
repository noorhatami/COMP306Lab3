using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Models;
using AutoMapper;
using ProjectWebAPI.Services;
using ProjectWebAPI.DTOs;

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookCreateDTO>>> GetBooks()
        {
            var books = await _bookRepository.GetBooksAsync();
            var bookViewModel = _mapper.Map<IEnumerable<BookCreateDTO>>(books);
            return Ok(bookViewModel);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookCreateDTO>> GetBook(string id)
        {
            var book = await _bookRepository.GetBookAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooks(string id, BookUpdateDTO bookUpdateDTO)
        {
            await _bookRepository.UpdateAsync(id, _mapper.Map<Books>(bookUpdateDTO));
            return NoContent();
        }

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<BookCreateDTO>> PostBooks(BookCreateDTO bookCreateDTO)
        {
            var book = _mapper.Map<Books>(bookCreateDTO);
            await _bookRepository.AddAsync(book);
            return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, bookCreateDTO);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooks(string id)
        {
            await _bookRepository.DeleteAsync(id);
            return NoContent();
        }

        // PATCH: api/Books/5
        [HttpPatch("{id}")]
        public async Task<ActionResult<BookInfoDTO>> PatchBook(string id, BookUpdateDTO bookUpdateDTO)
        {
            var book = await _bookRepository.GetBookAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            // Map properties from bookUpdateDTO to the existing book object using AutoMapper
            _mapper.Map(bookUpdateDTO, book);

            // Call the UpdateAsync method in the repository to update the book in the database
            await _bookRepository.UpdateAsync(id, book);

            // Return the updated book information
            return Ok(_mapper.Map<BookInfoDTO>(book));
        }
    }
}
