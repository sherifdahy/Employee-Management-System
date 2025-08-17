using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public interface IEmployeeService
    {
        Task CreateAsync(ApplicationUser applicationUser);
        Task<Pagination<ApplicationUser>> GetAllAsync(int currentPage, int pageSize, string value);
        Task UpdateRangeAsync(IEnumerable<ApplicationUser> applicationUsers);
        Task<OperationResult<bool, string>> DeleteAsync(int id);
        Task<OperationResult<ApplicationUser,string>> GetByIdAsync(int id);
        Task<OperationResult<bool, string>> UpdateAsync(ApplicationUser applicationUser);
    }
}
