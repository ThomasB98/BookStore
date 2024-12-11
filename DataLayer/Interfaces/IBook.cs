using DataLayer.Utilities.ResponseBody;
using ModelLayer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IBook
    {
        Task<ResponseBody<IEnumerable<Book>>> GetAllBooksAsync();

        Task<ResponseBody<Book?>> GetBookByIdAsync(int id);

        Task<ResponseBody<Book>> AddBookAsync(Book book);

        Task<ResponseBody<Book>> UpdateBookAsync(Book book);

        Task<ResponseBody<bool>> DeleteBookAsync(int id);
    }
}
