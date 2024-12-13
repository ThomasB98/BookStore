using DataLayer.Utilities.ResponseBody;
using ModelLayer.DTO.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IBookService
    {
        Task<ResponseBody<IEnumerable<BookResponseDto>>> GetAllBooksAsync();

        Task<ResponseBody<BookResponseDto?>> GetBookByIdAsync(int id);

        Task<ResponseBody<bool>> AddBookAsync(BookCreateDto book);

        Task<ResponseBody<BookResponseDto>> UpdateBookAsync(BookUpdateDto book);

        Task<ResponseBody<bool>> DeleteBookAsync(int id);
    }
}
