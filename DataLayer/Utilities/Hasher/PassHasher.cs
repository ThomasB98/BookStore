using DataLayer.Constants.DBContext;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Utilities.Hasher
{
    public class PassHasher: IPassHasher
    {
        private DataContext _dataContext;

        PassHasher(DataContext dataContext) { 
            _dataContext = dataContext; 
        }

       public string encrypt(string password)
       {
            return BCrypt.Net.BCrypt.HashPassword(password);
       }

        public bool verfiy(string hashPassword,string password)
        {
            return BCrypt.Net.BCrypt.Verify(password,hashPassword);
        }
    }
}
