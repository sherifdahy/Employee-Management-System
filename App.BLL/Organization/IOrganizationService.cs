using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public interface IOrganizationService
    {
        Task<OperationResult<IEnumerable<Organization>>> GetAllAsync();
        Task<OperationResult<bool>> CreateAsync(Organization organization);
        Task<OperationResult<bool>> DeleteAsync(int id);
        Task<OperationResult<Organization>> GetByIdAsync(int id);
        Task<OperationResult<Organization>> UpdateAsync(Organization organization);
    }
}
