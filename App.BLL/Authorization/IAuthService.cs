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
        Task<OperationResult<ApplicationUser>> LoginAsync(string username, string password);
        Task<OperationResult<ApplicationUser>> GetByIdAsync(int id);
        Task UpdateAsync(ApplicationUser applicationUser);
    }
}
