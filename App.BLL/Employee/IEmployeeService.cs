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
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
        void UpdateRange(IEnumerable<ApplicationUser> applicationUsers);
    }
}
