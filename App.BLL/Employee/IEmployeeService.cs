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
        void UpdateRange(IEnumerable<ApplicationUser> applicationUsers);
        Task<OperationResult<bool, string>> DeleteAsync(Guid id);
        Task<OperationResult<ApplicationUser,string>> GetByIdAsync(Guid id);
        public OperationResult<bool, string> Update(ApplicationUser applicationUser);
    }
}
