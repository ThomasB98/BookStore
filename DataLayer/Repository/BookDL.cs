using AutoMapper;
using DataLayer.Constants.DBContext;
using DataLayer.Exceptions;
using DataLayer.Interfaces;
using DataLayer.Utilities.ResponseBody;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModelLayer.DTO.Book;
using ModelLayer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class BookDL : IBook
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<BookDL> _logger;

        public BookDL(DataContext context, ILogger<BookDL> logger,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResponseBody<IEnumerable<BookResponseDto>>> GetAllBooksAsync()
        {
            _logger.LogInformation("Fetching all books from the database.");
            var books = await _context.Book.ToListAsync();

            var bookDto = _mapper.Map<IEnumerable<BookResponseDto>>(books);

            return new ResponseBody<IEnumerable<BookResponseDto>>
            {
                Data = bookDto,
                Success = true,
                Message = "Books retrieved successfully.",
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<ResponseBody<bool>> AddBookAsync(BookCreateDto bookCreateDto)
        {
           
            var book = _mapper.Map<Book>(bookCreateDto);
            _logger.LogInformation("Adding a new book to the database.");
            await _context.Book.AddAsync(book);

            await _context.SaveChangesAsync();

            int changes = await _context.SaveChangesAsync();

            if (changes <= 0)
            {
                _logger.LogError("Database operation failed during user creation");
                throw new DatabaseOperationException("DataBase error");
            }

            _logger.LogInformation("Succefully Added a new book to the database.");
            return new ResponseBody<bool>
            {
                Data = true,
                Success = true,
                Message = "Book added successfully.",
                StatusCode = HttpStatusCode.Created
            };
        }

        public async Task<ResponseBody<bool>> DeleteBookAsync(int id)
        {
            _logger.LogInformation($"Deleting a book with ID: {id}.");
            var book = await _context.Book.FindAsync(id);

            if (book == null)
            {
                _logger.LogWarning($"Book does not exist. ID: {id}");
                throw new BookNotFoundException("Invalid book id");
            }

            _context.Set<Book>().Remove(book);
            await _context.SaveChangesAsync();

            return new ResponseBody<bool>
            {
                Data = true,
                Success = true,
                Message = "Book deleted successfully.",
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<ResponseBody<BookResponseDto?>> GetBookByIdAsync(int id)
        {
            _logger.LogInformation($"Fetching a book with ID: {id}.");
            var book = await _context.Set<Book>().FindAsync(id);

            if (book == null)
            {
                throw new BookNotFoundException($"book NotFound id:{id}");
            }

            var bookDto = _mapper.Map<BookResponseDto>(book);

            return new ResponseBody<BookResponseDto?>
            {
                Data = bookDto,
                Success = true,
                Message = "Book retrieved successfully.",
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<ResponseBody<BookResponseDto>> UpdateBookAsync(BookUpdateDto bookUpdateDto)
        {
            _logger.LogInformation($"Updating a book with ID: {bookUpdateDto.Id}.");
            var existingBook = await _context.Set<Book>().FindAsync(bookUpdateDto.Id);

            if (existingBook == null)
            {
                throw new BookNotFoundException($"Book with id:{bookUpdateDto.Id} not found");
            }

            _mapper.Map(bookUpdateDto, existingBook);
            await _context.SaveChangesAsync();

            var bookDto=_mapper.Map<BookResponseDto>(existingBook);

            return new ResponseBody<BookResponseDto>
            {
                Data = bookDto,
                Success = true,
                Message = "Book updated successfully.",
                StatusCode = HttpStatusCode.OK
            };

        }
    }
}
