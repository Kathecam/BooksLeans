using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookLendApi.Domain.Entities;
using BookLendApi.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLendApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;

        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _booksRepository.GetAll();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _booksRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [Authorize]
         [HttpPost]
        public IActionResult Add(Books book)
        {
            _booksRepository.Add(book);
            return Ok();
        }

        [HttpGet("search")]
        public IActionResult Search(string criteria)
        {
            var books = _booksRepository.Search(criteria);
            return Ok(books);
        }

        [HttpGet("filter")]
        public IActionResult Filter(string genre, string author, int publicationYear)
        {
            var books = _booksRepository.Filter(genre, author, publicationYear);
            return Ok(books);
        }
    }
}