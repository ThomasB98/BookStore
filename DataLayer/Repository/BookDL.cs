using DataLayer.Interfaces;
using DataLayer.Utilities.ResponseBody;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class BookDL : IBook
    {
        private readonly DbContext _context;

        public BookDL(DbContext context)
        {
            _context = context;
        }

        public Task<ResponseBody<IEnumerable<Book>>> GetAllBooksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBody<Book>> AddBookAsync(Book book)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBody<bool>> DeleteBookAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBody<Book?>> GetBookByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBody<Book>> UpdateBookAsync(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
