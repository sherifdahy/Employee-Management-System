using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public interface ICompanyService
    {
        Task CreateAsync(Company company);
        Task<IEnumerable<Company>> GetAllAsync();
        Task<IEnumerable<Company>> SearchAsync(string value);
    }
}
