using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Exceptions
{
    public class ShippingAddressNotFoundException : Exception
    {
        public ShippingAddressNotFoundException(string message):base(message) { }
    }
}
