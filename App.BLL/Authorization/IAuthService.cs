using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public interface IAuthService
    {
        Task<OperationResult<ApplicationUser, string>> LoginAsync(string username, string password);
        Task<OperationResult<ApplicationUser, string>> GetByIdAsync(int id);
        Task UpdateAsync(ApplicationUser applicationUser);
    }
}
