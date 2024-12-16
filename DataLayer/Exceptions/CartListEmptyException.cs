using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Exceptions
{
    public class CartListEmptyException : Exception
    {
        public CartListEmptyException() : base("Cart List is Empty")
        {
            
        }
    }
}
