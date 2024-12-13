using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Exceptions
{
    public class NoWishListException : Exception
    {
        public NoWishListException() : base("No wish List Found") { }
    }
}
