using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Enums
{
    public enum TransactionState : byte
    {
        Initial = 0,    
        Pending = 1,   
        Approved = 2,  
        Rejected = 3  
    }
}
