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
        Task<OperationResult<object>> CreateAsync(ApplicationUser applicationUser);
        Task<OperationResult<Pagination<ApplicationUser>>> GetAllAsync(int currentPage, int pageSize, string value);
        Task<OperationResult<object>> UpdateRangeAsync(IEnumerable<ApplicationUser> applicationUsers);
        Task<OperationResult<object>> DeleteAsync(int id);
        Task<OperationResult<ApplicationUser>> GetByIdAsync(int id);
        Task<OperationResult<object>> UpdateAsync(ApplicationUser applicationUser);
    }
}
