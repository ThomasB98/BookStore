using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Model.Enums
{
    public enum PaymentStatus
    {
        Pending,      
        Completed,    
        Failed,       
        Canceled,     
        Refunded,      
        PartiallyPaid,
        Processing
    }
}
