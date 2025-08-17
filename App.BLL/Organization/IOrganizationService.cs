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
        Task<OperationResult<IEnumerable<Organization>, string>> GetAllAsync();
        Task<OperationResult<bool, string>> CreateAsync(Organization organization);
        Task<OperationResult<bool, string>> DeleteAsync(int id);
        Task<OperationResult<Organization,string>> GetByIdAsync(int id);
        Task<OperationResult<Organization, string>> UpdateAsync(Organization organization);
    }
}
