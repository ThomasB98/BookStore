using BusinessLayer.Interfaces;
using DataLayer.Interfaces;
using DataLayer.Utilities.ResponseBody;
using ModelLayer.DTO.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.service
{
    public class BookBL : IBookService
    {
        private readonly IBook _bookRepo;  

        public BookBL(IBook bookRepo)
        {
            _bookRepo = bookRepo;
        }
        public Task<ResponseBody<bool>> AddBookAsync(BookCreateDto book)
        {
           return _bookRepo.AddBookAsync(book);
        }

        public Task<ResponseBody<bool>> DeleteBookAsync(int id)
        {
           return _bookRepo.DeleteBookAsync(id);
        }

        public Task<ResponseBody<IEnumerable<BookResponseDto>>> GetAllBooksAsync()
        {
            return _bookRepo.GetAllBooksAsync();
        }

        public Task<ResponseBody<BookResponseDto?>> GetBookByIdAsync(int id)
        {
            return _bookRepo.GetBookByIdAsync(id);
        }

        public Task<ResponseBody<BookResponseDto>> UpdateBookAsync(BookUpdateDto book)
        {
            return _bookRepo.UpdateBookAsync(book);
        }
    }
}
