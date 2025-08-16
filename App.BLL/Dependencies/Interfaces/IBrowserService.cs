using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL.Dependencies.Interfaces
{
    public interface IBrowserService
    {
        void Open(Email email, byte browser = 1);
    }
}
