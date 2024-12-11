using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Utilities.Hasher
{
    public interface IPassHasher
    {
        public string encrypt(string password);

        public bool verfiy(string hashPassword, string password);
    }
}
