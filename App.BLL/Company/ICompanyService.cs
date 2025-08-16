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
        Task<OperationResult<bool, string>> CreateAsync(Company company);
        OperationResult<string,string> Update(Company company);
        Task<Pagination<Company>> GetAllAsync(int currentPage, int displayCount = 10, Guid userId = default, string? value = null);
        Task<OperationResult<IEnumerable<Company>, string>> GetAllAsync();
        Task<OperationResult<string, string>> DeleteAsync(Guid id);
        Task<OperationResult<Company, string>> GetByIdAsync(Guid id);
        Task<IEnumerable<Company>> SearchAsync(string value);
        Task<IEnumerable<Company>> GetRelatedCompaniesAsync(Guid userId);
        
    }
}
