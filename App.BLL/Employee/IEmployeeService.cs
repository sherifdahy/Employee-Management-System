using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public interface IEmployeeService
    {
        Task CreateAsync(ApplicationUser applicationUser);
        Task<Pagination<ApplicationUser>> GetAllAsync(int currentPage, int pageSize, string value);
        void UpdateRange(IEnumerable<ApplicationUser> applicationUsers);
    }
}
